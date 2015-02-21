using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAscoop.Models
{
    public class Booking
    {
        public int id { get; set; }
        public int nrOfTickets { get; set; }
        public Guest guests { get; set; }
        public string accountNumber { get; set; }
        public int totalPrice { get; set; }
        public string adres { get; set; }
        public string city { get; set; }
        public string postal { get; set; }
        public Discount discount { get; set; }
        public Popcorntime popcorntime { get; set; }



    }
}