using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Тип задачи
    /// </summary>
    public class TaskType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public ICollection<Task> Tasks { get; set; }
        public ICollection<TaskResultsGroup> ResultsGroups { get; set; }
    }
}
