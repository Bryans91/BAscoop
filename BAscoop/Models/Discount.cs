using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BAscoop.Models
{
    public class Discount
    {
        [Key]
        public int id { get; set; }
        public int percentage { get; set; }
        public string code { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartTijd { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime endDate { get; set; }


    }
}