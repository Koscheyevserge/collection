using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Upr.Entities
{
    public class Payment
    {
        [Key]
        public string Id { get; set; }
    }
}
