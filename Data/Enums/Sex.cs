using Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Enums
{
    /// <summary>
    /// Пол
    /// </summary>
    public enum Sex
    {
        [RussianName("Н/Д")]
        Undefined = 0,
        [RussianName("Мужской")]
        Male = 1,
        [RussianName("Женский")]
        Female = 2
    }
}
