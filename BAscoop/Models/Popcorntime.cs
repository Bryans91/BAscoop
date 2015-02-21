using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAscoop.Models
{
    public class Popcorntime
    {
        public int id { get; set; }
        public DateTime startingTime { get; set; }
        public DateTime endTime { get; set; }
        public Cinemaroom room { get; set; }
        public Movie movie { get; set; }
    }
}