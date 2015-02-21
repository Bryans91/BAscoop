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
    public class PopcorntimeController : Controller
    {
        private BioscoopDb db = new BioscoopDb();

        // GET: /Popcorntime/
        public ActionResult Index()
        {
            return View(db.Popcorntimes.ToList());
        }

        // GET: /Popcorntime/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Popcorntime popcorntime = db.Popcorntimes.Find(id);
            if (popcorntime == null)
            {
                return HttpNotFound();
            }
            return View(popcorntime);
        }

        // GET: /Popcorntime/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Popcorntime/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,startingTime,endTime")] Popcorntime popcorntime)
        {
            if (ModelState.IsValid)
            {
                db.Popcorntimes.Add(popcorntime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(popcorntime);
        }

        // GET: /Popcorntime/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Popcorntime popcorntime = db.Popcorntimes.Find(id);
            if (popcorntime == null)
            {
                return HttpNotFound();
            }
            return View(popcorntime);
        }

        // POST: /Popcorntime/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,startingTime,endTime")] Popcorntime popcorntime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(popcorntime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(popcorntime);
        }

        // GET: /Popcorntime/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Popcorntime popcorntime = db.Popcorntimes.Find(id);
            if (popcorntime == null)
            {
                return HttpNotFound();
            }
            return View(popcorntime);
        }

        // POST: /Popcorntime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Popcorntime popcorntime = db.Popcorntimes.Find(id);
            db.Popcorntimes.Remove(popcorntime);
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
