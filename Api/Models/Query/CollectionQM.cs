using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class CollectionQM<T> where T : class
    {
        public int Count { get; set; }
        public IEnumerable<T> Values { get; set; }
    }
}
