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
    public class PossibleOtherMedsController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: PossibleOtherMeds
        public ActionResult Index()
        {
            return View(db.PossibleOtherMeds.ToList());
        }

        // GET: PossibleOtherMeds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleOtherMed possibleOtherMed = db.PossibleOtherMeds.Find(id);
            if (possibleOtherMed == null)
            {
                return HttpNotFound();
            }
            return View(possibleOtherMed);
        }

        // GET: PossibleOtherMeds/Create
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
        // POST: PossibleOtherMeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] PossibleOtherMed possibleOtherMed)
        {
            if (ModelState.IsValid)
            {
                db.PossibleOtherMeds.Add(possibleOtherMed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(possibleOtherMed);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromPatient([Bind(Include = "Id,PercentShowing,Name,TempPatientID")] PossibleOtherMed possibleOtherMed)
        {
            if (ModelState.IsValid)
            {
                db.PossibleOtherMeds.Add(possibleOtherMed);
                db.SaveChanges();
                return RedirectToAction("Create", "OtherMedsPivots", new { patientID = possibleOtherMed.TempPatientID });
            }

            return View(possibleOtherMed);
        }

        // GET: PossibleOtherMeds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleOtherMed possibleOtherMed = db.PossibleOtherMeds.Find(id);
            if (possibleOtherMed == null)
            {
                return HttpNotFound();
            }
            return View(possibleOtherMed);
        }

        // POST: PossibleOtherMeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] PossibleOtherMed possibleOtherMed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(possibleOtherMed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(possibleOtherMed);
        }

        // GET: PossibleOtherMeds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleOtherMed possibleOtherMed = db.PossibleOtherMeds.Find(id);
            if (possibleOtherMed == null)
            {
                return HttpNotFound();
            }
            int a = 0;
            OtherMedsPivot[] tp = db.OtherMedsPivots.ToArray();
            while (a < db.OtherMedsPivots.Count())
            {
                if (tp[a].datapieceID == id)
                {
                    return RedirectToAction("FK_Error", "Home");
                }
                a++;
            }
            return View(possibleOtherMed);
        }

        // POST: PossibleOtherMeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PossibleOtherMed possibleOtherMed = db.PossibleOtherMeds.Find(id);
            db.PossibleOtherMeds.Remove(possibleOtherMed);
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
