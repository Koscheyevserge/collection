using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Upr.Entities
{
    public class Client
    {
        [Key]
        public string Id { get; set; }
        public string Code { get; set; }
        public string TaxNumber { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }
        public string AddressJur { get; set; }
        public string AddressPhys { get; set; }
    }
}
