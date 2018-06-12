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
        [RussianName("Н/Д")]
        Undefined = 0,
        [RussianName("Грн")]
        UAH = 1,
        [RussianName("Долл")]
        USD_Межбанк = 2
    }
}
