using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Связь с клиентом
    /// </summary>
    public class ContactCommunication : BaseEntity
    {
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public ContactCommunicationType Type { get; set; }
        public string Number { get; set; }
        public bool IsMain { get; set; }
    }
}
