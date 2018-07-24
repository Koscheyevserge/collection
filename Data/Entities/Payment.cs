using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Платеж по ДФЛ
    /// </summary>
    public class Payment : BaseEntity
    {
        public Guid? UprId { get; set; }
        public DateTime? Date { get; set; }
        public PaymentSource Source { get; set; }
        public Currency Currency { get; set; }
        public double Amount { get; set; }
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
    }
}
