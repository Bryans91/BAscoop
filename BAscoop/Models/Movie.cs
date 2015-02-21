using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BAscoop.Models
{
    public class Movie
    {
        [Key]
        public int id { get; set; }
        public int price { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int duration { get; set; }

        public virtual List<Performance> PerformanceList { get; set; }


    }
}