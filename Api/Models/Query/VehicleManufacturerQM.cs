﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class VehicleManufacturerQM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<VehicleModelQM> Models { get; set; }
    }
}
