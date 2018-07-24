using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Upr.Entities
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public decimal? Sum { get; set; }
        public string AgreementId { get; set; }
        public string Currency { get; set; }
        public DateTime? Date { get; set; }
    }
}
