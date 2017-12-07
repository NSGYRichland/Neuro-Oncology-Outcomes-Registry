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
    public class PossibleTreatmentsController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: PossibleTreatments
        public ActionResult Index()
        {
            return View(db.PossibleTreatments.ToList());
        }

        // GET: PossibleTreatments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleTreatment possibleTreatment = db.PossibleTreatments.Find(id);
            if (possibleTreatment == null)
            {
                return HttpNotFound();
            }
            return View(possibleTreatment);
        }

        // GET: PossibleTreatments/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CreateFromPatient(int? TempPatientID)
        {
            if (TempPatientID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient p = db.Patients.Find(TempPatientID);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View();
        }
        // POST: PossibleTreatments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] PossibleTreatment possibleTreatment)
        {
            if (ModelState.IsValid)
            {
                db.PossibleTreatments.Add(possibleTreatment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(possibleTreatment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromPatient([Bind(Include = "Id,PercentShowing,Name,TempPatientID")] PossibleTreatment possibleTreatment)
        {
            if (ModelState.IsValid)
            {
                db.PossibleTreatments.Add(possibleTreatment);
                db.SaveChanges();
                return RedirectToAction("Create", "TreatmentsPivots", new { patientID = possibleTreatment.TempPatientID });
            }

            return View(possibleTreatment);
        }

        // GET: PossibleTreatments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleTreatment possibleTreatment = db.PossibleTreatments.Find(id);
            if (possibleTreatment == null)
            {
                return HttpNotFound();
            }
            return View(possibleTreatment);
        }

        // POST: PossibleTreatments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] PossibleTreatment possibleTreatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(possibleTreatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(possibleTreatment);
        }

        // GET: PossibleTreatments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleTreatment possibleTreatment = db.PossibleTreatments.Find(id);
            if (possibleTreatment == null)
            {
                return HttpNotFound();
            }
            int a = 0;
            TreatmentsPivot[] tp = db.TreatmentsPivots.ToArray();
            while (a < db.TreatmentsPivots.Count())
            {
                if (tp[a].datapieceID == id)
                {
                    return RedirectToAction("FK_Error", "Home");
                }
                a++;
            }
            return View(possibleTreatment);
        }

        // POST: PossibleTreatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PossibleTreatment possibleTreatment = db.PossibleTreatments.Find(id);
            db.PossibleTreatments.Remove(possibleTreatment);
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
