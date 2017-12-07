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
    public class PossibleHealthFactorsController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: PossibleHealthFactors
        public ActionResult Index()
        {
            return View(db.PossibleHealthFactors.ToList());
        }

        // GET: PossibleHealthFactors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleHealthFactor possibleHealthFactor = db.PossibleHealthFactors.Find(id);
            if (possibleHealthFactor == null)
            {
                return HttpNotFound();
            }
            return View(possibleHealthFactor);
        }

        // GET: PossibleHealthFactors/Create
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
        // POST: PossibleHealthFactors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] PossibleHealthFactor possibleHealthFactor)
        {
            if (ModelState.IsValid)
            {
                db.PossibleHealthFactors.Add(possibleHealthFactor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(possibleHealthFactor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromPatient([Bind(Include = "Id,PercentShowing,Name,TempPatientID")] PossibleHealthFactor possibleHealthFactor)
        {
            if (ModelState.IsValid)
            {
                db.PossibleHealthFactors.Add(possibleHealthFactor);
                db.SaveChanges();
                return RedirectToAction("Create", "HealthFactorsPivots", new { patientID = possibleHealthFactor.TempPatientID });
            }

            return View(possibleHealthFactor);
        }

        // GET: PossibleHealthFactors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleHealthFactor possibleHealthFactor = db.PossibleHealthFactors.Find(id);
            if (possibleHealthFactor == null)
            {
                return HttpNotFound();
            }
            return View(possibleHealthFactor);
        }

        // POST: PossibleHealthFactors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] PossibleHealthFactor possibleHealthFactor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(possibleHealthFactor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(possibleHealthFactor);
        }

        // GET: PossibleHealthFactors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleHealthFactor possibleHealthFactor = db.PossibleHealthFactors.Find(id);
            if (possibleHealthFactor == null)
            {
                return HttpNotFound();
            }
            int a = 0;
            HealthFactorsPivot[] tp = db.HealthFactorsPivots.ToArray();
            while (a < db.HealthFactorsPivots.Count())
            {
                if (tp[a].datapieceID == id)
                {
                    return RedirectToAction("FK_Error", "Home");
                }
                a++;
            }
            return View(possibleHealthFactor);
        }

        // POST: PossibleHealthFactors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PossibleHealthFactor possibleHealthFactor = db.PossibleHealthFactors.Find(id);
            db.PossibleHealthFactors.Remove(possibleHealthFactor);
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
