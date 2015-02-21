using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BAscoop.Models
{
    public class Cinemaroom
    {
        [Key]
        public int id { get; set; }
        public int number { get; set; }
        public int capacity { get; set; }
        public string name { get; set; }
    }
}