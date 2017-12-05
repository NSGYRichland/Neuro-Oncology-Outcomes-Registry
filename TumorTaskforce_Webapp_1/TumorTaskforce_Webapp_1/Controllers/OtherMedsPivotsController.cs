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
    public class OtherMedsPivotsController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: OtherMedsPivots
        public ActionResult Index()
        {
            var otherMedsPivots = db.OtherMedsPivots.Include(o => o.Patient).Include(o => o.PossibleOtherMed);
            return View(otherMedsPivots.ToList());
        }

        // GET: OtherMedsPivots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherMedsPivot otherMedsPivot = db.OtherMedsPivots.Find(id);
            if (otherMedsPivot == null)
            {
                return HttpNotFound();
            }
            return View(otherMedsPivot);
        }

        // GET: OtherMedsPivots/Create
        public ActionResult Create()
        {
            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID");
            ViewBag.datapieceID = new SelectList(db.PossibleOtherMeds, "Id", "Name");
            return View();
        }

        // POST: OtherMedsPivots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,patientID,datapieceID,date,frequency,notes")] OtherMedsPivot otherMedsPivot)
        {
            if (ModelState.IsValid)
            {
                db.OtherMedsPivots.Add(otherMedsPivot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID", otherMedsPivot.patientID);
            ViewBag.datapieceID = new SelectList(db.PossibleOtherMeds, "Id", "Name", otherMedsPivot.datapieceID);
            return View(otherMedsPivot);
        }

        // GET: OtherMedsPivots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherMedsPivot otherMedsPivot = db.OtherMedsPivots.Find(id);
            if (otherMedsPivot == null)
            {
                return HttpNotFound();
            }
            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID", otherMedsPivot.patientID);
            ViewBag.datapieceID = new SelectList(db.PossibleOtherMeds, "Id", "Name", otherMedsPivot.datapieceID);
            return View(otherMedsPivot);
        }

        // POST: OtherMedsPivots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,patientID,datapieceID,date,frequency,notes")] OtherMedsPivot otherMedsPivot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(otherMedsPivot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID", otherMedsPivot.patientID);
            ViewBag.datapieceID = new SelectList(db.PossibleOtherMeds, "Id", "Name", otherMedsPivot.datapieceID);
            return View(otherMedsPivot);
        }

        // GET: OtherMedsPivots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherMedsPivot otherMedsPivot = db.OtherMedsPivots.Find(id);
            if (otherMedsPivot == null)
            {
                return HttpNotFound();
            }
            return View(otherMedsPivot);
        }

        // POST: OtherMedsPivots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OtherMedsPivot otherMedsPivot = db.OtherMedsPivots.Find(id);
            db.OtherMedsPivots.Remove(otherMedsPivot);
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
