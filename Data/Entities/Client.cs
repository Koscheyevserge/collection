using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class Client : BaseEntity
    {
        /// <summary>
        /// Название организации или ФИО клиента
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Полное название организации или ФИО клиента
        /// </summary>
        public string NameFull { get; set; }
        /// <summary>
        /// ЕГРПОУ
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Налоговый номер
        /// </summary>
        public string TaxNumber { get; set; }
        /// <summary>
        /// Тип клиента - Юрлицо, Физлицо или СПД
        /// </summary>
        public ClientType Type { get; set; }
        /// <summary>
        /// Идентификатор записи в upr_ulf_finance._InfoRg7815
        /// </summary>
        public Guid? UprId { get; set; }
        public string AddressJuridical { get; set; }
        public string AddressPhysical { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Agreement> Agreements { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
