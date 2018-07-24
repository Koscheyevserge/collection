using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class ContactCommunicationQM
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public ContactCommunicationTypeQM Type { get; set; }
        public string Number { get; set; }
        public bool IsMain { get; set; }
    }
}
