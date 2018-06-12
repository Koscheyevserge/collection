using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class AccrualQM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public double CurrencyRate { get; set; }
        public double AmountUAH { get; set; }
        public string Number { get; set; }
        public int AgreementId { get; set; }
    }
}
