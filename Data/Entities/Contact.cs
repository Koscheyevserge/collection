using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Контакт
    /// </summary>
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronym { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Sex Sex { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public bool IsMain { get; set; }

        public virtual ICollection<ContactCommunication> Communications { get; set; }
    }
}
