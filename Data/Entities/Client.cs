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
        /// ЕГРПОУ/ИНН
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Тип клиента - Юрлицо, Физлицо или ФОН
        /// </summary>
        public ClientType Type { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Agreement> Agreements { get; set; }
    }
}
