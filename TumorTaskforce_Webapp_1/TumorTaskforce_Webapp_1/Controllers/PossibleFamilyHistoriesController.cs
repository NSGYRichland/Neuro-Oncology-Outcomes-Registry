using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TumorTaskforce_Webapp_1;

namespace TumorTaskforce_Webapp_1.Controllers
{
    public class PossibleFamilyHistoriesController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: PossibleFamilyHistories
        public ActionResult Index()
        {
            return View(db.PossibleFamilyHistories.ToList());
        }

        // GET: PossibleFamilyHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleFamilyHistory possibleFamilyHistory = db.PossibleFamilyHistories.Find(id);
            if (possibleFamilyHistory == null)
            {
                return HttpNotFound();
            }
            return View(possibleFamilyHistory);
        }

        // GET: PossibleFamilyHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PossibleFamilyHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] PossibleFamilyHistory possibleFamilyHistory)
        {
            if (ModelState.IsValid)
            {
                db.PossibleFamilyHistories.Add(possibleFamilyHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(possibleFamilyHistory);
        }

        // GET: PossibleFamilyHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleFamilyHistory possibleFamilyHistory = db.PossibleFamilyHistories.Find(id);
            if (possibleFamilyHistory == null)
            {
                return HttpNotFound();
            }
            return View(possibleFamilyHistory);
        }

        // POST: PossibleFamilyHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] PossibleFamilyHistory possibleFamilyHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(possibleFamilyHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(possibleFamilyHistory);
        }

        // GET: PossibleFamilyHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleFamilyHistory possibleFamilyHistory = db.PossibleFamilyHistories.Find(id);
            if (possibleFamilyHistory == null)
            {
                return HttpNotFound();
            }
            return View(possibleFamilyHistory);
        }

        // POST: PossibleFamilyHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PossibleFamilyHistory possibleFamilyHistory = db.PossibleFamilyHistories.Find(id);
            db.PossibleFamilyHistories.Remove(possibleFamilyHistory);
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
