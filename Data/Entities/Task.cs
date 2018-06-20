using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Задача
    /// </summary>
    public class Task : BaseEntity
    {
        public DateTime Date { get; set; }

        public int? ClientId { get; set; }
        public Client Client { get; set; }

        public int TypeId { get; set; }
        public TaskType Type { get; set; }

        public int? ResultId { get; set; }
        public TaskResultsItem Result { get; set; }
    }
}
