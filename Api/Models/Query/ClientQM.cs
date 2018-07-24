using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class ClientQM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ClientTypeQM Type { get; set; }
        public IEnumerable<AgreementQM> Agreements { get; set; }
        public IEnumerable<TaskQM> Tasks { get; set; }
        public IEnumerable<ContactQM> Contacts { get; set; }
    }
}
