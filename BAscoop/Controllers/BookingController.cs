using BAscoop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BAscoop.Controllers
{
    public class BookingController : Controller
    {
        //
        // GET: /Booking/
        private BioscoopDb db = new BioscoopDb();

        public ActionResult FirstStep(int performanceId)
        {
            BookingInformationViewModel vm = new BookingInformationViewModel();

            using (var context = new BioscoopDb())
            {
                vm.Performance = context.PerformanceList.Single(p => p.PerformanceId == performanceId);
            }

            Session["booking"] = vm;


            return View(vm);
        }

        [HttpPost]
        public ActionResult SecondStep(BookingInformationViewModel vm)
        {
            BookingInformationViewModel oudeVM = Session["booking"] as BookingInformationViewModel;
            oudeVM.AantalMensen = vm.AantalMensen;
            Session["booking"] = oudeVM;

            Booking booking = new Booking();

            return View();
        }

        


        public ActionResult Index()
        {

            List<Booking> booking = db.Bookings.ToList();
            return View(booking);
        }

        //
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

        //
        // GET: /Booking/Create

        public ActionResult Create(int? movieId)
        {
            return View(movieId);
        }

        //
        // POST: /Booking/Create

        [HttpPost]
        public ActionResult Create([Bind(Include = "nrOfTickets, accountNumber, totalPrice, adres, city, postal")]Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (System.Data.DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(booking);
        }



        //Edit post
        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bookingToUpdate = db.Bookings.Find(id);
            if (TryUpdateModel(bookingToUpdate, "",
               new string[] { "nrOfTickets", "accountNumber", "totalPrice", "adres","city","postal" }))
            {
                try
                {
                    db.Entry(bookingToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(bookingToUpdate);
        }

        //

        // GET: /Booking/Edit/5

        public ActionResult Edit(int id)
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

        //
        // GET: /Booking/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        //
        // POST: /Booking/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Booking booking = db.Bookings.Find(id);
                db.Bookings.Remove(booking);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}
