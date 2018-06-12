using Data.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Enums
{
    /// <summary>
    /// Вид клиента
    /// </summary>
    public enum ClientType
    {
        [RussianName("Н/Д")]
        Undefined = 0,
        [RussianName("Юрлицо")]
        Juridical = 1,
        [RussianName("Физлицо")]
        Physical = 2,
        [RussianName("ФЛП")]
        Entrepreneur = 3
    }
}
