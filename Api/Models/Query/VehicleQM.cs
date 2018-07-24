using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class VehicleQM
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string VIN { get; set; }
        public int? ModelId { get; set; }
        public int? ManufacturedYear { get; set; }
        public VehicleModelQM Model { get; set; }
        public IEnumerable<AgreementQM> Agreements { get; set; }
        public VehicleTypeQM Type { get; set; }
    }
}
