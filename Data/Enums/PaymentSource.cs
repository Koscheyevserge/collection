using Data.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Enums
{
    /// <summary>
    /// Источник погашения задолженности по ДФЛ
    /// </summary>
    public enum PaymentSource
    {
        [RussianName("Н/Д")]
        Undefined = 0,
        [RussianName("ULF")]
        ULF = 1,
        [RussianName("ТКБ")]
        TKB = 2
    }
}
