MERGE VehicleManufacturers AS target  
USING (SELECT DISTINCT
	LTRIM(LTRIM(_Fld9363))
FROM upr_ulf_finance.dbo._InfoRg7858
WHERE LTRIM(LTRIM(ISNULL(_Fld9363, ''))) != '') AS source (Manufacturer)  
ON (target.Name = LTRIM(LTRIM(ISNULL(source.Manufacturer, ''))))
WHEN NOT MATCHED THEN  
	INSERT (Name)
	VALUES (source.Manufacturer);   
GO

MERGE VehicleModels AS target  
USING (SELECT DISTINCT
	LTRIM(LTRIM(I._Fld7863)),
	MAX(CAST(I._Fld7864 AS INT)),
	0, /*v._Fld7865, = Type*/
	MAX(ISNULL(VM.Id, -1))
FROM upr_ulf_finance.dbo._InfoRg7858 I
LEFT JOIN VehicleManufacturers VM ON VM.Name = LTRIM(LTRIM(ISNULL(_Fld9363, ''))) AND LTRIM(LTRIM(ISNULL(_Fld9363, ''))) != ''
WHERE LTRIM(LTRIM(ISNULL(_Fld7863, ''))) != '' GROUP BY I._Fld7863) AS source (Model, EngineVolume, Type, ManufacturerId)  
ON (target.Name = LTRIM(LTRIM(ISNULL(source.Model, ''))))
WHEN MATCHED THEN
	UPDATE SET
	EngineVolume = IIF(source.EngineVolume > 0 AND source.EngineVolume != NULL, source.EngineVolume, target.EngineVolume),
	ManufacturerId = IIF(source.ManufacturerId != -1, source.ManufacturerId, target.ManufacturerId)
WHEN NOT MATCHED THEN  
	INSERT (Name, EngineVolume, Type, ManufacturerId)
	VALUES (source.Model, source.EngineVolume, source.Type, IIF(source.ManufacturerId != -1, source.ManufacturerId, NULL));   
GO

MERGE Vehicles AS target  
USING (SELECT
	CONVERT(UNIQUEIDENTIFIER, v._Fld7860),
	LTRIM(LTRIM(ISNULL(v._Fld7867, ''))),
	LTRIM(LTRIM(ISNULL(v._Fld7862, ''))),
	MAX(LTRIM(LTRIM(ISNULL(v._Fld9363, '')))),
	MAX(LTRIM(LTRIM(ISNULL(v._Fld7863, '')))),
	MAX(PARSE(v._Fld9840 AS INT)),
	MAX(ISNULL(Mo.Id, -1))
FROM upr_ulf_finance.dbo._InfoRg7858 v
INNER JOIN upr_ulf_finance.dbo._InfoRg7874 a ON a._Fld7887 = v._Fld7860 AND a._Fld7878 > CONVERT(DATETIME2, '4001-01-01')
LEFT JOIN VehicleManufacturers Ma ON Ma.Name = LTRIM(LTRIM(ISNULL(v._Fld9363, ''))) AND LTRIM(LTRIM(ISNULL(v._Fld9363, ''))) != ''
LEFT JOIN VehicleModels Mo ON Mo.Name = LTRIM(LTRIM(ISNULL(v._Fld7863, ''))) AND LTRIM(LTRIM(ISNULL(v._Fld7863, ''))) != '' AND Ma.Id = Mo.ManufacturerId
GROUP BY v._Fld7860, v._Fld7867, v._Fld7862)
	AS source (UprId, VIN, Number, ManufacturerName, ModelName, ManufacturedYear, ModelId)  
ON (target.UprId = source.UprId)
WHEN MATCHED THEN
	UPDATE SET
	ModelId = IIF(source.ModelId != -1, source.ModelId, target.ModelId)
WHEN NOT MATCHED THEN  
	INSERT (UprId, VIN, Number, ManufacturedYear, ModelId)
	VALUES (source.UprId, source.VIN, source.Number, source.ManufacturedYear, IIF(source.ModelId != -1, source.ModelId, NULL));   
GO

MERGE Clients AS target  
USING (SELECT
_fld7817,
LTRIM(RTRIM(_fld7819)),
LTRIM(RTRIM(_fld7823)),
LTRIM(RTRIM(_fld7818)),
LTRIM(RTRIM(_fld7829)),
LTRIM(RTRIM(_fld7834)),
LTRIM(RTRIM(_fld7835))
FROM upr_ulf_finance.dbo._InfoRg7815 WHERE LTRIM(RTRIM(_fld7819)) <> '' AND _fld7850 like '%Клиент%')
	AS source (UprId, Code, TaxNumber, Name, NameFull, AddressJuridical, AddressPhysical)  
ON (target.UprId = source.UprId)
WHEN MATCHED THEN
	UPDATE SET
	AddressJuridical = ISNULL(source.AddressJuridical, target.AddressJuridical),
	AddressPhysical = ISNULL(source.AddressPhysical, target.AddressPhysical)
WHEN NOT MATCHED THEN  
	INSERT (UprId, Code, TaxNumber, Name, NameFull, AddressJuridical, AddressPhysical, Type)
	VALUES (source.UprId, source.Code, source.TaxNumber, source.Name, source.NameFull, source.AddressJuridical, source.AddressPhysical,
	IIF(LEN(source.Code) != 8 AND LEN(source.Code) != 10, 0, IIF(LEN(source.Code) = 8, 1,
		IIF(LEN(source.Code) = 10 AND (source.Name LIKE '%ФОП%' OR source.Name LIKE '%СПД%' OR source.NameFull LIKE '%Фізична особа-підприємець%'), 3, 2))));
GO

MERGE Agreements AS target  
USING (SELECT 
	_Fld7875,
	MAX(CONVERT(INT, _Fld7893)),
	MAX(LTRIM(RTRIM(_Fld7877))),
	MAX(IIF(_Fld7878 > CONVERT(DATETIME2, '4001-01-01'), DATEADD(YEAR, -2000, _Fld7878), CONVERT(DATETIME2, '0001-01-01'))),
	MAX(IIF(_Fld9368 > CONVERT(DATETIME2, '4001-01-01'), DATEADD(YEAR, -2000, _Fld9368), CONVERT(DATETIME2, '0001-01-01'))),
	MAX(_Fld7883),
	MAX(ISNULL(C.Id, -1)),
	MAX(ISNULL(V.Id, -1))
FROM upr_ulf_finance.dbo._InfoRg7874
JOIN Clients C ON C.UprId = _Fld7876
JOIN Vehicles V ON V.UprId = _Fld7887
WHERE _Fld7892 != 0
GROUP BY _Fld7875)
	AS source (UprId, CalcId, Code, Date, ShippmentDate, Currency, ClientId, VehicleId)  
ON (target.UprId = source.UprId)
WHEN MATCHED THEN
	UPDATE SET
	Date = IIF(source.Date > CONVERT(DATETIME2, '0001-01-01'), source.Date, target.Date)
WHEN NOT MATCHED THEN  
	INSERT (UprId, CalcId, Code, ClientId, VehicleId, Date, ShippmentDate, ParticipationAmount, Currency)
	VALUES (source.UprId, source.CalcId, source.Code,
	IIF(source.ClientId != -1, source.ClientId, NULL),
	IIF(source.VehicleId != -1, source.VehicleId, NULL),
	IIF(source.Date > CONVERT(DATETIME2, '0001-01-01'), source.Date, NULL),
	IIF(source.ShippmentDate > CONVERT(DATETIME2, '0001-01-01'), source.ShippmentDate, NULL), 0,
	IIF(source.Currency = 'грн', 1,
		IIF(source.Currency = 'usdмежбанк' OR source.Currency = 'usd', 2,
			IIF(source.Currency = 'долармбфін', 3, 0))));
GO

MERGE Accruals AS target  
USING (SELECT
	_Fld16304,
	IIF(_Fld16302 > CONVERT(DATETIME2, '4001-01-01'), DATEADD(YEAR, -2000, _Fld16302), NULL),
	SUM(_Fld13965),
	1
FROM homnet_fin.dbo._InfoRg16300 WHERE _Fld13965 < 0) AS source (AgreementId, Date, Amount, Currency)  
ON (target.AgreementId = source.AgreementId AND target.Date = source.Date AND target.Currency = source.Currency)
WHEN MATCHED THEN
	UPDATE SET Amount = source.Amount
WHEN NOT MATCHED THEN  
	INSERT (AgreementId, Date, Amount, Currency)
	VALUES (source.AgreementId, source.Date, -source.Amount,source.Currency);
GO

MERGE Payments AS target  
USING (SELECT
	_Fld16304,
	IIF(_Fld16302 > CONVERT(DATETIME2, '4001-01-01'), DATEADD(YEAR, -2000, _Fld16302), NULL),
	SUM(_Fld13965),
	1
FROM homnet_fin.dbo._InfoRg16300 WHERE _Fld13965 > 0) AS source (AgreementId, Date, Amount, Currency)  
ON (target.AgreementId = source.AgreementId AND target.Date = source.Date AND target.Currency = source.Currency)
WHEN MATCHED THEN
	UPDATE SET Amount = source.Amount
WHEN NOT MATCHED THEN  
	INSERT (AgreementId, Date, Amount, Currency)
	VALUES (source.AgreementId, source.Date, -source.Amount,source.Currency);
GO