using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BAscoop.Models
{
    public class Guest
    {
        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public string suffix { get; set; }
        public string lastName { get; set; }


    }
}