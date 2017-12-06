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
    public class TreatmentsPivotsController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: TreatmentsPivots
        public ActionResult Index()
        {
            var treatmentsPivots = db.TreatmentsPivots.Include(t => t.Patient).Include(t => t.PossibleTreatment);
            return View(treatmentsPivots.ToList());
        }

        // GET: TreatmentsPivots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentsPivot treatmentsPivot = db.TreatmentsPivots.Find(id);
            if (treatmentsPivot == null)
            {
                return HttpNotFound();
            }
            return View(treatmentsPivot);
        }

        // GET: TreatmentsPivots/Create
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
            ViewBag.datapieceID = new SelectList(db.PossibleTreatments, "Id", "Name");
            ViewBag.effectiveness = new SelectList(getEffectiveness());
            return View();
        }
        public int[] getEffectiveness()
        {
            int[] ret = new int[11];
            int a = 0;
            while (a < 11)
            {
                ret[a] = a;
                a++;
            }
            return ret;
        }

        // POST: TreatmentsPivots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,patientID,datapieceID,date,frequency,notes,effectiveness")] TreatmentsPivot treatmentsPivot)
        {
            if (ModelState.IsValid)
            {
                db.TreatmentsPivots.Add(treatmentsPivot);
                db.SaveChanges();
                return RedirectToAction("Details", "Patients", new { id = treatmentsPivot.patientID });
            }
            Patient[] sel = new Patient[1];
            sel[0] = db.Patients.Find(treatmentsPivot.patientID);
            ViewBag.patientID = new SelectList(sel, "patientID", "patientID");
            ViewBag.datapieceID = new SelectList(db.PossibleTreatments, "Id", "Name", treatmentsPivot.datapieceID);
            ViewBag.effectiveness = new SelectList(getEffectiveness());
            return View(treatmentsPivot);
        }

        // GET: TreatmentsPivots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentsPivot treatmentsPivot = db.TreatmentsPivots.Find(id);
            if (treatmentsPivot == null)
            {
                return HttpNotFound();
            }
            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID", treatmentsPivot.patientID);
            ViewBag.datapieceID = new SelectList(db.PossibleTreatments, "Id", "Name", treatmentsPivot.datapieceID);
            return View(treatmentsPivot);
        }

        // POST: TreatmentsPivots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,patientID,datapieceID,date,frequency,notes,effectiveness")] TreatmentsPivot treatmentsPivot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatmentsPivot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID", treatmentsPivot.patientID);
            ViewBag.datapieceID = new SelectList(db.PossibleTreatments, "Id", "Name", treatmentsPivot.datapieceID);
            return View(treatmentsPivot);
        }

        // GET: TreatmentsPivots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentsPivot treatmentsPivot = db.TreatmentsPivots.Find(id);
            if (treatmentsPivot == null)
            {
                return HttpNotFound();
            }
            return View(treatmentsPivot);
        }

        // POST: TreatmentsPivots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TreatmentsPivot treatmentsPivot = db.TreatmentsPivots.Find(id);
            db.TreatmentsPivots.Remove(treatmentsPivot);
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
