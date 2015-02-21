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
    public class RoomController : Controller
    {
        private BioscoopDb db = new BioscoopDb();

        // GET: /Room/
        public ActionResult Index()
        {
            return View(db.Rooms.ToList());
        }

        // GET: /Room/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinemaroom cinemaroom = db.Rooms.Find(id);
            if (cinemaroom == null)
            {
                return HttpNotFound();
            }
            return View(cinemaroom);
        }

        // GET: /Room/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,number,capacity,name")] Cinemaroom cinemaroom)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(cinemaroom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cinemaroom);
        }

        // GET: /Room/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinemaroom cinemaroom = db.Rooms.Find(id);
            if (cinemaroom == null)
            {
                return HttpNotFound();
            }
            return View(cinemaroom);
        }

        // POST: /Room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,number,capacity,name")] Cinemaroom cinemaroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cinemaroom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cinemaroom);
        }

        // GET: /Room/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinemaroom cinemaroom = db.Rooms.Find(id);
            if (cinemaroom == null)
            {
                return HttpNotFound();
            }
            return View(cinemaroom);
        }

        // POST: /Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cinemaroom cinemaroom = db.Rooms.Find(id);
            db.Rooms.Remove(cinemaroom);
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
