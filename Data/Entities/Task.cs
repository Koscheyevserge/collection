using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    /// <summary>
    /// Задача
    /// </summary>
    [Table("Tasks")]
    public class Task
    {
        public int Id { get; set; }
    }
}
