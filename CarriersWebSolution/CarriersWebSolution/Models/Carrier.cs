using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarriersWebSolution.Models
{
    public class Carrier
    {
        [Key]
        public int CarriersId { get; set; }

        public string Description { get; set; }
        public string Name { get; set; }
        public virtual List<Rate> RateId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}