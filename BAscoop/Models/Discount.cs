using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAscoop.Models
{
    public class Discount
    {
        public int id { get; set; }
        public int percentage { get; set; }
        public string code { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }


    }
}