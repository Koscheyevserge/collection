using Api.Controllers.Query;
using System.Collections.Generic;

namespace Api.Models.Query
{
    public class TaskResultsGroupQM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public int TaskTypeId { get; set; }

        public IEnumerable<TaskResultsItemQM> Items { get; set; }
    }
}