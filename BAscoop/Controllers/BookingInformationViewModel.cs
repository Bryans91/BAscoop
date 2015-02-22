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

        public int TotaalPrijs { get; set; }
    }
}
