using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BAscoop.Models
{
    public class Performance
    {
        [Key]
        public int PerformanceId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartTijd { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        [ForeignKey("Cinemaroom")]
        public int CinemaroomId { get; set; }

        public virtual Cinemaroom Cinemaroom {get; set;}

    }
}