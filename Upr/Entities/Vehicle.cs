using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Upr.Entities
{
    public class Vehicle
    {
        [Key]
        public string Id { get; set; }
        public string VIN { get; set; }
        public string Number { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int? EngineVolume { get; set; }
        public string ManufacturedYear { get; set; }
    }
}
