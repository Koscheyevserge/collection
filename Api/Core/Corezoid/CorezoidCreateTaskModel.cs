using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Api.Core.Corezoid
{
    public class CorezoidCreateTaskModel
    {
        public string Ref { get; set; }
        public string Type { get; set; }
        public string Obj { get; set; }
        public string Conv_id { get; set; }
        public string Data { get; set; }

        public CorezoidCreateTaskModel(object data, int conv_id, string key)
        {
            Type = "create";
            Obj = "task";
            Ref = key;
            Conv_id = conv_id.ToString();
            Data = JsonConvert.SerializeObject(data == null ? new { } : data);
        }
    }
}
