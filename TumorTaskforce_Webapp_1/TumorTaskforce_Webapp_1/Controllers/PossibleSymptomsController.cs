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
    public class PossibleSymptomsController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: PossibleSymptoms
        public ActionResult Index()
        {
            return View(db.PossibleSymptoms.ToList());
        }

        // GET: PossibleSymptoms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleSymptom possibleSymptom = db.PossibleSymptoms.Find(id);
            if (possibleSymptom == null)
            {
                return HttpNotFound();
            }
            return View(possibleSymptom);
        }

        // GET: PossibleSymptoms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PossibleSymptoms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PercentShowing,Name")] PossibleSymptom possibleSymptom)
        {
            if (ModelState.IsValid)
            {
                db.PossibleSymptoms.Add(possibleSymptom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(possibleSymptom);
        }

        // GET: PossibleSymptoms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleSymptom possibleSymptom = db.PossibleSymptoms.Find(id);
            if (possibleSymptom == null)
            {
                return HttpNotFound();
            }
            return View(possibleSymptom);
        }

        // POST: PossibleSymptoms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PercentShowing,Name")] PossibleSymptom possibleSymptom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(possibleSymptom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(possibleSymptom);
        }

        // GET: PossibleSymptoms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PossibleSymptom possibleSymptom = db.PossibleSymptoms.Find(id);
            if (possibleSymptom == null)
            {
                return HttpNotFound();
            }
            return View(possibleSymptom);
        }

        // POST: PossibleSymptoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PossibleSymptom possibleSymptom = db.PossibleSymptoms.Find(id);
            db.PossibleSymptoms.Remove(possibleSymptom);
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
