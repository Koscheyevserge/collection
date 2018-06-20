using Data.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Enums
{
    /// <summary>
    /// Валюта
    /// </summary>
    public enum Currency
    {
        [RussianName(ShortName = "н/д", Symbol = "", Abbreviation = "", Name = "Неизвестно")]
        Undefined = 0,
        [RussianName(ShortName = "грн", Symbol = "₴", Abbreviation = "UAH", Name = "Украинская гривна")]
        UAH = 1,
        [RussianName(ShortName = "долл", Symbol = "$", Abbreviation = "USD", Name = "Доллар США (Межбанк)")]
        USD_Межбанк = 2
    }
}
