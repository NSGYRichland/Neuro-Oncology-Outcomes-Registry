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
    public class patientsController : Controller
    {
        private healthEntities db = new healthEntities();

        // GET: patients
        public ActionResult Index()
        {
            var patients = db.patients.Include(p => p.record).Include(p => p.record1);
            return View(patients.ToList());
        }

        // GET: patients/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: patients/Create
        public ActionResult Create()
        {
            ViewBag.Family_History = new SelectList(db.records, "Record_ID", "Record_ID");
            ViewBag.Medical_Record = new SelectList(db.records, "Record_ID", "Record_ID");
            return View();
        }

        // POST: patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "patients_ID,First_Name,Last_Name,DOB,Sex_Male,Diagnosis,Medical_Record,Family_History")] patient patient)
        {
            if (ModelState.IsValid)
            {
                db.patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Family_History = new SelectList(db.records, "Record_ID", "Record_ID", patient.Family_History);
            ViewBag.Medical_Record = new SelectList(db.records, "Record_ID", "Record_ID", patient.Medical_Record);
            return View(patient);
        }

        // GET: patients/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.Family_History = new SelectList(db.records, "Record_ID", "Record_ID", patient.Family_History);
            ViewBag.Medical_Record = new SelectList(db.records, "Record_ID", "Record_ID", patient.Medical_Record);
            return View(patient);
        }

        // POST: patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "patients_ID,First_Name,Last_Name,DOB,Sex_Male,Diagnosis,Medical_Record,Family_History")] patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Family_History = new SelectList(db.records, "Record_ID", "Record_ID", patient.Family_History);
            ViewBag.Medical_Record = new SelectList(db.records, "Record_ID", "Record_ID", patient.Medical_Record);
            return View(patient);
        }

        // GET: patients/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            patient patient = db.patients.Find(id);
            db.patients.Remove(patient);
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
