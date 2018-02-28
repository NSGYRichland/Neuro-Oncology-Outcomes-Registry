using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TumorTaskforce_Webapp_1;
using TumorTaskforce_Webapp_1.Models;

namespace TumorTaskforce_Webapp_1.Controllers
{
    public class PossibleSymptomsController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: PossibleSymptoms
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
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
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            return View(possibleSymptom);
        }

        // GET: PossibleSymptoms/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
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
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
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
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            return View(possibleSymptom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromPatient([Bind(Include = "Id,PercentShowing,Name,TempPatientID")] PossibleSymptom possibleSymptom)
        {
            if (ModelState.IsValid)
            {
                db.PossibleSymptoms.Add(possibleSymptom);
                db.SaveChanges();
                return RedirectToAction("Create", "SymptomsPivots", new { patientID = possibleSymptom.TempPatientID });
            }
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
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
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
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
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
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
            int a = 0;
            SymptomsPivot[] tp = db.SymptomsPivots.ToArray();
            while (a < db.SymptomsPivots.Count())
            {
                if (tp[a].datapieceID == id)
                {
                    return RedirectToAction("FK_Error", "Home");
                }
                a++;
            }
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
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

        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
