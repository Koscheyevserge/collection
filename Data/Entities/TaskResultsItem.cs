using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Вариант результата задачи
    /// </summary>
    public class TaskResultsItem : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public int ResultsGroupId { get; set; }
        public TaskResultsGroup ResultsGroup { get; set; }
    }
}
