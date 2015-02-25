namespace BAscoop.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BAscoop.Models;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<BAscoop.Models.BioscoopDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BAscoop.Models.BioscoopDb";
        }

        protected override void Seed(BAscoop.Models.BioscoopDb context)
        {

            var movies = new List<Movie>{
                 new Movie { id = 1, title = "Sharknado 2", description = "Slechte film met een tornado met haaien", price = 3, duration = 123 },
                new Movie { id = 2, title = "Mega Mindy", description = "Superheldenfilm voor kids", price = 7, duration = 131 },
                new Movie { id = 3, title = "The Imitationgame", description = "Film over Alan Turing", price = 13, duration = 141 },
                new Movie { id = 4, title = "Batman", description = "Met Adam West", price = 10, duration = 100 },
                new Movie { id = 5, title = "Pokemon de film", description = "Pokemon film met MewTwo", price = 12, duration = 120 },
                new Movie { id = 6, title = "The Hobbit", description = "Prequel van LOTR, in 3D", price = 14, duration = 151 }

            };
            movies.ForEach(mov => context.Movies.AddOrUpdate(m => m.id, mov));
            context.SaveChanges();


            var discounts = new List<Discount>{
                new Discount {id = 1, code = "januari" , percentage = 20 ,  StartTijd = new DateTime(2015,1,1) , endDate = new DateTime(2015,2,1) },
                new Discount { id = 2, code = "now", percentage = 40, StartTijd = DateTime.Today, endDate = new DateTime(2016, 2, 1) }
            };
            discounts.ForEach(disc => context.Discounts.AddOrUpdate(d => d.id, disc));
            context.SaveChanges();



            var cinemarooms = new List<Cinemaroom>{   
                new Cinemaroom { id = 1, name = "Zaal 1", capacity = 50, number = 1 },
                new Cinemaroom { id = 2, name = "Zaal 2", capacity = 70, number = 2 },
                new Cinemaroom { id = 3, name = "Zaal 3", capacity = 75, number = 3 },
                new Cinemaroom { id = 4, name = "Zaal 4", capacity = 80, number = 4 }
            };
            cinemarooms.ForEach(room => context.Rooms.AddOrUpdate(r => r.id, room));
            context.SaveChanges();

            var guests = new List<Guest>{
                new Guest { id = 1, firstName = "Alexander", suffix = "van", lastName = "Doorn" },
                new Guest { id = 2, firstName = "Bryan", suffix = "", lastName = "Schreuder" },
                new Guest { id = 3, firstName = "Ger", suffix = "", lastName = "Saris" },
                new Guest { id = 4, firstName = "Jasper", suffix = "van", lastName = "Rosmalen" },
                new Guest { id = 5, firstName = "Joep", suffix = "van der", lastName = "Broek" }
            };
            guests.ForEach(guest => context.Guests.AddOrUpdate(g => g.id, guest));
            context.SaveChanges();

            var performances = new List<Performance>{
                new Performance { PerformanceId = 1, StartTijd = new DateTime(2014,11,1,8,45,30), CinemaroomId = cinemarooms.Single(r => r.id == 1).id , MovieId = movies.Single(m => m.id == 2).id},
                new Performance { PerformanceId = 2,  StartTijd = new DateTime(2014,11,1,8,45,30), CinemaroomId = cinemarooms.Single(r => r.id == 3).id,MovieId = movies.Single(m => m.id == 2).id},
                new Performance { PerformanceId = 3 , StartTijd = new DateTime(2014,10,1,8,45,30), CinemaroomId = cinemarooms.Single(r => r.id == 2).id, MovieId = movies.Single(m => m.id == 4).id}

            };
            performances.ForEach(perforamce => context.PerformanceList.AddOrUpdate(p => p.PerformanceId, perforamce));
            context.SaveChanges();

            var bookings = new List<Booking>{
                new Booking { id = 1 , adres = "Straat 4", postal = "1234AB", city = "Amsterdam" , nrOfTickets = 4 , totalPrice = 50 , guestId = guests.Single(i => i.id == 1).id , DiscountId = discounts.Single(d => d.id == 1).id , PerformanceId = performances.Single(p => p.PerformanceId == 1).PerformanceId },
                new Booking { id = 2 , adres = "Straat 1", postal = "4321AD", city = "Utrecht" , nrOfTickets = 2 , totalPrice = 25 , guestId = guests.Single(i => i.id == 2).id , DiscountId = discounts.Single(d => d.id == 2).id , PerformanceId = performances.Single(p => p.PerformanceId == 2).PerformanceId },
                new Booking { id = 3 , adres = "Straat 2", postal = "1244AC", city = "Den Bosch" , nrOfTickets = 1 , totalPrice = 13 , guestId = guests.Single(i => i.id == 3).id , DiscountId = discounts.Single(d => d.id == 1).id , PerformanceId = performances.Single(p => p.PerformanceId == 3).PerformanceId }
            };
            bookings.ForEach(booking => context.Bookings.AddOrUpdate(b => b.id, booking));
            context.SaveChanges();

            userSeed();
        }



        protected void userSeed()
        {

            var context = new BAscoop.Models.ApplicationDbContext();
      
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
              
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin" };
                user.PasswordHash = manager.PasswordHasher.HashPassword("admin");
                manager.Create(user);  
                manager.AddToRole(user.Id, "Admin");
                
            }
        }

    
 
    }
}
