using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class PaymentScheduleItemQM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double CommissionAmount { get; set; }
        public double MainAmount { get; set; }
    }
}
