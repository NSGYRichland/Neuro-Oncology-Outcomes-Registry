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
    public class TreatmentsPivotsController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: TreatmentsPivots
        public ActionResult Index()
        {
            var treatmentsPivots = db.TreatmentsPivots.Include(t => t.Patient).Include(t => t.PossibleTreatment);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
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
            Patient p = treatmentsPivot.Patient;
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if(isAdminUser() || (p.isCompare && p.userName == User.Identity.GetUserName()))
                {
                    ViewBag.displayMenu = "Yes";
                }
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
            ViewBag.showLink = isAdminUser();
            ViewBag.patientID = new SelectList(sel, "patientID", "patientID");
            ViewBag.datapieceID = new SelectList(db.PossibleTreatments, "Id", "Name");
            ViewBag.effectiveness = new SelectList(getEffectiveness());
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser() || (p.isCompare && p.userName == User.Identity.GetUserName()))
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
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
            int[] list = getEffectiveness();
            ViewBag.effectiveness = new SelectList(list, list[treatmentsPivot.effectiveness]);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser() || (sel[0].isCompare && sel[0].userName == User.Identity.GetUserName()))
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
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
            Patient[] sel = new Patient[1];
            sel[0] = db.Patients.Find(treatmentsPivot.patientID);
            ViewBag.patientID = new SelectList(sel, "patientID", "patientID");
            ViewBag.datapieceID = new SelectList(db.PossibleTreatments, "Id", "Name", treatmentsPivot.datapieceID);
            int[] list = getEffectiveness();
            ViewBag.effectiveness = new SelectList(list, list[treatmentsPivot.effectiveness]);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser() || (sel[0].isCompare && sel[0].userName == User.Identity.GetUserName()))
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
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
                return RedirectToAction("Details", "Patients", new { id = treatmentsPivot.patientID });
            }
            Patient[] sel = new Patient[1];
            sel[0] = db.Patients.Find(treatmentsPivot.patientID);
            ViewBag.patientID = new SelectList(sel, "patientID", "patientID");
            ViewBag.datapieceID = new SelectList(db.PossibleTreatments, "Id", "Name", treatmentsPivot.datapieceID);
            int[] list = getEffectiveness();
            ViewBag.effectiveness = new SelectList(list, list[treatmentsPivot.effectiveness]);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser() || (sel[0].isCompare && sel[0].userName == User.Identity.GetUserName()))
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
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
            Patient p = treatmentsPivot.Patient;
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser() || (p.isCompare && p.userName == User.Identity.GetUserName()))
                {
                    ViewBag.displayMenu = "Yes";
                }
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
            return RedirectToAction("Details", "Patients", new { id = treatmentsPivot.patientID });
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
