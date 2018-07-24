using Api.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class TaskResultQM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public int TypeId { get; set; }
    }
}
