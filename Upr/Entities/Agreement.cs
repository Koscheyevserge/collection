using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Upr.Entities
{
    public class Agreement
    {
        [Key]
        public string Id { get; set; }
        public string Code { get; set; }
        public int? CalcId { get; set; }
        /// <summary>
        /// Идентификатор в базе данных upr_ulf_finance
        /// </summary>
        public string ClientId { get; set; }
        public string VehicleId { get; set; }
        public string Currency { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? ShippmentDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
