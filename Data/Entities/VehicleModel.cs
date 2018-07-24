using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Модель ТС
    /// </summary>
    public class VehicleModel : BaseEntity
    {
        public string Name { get; set; }
        public int? ManufacturerId { get; set; }
        public VehicleManufacturer Manufacturer { get; set; }
        public int? EngineVolume { get; set; }
        public VehicleType Type { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
