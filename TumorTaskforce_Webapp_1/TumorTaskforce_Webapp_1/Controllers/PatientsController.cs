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
    public class PatientsController : Controller
    {
        private tumorDBEntities db = new tumorDBEntities();

        // GET: Patients
        public ActionResult Index(string q, string tumLoc, string clss, string grade, string sex)
        {
            var patients = from p in db.Patients select p;
            int id = Convert.ToInt32(Request["SearchType"]);
            int who = Convert.ToInt32(Request["SearchType"]);
            patients = patients.Where(p => p.isCompare == false);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }

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

            return View(patients);          
        }

        public ActionResult CompIndex(string q, string tumLoc, string clss, string grade, string sex)
        {
            var patients = from p in db.Patients select p;
            int id = Convert.ToInt32(Request["SearchType"]);
            int who = Convert.ToInt32(Request["SearchType"]);
            patients = patients.Where(p => p.isCompare);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }

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

            return View(patients);
        }

        //public ActionResult Compare()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        ViewBag.displayMenu = "No";
        //        if (isAdminUser())
        //        {
        //            ViewBag.displayMenu = "Yes";
        //        }
        //    }
        //    return View(db.Patients.ToList());
        //}

        public ActionResult Results(int? id)
        {
            string[] TargetData = new string[3];//Idea for moving data 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            if (!patient.isCompare)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }


            //ALGORITHM SHOULD GO HERE
            //MAKE SURE TO ONLY COMPARE AGAINST PATIENTS WHERE isCompare == false
            
            Patient target = new Patient();//target variable keeps most recent "similar patient" during search
            int targetSimilarity = 0;//updated variable that hold most "similar" variable
            int currEffect = 0, targetEffect = 0; bool surgery = false;
            String targetRecord = "000000000000000000";//this is a primitive testing variable that I made to make sure its recording everything
                                                       // correctly. im going to comment these out for now
            foreach (var curr in db.Patients)
            {
                int similarity = 0, i = 0;
                String record = "000000000000000000";
                if (patient.patientID == curr.patientID || curr.isCompare == true)
                {
                    continue;
                }
                else
                {
                    /*double tVol = 0, currVol = 0;
                    tVol = (double)(Model.TumorHeight * Model.TumorLength * Model.TumorWidth);
                    currVol = (double)(curr.TumorHeight * curr.TumorLength * curr.TumorWidth); */
                    if (patient.Sex.Equals(curr.Sex))
                    {
                        similarity++;
                        record = record.Insert(0, "1");
                    }
                    if (patient.Age == curr.Age)
                    {
                        similarity++;
                        record = record.Insert(1, "1");
                    }
                    

                    /*if (patient.TumorLength == curr.TumorLength)
                    {
                        similarity++;
                        //record = record.Insert(5, "1");
                    }
                    if (patient.TumorWidth == curr.TumorWidth)
                    {
                        similarity++;
                        //record = record.Insert(6, "1");
                    }
                    if (patient.TumorHeight == curr.TumorHeight)
                    {
                        similarity++;
                        //record = record.Insert(7, "1");
                    }
                    if (patient.TumorLocation.Equals(curr.TumorLocation))
                    {
                        similarity += 3;
                        //record = record.Insert(8, "1");
                    }*/


                    try
                    {
                        if (patient.HistologicalClassification.Equals(curr.HistologicalClassification))
                        {
                            i = 3;
                            record = record.Insert(2, "1");
                            if (patient.HistologicalGrade >= curr.HistologicalGrade)
                            {
                                i++;
                                record = record.Insert(3, "1");
                                if (patient.HistologicalGrade == curr.HistologicalGrade)
                                {
                                    i++;
                                    record = record.Insert(4, "1");
                                }
                            }
                            similarity += i;
                            i = 0;
                        }

                        if (patient.TumorLength == curr.TumorLength
                         & patient.TumorWidth == curr.TumorWidth
                             & patient.TumorHeight == curr.TumorHeight
                                 & patient.TumorLocation.Equals(curr.TumorLocation))
                        {
                            foreach (TreatmentsPivot var in curr.TreatmentsPivots)
                            {
                                if (var.PossibleTreatment.Name.Equals("Surgery"))
                                {
                                    surgery = true;
                                }
                            }

                            //similarity++;
                            //record = record.Insert(5, "1");
                        }
                        if (!patient.Constitutional.Equals(null)
                                & !patient.Constitutional.Equals("normal"))
                        {
                            if (patient.Constitutional.Equals(curr.Constitutional))
                            {
                                similarity++;
                                record = record.Insert(9, "1");
                            }
                        }
                        if (!patient.Respiratory.Equals(null)
                                & !patient.Respiratory.Equals("normal"))
                        {
                            if (patient.Respiratory.Equals(curr.Respiratory))
                            {
                                similarity++;
                                record = record.Insert(10, "1");
                            }
                        }
                        if (!patient.Cardiovascular.Equals(null)
                                & !patient.Cardiovascular.Equals("normal"))
                        {
                            if (patient.Cardiovascular.Equals(curr.Cardiovascular))
                            {
                                similarity++;
                                record = record.Insert(11, "1");
                            }
                        }
                        if (!patient.Gastrointestinal.Equals(null)
                                & !patient.Gastrointestinal.Equals("normal"))
                        {
                            if (patient.Gastrointestinal.Equals(curr.Gastrointestinal))
                            {
                                similarity++;
                                record = record.Insert(12, "1");
                            }
                        }
                        if (!patient.Musculoskeletal.Equals(null)
                                & !patient.Musculoskeletal.Equals("normal"))
                        {
                            if (patient.Musculoskeletal.Equals(curr.Musculoskeletal))
                            {
                                similarity++;
                                record = record.Insert(13, "1");
                            }
                        }
                        if (!patient.Integumentary.Equals(null)
                                & !patient.Integumentary.Equals("normal"))
                        {
                            if (patient.Integumentary.Equals(curr.Integumentary))
                            {
                                similarity++;
                                record = record.Insert(14, "1");
                            }
                        }
                        if (!patient.Neurologic.Equals(null)
                                & !patient.Neurologic.Equals("normal"))
                        {
                            if (patient.Neurologic.Equals(curr.Neurologic))
                            {
                                similarity++;
                                record = record.Insert(15, "1");
                            }
                        }
                        if (!patient.Exercize.Equals(null)
                                & !patient.Exercize.Equals("normal"))
                        {
                            if (patient.Exercize.Equals(curr.Exercize))
                            {
                                similarity++;
                                record = record.Insert(16, "1");
                            }
                        }
                        if (!patient.Diet.Equals(null)
                                & !patient.Diet.Equals("normal"))
                        {
                            if (patient.Diet.Equals(curr.Diet))
                            {
                                similarity++;
                                record = record.Insert(17, "1");
                            }
                        }
                    }catch (NullReferenceException e) { }
                        


                }
                if (similarity > targetSimilarity)
                {
                    target = curr;
                    targetSimilarity = similarity;
                    targetRecord = record;
                }
                else if (similarity == targetSimilarity)
                {
                    currEffect = 0;
                    targetEffect = 0;
                    foreach (TreatmentsPivot sp in curr.TreatmentsPivots)
                    {
                       currEffect += sp.effectiveness;
                    }
                    foreach (TreatmentsPivot sp in target.TreatmentsPivots)
                    {
                        targetEffect += sp.effectiveness;
                    }
                    if (currEffect > targetEffect)
                    {
                        target = curr;
                        targetRecord = record;
                    }
                }
                    /*< text > Patient: </ text >< span > @curr.patientID </ span >< text > | Sim: </ text >< span > @similarity </ span >< text > | Record: </ text >< span > @record </ span >< text > | Effect: </ text >< span > @currEffect </ span >< br /> */

            }




            //var tuple = new Tuple<TumorTaskforce_Webapp_1.Patient, IEnumerable<TumorTaskforce_Webapp_1.Patient>>(patient, db.Patients.ToList());

            //PUT SUGGESTED TREATMENTS AS STRING INTO patient.comparisonResults
            TargetData[0] = target.patientID.ToString();
            TargetData[1] = targetSimilarity.ToString();
            TargetData[2] = "m";
            //patient.comparisonResults = "empty";
            if (surgery == true)
            {
                patient.comparisonResults = "Surgery  ";
            }
            foreach (TreatmentsPivot var in target.TreatmentsPivots)
            {
                string str;
                if (var.PossibleTreatment.Name.Equals("Drug"))
                {
                    try
                    {
                        str = "Drug: " + var.notes.ToString();
                        patient.comparisonResults += str + " // ";
                    } 
                    catch (NullReferenceException e) { }
                }
                else
                {
                    str = var.PossibleTreatment.Name;
                    patient.comparisonResults += str + " // ";
                }
                
            }
            patient.comparisonResults += "| " + targetRecord;
            //patient.comparisonResults = target.patientID.ToString();//omg that worked haha
            //patient.comparisonResults = "Our Comparison Algorithm is Under Contruction! Check back soon. Sorry for any inconvenience.";
            db.SaveChanges();


            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            ViewBag.TargetData = TargetData;
            return View(patient);
        }
        
        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.isCompare = patient.isCompare;
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            return View(patient);
        }
       

        // GET: Patients/Create
        public ActionResult Create(bool? isCompare)
        {
            if (isCompare == null) { isCompare = false; }
            ViewBag.isCompare = isCompare;
            ViewBag.Sex = new SelectList(getSexes(), "Value", "Text");
            ViewBag.HistologicalGrade = new SelectList(getGrades(), "Value", "Text");
            ViewBag.HistologicalClassification = new SelectList(getTumorTypes(), "Value", "Text");
            ViewBag.TumorLocation = new SelectList(getLocations(), "Value", "Text");
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

        
        public SelectListItem[] getSexes()
        {
            SelectListItem[] sex = new SelectListItem[3];
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
            SelectListItem[] grade = new SelectListItem[5];
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
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(Patient p in db.Patients.Where((item)=>item.isCompare==false))
            {
                if (list.Where((item)=>item.Text == p.TumorLocation).Count() < 1)
                {
                    SelectListItem sli = new SelectListItem
                    {
                        Text = p.TumorLocation,
                        Value = p.TumorLocation
                    };
                    list.Add(sli);
                }
            }
            SelectListItem other = new SelectListItem
            {
                Text = "Other",
                Value = "Other"
            };
            list.Add(other);
            return list.ToArray();
        }

        public SelectListItem[] getTumorTypes()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Patient p in db.Patients.Where((item) => item.isCompare == false))
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
            SelectListItem other = new SelectListItem
            {
                Text = "Other",
                Value = "Other"
            };
            list.Add(other);
            return list.ToArray();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "patientID,Sex,Married,Age,HistologicalClassification,HistologicalGrade,TumorWidth,TumorHeight,TumorLength,TumorLocation,Constitutional,Respiratory,Cardiovascular,Gastrointestinal,Musculoskeletal,Integumentary,Neurologic,Exercize,Diet,isCompare,comparisonResults,userName")] Patient patient)
        {
            
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                //db.SaveChanges();
                int x = await db.SaveChangesAsync();
                int id = patient.patientID;
                if (patient.isCompare)
                {
                    return RedirectToAction("Details", new { id });
                }  
                return RedirectToAction("Index");
            }
            ViewBag.Sex = new SelectList(getSexes(), "Value", "Text", patient.Sex);
            ViewBag.HistologicalGrade = new SelectList(getGrades(), "Value", "Text", patient.HistologicalGrade);
            ViewBag.HistologicalClassification = new SelectList(getTumorTypes(), "Value", "Text",patient.HistologicalClassification);
            ViewBag.TumorLocation = new SelectList(getLocations(), "Value", "Text", patient.TumorLocation);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.isCompare = patient.isCompare;
            ViewBag.Sex = new SelectList(getSexes(), "Value", "Text", patient.Sex);
            ViewBag.HistologicalGrade = new SelectList(getGrades(), "Value", "Text", patient.HistologicalGrade);
            ViewBag.HistologicalClassification = new SelectList(getTumorTypes(), "Value", "Text", patient.HistologicalClassification);
            ViewBag.TumorLocation = new SelectList(getLocations(), "Value", "Text", patient.TumorLocation);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "patientID,Sex,Married,Age,HistologicalClassification,HistologicalGrade,TumorWidth,TumorHeight,TumorLength,TumorLocation,Constitutional,Respiratory,Cardiovascular,Gastrointestinal,Musculoskeletal,Integumentary,Neurologic,Exercize,Diet,isCompare,userName,comparisonResults")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = patient.patientID });
            }
            ViewBag.Sex =new SelectList(getSexes(), "Value", "Text", patient.Sex);
            ViewBag.HistologicalGrade = new SelectList(getGrades(), "Value", "Text", patient.HistologicalGrade);
            ViewBag.HistologicalClassification = new SelectList(getTumorTypes(), "Value", "Text", patient.HistologicalClassification);
            ViewBag.TumorLocation = new SelectList(getLocations(), "Value", "Text", patient.TumorLocation);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
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
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            bool isCompare = patient.isCompare;
            foreach( SymptomsPivot sp in db.SymptomsPivots){
                if (sp.patientID == id)
                {
                    db.SymptomsPivots.Remove(sp);
                }
            }
            foreach (TreatmentsPivot tp in db.TreatmentsPivots)
            {
                if (tp.patientID == id)
                {
                    db.TreatmentsPivots.Remove(tp);
                }
            }
            foreach (HealthFactorsPivot hp in db.HealthFactorsPivots)
            {
                if (hp.patientID == id)
                {
                    db.HealthFactorsPivots.Remove(hp);
                }
            }
            foreach (OtherMedsPivot op in db.OtherMedsPivots)
            {
                if (op.patientID == id)
                {
                    db.OtherMedsPivots.Remove(op);
                }
            }
            foreach (FamilyHistoryPivot fp in db.FamilyHistoryPivots)
            {
                if (fp.patientID == id)
                {
                    db.FamilyHistoryPivots.Remove(fp);
                }
            }
            db.SaveChanges();
            db.Patients.Remove(patient);
            db.SaveChanges();
            if (isCompare)
            {
                return RedirectToAction("Index", "Home");
            }
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
