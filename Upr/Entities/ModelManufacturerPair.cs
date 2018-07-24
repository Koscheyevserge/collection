using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Upr.Entities
{
    public class ModelManufacturerPair
    {
        [Key]
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int? EngineVolume { get; set; }
    }
}
