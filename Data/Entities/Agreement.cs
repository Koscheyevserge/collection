using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Договор финансового лизинга
    /// </summary>
    public class Agreement : BaseEntity
    {
        /// <summary>
        /// Дата подписания
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// Номер договора
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Дата поставки Предмета лизинга
        /// </summary>
        public DateTime? ShippmentDate { get; set; }
        /// <summary>
        /// Идентификатор записи в upr_ulf_finance._InfoRg7874
        /// </summary>
        public Guid? UprId { get; set; }
        /// <summary>
        /// Идентификатор записи в front
        /// </summary>
        public int? CalcId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        /// <summary>
        /// Валюта договора
        /// </summary>
        public Currency Currency { get; set; }
        /// <summary>
        /// Сумма участия в единицах валюты
        /// </summary>
        public double ParticipationAmount { get; set; }

        public virtual ICollection<Accrual> Accruals { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<PaymentSheduleItem> PaymentShedule { get; set; }
    }
}
