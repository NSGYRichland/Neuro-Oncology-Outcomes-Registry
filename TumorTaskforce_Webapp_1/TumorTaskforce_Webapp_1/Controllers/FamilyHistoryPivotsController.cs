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
    public class FamilyHistoryPivotsController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: FamilyHistoryPivots
        public ActionResult Index()
        {
            var familyHistoryPivots = db.FamilyHistoryPivots.Include(f => f.Patient).Include(f => f.PossibleFamilyHistory);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            return View(familyHistoryPivots.ToList());

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

        // GET: FamilyHistoryPivots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyHistoryPivot familyHistoryPivot = db.FamilyHistoryPivots.Find(id);
            if (familyHistoryPivot == null)
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
            return View(familyHistoryPivot);
        }

        // GET: FamilyHistoryPivots/Create
        public ActionResult Create()
        {
            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID");
            ViewBag.datapieceID = new SelectList(db.PossibleFamilyHistories, "Id", "Name");
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

        // POST: FamilyHistoryPivots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,patientID,datapieceID,date,frequency,notes")] FamilyHistoryPivot familyHistoryPivot)
        {
            if (ModelState.IsValid)
            {
                db.FamilyHistoryPivots.Add(familyHistoryPivot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID", familyHistoryPivot.patientID);
            ViewBag.datapieceID = new SelectList(db.PossibleFamilyHistories, "Id", "Name", familyHistoryPivot.datapieceID);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            return View(familyHistoryPivot);
        }

        // GET: FamilyHistoryPivots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyHistoryPivot familyHistoryPivot = db.FamilyHistoryPivots.Find(id);
            if (familyHistoryPivot == null)
            {
                return HttpNotFound();
            }
            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID", familyHistoryPivot.patientID);
            ViewBag.datapieceID = new SelectList(db.PossibleFamilyHistories, "Id", "Name", familyHistoryPivot.datapieceID);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            return View(familyHistoryPivot);
        }

        // POST: FamilyHistoryPivots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,patientID,datapieceID,date,frequency,notes")] FamilyHistoryPivot familyHistoryPivot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familyHistoryPivot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.patientID = new SelectList(db.Patients, "patientID", "patientID", familyHistoryPivot.patientID);
            ViewBag.datapieceID = new SelectList(db.PossibleFamilyHistories, "Id", "Name", familyHistoryPivot.datapieceID);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            return View(familyHistoryPivot);
        }

        // GET: FamilyHistoryPivots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyHistoryPivot familyHistoryPivot = db.FamilyHistoryPivots.Find(id);
            if (familyHistoryPivot == null)
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
            return View(familyHistoryPivot);
        }

        // POST: FamilyHistoryPivots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FamilyHistoryPivot familyHistoryPivot = db.FamilyHistoryPivots.Find(id);
            db.FamilyHistoryPivots.Remove(familyHistoryPivot);
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
