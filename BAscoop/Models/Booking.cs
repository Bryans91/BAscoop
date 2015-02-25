using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BAscoop.Models
{
    public class Booking
    {
        [Key]
        public int id { get; set; }
        public int nrOfTickets { get; set; }
        public int totalPrice { get; set; }
        public string adres { get; set; }
        public string city { get; set; }
        public string postal { get; set; }

        [ForeignKey("Guest")]
        public int guestId { get; set; }
        public virtual Guest Guest { get; set; }

        [ForeignKey("Discount")]
        public int DiscountId { get; set; }
        public virtual Discount Discount { get; set; }

        [ForeignKey("Performance")]
        public int PerformanceId { get; set; }
        public virtual Performance Performance { get; set; }



    }
}