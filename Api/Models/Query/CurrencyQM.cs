using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Query
{
    public class CurrencyQM
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string ShortName { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Курс к гривне
        /// </summary>
        public double CurrencyRate { get; set; }
    }
}
