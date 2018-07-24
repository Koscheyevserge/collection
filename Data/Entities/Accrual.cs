using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Начисление по ДФЛ
    /// </summary>
    public class Accrual : BaseEntity
    {
        public Guid? UprId { get; set; }
        public DateTime? Date { get; set; }
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
    }
}
