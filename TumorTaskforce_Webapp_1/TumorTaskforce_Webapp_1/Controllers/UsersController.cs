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
            ViewBag.currentUserID = User.Identity.GetUserId();
            IEnumerable<UserViewModel> users = Enumerable.Empty<UserViewModel>();
            foreach (var item in UserManager.Users.ToList())
            {
                UserViewModel t = new UserViewModel();
                t.Email = item.Email;
                t.Id = item.Id;
                t.Role = UserManager.GetRoles(item.Id).ToArray()[0];
                t.UserName = item.UserName;
                users = users.Concat(new[] { t });
            }
            return View(users);
        }
        public ActionResult Edit(string id)
        {
            if (id==null)
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
            UserViewModel t = new UserViewModel();
            t.Email = theUser.Email;
            t.Id = theUser.Id;
            t.Role = UserManager.GetRoles(theUser.Id).ToArray()[0];
            t.UserName = theUser.UserName;
            ViewBag.roles = new SelectList(context.Roles.ToList(), "Name", "Name");
            return View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit([Bind(Include = "Id,UserName,Email,Role")] UserViewModel user)
        {
            if (user.Role == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            await UserManager.RemoveFromRolesAsync(user.Id, UserManager.GetRolesAsync(user.Id).Result.ToArray());
            await UserManager.AddToRoleAsync(user.Id, user.Role);
            return RedirectToAction("Index");
        }

        //public async System.Threading.Tasks.Task<ActionResult> SaveAsync(string user, SelectList roles)
        //{
        //    if (user==null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationDbContext context = new ApplicationDbContext();
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        //    var theUser = UserManager.Users.ToList().Find(i => i.Id.Equals(user));
        //    string role = roles.SelectedValue.ToString();
        //    if (theUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (role==null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    await UserManager.RemoveFromRolesAsync(user, UserManager.GetRolesAsync(user).Result.ToArray());
        //    await UserManager.AddToRoleAsync(user, role);
        //    return RedirectToAction("Index");
        //}

        public ActionResult Delete(string id)
        {
            if (id==null)
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
            if (User.Identity.GetUserId().Equals(id)||UserManager.FindById(id).UserName.Equals("c.burdell"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
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