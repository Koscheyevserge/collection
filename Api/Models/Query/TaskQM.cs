using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class TaskQM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TaskTypeQM Type { get; set; }
        public ClientQM Client { get; set; }
        public TaskResultsItemQM Result { get; set; }
    }
}
