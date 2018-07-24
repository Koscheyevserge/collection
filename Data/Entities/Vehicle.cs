using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Vehicle : BaseEntity
    {
        public string Number { get; set; }
        public Guid? UprId { get; set; }
        public string VIN { get; set; }
        public int? ModelId { get; set; }
        public int? ManufacturedYear { get; set; }
        public VehicleModel Model { get; set; }
        public virtual ICollection<Agreement> Agreements { get; set; }
    }
}
