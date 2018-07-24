using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Вариант результата задачи
    /// </summary>
    public class TaskResult : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public int TypeId { get; set; }
        public TaskType Type { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
