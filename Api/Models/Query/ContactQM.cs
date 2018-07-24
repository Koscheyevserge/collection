using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class ContactQM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronym { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public SexQM Sex { get; set; }
        public int? ClientId { get; set; }
        public bool IsMain { get; set; }

        public IEnumerable<ContactCommunicationQM> Communications { get; set; }
    }
}
