using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class VehicleModelQM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ManufacturerId { get; set; }
        public VehicleManufacturerQM Manufacturer { get; set; }
        public int? EngineVolume { get; set; }
        public VehicleTypeQM Type { get; set; }
        public IEnumerable<VehicleQM> Vehicles { get; set; }
    }
}
