using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarriersWebSolution.Models;
using Microsoft.AspNet.Identity;

namespace CarriersWebSolution.Controllers
{
    [Authorize]
    public class RateController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rate
        public ActionResult Index()
        {
            var rates = db.Rates.Include(r => r.Carrier).Include(r => r.User);
            return View(rates.ToList());
        }

        // GET: Rate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // GET: Rate/Create
        public ActionResult Create()
        {
            ViewBag.CarrierId = new SelectList(db.Carriers, "CarriersId", "Description");
            return View();
        }

        // POST: Rate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Value,Description,CarrierId")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                rate.UserId = User.Identity.GetUserId();
                if (db.Rates.Any(x => x.UserId == rate.UserId))
                {
                    ModelState.AddModelError("", "It is only possible one rate for each user");
                    return View(rate);
                }
                db.Rates.Add(rate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarrierId = new SelectList(db.Carriers, "CarriersId", "Description", rate.CarrierId);
            return View(rate);
        }

        // GET: Rate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarrierId = new SelectList(db.Carriers, "CarriersId", "Description", rate.CarrierId);
            return View(rate);
        }

        // POST: Rate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Value,Description,CarrierId")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarrierId = new SelectList(db.Carriers, "CarriersId", "Description", rate.CarrierId);
            return View(rate);
        }

        // GET: Rate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // POST: Rate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rate rate = db.Rates.Find(id);
            db.Rates.Remove(rate);
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
