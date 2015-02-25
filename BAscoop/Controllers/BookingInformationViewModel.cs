using BAscoop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAscoop.Controllers
{
    public class BookingInformationViewModel
    {
        public Performance Performance { get; set; }

        public Movie Movie { get; set; }

        public int AantalMensen { get; set; }

        public double TotaalPrijs { get; set; }

        public Discount Discount { get; set; }

        public string Discountcode { get; set; }

        public Guest Guest { get; set; }
    }
}
