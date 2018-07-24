using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Марка ТС
    /// </summary>
    public class VehicleManufacturer : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<VehicleModel> Models { get; set; }
    }
}
