using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Command
{
    /// <summary>
    /// Базовый класс для всех моделей команд
    /// </summary>
    public class BaseСM
    {
        /// <summary>
        /// Ключ, по которому можно идентифицировать и отменить задачу
        /// </summary>
        public Guid Key { get; set; }
    }
}
