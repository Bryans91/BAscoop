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

            using (var context = new BioscoopDb())
            {
                vm.Performance = context.PerformanceList.Single(p => p.PerformanceId == performanceId);
                vm.Movie = context.Movies.Single(p => p.id == vm.Performance.MovieId);
                vm.Discount = context.Discounts.Single(p => p.id == vm.Performance.MovieId);
            }

            Session["booking"] = vm;


            return View(vm);
        }

        [HttpPost]
        public ActionResult SecondStep(BookingInformationViewModel oudeVM)
        {
            BookingInformationViewModel vm = Session["booking"] as BookingInformationViewModel;
            vm.AantalMensen = oudeVM.AantalMensen;
            using (var context = new BioscoopDb())
            {
                if (oudeVM.Discountcode != null && context.Discounts.Single(p => p.code == oudeVM.Discountcode) != null)
                {
                    vm.Discount = context.Discounts.Single(p => p.code == oudeVM.Discountcode);
                    vm.TotaalPrijs = ((double)vm.AantalMensen * (double)vm.Movie.price) * ((double)(100 - vm.Discount.percentage) / (double)100);
                }
                else
                {
                    vm.TotaalPrijs = (double) vm.AantalMensen * (double) vm.Movie.price;
                }
            }
            Session["booking"] = vm;

            Booking booking = new Booking();

            ViewBag.DiscountId = new SelectList(db.Discounts, "id", "code", booking.DiscountId);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ThirdStep(BookingInformationViewModel oudeVM)
        {

            BookingInformationViewModel vm = Session["booking"] as BookingInformationViewModel;
            vm.AantalMensen = oudeVM.AantalMensen;
            vm.TotaalPrijs = (vm.AantalMensen * vm.Movie.price) / (vm.Discount.percentage / 100);
            Session["booking"] = oudeVM;

            Booking booking = new Booking();

            return View(oudeVM);
        }

        // GET: /Booking/
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Discount).Include(b => b.Guest).Include(b => b.Performance);
            return View(bookings.ToList());
        }

        // GET: /Booking/Details/5
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
        public ActionResult Create([Bind(Include="id,nrOfTickets,totalPrice,adres,city,postal,guestId,DiscountId,PerformanceId")] Booking booking)
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,nrOfTickets,totalPrice,adres,city,postal,guestId,DiscountId,PerformanceId")] Booking booking)
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
