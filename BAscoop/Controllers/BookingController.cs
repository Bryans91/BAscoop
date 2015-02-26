using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BAscoop.Models;

namespace BAscoop.Controllers
{
    public class BookingController : Controller
    {
        private BioscoopDb db = new BioscoopDb();
       
        public ActionResult FirstStep(int performanceId)
        {
            BookingInformationViewModel vm = new BookingInformationViewModel();

            //vm.Performance = db.PerformanceList.Single(p => p.PerformanceId == performanceId);
            //vm.Movie = db.Movies.Single(p => p.id == vm.Performance.MovieId);
            vm.Performance = db.PerformanceList.Find(performanceId);
            vm.Movie = vm.Performance.Movie;

            Session["booking"] = vm;


            return View(vm);
        }

        [HttpPost]
        public ActionResult SecondStep(BookingInformationViewModel oudeVM)
        {
            BookingInformationViewModel vm = Session["booking"] as BookingInformationViewModel;
            int counter = 0;
            int maxPersons = db.Rooms.Find(vm.Performance.CinemaroomId).capacity; 
            foreach(Booking b in db.PerformanceList.Find(vm.Performance.PerformanceId).BookingList) {
                counter += b.nrOfTickets;
            }
            if (counter + oudeVM.AantalMensen <= maxPersons)
            {
                vm.AantalMensen = oudeVM.AantalMensen;
            }
            else
            {
                return View("Error");
            }
            if (oudeVM.Discountcode != null && db.Discounts.Where(d => d.code == oudeVM.Discountcode).First() != null && db.Discounts.Where(d => d.code == oudeVM.Discountcode).First().StartTijd >= DateTime.Now && db.Discounts.Where(d => d.code == oudeVM.Discountcode).First().endDate <= DateTime.Now)
            {
                vm.Discount = db.Discounts.Where(d => d.code == oudeVM.Discountcode).First();
                vm.TotaalPrijs = ((double)vm.AantalMensen * (double)vm.Movie.price) * ((double)(100 - vm.Discount.percentage) / (double)100);
            }
            else
            {
                vm.TotaalPrijs = (double)vm.AantalMensen * (double)vm.Movie.price;
            }
            Session["booking"] = vm;

            return View(vm);
        }

        [HttpPost]
        public ActionResult ThirdStep(BookingInformationViewModel oudeVM)
        {
            BookingInformationViewModel vm = Session["booking"] as BookingInformationViewModel;
            Session["booking"] = vm;
            Booking booking = new Booking();
            Guest guest = new Guest();
            guest.firstName = oudeVM.Guest.firstName;
            guest.suffix = oudeVM.Guest.suffix;
            guest.lastName = oudeVM.Guest.lastName;
            guest.adres = oudeVM.Guest.adres;
            guest.city = oudeVM.Guest.city;
            guest.postal = oudeVM.Guest.postal;
            guest = db.Guests.Add(guest);
            booking.guestId = guest.id;
            vm.Guest = guest;
            //booking.Guest = db.Guests.Single(p => p.id == (int)1);
            //vm.Guest = booking.Guest;
            booking.nrOfTickets = vm.AantalMensen;
            booking.totalPrice = vm.TotaalPrijs;
            booking.PerformanceId = vm.Performance.PerformanceId;
            if (vm.Discount != null)
            {
                booking.DiscountId = vm.Discount.id;
            }
            else
            {
                booking.DiscountId = (int)1;
            }
            vm.Discount = booking.Discount;
            booking = db.Bookings.Add(booking);
            db.SaveChanges();
            return View(vm);
        }

        // GET: /Booking/
        [Authorize]
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Discount).Include(b => b.Guest).Include(b => b.Performance);
            return View(bookings.ToList());
        }

        // GET: /Booking/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: /Booking/Create
        public ActionResult Create()
        {
            ViewBag.DiscountId = new SelectList(db.Discounts, "id", "code");
            ViewBag.guestId = new SelectList(db.Guests, "id", "firstName");
            ViewBag.PerformanceId = new SelectList(db.PerformanceList, "PerformanceId", "PerformanceId");
            return View();
        }

        // POST: /Booking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nrOfTickets,totalPrice,guestId,DiscountId,PerformanceId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DiscountId = new SelectList(db.Discounts, "id", "code", booking.DiscountId);
            ViewBag.guestId = new SelectList(db.Guests, "id", "firstName", booking.guestId);
            ViewBag.PerformanceId = new SelectList(db.PerformanceList, "PerformanceId", "PerformanceId", booking.PerformanceId);
            return View(booking);
        }

        // GET: /Booking/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiscountId = new SelectList(db.Discounts, "id", "code", booking.DiscountId);
            ViewBag.guestId = new SelectList(db.Guests, "id", "firstName", booking.guestId);
            ViewBag.PerformanceId = new SelectList(db.PerformanceList, "PerformanceId", "PerformanceId", booking.PerformanceId);
            return View(booking);
        }

        // POST: /Booking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nrOfTickets,totalPrice,guestId,DiscountId,PerformanceId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiscountId = new SelectList(db.Discounts, "id", "code", booking.DiscountId);
            ViewBag.guestId = new SelectList(db.Guests, "id", "firstName", booking.guestId);
            ViewBag.PerformanceId = new SelectList(db.PerformanceList, "PerformanceId", "PerformanceId", booking.PerformanceId);
            return View(booking);
        }

        // GET: /Booking/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: /Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
