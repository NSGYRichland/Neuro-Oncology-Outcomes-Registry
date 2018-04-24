using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TumorTaskforce_Webapp_1.Models;

namespace TumorTaskforce_Webapp_1.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message, string q, string tumLoc, string clss, string grade, string sex)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
            

            var patients = db.Patients.Where(i => i.userName == userName && i.isCompare);
            int id = Convert.ToInt32(Request["SearchType"]);
            int who = Convert.ToInt32(Request["SearchType"]);

            if (!string.IsNullOrWhiteSpace(q))
            {
                int pID = int.Parse(q);
                patients = patients.Where(p => p.patientID.Equals(pID));
            }

            if (!string.IsNullOrWhiteSpace(tumLoc))
            {
                patients = patients.Where(r => r.TumorLocation.Contains(tumLoc));
            }
            if (!string.IsNullOrWhiteSpace(clss))
            {
                patients = patients.Where(s => s.HistologicalClassification.Contains(clss));
            }

            if (!string.IsNullOrWhiteSpace(grade))
            {
                int hisGrade = int.Parse(grade);
                patients = patients.Where(t => t.HistologicalGrade == hisGrade);
            }

            if (!string.IsNullOrWhiteSpace(sex))
            {
                patients = patients.Where(u => u.Sex.Contains(sex));
            }

            var model = new IndexViewModel
            {
                MyComparePatients = patients.ToList(),
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };

            ViewBag.tumLoc = new SelectList(getLocations(), "Value", "Text");
            ViewBag.sex = new SelectList(getSexes(), "Value", "Text");
            ViewBag.clss = new SelectList(getTumorTypes(), "Value", "Text");
            ViewBag.grade = new SelectList(getGrades(), "Value", "Text");

            return View(model);
        }

        public SelectListItem[] getSexes()
        {
            SelectListItem[] sex;
            //if (searchPurpose != null)
            {
                sex = new SelectListItem[4];
                SelectListItem any = new SelectListItem
                {
                    Text = "Any",
                    Value = null
                };
                sex[3] = any;
            }
            
            SelectListItem male = new SelectListItem
            {
                Text = "Male",
                Value = "M"
            };
            sex[0] = male;
            SelectListItem female = new SelectListItem
            {
                Text = "Female",
                Value = "F"
            };
            sex[1] = female;
            SelectListItem other = new SelectListItem
            {
                Text = "Other",
                Value = "O"
            };
            sex[2] = other;
            return sex;
        }

        public SelectListItem[] getGrades()
        {
            SelectListItem[] grade;
            //if (searchPurpose != null)
            {
                grade = new SelectListItem[6];
                SelectListItem any = new SelectListItem
                {
                    Text = "Any",
                    Value = null
                };
                grade[5] = any;
            }
           // else
             //   grade = new SelectListItem[5];
            SelectListItem zero = new SelectListItem
            {
                Text = "0",
                Value = "0"
            };
            grade[0] = zero;
            SelectListItem one = new SelectListItem
            {
                Text = "1",
                Value = "1"
            };
            grade[1] = one;
            SelectListItem two = new SelectListItem
            {
                Text = "2",
                Value = "2"
            };
            grade[2] = two;
            SelectListItem three = new SelectListItem
            {
                Text = "3",
                Value = "3"
            };
            grade[3] = three;
            SelectListItem four = new SelectListItem
            {
                Text = "4",
                Value = "4"
            };
            grade[4] = four;
            return grade;
        }
        public SelectListItem[] getLocations()
        {
            var userName = User.Identity.GetUserName();
            List<SelectListItem> list = new List<SelectListItem>();
            //if (searchPurpose == CompareSearch)
            {
                foreach (Patient p in db.Patients.Where((item) => item.userName == userName && item.isCompare && item.TumorLocation != "" && item.TumorLocation != null))
                {
                    if (list.Where((item) => item.Text == p.TumorLocation).Count() < 1)
                    {
                        SelectListItem sli = new SelectListItem
                        {
                            Text = p.TumorLocation,
                            Value = p.TumorLocation
                        };
                        list.Add(sli);
                    }
                }
            }
            //else
            //{
            //    foreach (Patient p in db.Patients.Where((item) => item.isCompare == false && item.TumorLocation != "" && item.TumorLocation != null))
            //    {
            //        if (list.Where((item) => item.Text == p.TumorLocation).Count() < 1)
            //        {
            //            SelectListItem sli = new SelectListItem
            //            {
            //                Text = p.TumorLocation,
            //                Value = p.TumorLocation
            //            };
            //            list.Add(sli);
            //        }
            //    }
            //}


            //if (searchPurpose != null)
            {
                SelectListItem any = new SelectListItem
                {
                    Text = "Any",
                    Value = null
                };
                list.Add(any);
            }
            //else
            //{
            //    SelectListItem other = new SelectListItem
            //    {
            //        Text = "Other",
            //        Value = "Other"
            //    };
            //    list.Add(other);
            //}
            return list.ToArray();
        }

        public SelectListItem[] getTumorTypes()
        {
            var userName = User.Identity.GetUserName();
            List<SelectListItem> list = new List<SelectListItem>();
            //if (searchPurpose == CompareSearch)
            {
                foreach (Patient p in db.Patients.Where((item) => item.userName == userName && item.isCompare && item.HistologicalClassification != "" && item.HistologicalClassification != null))
                {
                    if (list.Where((item) => item.Text == p.HistologicalClassification).Count() < 1)
                    {
                        SelectListItem sli = new SelectListItem
                        {
                            Text = p.HistologicalClassification,
                            Value = p.HistologicalClassification
                        };
                        list.Add(sli);
                    }
                }
            }
            //else
            //{
            //    foreach (Patient p in db.Patients.Where((item) => item.isCompare == false && item.HistologicalClassification != "" && item.HistologicalClassification != null))
            //    {
            //        if (list.Where((item) => item.Text == p.HistologicalClassification).Count() < 1)
            //        {
            //            SelectListItem sli = new SelectListItem
            //            {
            //                Text = p.HistologicalClassification,
            //                Value = p.HistologicalClassification
            //            };
            //            list.Add(sli);
            //        }
            //    }
            //}


            //if (searchPurpose != null)
            {
                SelectListItem any = new SelectListItem
                {
                    Text = "Any",
                    Value = null
                };
                list.Add(any);
            }
            //else
            //{
            //    SelectListItem other = new SelectListItem
            //    {
            //        Text = "Other",
            //        Value = "Other"
            //    };
            //    list.Add(other);
            //}
            return list.ToArray();
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}