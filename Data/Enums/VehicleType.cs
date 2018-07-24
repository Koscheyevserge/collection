using Data.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Enums
{
    /// <summary>
    /// Тип ТС
    /// </summary>
    public enum VehicleType
    {
        [RussianName("Н/Д")]
        Undefined = 0,
        [RussianName("Легковой")]
        Car = 1,
        [RussianName("Прицеп")]
        Trailer = 2
    }
}
