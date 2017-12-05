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
    public class HealthFactorsPivotsController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: HealthFactorsPivots
        public ActionResult Index()
        {
            var healthFactorsPivots = db.HealthFactorsPivots.Include(h => h.Patient).Include(h => h.PossibleHealthFactor);
            return View(healthFactorsPivots.ToList());
        }

        // GET: HealthFactorsPivots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthFactorsPivot healthFactorsPivot = db.HealthFactorsPivots.Find(id);
            if (healthFactorsPivot == null)
            {
                return HttpNotFound();
            }
            return View(healthFactorsPivot);
        }

        // GET: HealthFactorsPivots/Create
        public ActionResult Create()
        {
            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID");
            ViewBag.datapieceID = new SelectList(db.PossibleHealthFactors, "Id", "Name");
            return View();
        }

        // POST: HealthFactorsPivots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,patientID,datapieceID,date,frequency,notes")] HealthFactorsPivot healthFactorsPivot)
        {
            if (ModelState.IsValid)
            {
                db.HealthFactorsPivots.Add(healthFactorsPivot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID", healthFactorsPivot.patientID);
            ViewBag.datapieceID = new SelectList(db.PossibleHealthFactors, "Id", "Name", healthFactorsPivot.datapieceID);
            return View(healthFactorsPivot);
        }

        // GET: HealthFactorsPivots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthFactorsPivot healthFactorsPivot = db.HealthFactorsPivots.Find(id);
            if (healthFactorsPivot == null)
            {
                return HttpNotFound();
            }
            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID", healthFactorsPivot.patientID);
            ViewBag.datapieceID = new SelectList(db.PossibleHealthFactors, "Id", "Name", healthFactorsPivot.datapieceID);
            return View(healthFactorsPivot);
        }

        // POST: HealthFactorsPivots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,patientID,datapieceID,date,frequency,notes")] HealthFactorsPivot healthFactorsPivot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(healthFactorsPivot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID", healthFactorsPivot.patientID);
            ViewBag.datapieceID = new SelectList(db.PossibleHealthFactors, "Id", "Name", healthFactorsPivot.datapieceID);
            return View(healthFactorsPivot);
        }

        // GET: HealthFactorsPivots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthFactorsPivot healthFactorsPivot = db.HealthFactorsPivots.Find(id);
            if (healthFactorsPivot == null)
            {
                return HttpNotFound();
            }
            return View(healthFactorsPivot);
        }

        // POST: HealthFactorsPivots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HealthFactorsPivot healthFactorsPivot = db.HealthFactorsPivots.Find(id);
            db.HealthFactorsPivots.Remove(healthFactorsPivot);
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
