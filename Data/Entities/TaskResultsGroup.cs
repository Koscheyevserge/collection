using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Результат задачи
    /// </summary>
    public class TaskResultsGroup : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public int TaskTypeId { get; set; }
        public TaskType TaskType { get; set; }

        public ICollection<TaskResultsItem> Items { get; set; }
    }
}
