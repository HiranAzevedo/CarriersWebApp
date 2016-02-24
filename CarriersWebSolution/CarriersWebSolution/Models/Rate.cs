using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarriersWebSolution.Models
{
    public class Rate
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RateId { get; set; }
        [Key, Column(Order = 1), Index(IsUnique = true)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public int CarrierId { get; set; }
        [ForeignKey("CarrierId")]
        public virtual Carrier Carrier { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}