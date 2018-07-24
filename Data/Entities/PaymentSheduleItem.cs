using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class PaymentSheduleItem : BaseEntity
    {
        public Guid? UprId { get; set; }
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
        public DateTime? Date { get; set; }
        public double Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
