using AdoNetCore.AseClient;
using Api.Core;
using AutoMapper;
using Dapper;
using Data.Contexts;
using Data.Entities;
using Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Upr.Contexts;

namespace Api.Initializers
{
    public class TestInitializer : IDbInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly UprContext _upr;
        private readonly CollectionContext _collection;
        private readonly IMapper _mapper;
        private readonly RoleManager<UserRole> _roleManager;

        public TestInitializer(UprContext upr, CollectionContext collection, IMapper mapper, UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _upr = upr;
            _collection = collection;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            //return;
            _collection.Database.Migrate();
            return;
            //Import transactions
            var transactionsNew = _upr.GetTransactions().Where(t => t.Sum != null).ToList();

            var accruals = _collection.Accruals.ToList();
            var payments = _collection.Payments.ToList();

            transactionsNew.ForEach(transactionNew => {
                var filteredAgreements = _collection.Agreements.Where(a => a.UprId == Guid.Parse(transactionNew.AgreementId))
                    .Include(a => a.Accruals).Include(a => a.Payments).ToList();
                if (filteredAgreements.Count() > 1)
                    throw new Exception("Найдено более одного договора с номером " + transactionNew.AgreementId);
                if (filteredAgreements.Count() == 1)
                {
                    var newCurrency = transactionNew.Currency.Trim().ToLower();
                    var c = newCurrency == "грн" ? Currency.UAH : Currency.Undefined;
                    if (c != Currency.UAH)
                        throw new Exception("Валюта не гривна: " + newCurrency);
                    var agreement = filteredAgreements.First();
                    if (transactionNew.Sum < 0)
                    {
                        var filteredAccruals = agreement.Accruals.Where(a => (double)(-1 * transactionNew.Sum) == a.Amount && transactionNew.Date == a.Date);
                        if (filteredAccruals.Count() > 1)
                            throw new Exception("Найдено более одного начисления на сумму " + (double)(-1 * transactionNew.Sum) + " и дату " + transactionNew.Date);
                        if (filteredAccruals.Count() == 1)
                        {
                            var accrual = filteredAccruals.First();
                            accrual.Currency = c == Currency.Undefined ? accrual.Currency : c;
                        }
                        if (filteredAccruals.Count() == 0)
                        {
                            agreement.Accruals.Add(new Accrual { Amount = (double)(-1 * transactionNew.Sum), Date = transactionNew.Date, Currency = c });
                        }
                    }
                }
                if (filteredAgreements.Count() == 0)
                {
                    //throw new Exception("Не найдено ни одного договора с идентификатором " + transactionNew.AgreementId);
                }
            });

            _collection.SaveChanges();
            return;
            /*var role1 = new UserRole { Name = "УОК" };
            var role2 = new UserRole { Name = "ЮД" };
            var role3 = new UserRole { Name = "СБ" };
            _roleManager.Create(role1);
            _roleManager.Create(role2);
            _roleManager.Create(role3);
            var user = _userManager.FindByName("koscheyevserge");
            _userManager.AddToRoles(user, new List<string> { "УОК", "ЮД", "СБ" });*/
            if (!_collection.TaskTypes.Any())
            {
                var taskType1 = new TaskType
                {
                    Name = "Позвонить клиенту просрочка 10+ дней",
                    Code = "CallToClient10DaysDebt",
                    Description = "Позвонить клиенту",
                    Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Согласился платить",
                        Code = "Agreed"
                    },
                    new TaskResult
                    {
                        Name = "Отказался платить",
                        Code = "NotAgreed"
                    },
                    new TaskResult
                    {
                        Name = "Просит реструктуризацию",
                        Code = "NeedRestruckt"
                    },
                    new TaskResult
                    {
                        Name = "Не берет трубку",
                        Code = "Ignore"
                    },
                    new TaskResult
                    {
                        Name = "Вне зоны",
                        Code = "Unavailable"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
                };
                var taskType2 = new TaskType
                {
                    Name = "Реструктуризировать ДЗ 10+ дней",
                    Code = "Restruckt10DaysDebt",
                    Description = "Отправить запрос на реструктуризацию",
                    Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Запрос отправлен",
                        Code = "Sent"
                    },
                    new TaskResult
                    {
                        Name = "Реструктуризация не одобрена",
                        Code = "NotAgreed"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
                };
                var taskType3 = new TaskType
                {
                    Name = "Отправить требования 30+ дней",
                    Code = "SendRequest30DaysDebt",
                    Description = "Отправить требования клиенту",
                    Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Требования отправлены",
                        Code = "Sent"
                    },
                    new TaskResult
                    {
                        Name = "Клиент недоступен",
                        Code = "Unavailable"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
                };
                var taskType4 = new TaskType
                {
                    Name = "Реструктуризировать ДЗ 30+ дней",
                    Code = "Restruckt30DaysDebt",
                    Description = "Отправить запрос на реструктуризацию",
                    Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Запрос отправлен",
                        Code = "Sent"
                    },
                    new TaskResult
                    {
                        Name = "Реструктуризация не одобрена",
                        Code = "NotAgreed"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
                };
                var taskType5 = new TaskType
                {
                    Name = "Реструктуризировать ДЗ 60+ дней",
                    Code = "Restruckt60DaysDebt",
                    Description = "Отправить запрос на реструктуризацию",
                    Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Запрос отправлен",
                        Code = "Sent"
                    },
                    new TaskResult
                    {
                        Name = "Реструктуризация не одобрена",
                        Code = "NotAgreed"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
                };
                var taskType6 = new TaskType
                {
                    Name = "Скип-трейсинг 30+ дней",
                    Code = "Trace30DaysDebt",
                    Description = "Идентифицировать местонахождение клиента и предмета лизинга",
                    Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Клиент и ПЛ найдены",
                        Code = "Found"
                    },
                    new TaskResult
                    {
                        Name = "Клиент найден, ПЛ не найден",
                        Code = "NotFoundVehicle"
                    },
                    new TaskResult
                    {
                        Name = "ПЛ найден, клиент не найден",
                        Code = "NotFoundClient"
                    },
                    new TaskResult
                    {
                        Name = "Клиент не найден",
                        Code = "NotFound"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
                };
                var taskType7 = new TaskType
                {
                    Name = "Посетить клиента 30+ дней",
                    Code = "Visit30DaysDebt",
                    Description = "Гарантийное письмо, акт осмотра ПЛ, акт сверки задолженности",
                    Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Встреча совершилась, акты подписаны",
                        Code = "Success"
                    },
                    new TaskResult
                    {
                        Name = "Встреча совершилась, акты не подписаны",
                        Code = "VisitNotSigned"
                    },
                    new TaskResult
                    {
                        Name = "Встреча не совершилась",
                        Code = "Fail"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
                };
                var taskType8 = new TaskType
                {
                    Name = "Ограничить право пользования ПЛ 30+ дней",
                    Code = "Restrict30DaysDebt",
                    Description = "Ограничить право пользования ПЛ"
                };
                var taskType9 = new TaskType
                {
                    Name = "Расторгнуть ДЛ 60+ дней",
                    Code = "Disolve60DaysDebt",
                    Description = "Расторгнуть договор лизинга",
                    Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Задача выполнена",
                        Code = "Success"
                    },
                    new TaskResult
                    {
                        Name = "Задача не выполнена",
                        Code = "Fail"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
                };
                var taskType10 = new TaskType
                {
                    Name = "Уведомить клиента о рассторжении ДЛ 60+ дней",
                    Code = "DisolveNotificate60DaysDebt",
                    Description = "Уведомить клиента о рассторжении договора лизинга",
                    Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Клиент уведомлен",
                        Code = "Success"
                    },
                    new TaskResult
                    {
                        Name = "Клиент игнорирует",
                        Code = "Fail"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
                };
                var taskType11 = new TaskType
                {
                    Name = "Скип-трейсинг 60+ дней",
                    Code = "Trace60DaysDebt",
                    Description = "Идентифицировать местонахождение клиента и предмета лизинга",
                    Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Клиент и ПЛ найдены",
                        Code = "Found"
                    },
                    new TaskResult
                    {
                        Name = "Клиент найден, ПЛ не найден",
                        Code = "NotFoundVehicle"
                    },
                    new TaskResult
                    {
                        Name = "ПЛ найден, клиент не найден",
                        Code = "NotFoundClient"
                    },
                    new TaskResult
                    {
                        Name = "Клиент не найден",
                        Code = "NotFound"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
                };
                var taskType12 = new TaskType
                {
                    Name = "Изъять ПЛ 60+ дней",
                    Code = "Take60DaysDebt",
                    Description = "Изъять предмет лизинга и отвезти его на арендованную парковку",
                    Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "ПЛ изъят",
                        Code = "Success"
                    },
                    new TaskResult
                    {
                        Name = "ПЛ не изъят",
                        Code = "Fail"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
                };

                _collection.TaskTypes.Add(taskType1);
                _collection.TaskTypes.Add(taskType2);
                _collection.TaskTypes.Add(taskType3);
                _collection.TaskTypes.Add(taskType4);
                _collection.TaskTypes.Add(taskType5);
                _collection.TaskTypes.Add(taskType6);
                _collection.TaskTypes.Add(taskType7);
                _collection.TaskTypes.Add(taskType8);
                _collection.TaskTypes.Add(taskType9);
                _collection.TaskTypes.Add(taskType10);
                _collection.TaskTypes.Add(taskType11);
                _collection.TaskTypes.Add(taskType12);
            }

            var modelManufacturerPairsNew = _upr.ModelManufacturerPairs
                .FromSql("SELECT " +
                "NEWID() AS Id, " +
                "_Fld9363 as Manufacturer, " +
                "CAST(_Fld7864 AS INT) AS EngineVolume, " +
                "_Fld7863 as Model " +
                "FROM upr_ulf_finance.dbo._InfoRg7858 WHERE _Fld9363 != '' AND _Fld7863 != ''").ToList();

            //Import manufacturers and models
            var manufacturers = _collection.VehicleManufacturers.Include(vm => vm.Models).ToList();
            modelManufacturerPairsNew.ForEach(pair =>
            {
                var filteredManufacturers = manufacturers.Where(mn => pair.Manufacturer.Trim().ToUpper() == mn.Name
                    && !string.IsNullOrWhiteSpace(pair.Manufacturer));
                if (filteredManufacturers.Count() > 1)
                    throw new Exception("Найдено более одной марки ТС с именем " + pair.Manufacturer);
                if (filteredManufacturers.Count() == 1)
                {
                    var manufacturer = filteredManufacturers.First();
                    var filteredModels = manufacturer.Models.Where(mn => pair.Model.Trim().ToUpper() == mn.Name
                        && !string.IsNullOrWhiteSpace(pair.Model));
                    if (filteredModels.Count() > 1)
                        throw new Exception("Найдено более одной модели ТС с именем " + pair.Model);
                    if (filteredModels.Count() == 1)
                    {
                        var model = filteredModels.First();
                        model.EngineVolume =
                            (model.EngineVolume == 0 || model.EngineVolume == null) && pair.EngineVolume > 0
                                ? pair.EngineVolume : model.EngineVolume;
                    }
                    if (filteredModels.Count() == 0 && !string.IsNullOrWhiteSpace(pair.Model))
                    {
                        manufacturer.Models.Add(new VehicleModel { EngineVolume = pair.EngineVolume > 0 ? pair.EngineVolume : null, Name = pair.Model.Trim().ToUpper() });
                    }
                }
                if (filteredManufacturers.Count() == 0 && !string.IsNullOrWhiteSpace(pair.Manufacturer))
                {
                    var manufacturer = new VehicleManufacturer
                    {
                        Name = pair.Manufacturer.Trim().ToUpper(),
                        Models = new List<VehicleModel>()
                    };
                    if(!string.IsNullOrWhiteSpace(pair.Model))
                        manufacturer.Models.Add(new VehicleModel { Name = pair.Model.Trim().ToUpper(), EngineVolume = pair.EngineVolume > 0 ? pair.EngineVolume : null });
                    manufacturers.Add(manufacturer);
                }
            });

            _collection.VehicleManufacturers.AddRange(manufacturers.Where(m => m.Id == 0));
            _collection.SaveChanges();

            //Import vehicles
            var vehicles = _collection.Vehicles.Include(v => v.Model).ToList();
            var vehiclesNew = _upr.Vehicles
                .FromSql("SELECT " +
                "v._Fld7860 AS Id, " +
                "v._Fld7867 AS VIN, " +
                "v._Fld7862 AS Number, " +
                "v._Fld9363 AS Manufacturer, " +
                "v._Fld7863 AS Model, " +
                "CAST(v._Fld7864 AS INT) AS EngineVolume, " +
                "v._Fld7865 AS Type, " +
                "v._Fld9840 AS ManufacturedYear " +
                "FROM _InfoRg7858 v " +
                "INNER JOIN _InfoRg7874 a ON a._Fld7887 = v._Fld7860 AND a._Fld7878 > CONVERT(DATETIME2, '4001-01-01') ").ToList();
            
            vehiclesNew.ForEach(vehicleNew =>
            {
                var filteredVehicles = vehicles.Where(vehicle => vehicle.UprId == Guid.Parse(vehicleNew.Id));
                if (filteredVehicles.Count() > 1)
                    throw new Exception("Найдено более одного предмета лизинга с Id " + vehicleNew.Id);
                var models = _collection.VehicleModels.Include(m => m.Manufacturer)
                    .Where(m => m.Name == vehicleNew.Model.Trim().ToUpper() && m.Manufacturer.Name == vehicleNew.Manufacturer.Trim().ToUpper()).ToList();
                if (models.Count() > 1)
                    throw new Exception("Найдено более одной модели для производителя " + vehicleNew.Manufacturer);
                if (models.Count() == 0)
                {
                    var manufacturer = _collection.VehicleManufacturers.SingleOrDefault(vm => vm.Name == vehicleNew.Manufacturer.Trim().ToUpper());
                    if (manufacturer == null && !string.IsNullOrWhiteSpace(vehicleNew.Manufacturer))
                    {
                        manufacturer = new VehicleManufacturer { Name = vehicleNew.Manufacturer.Trim().ToUpper() };
                    }
                    if (!string.IsNullOrWhiteSpace(vehicleNew.Model))
                    {
                        models.Add(new VehicleModel
                        {
                            Name = vehicleNew.Model.Trim().ToUpper(),
                            EngineVolume = vehicleNew.EngineVolume > 0 ? vehicleNew.EngineVolume : null,
                            Manufacturer = manufacturer
                        });
                    }
                }
                if (filteredVehicles.Count() == 1)
                {
                    var vehicle = filteredVehicles.First();
                    vehicle.Model = models.FirstOrDefault();
                    vehicle.ManufacturedYear = string.IsNullOrWhiteSpace(vehicleNew.ManufacturedYear)
                        ? null : (int?)int.Parse(vehicleNew.ManufacturedYear.Trim().Replace(" ", ""));
                    vehicle.Number = vehicleNew.Number.Trim().ToUpper();
                    vehicle.VIN = vehicleNew.VIN.Trim().ToUpper();
                }
                if (filteredVehicles.Count() == 0)
                {
                    vehicles.Add(new Vehicle
                    {
                        Model = models.FirstOrDefault(),
                        Number = vehicleNew.Number.Trim().ToUpper(),
                        UprId = Guid.Parse(vehicleNew.Id),
                        VIN = vehicleNew.VIN.Trim().ToUpper(),
                        ManufacturedYear = string.IsNullOrWhiteSpace(vehicleNew.ManufacturedYear)
                            ? null : (int?)int.Parse(vehicleNew.ManufacturedYear.Trim().Replace(" ", ""))
                    });
                }
            });

            _collection.Vehicles.AddRange(vehicles.Where(v => v.Id == 0));
            _collection.SaveChanges();

            //Import clients
            var clientsNew = _mapper.Map<List<Client>>(_upr.Clients
                .FromSql("SELECT " +
                "_fld7817 as Id, " +
                "LTRIM(RTRIM(_fld7819)) as Code, " +
                "LTRIM(RTRIM(_fld7823)) as TaxNumber, " +
                "LTRIM(RTRIM(_fld7818)) as Name, " +
                "LTRIM(RTRIM(_fld7829)) as NameFull, " +
                "LTRIM(RTRIM(_fld7834)) as AddressJur, " +
                "LTRIM(RTRIM(_fld7835)) as AddressPhys " +
                "FROM _InfoRg7815 WHERE LTRIM(RTRIM(_fld7819)) <> '' AND _fld7850 like '%Клиент%'")
                .ToList());            

            var clients = _collection.Clients.ToList();
            clientsNew.ForEach(clientNew =>
            {
                var filteredClients = clients.Where(client => clientNew.Code.Trim() == client.Code);
                if (filteredClients.Count() > 1)
                    throw new Exception("Найдено более одного клиента по ЕГРПОУ/ИНН " + clientNew.Code);
                if (filteredClients.Count() == 1)
                {
                    var filteredClient = filteredClients.First();
                    filteredClient.AddressJuridical = clientNew.AddressJuridical;
                    filteredClient.AddressPhysical = clientNew.AddressPhysical;
                    filteredClient.Name = clientNew.Name;
                    filteredClient.NameFull = clientNew.NameFull;
                    filteredClient.TaxNumber = clientNew.TaxNumber;
                    filteredClient.Type = clientNew.Type;
                }
                if (filteredClients.Count() == 0)
                {
                    clients.Add(clientNew);
                }
            });
            _collection.Clients.AddRange(clients.Where(c => c.Id == 0));
            _collection.SaveChanges();

            //Import agreements
            var agreementsNew = _upr.Agreements
                .FromSql("SELECT " +
                "_Fld7875 AS Id, " +
                "CONVERT(INT, _Fld7893) AS CalcId, " +
                "LTRIM(RTRIM(_Fld7877)) AS Code, " +
                "_Fld7876 AS ClientId, " +
                "_Fld7887 as VehicleId, " + 
                "IIF(_Fld7878 > CONVERT(DATETIME2, '4001-01-01'), DATEADD(YEAR, -2000, _Fld7878), NULL) as Date," +
                "IIF(_Fld9368 > CONVERT(DATETIME2, '4001-01-01'), DATEADD(YEAR, -2000, _Fld9368), NULL) as ShippmentDate, " +
                "IIF(_Fld7880 > CONVERT(DATETIME2, '4001-01-01'), DATEADD(YEAR, -2000, _Fld7880), NULL) as EndDate, " +
                "_Fld7883 AS Currency " +
                "FROM _InfoRg7874 WHERE _Fld7892 != 0").ToList();

            var agreements = _collection.Agreements.Include(a => a.Client).Include(a => a.Vehicle).ToList();

            agreementsNew.ForEach(agreementNew => {
                var filteredAgreements = _collection.Agreements.Where(a => a.UprId == Guid.Parse(agreementNew.Id)).ToList();
                if (filteredAgreements.Count() > 1)
                    throw new Exception("Найдено более одного договора с номером " + agreementNew.Code);
                var client = _collection.Clients.SingleOrDefault(c => c.UprId == Guid.Parse(agreementNew.ClientId));
                var vehicle = _collection.Vehicles.SingleOrDefault(v => v.UprId == Guid.Parse(agreementNew.VehicleId));
                if (filteredAgreements.Count() == 1)
                {
                    var agreement = filteredAgreements.First();
                    agreement.ShippmentDate = agreementNew.ShippmentDate;
                    agreement.Date = agreementNew.Date;
                    var newCurrency = agreementNew.Currency.Trim().ToLower();
                    agreement.Currency = newCurrency == "грн"
                        ? Currency.UAH : newCurrency == "usdмежбанк" || newCurrency == "usd"
                            ? Currency.USD_Межбанк : newCurrency == "долармбфін"
                                ? Currency.USD_MB_Fin : Currency.Undefined;
                }
                if (filteredAgreements.Count() == 0 && client != null && vehicle != null)
                {
                    var newCurrency = agreementNew.Currency.Trim().ToLower();
                    var agreement = new Agreement
                    {
                        Currency = newCurrency == "грн"
                        ? Currency.UAH : newCurrency == "usdмежбанк" || newCurrency == "usd"
                            ? Currency.USD_Межбанк : newCurrency == "долармбфін"
                                ? Currency.USD_MB_Fin : Currency.Undefined,
                        UprId = Guid.Parse(agreementNew.Id),
                        Code = agreementNew.Code.Trim().ToUpper(),
                        Date = agreementNew.Date,
                        ShippmentDate = agreementNew.ShippmentDate,
                        CalcId = agreementNew.CalcId,
                        Client = client,
                        Vehicle = vehicle
                    };
                    agreements.Add(agreement);
                }
            });
            
            _collection.Agreements.AddRange(agreements);
            _collection.SaveChanges(); 
            
        }
    }
}

//	_Fld7887 as ID_Авто,
//	_Fld7879 AS ДатаНачала,
//	_Fld7880 AS ДатаОкончания,
//  _Fld9369 AS СуммаКомиссия,
//  _Fld9370 AS СуммаТело,
//  _Fld9371 AS СуммаСервис,
//  _Fld9372 AS СуммаАгентские,
//  _Fld7891 AS ПервоначальныйВзнос,
//  _Fld7881 AS СтоимостьТС_ДЛ,
//  _Fld9373 AS СтоимостьТС_ДКП,
//  _Fld9374 AS СрокЛизинга,
//  _Fld7882 AS ВидЛизинга,
//  _Fld7884 AS Курс,
//  _Fld7885 AS КонтрактНомер,
//  _Fld7886 AS КонтрактДата,
//  _Fld9375 AS Статус,
//  _Fld9376 AS Менеджер,
//  _Fld9377 AS Регион,
//  _Fld9379 AS Продукт

/*
Взаиморасчеты
SELECT 
    date(case when "DBA"."_AccumRg13959"."_Fld15653" between '0001-01-01' AND '4001-01-01' then '0001-01-01' else DATEADD(year , -2000, "DBA"."_AccumRg13959"."_Fld15653") end) AS "ДатаОперации",
    date(case when "DBA"."_AccumRg13959"."_Fld13962" between '0001-01-01' AND '4001-01-01' then '0001-01-01' else DATEADD(year , -2000, "DBA"."_AccumRg13959"."_Fld13962") end) AS "ДатаГрафика",
    "DBA"."_AccumRg13959"."_Fld15654" AS "ID_ДоговорЛизинга",
    "DBA"."_AccumRg13959"."_Fld15655" AS "ID_Приложение",
    "DBA"."_AccumRg13959"."_Fld15656" AS "ID_Статья",
    "DBA"."_AccumRg13959"."_Fld15657" AS "Валюта",
    "DBA"."_AccumRg13959"."_Fld15658" AS "Сумма",
    "DBA"."_AccumRg13959"."_Fld15659" AS "Сторно",
    "DBA"."_AccumRg13959"."_Fld15660" AS "Комментарий"
FROM 
    "DBA"."_AccumRg13959" 
*/

/*
Взаиморасчеты бух
SELECT 
    date(case when "DBA"."_AccumRg15815"."_Period" between '0001-01-01' AND '4001-01-01' then '0001-01-01' else DATEADD(year , -2000, "DBA"."_AccumRg15815"."_Period") end) AS "ДатаОперации",
    date(case when "DBA"."_AccumRg15815"."_Fld15818" between '0001-01-01' AND '4001-01-01' then '0001-01-01' else DATEADD(year , -2000, "DBA"."_AccumRg15815"."_Fld15818") end) AS "ДатаГрафика",
    "DBA"."_AccumRg15815"."_Fld15826" AS "Сумма",
    "DBA"."_AccumRg15815"."_Fld15822" AS "ID_Контрагент",
    "DBA"."_AccumRg15815"."_Fld15823" AS "ID_Заказ",
    "DBA"."_AccumRg15815"."_Fld15824" AS "ID_Статья",
    "DBA"."_AccumRg15815"."_Fld15825" AS "ID_Валюта"
FROM 
    "DBA"."_AccumRg15815"
    */

/*
Портфель
SELECT 
    date(case when "_AccumRg158370"."_Period" between '0001-01-01' AND '4001-01-01' then '0001-01-01' else DATEADD(year , -2000, "_AccumRg158370"."_Period") end) AS "ДатаОперации",
    "_AccumRg158370"."_Fld15841" AS "ID_Контрагент",
    "_AccumRg158370"."_Fld15842" AS "ID_Заказ",
    "_AccumRg158370"."_Fld15843" AS "Сумма"
FROM 
    "DBA"."_AccumRg15837" AS "_AccumRg158370"
 */

/*
 договоры
    SELECT 
    TRIM("DBA"."_InfoRg14815"."_Fld14820")AS "НомерДоговора",
    date(case when "DBA"."_InfoRg14815"."_Fld14821" between '0001-01-01' AND '4001-01-01' then null else DATEADD(year , -2000, "DBA"."_InfoRg14815"."_Fld14821") end) AS "ДатаДоговора",
    date(case when "DBA"."_InfoRg14815"."_Fld14822" between '0001-01-01' AND '4001-01-01' then null else DATEADD(year , -2000, "DBA"."_InfoRg14815"."_Fld14822") end) AS "ДатаПодписания",
    date(case when "DBA"."_InfoRg14815"."_Fld14823" between '0001-01-01' AND '4001-01-01' then null else DATEADD(year , -2000, "DBA"."_InfoRg14815"."_Fld14823") end) AS "ДатаПередачи",
    date(case when "DBA"."_InfoRg14815"."_Fld14824" between '0001-01-01' AND '4001-01-01' then null else DATEADD(year , -2000, "DBA"."_InfoRg14815"."_Fld14824") end) AS "ДатаОкончания",
    "DBA"."_InfoRg14815"."_Fld14825" AS "СуммаАванса",
    date(case when "DBA"."_InfoRg14815"."_Fld14826" between '0001-01-01' AND '4001-01-01' then null else DATEADD(year , -2000, "DBA"."_InfoRg14815"."_Fld14826") end) AS "ДатаАвансаОплата",
    "DBA"."_InfoRg14815"."_Fld14827" AS "СчетАванса",
    "DBA"."_InfoRg14815"."_Fld14828" AS "СуммаАвансаОплата",
    "DBA"."_InfoRg14815"."_Fld14829" AS "СуммаАдминПлатеж",
    date(case when "DBA"."_InfoRg14815"."_Fld14830" between '0001-01-01' AND '4001-01-01' then null else DATEADD(year , -2000, "DBA"."_InfoRg14815"."_Fld14830") end) AS "ДатаАдминПлатеж",
    "DBA"."_InfoRg14815"."_Fld14831" AS "СуммаАдминПлатежОплата",
    "DBA"."_InfoRg14815"."_Fld14832" AS "СуммаКомисия",
    "DBA"."_InfoRg14815"."_Fld14833" AS "СуммаТело",
    "DBA"."_InfoRg14815"."_Fld14834" AS "СуммаСервис",     
    "DBA"."_InfoRg14815"."_Fld14836" AS "IDрасчета",
    "DBA"."_InfoRg14815"."_Fld14837" AS "IDxml",
    "DBA"."_InfoRg14815"."_Fld14839" AS "СтатусДоговора",
    "DBA"."_InfoRg14815"."_Fld14840" AS "Менеджер",
    "DBA"."_InfoRg14815"."_Fld14841" AS "IDДоговорЛизинга",
    "DBA"."_InfoRg14815"."_Fld14842" AS "IDПриложение",
    "DBA"."_InfoRg14815"."_Fld14843" AS "IDКлиент",
    "DBA"."_InfoRg14815"."_Fld14844" AS "Валюта",
    "DBA"."_InfoRg14815"."_Fld14845" AS "Курс",
    "DBA"."_InfoRg14815"."_Fld14846" AS "Кратность",
    date(case when "DBA"."_InfoRg14815"."_Fld16072" between '0001-01-01' AND '4001-01-01' then null else DATEADD(year, -2000, "DBA"."_InfoRg14815"."_Fld16072") end) AS "ДатаСлива",
    "DBA"."_InfoRg14815"."_Fld16223" AS "Организация"
    FROM homnet_2.dbo._InfoRg14815"
*/