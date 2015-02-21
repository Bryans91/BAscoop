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
    public class PerformanceController : Controller
    {
        private BioscoopDb db = new BioscoopDb();

        // GET: /Performance/
        public ActionResult Index()
        {
            var performancelist = db.PerformanceList.Include(p => p.Cinemaroom).Include(p => p.Movie);
            return View(performancelist.ToList());
        }

        // GET: /Performance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.PerformanceList.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // GET: /Performance/Create
        public ActionResult Create()
        {
            ViewBag.CinemaroomId = new SelectList(db.Rooms, "id", "name");
            ViewBag.MovieId = new SelectList(db.Movies, "id", "title");
            return View();
        }

        // POST: /Performance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PerformanceId,StartTijd,MovieId,CinemaroomId")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.PerformanceList.Add(performance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CinemaroomId = new SelectList(db.Rooms, "id", "name", performance.CinemaroomId);
            ViewBag.MovieId = new SelectList(db.Movies, "id", "title", performance.MovieId);
            return View(performance);
        }

        // GET: /Performance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.PerformanceList.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            ViewBag.CinemaroomId = new SelectList(db.Rooms, "id", "name", performance.CinemaroomId);
            ViewBag.MovieId = new SelectList(db.Movies, "id", "title", performance.MovieId);
            return View(performance);
        }

        // POST: /Performance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PerformanceId,StartTijd,MovieId,CinemaroomId")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CinemaroomId = new SelectList(db.Rooms, "id", "name", performance.CinemaroomId);
            ViewBag.MovieId = new SelectList(db.Movies, "id", "title", performance.MovieId);
            return View(performance);
        }

        // GET: /Performance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.PerformanceList.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // POST: /Performance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Performance performance = db.PerformanceList.Find(id);
            db.PerformanceList.Remove(performance);
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
