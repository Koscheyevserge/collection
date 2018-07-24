using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class TaskTypeQM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<TaskResultQM> Results { get; set; }
    }
}
