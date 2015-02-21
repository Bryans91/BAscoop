using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BAscoop.Models
{
    public class BioscoopDb : DbContext
    {

        public BioscoopDb()
            : base("name=DefaultConnection")
        {
            Database.SetInitializer<BioscoopDb>(new DropCreateDatabaseIfModelChanges<BioscoopDb>());
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Cinemaroom> Rooms { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Popcorntime> Popcorntimes { get; set; }
        public DbSet<Performance> PerformanceList { get; set; }



    }
}