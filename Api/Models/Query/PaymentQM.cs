using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class PaymentQM
    {
        public int Id { get; set; }
        public int? AgreementId { get; set; }
        public string Code { get; set; }
        public CurrencyQM Currency { get; set; }
        public DateTime? Date { get; set; }
        public PaymentSourceQM Source { get; set; }
        public double Amount { get; set; }
    }
}
