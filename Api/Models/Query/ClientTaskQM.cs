using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class ClientTaskQM : TaskQM
    {
        public ClientQM Client { get; set; }
        public override int TypeId { get { return TypeId; } set { TypeId = (int)TaskType.Client; } }
    }
}
