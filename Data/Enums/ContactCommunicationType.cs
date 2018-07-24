using Data.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Enums
{
    /// <summary>
    /// Тип связи контакта
    /// </summary>
    public enum ContactCommunicationType
    {
        [RussianName("Н/Д")]
        Undefined = 0,
        [RussianName("Телефон")]
        Phone = 1,
        [RussianName("Email")]
        Email = 2
    }
}
