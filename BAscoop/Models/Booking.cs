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
        public string accountNumber { get; set; }
        public int totalPrice { get; set; }
        public string adres { get; set; }
        public string city { get; set; }
        public string postal { get; set; }

        [ForeignKey("Guest")]
        public int GuestId { get; set; }

        public virtual Guest guest { get; set; }

        [ForeignKey("Discount")]
        public int DiscountId { get; set; }

        public virtual Discount discount { get; set; }

        [ForeignKey("Popcorntime")]
        public int PopcorntimeId { get; set; }

        public virtual Popcorntime popcorntime { get; set; }



    }
}