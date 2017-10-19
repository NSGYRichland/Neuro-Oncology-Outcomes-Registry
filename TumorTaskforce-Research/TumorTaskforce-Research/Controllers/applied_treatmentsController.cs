using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TumorTaskforce_Research;

namespace TumorTaskforce_Research.Controllers
{
    public class applied_treatmentsController : Controller
    {
        private healthEntities db = new healthEntities();

        // GET: applied_treatments
        public ActionResult Index()
        {
            var applied_treatments = db.applied_treatments.Include(a => a.treatment).Include(a => a.patient);
            return View(applied_treatments.ToList());
        }

        // GET: applied_treatments/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            applied_treatments applied_treatments = db.applied_treatments.Find(id);
            if (applied_treatments == null)
            {
                return HttpNotFound();
            }
            return View(applied_treatments);
        }

        // GET: applied_treatments/Create
        public ActionResult Create()
        {
            ViewBag.GlobalTreatment_ID = new SelectList(db.treatments, "GlobalTreatment_ID", "Treatment_Name");
            ViewBag.Patient_ID = new SelectList(db.patients, "patients_ID", "First_Name");
            return View();
        }

        // POST: applied_treatments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppliedTreatments_ID,Patient_ID,GlobalTreatment_ID,Date,Effect")] applied_treatments applied_treatments)
        {
            if (ModelState.IsValid)
            {
                db.applied_treatments.Add(applied_treatments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GlobalTreatment_ID = new SelectList(db.treatments, "GlobalTreatment_ID", "Treatment_Name", applied_treatments.GlobalTreatment_ID);
            ViewBag.Patient_ID = new SelectList(db.patients, "patients_ID", "First_Name", applied_treatments.Patient_ID);
            return View(applied_treatments);
        }

        // GET: applied_treatments/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            applied_treatments applied_treatments = db.applied_treatments.Find(id);
            if (applied_treatments == null)
            {
                return HttpNotFound();
            }
            ViewBag.GlobalTreatment_ID = new SelectList(db.treatments, "GlobalTreatment_ID", "Treatment_Name", applied_treatments.GlobalTreatment_ID);
            ViewBag.Patient_ID = new SelectList(db.patients, "patients_ID", "First_Name", applied_treatments.Patient_ID);
            return View(applied_treatments);
        }

        // POST: applied_treatments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppliedTreatments_ID,Patient_ID,GlobalTreatment_ID,Date,Effect")] applied_treatments applied_treatments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applied_treatments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GlobalTreatment_ID = new SelectList(db.treatments, "GlobalTreatment_ID", "Treatment_Name", applied_treatments.GlobalTreatment_ID);
            ViewBag.Patient_ID = new SelectList(db.patients, "patients_ID", "First_Name", applied_treatments.Patient_ID);
            return View(applied_treatments);
        }

        // GET: applied_treatments/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            applied_treatments applied_treatments = db.applied_treatments.Find(id);
            if (applied_treatments == null)
            {
                return HttpNotFound();
            }
            return View(applied_treatments);
        }

        // POST: applied_treatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            applied_treatments applied_treatments = db.applied_treatments.Find(id);
            db.applied_treatments.Remove(applied_treatments);
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
