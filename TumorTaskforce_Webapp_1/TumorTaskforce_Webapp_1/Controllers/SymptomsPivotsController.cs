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
    public class SymptomsPivotsController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: SymptomsPivots
        public ActionResult Index()
        {
            var symptomsPivots = db.SymptomsPivots.Include(s => s.Patient).Include(s => s.PossibleSymptom);
            return View(symptomsPivots.ToList());
        }

        // GET: SymptomsPivots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SymptomsPivot symptomsPivot = db.SymptomsPivots.Find(id);
            if (symptomsPivot == null)
            {
                return HttpNotFound();
            }
            return View(symptomsPivot);
        }

        // GET: SymptomsPivots/Create
        //public ActionResult Create()
        //{
        //    ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID");
        //    ViewBag.datapieceID = new SelectList(db.PossibleSymptoms, "Id", "Name");
        //    return View();
        //}

        public ActionResult Create(int? patientID)
        {
            if (patientID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient p = db.Patients.Find(patientID);
            if (p == null)
            {
                return HttpNotFound();
            }
            Patient[] sel = new Patient[1];
            sel[0] = p;
            ViewBag.patientID = new SelectList(sel, "patientID", "patientID");
            ViewBag.datapieceID = new SelectList(db.PossibleSymptoms, "Id", "Name");
            return View();
        }

        // POST: SymptomsPivots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,patientID,datapieceID,date,frequency,notes")] SymptomsPivot symptomsPivot)
        {
            if (ModelState.IsValid)
            {
                db.SymptomsPivots.Add(symptomsPivot);
                db.SaveChanges();
                return RedirectToAction("Details", "Patients", new { id = symptomsPivot.patientID });
            }

            Patient[] sel = new Patient[1];
            sel[0] = db.Patients.Find(symptomsPivot.patientID);
            ViewBag.patientID = new SelectList(sel, "patientID", "patientID");
            ViewBag.datapieceID = new SelectList(db.PossibleSymptoms, "Id", "Name", symptomsPivot.datapieceID);
            return View(symptomsPivot);
        }

        // GET: SymptomsPivots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SymptomsPivot symptomsPivot = db.SymptomsPivots.Find(id);
            if (symptomsPivot == null)
            {
                return HttpNotFound();
            }
            Patient[] sel = new Patient[1];
            sel[0] = db.Patients.Find(symptomsPivot.patientID);
            ViewBag.patientID = new SelectList(sel, "patientID", "patientID");
            ViewBag.datapieceID = new SelectList(db.PossibleSymptoms, "Id", "Name", symptomsPivot.datapieceID);
            return View(symptomsPivot);
        }

        // POST: SymptomsPivots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,patientID,datapieceID,date,frequency,notes")] SymptomsPivot symptomsPivot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(symptomsPivot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Patients", new { id = symptomsPivot.patientID });
            }
            Patient[] sel = new Patient[1];
            sel[0] = db.Patients.Find(symptomsPivot.patientID);
            ViewBag.patientID = new SelectList(sel, "patientID", "patientID");
            ViewBag.datapieceID = new SelectList(db.PossibleSymptoms, "Id", "Name", symptomsPivot.datapieceID);
            return View(symptomsPivot);
        }

        // GET: SymptomsPivots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SymptomsPivot symptomsPivot = db.SymptomsPivots.Find(id);
            if (symptomsPivot == null)
            {
                return HttpNotFound();
            }
            return View(symptomsPivot);
        }

        // POST: SymptomsPivots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SymptomsPivot symptomsPivot = db.SymptomsPivots.Find(id);
            db.SymptomsPivots.Remove(symptomsPivot);
            db.SaveChanges();
            return RedirectToAction("Details", "Patients", new { id = symptomsPivot.patientID });
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
