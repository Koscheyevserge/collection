using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class AgreementQM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime AgreementDate { get; set; }
        public DateTime ShippmentDate { get; set; }
        public double ParticipationAmount { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public double CurrencyRate { get; set; }
        public double ParticipationAmountUAH { get; set; }
        public int ClientId { get; set; }
        public IEnumerable<PaymentScheduleItemQM> PaymentSchedule { get; set; }
        public IEnumerable<PaymentQM> Payments { get; set; }
        public IEnumerable<AccrualQM> Accruals { get; set; }
    }
}
