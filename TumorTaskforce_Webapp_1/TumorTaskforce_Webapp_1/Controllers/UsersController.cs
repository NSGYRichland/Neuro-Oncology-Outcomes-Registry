using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TumorTaskforce_Webapp_1.Models;

namespace TumorTaskforce_Webapp_1.Controllers
{
    public class UsersController : Controller
    {
        

        // GET: Users
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            ViewBag.userMGR = UserManager;
            ViewBag.userlist = UserManager.Users.ToList();
            ViewBag.currentUserID = User.Identity.GetUserId();
            return View();
        }
        public ActionResult Edit(string id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var theUser = UserManager.Users.ToList().Find(i => i.Id.Equals(id));
            if (theUser == null)
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
            ViewBag.userMGR = UserManager;
            ViewBag.user = theUser;
            return View();
        }
        public ActionResult Delete(string id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var theUser = UserManager.Users.ToList().Find(i => i.Id.Equals(id));
            if (theUser == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId().Equals(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                UserManager.DeleteAsync(theUser);
                return RedirectToAction("Index");
            }
        }

        // POST: TreatmentsPivots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,patientID,datapieceID,date,frequency,notes,effectiveness")] TreatmentsPivot treatmentsPivot)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(treatmentsPivot).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Details", "Patients", new { id = treatmentsPivot.patientID });
        //    }
        //    Patient[] sel = new Patient[1];
        //    sel[0] = db.Patients.Find(treatmentsPivot.patientID);
        //    ViewBag.patientID = new SelectList(sel, "patientID", "patientID");
        //    ViewBag.datapieceID = new SelectList(db.PossibleTreatments, "Id", "Name", treatmentsPivot.datapieceID);
        //    int[] list = getEffectiveness();
        //    ViewBag.effectiveness = new SelectList(list, list[treatmentsPivot.effectiveness]);
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        ViewBag.displayMenu = "No";
        //        if (isAdminUser())
        //        {
        //            ViewBag.displayMenu = "Yes";
        //        }
        //    }
        //    return View(treatmentsPivot);
        //}
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