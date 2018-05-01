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
        private static int PatientSearch = 1;
        private static int CompareSearch = 2;
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

            ViewBag.tumLoc = new SelectList(getLocations(PatientSearch), "Value", "Text");
            ViewBag.sex = new SelectList(getSexes(PatientSearch), "Value", "Text");
            ViewBag.clss = new SelectList(getTumorTypes(PatientSearch), "Value", "Text");
            ViewBag.grade = new SelectList(getGrades(PatientSearch), "Value", "Text");

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

            ViewBag.tumLoc = new SelectList(getLocations(CompareSearch), "Value", "Text");
            ViewBag.sex = new SelectList(getSexes(CompareSearch), "Value", "Text");
            ViewBag.clss = new SelectList(getTumorTypes(CompareSearch), "Value", "Text");
            ViewBag.grade = new SelectList(getGrades(CompareSearch), "Value", "Text");

            return View(patients);
        }

        /*public ActionResult Compare()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            return View(db.Patients.ToList());
        }*/

        public ActionResult Results(int? id)
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
            if (!patient.isCompare)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }

            int wSex = 100, wAge = 100, wClass = 100, wGrade = 100, wVol = 100, wLoca = 100, wConst = 100, wResp = 100, wCardio = 100, wGast = 100, wMusc = 100, wInt = 100,
                wNeuro = 100, wExer = 100, wDiet = 100, wSym = 100, wMeds = 100, wHF = 100, wDeath = 100, wFH = 100; //Weighted Variables, for changing the weight of a patients Similarity impact.
            float simMax = (wSex + wAge + wClass + wGrade + wVol + wLoca + wConst + wResp + wCardio + wGast + wMusc + wInt
                + wNeuro + wExer + wDiet + wDeath);//General Similarity Max, we will add to soon for multi-input variables
            string simData = "";

            string[] muscString = null;//These Variables must be parsed, so I initialize these 
            string[] respString = null;//here for later use
            string[] neuroString = null;
            string[] cardString = null;


            try
            {
                if (!patient.Musculoskeletal.Equals(null))//I need to add to the Max similarity is our Compare patient has mulitple variables in these 
                {
                    muscString = patient.Musculoskeletal.Split(',');
                    if (patient.Musculoskeletal.Contains(','))
                    {
                        simMax += ((muscString.Length - 1) * wMusc);//If it does add the weight of that variable to the max that number of times. 
                    }
                }
                else simMax -= wMusc;//If it is null then these variables dont need to be added in the first place
            }
            catch (NullReferenceException e) { simMax -= wMusc; }

            try
            {
                if (!patient.Respiratory.Equals(null))//Do these for all 4 that this is possible in.
                {
                    respString = patient.Respiratory.Split(',');
                    if (patient.Respiratory.Contains(','))
                    {
                        simMax += ((respString.Length - 1) * wResp);
                    }
                }
                else simMax -= wResp;
            }
            catch (NullReferenceException e) { simMax -= wResp; }//I subtract from the max if the variable is null not too

            try
            {
                if (!patient.Neurologic.Equals(null))//I do this for all var that use this parsed string method 
                {
                    neuroString = patient.Neurologic.Split(',');
                    if (patient.Neurologic.Contains(','))
                    {
                        simMax += ((neuroString.Length - 1) * wNeuro);
                    }
                }
                else simMax -= wNeuro;
            }
            catch (NullReferenceException e) { simMax -= wNeuro; }

            try
            {
                if (!patient.Cardiovascular.Equals(null))
                {
                    cardString = patient.Cardiovascular.Split(',');
                    if (patient.Cardiovascular.Contains(','))
                    {
                        simMax += ((cardString.Length - 1) * wCardio);
                    }
                }
                else simMax -= wCardio;
            }
            catch (NullReferenceException e) { simMax -= wCardio; }

            simMax += (patient.SymptomsPivots.Count * wSym);//I also need to do this for every pivot variable to look at
            simMax += (patient.HealthFactorsPivots.Count * wHF);
            simMax += (patient.OtherMedsPivots.Count * wMeds);
            simMax += (patient.FamilyHistoryPivots.Count * wFH);


            simMax /= 100;// dividing by 100 gives me that max similarity a any patient could have with our compare patient that I used to find percent similar. 

            Patient target = new Patient();//target variable keeps most recent "similar patient" during search
            float targetSimilarity = 0;//updated variable that hold most "similar" variable
            int currEffect = 0, targetEffect = 0, count = 0; bool surgery = false;
            char[] targetRecord = { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' };//this is a primitive testing variable that I made to make sure its recording everything

            foreach (var curr in db.Patients)//Foreach goes though each patient in the database 
            {
                float similarity = 0;//use to hold how many variables were found similar 
                char[] record = { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' };//record keeping 


                if (patient.patientID == curr.patientID || curr.isCompare == true)//skips the patient if it finds itself
                {
                    continue;
                }
                else
                {
                    if (patient.Sex.Equals(curr.Sex))//start comparing variable 
                    {
                        similarity += (1 * (wSex / 100));//Add to similarity (1 * the weight) 
                        record[0] = '1';//record keeping
                    }
                    if (patient.Age == curr.Age)
                    {
                        similarity += (1 * (wAge / 100));
                        record[1] = '1';
                    }
                    if (patient.Married == curr.Married)//Deceased Variable
                    {
                        similarity += (1 * (wDeath / 100));
                        record[3] = '1';
                    }


                    int pVol = ((int)patient.TumorHeight) * (int)(patient.TumorLength) * (int)(patient.TumorWidth);//The surgery algorithm uses Vol not size so calculate here
                    int cVol = ((int)curr.TumorHeight) * (int)(curr.TumorLength) * (int)(curr.TumorWidth);

                    try
                    {
                        if (patient.HistologicalClassification.Equals(curr.HistologicalClassification))
                        {
                            similarity += (1 * (wClass / 100));
                            record[8] = '1';
                        }
                    }
                    catch (NullReferenceException e) { }//try catch blocks in case thesee are null 

                    try
                    {
                        if (patient.HistologicalGrade.Equals(curr.HistologicalGrade))
                        {
                            similarity += (1 * (wGrade / 100));
                            record[9] = '1';
                        }
                    }
                    catch (NullReferenceException e) { }

                    if (patient.TumorLength == curr.TumorLength
                      & patient.TumorWidth == curr.TumorWidth
                          & patient.TumorHeight == curr.TumorHeight
                              & patient.TumorLocation.Equals(curr.TumorLocation))
                    {
                        foreach (TreatmentsPivot var in curr.TreatmentsPivots)
                        {
                            if (var.PossibleTreatment.Name.Equals("Surgery"))
                            {
                                surgery = true;//this variable gets set true if the vol is same with curr patient & the location is same & That other patient had surgery
                            }
                        }
                    }

                    try
                    {
                        if (pVol == cVol)
                        {
                            similarity += (1 * (wVol / 100));
                            record[10] = '1';
                        }
                    }
                    catch (NullReferenceException e) { }

                    try
                    {
                        if (patient.TumorLocation.Equals(curr.TumorLocation))//tumor location is also calculated along with the rest of the variables 
                        {
                            similarity += (1 * (wLoca / 100));
                            record[11] = '1';
                        }
                    }
                    catch (NullReferenceException e) { }

                    try
                    {
                        if (!patient.Constitutional.Equals(null))//KPS Variable
                        {
                            if (patient.Constitutional.Equals(curr.Constitutional))
                            {
                                similarity += (1 * (wConst / 100));
                                record[7] = '1';
                            }
                        }
                    }
                    catch (NullReferenceException e) { }


                    try
                    {
                        if (!respString.Equals(null) && !curr.Respiratory.Equals(null))//Multi-strign variables are a little more complicated
                        {
                            int cord = 0;//This variable will adjust as we go for how many variables were similar for our record. 
                            if (respString.Length > 1)//if arr length is larger than one need to iterate through each one 
                            {
                                if (curr.Respiratory.Contains(','))
                                {
                                    string[] tString = curr.Respiratory.Split(',');//split curr patients variable as well
                                    foreach (var i in respString)
                                    {
                                        if (tString.Contains(i))
                                        {
                                            similarity += (1 * (wResp / 100));
                                            cord++;//record keeping 
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var i in respString)
                                    {
                                        if (i.Equals(curr.Respiratory))//Broke for duplicate 
                                        {
                                            similarity += (1 * (wResp / 100));
                                            cord++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (curr.Respiratory.Contains(patient.Respiratory))//Race Variable
                                {
                                    similarity += (1 * (wResp / 100));//again for if curr is complex 
                                    cord = 1;
                                }
                            }
                            record[12] = (char)cord;//record keeping 
                        }
                    }
                    catch (NullReferenceException e) { }

                    try
                    {
                        if (!cardString.Equals(null) && !curr.Cardiovascular.Equals(null))//Do the same for all multi-string variables 
                        {
                            int cord = 0;//This variable will adjust as we go for how many variables were similar for our record. 
                            if (cardString.Length > 1)
                            {
                                if (curr.Cardiovascular.Contains(','))
                                {
                                    string[] tString = curr.Cardiovascular.Split(',');
                                    foreach (var i in cardString)
                                    {
                                        if (tString.Contains(i))
                                        {
                                            similarity += (1 * (wCardio / 100));
                                            cord++;
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var i in cardString)
                                    {
                                        if (i.Equals(curr.Cardiovascular))
                                        {
                                            similarity += (1 * (wCardio / 100));
                                            cord++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (curr.Cardiovascular.Contains(patient.Cardiovascular))
                                {
                                    similarity += (1 * (wCardio / 100));
                                    cord = 1;
                                }
                            }
                            record[13] = (char)cord;
                        }
                    }
                    catch (NullReferenceException e) { }

                    try
                    {

                        if (!patient.Gastrointestinal.Equals(null))
                        {
                            if (patient.Gastrointestinal.Equals(curr.Gastrointestinal))//CCI variable
                            {
                                similarity += (1 * (wGast / 100));
                                record[6] = '1';
                            }
                        }
                    }
                    catch (NullReferenceException e) { }

                    try
                    {
                        if (!muscString.Equals(null) && !curr.Musculoskeletal.Equals(null))
                        {
                            int cord = 0;//This variable will adjust as we go for how many variables were similar for our record. 
                            if (muscString.Length > 1)
                            {
                                if (curr.Musculoskeletal.Contains(','))
                                {
                                    string[] tString = curr.Musculoskeletal.Split(',');
                                    foreach (var i in muscString)
                                    {
                                        if (tString.Contains(i))
                                        {
                                            similarity += (1 * (wMusc / 100));
                                            cord++;
                                        }
                                    } 
                                }
                                else
                                {
                                    foreach (var i in muscString)
                                    {
                                        if (i.Equals(curr.Musculoskeletal))
                                        {
                                            similarity += (1 * (wMusc / 100));
                                            cord++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (curr.Musculoskeletal.Contains(patient.Musculoskeletal))
                                {
                                    similarity += (1 * (wMusc / 100));
                                    cord = 1;
                                }
                            }
                            record[14] = (char)cord;
                        }
                    }
                    catch (NullReferenceException e) { }

                    try
                    {
                        if (!patient.Integumentary.Equals(null))
                        {
                            if (patient.Integumentary.Equals(curr.Integumentary))//Race Variable
                            {
                                similarity += (1 * (wInt / 100));
                                record[2] = '1';
                            }
                        }
                    }
                    catch (NullReferenceException e) { }

                    try
                    {
                        if (!neuroString.Equals(null) && !curr.Neurologic.Equals(null))
                        {
                            int cord = 0;//This variable will adjust as we go for how many variables were similar for our record. 
                            if (neuroString.Length > 1)
                            {
                                if (curr.Neurologic.Contains(','))
                                {
                                    string[] tString = curr.Neurologic.Split(',');
                                    foreach (var i in neuroString)
                                    {
                                        if (tString.Contains(i))
                                        {
                                            similarity += (1 * (wNeuro / 100));
                                            cord++;
                                        }
                                    } 
                                }
                                else
                                {
                                    foreach (var i in neuroString)
                                    {
                                        if (i.Equals(curr.Neurologic))
                                        {
                                            similarity += (1 * (wNeuro / 100));
                                            cord++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (curr.Neurologic.Contains(patient.Neurologic))
                                {
                                    similarity += (1 * (wNeuro / 100));
                                    cord = 1;
                                }
                            }
                            record[15] = (char)cord;
                        }
                    }
                    catch (NullReferenceException e) { }

                    try
                    {
                        if (!patient.Exercize.Equals(null))
                        {
                            if (patient.Exercize.Equals(curr.Exercize))//ASA variable 
                            {
                                similarity += (1 * (wExer / 100));
                                record[5] = '1';
                            }
                        }
                    }
                    catch (NullReferenceException e) { }

                    try
                    {
                        if (!patient.Diet.Equals(null))
                        {
                            if (patient.Diet.Equals(curr.Diet))//BMI Variable
                            {
                                similarity += (1 * (wDiet / 100));
                                record[4] = '1';
                            }
                        }
                    }
                    catch (NullReferenceException e) { }


                    try//Complex pivot variables need to be iterated though as well
                    {
                        int cord = 0; //use same record system because its possible for more than one and I want to be able to check that 
                        foreach(var i in patient.SymptomsPivots)
                        {
                            foreach(var j in curr.SymptomsPivots)//2 foreach statments parse though both patients at the same time.
                            {
                                if (i.PossibleSymptom.Name.Equals(j.PossibleSymptom.Name))
                                {
                                    similarity += (1 * (wSym / 100));
                                    cord++;
                                }
                            }
                        }
                        record[16] = (char)cord;//record keeping 

                    } catch (NullReferenceException e) { }


                    try//Same for all Complex Pivot variables 
                    {
                        int cord = 0;
                        foreach (var i in patient.HealthFactorsPivots)
                        {
                            foreach (var j in curr.HealthFactorsPivots)
                            {
                                if (i.PossibleHealthFactor.Name.Equals(j.PossibleHealthFactor.Name))
                                {
                                    similarity += (1 * (wHF / 100));
                                    cord++;
                                }
                            }
                        }
                        record[17] = (char)cord;

                    }
                    catch (NullReferenceException e) { }


                    try
                    {
                        int cord = 0;
                        foreach (var i in patient.OtherMedsPivots)
                        {
                            foreach (var j in curr.OtherMedsPivots)
                            {
                                if (i.PossibleOtherMed.Name.Equals(j.PossibleOtherMed.Name))
                                {
                                    similarity += (1 * (wMeds / 100));
                                    cord++;
                                }
                            }
                        }
                        record[18] = (char)cord;

                    }
                    catch (NullReferenceException e) { }
                }

                try
                {
                    int cord = 0;
                    foreach (var i in patient.FamilyHistoryPivots)
                    {
                        foreach (var j in curr.FamilyHistoryPivots)
                        {
                            if (i.PossibleFamilyHistory.Name.Equals(j.PossibleFamilyHistory.Name))
                            {
                                similarity += (1 * (wFH / 100));
                                cord++;
                            }
                        }
                    }
                    record[19] = (char)cord;

                }
                catch (NullReferenceException e) { }

                if (similarity > targetSimilarity)//Now check similarity that was calculated against current most similar 
                {
                    target = curr;//if greater change mostSimilar varibles to this curr patient 
                    targetSimilarity = similarity;
                    targetRecord = record;
                }
                else if (similarity == targetSimilarity)//they are equal add up their treatments effectivness's and give the win to the more effective one
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
                int simResult = (int)Math.Round((similarity / simMax) * 100, 0);//% similar is  (Similarity / Max Similarity possible) * 100... I use math round for clean numbesr 
                simData += (curr.patientID + "," + simResult + ",");//add the currentPatientID and similarity to a string parsed by commas 
                count++;//Keep a count of how many patients in database 
            }
            string[] TargetData = simData.Split(',');//take the similarityData string and split it into an array of strings 
            Array.Reverse(TargetData);//because of the way I make the simData string there leaves a null index at the end. Reverse the Array
            TargetData = TargetData.Skip(1).ToArray();//Delete the null index
            Array.Reverse(TargetData);//Reverse the Array leaving us with an Array of ID's followed by Similarities 
            int[] Data = new int[count * 2];// Create an integer array to cast TargetData into 
            int c = 0;//iterative var
            foreach (string x in TargetData)//Iterate through each ID and Similarity in TargetData, Parse that to an appropriate varible type (int) and save accordingly in Data[]
            {
                int m = 69;// if error
                int.TryParse(x, out m);
                Data[c] = m;
                c++;
            }


                //string str2 = new string(targetRecord);
            patient.comparisonResults = (count + "|" + target.patientID + "| ");//Add this data to CompareResults to be able to use it in the Results page before ViewBag
            //ViewBag will need a size for the Array to be accessible easily from the resutls page

            if (surgery == true)//Is surgery was found to be true add surgery to suggested treatments 
            {
                patient.comparisonResults += "Surgery, ";
            }
            foreach (TreatmentsPivot var in target.TreatmentsPivots)//Add the most similar patients treatments to suggested treaments variable 
            {

                string str;
                if (var.PossibleTreatment.Name.Equals("Drug"))//if Drug name is specified in notes, make it pretty 
                {
                    try
                    {
                        str = "Drug: " + var.notes.ToString();
                        patient.comparisonResults += str + ", ";
                    }
                    catch (NullReferenceException e) { }
                }
                else if (var.PossibleTreatment.Name.Equals("Surgery"))//Skip surgery so there arnt any duplicates 
                {
                    continue;
                }
                else
                {
                    str = var.PossibleTreatment.Name;
                    patient.comparisonResults += str + ", ";
                }

            }
            db.SaveChanges();

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayMenu = "No";
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            ViewBag.SimData = Data;
            int a = patient.comparisonResults.LastIndexOf('|') + 1;
            var temp = patient.comparisonResults.Substring(a);
            if (temp.Equals(" ") || temp.Equals(null))//If  surgery was false and no other treatments where found, then we can't accutally suggest any treatments 
            {
                temp = "No Treatments to recommend, most similar patient had only treatments not applicable...";
            }
            ViewBag.CompResults = temp.Substring(0, temp.Length - 2);//Send Comparison results to Viewbag
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
            ViewBag.Sex = new SelectList(getSexes(null), "Value", "Text");
            ViewBag.HistologicalGrade = new SelectList(getGrades(null), "Value", "Text");
            ViewBag.HistologicalClassification = new SelectList(getTumorTypes(null), "Value", "Text");
            ViewBag.TumorLocation = new SelectList(getLocations(null), "Value", "Text");
            ViewBag.Diet = new MultiSelectList(getBMIChoices(), "Value", "Text");
            ViewBag.Neurologic = new MultiSelectList(getNeurologicChoices(), "Value", "Text");
            ViewBag.Musculoskeletal = new MultiSelectList(getMusculoskeletalChoices(), "Value", "Text");
            ViewBag.Gastrointestinal = new MultiSelectList(getCCIChoices(), "Value", "Text");
            ViewBag.Cardiovascular = new MultiSelectList(getCardiovascularChoices(), "Value", "Text");
            ViewBag.Exercize = new MultiSelectList(getASAChoices(), "Value", "Text");
            ViewBag.Integumentary = new MultiSelectList(getRaceChoices(), "Value", "Text");
            ViewBag.Respiratory = new MultiSelectList(getRespiratoryChoices(), "Value", "Text");
            ViewBag.Constitutional = new MultiSelectList(getKPSChoices(), "Value", "Text");
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

        
        public SelectListItem[] getSexes(int? searchPurpose)
        {
            SelectListItem[] sex;
            if (searchPurpose != null)
            {
                sex = new SelectListItem[4];
                SelectListItem any = new SelectListItem
                {
                    Text = "Any",
                    Value = null
                };
                sex[3] = any;
            }
            else
                sex = new SelectListItem[3];
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

        public SelectListItem[] getGrades(int? searchPurpose)
        {
            SelectListItem[] grade;
            if (searchPurpose != null)
            {
                grade = new SelectListItem[6];
                SelectListItem any = new SelectListItem
                {
                    Text = "Any",
                    Value = null
                };
                grade[5] = any;
            }
            else
                grade = new SelectListItem[5];
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
        public SelectListItem[] getLocations(int? searchPurpose)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (searchPurpose == CompareSearch)
            {
                foreach (Patient p in db.Patients.Where((item) => item.isCompare && item.TumorLocation != "" && item.TumorLocation != null))
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
            else
            {
                foreach (Patient p in db.Patients.Where((item) => item.isCompare == false && item.TumorLocation != "" && item.TumorLocation != null))
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
            
            
            if (searchPurpose != null)
            {
                SelectListItem any = new SelectListItem
                {
                    Text = "Any",
                    Value = null
                };
                list.Add(any);
            }
            else
            {
                SelectListItem other = new SelectListItem
                {
                    Text = "Other",
                    Value = "Other"
                };
                list.Add(other);
            }
            return list.ToArray();
        }

        public SelectListItem[] getTumorTypes(int? searchPurpose)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (searchPurpose == CompareSearch)
            {
                foreach (Patient p in db.Patients.Where((item) => item.isCompare && item.HistologicalClassification != "" && item.HistologicalClassification != null))
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
            else
            {
                foreach (Patient p in db.Patients.Where((item) => item.isCompare == false && item.HistologicalClassification != "" && item.HistologicalClassification != null))
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
            
            
            if (searchPurpose != null)
            {
                SelectListItem any = new SelectListItem
                {
                    Text = "Any",
                    Value = null
                };
                list.Add(any);
            }
            else
            {
                SelectListItem other = new SelectListItem
                {
                    Text = "Other",
                    Value = "Other"
                };
                list.Add(other);
            }
            return list.ToArray();
        }
        public SelectListItem[] getBMIChoices()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "Underweight",
                Value = "Underweight"
            });
            list.Add(new SelectListItem
            {
                Text = "Normal",
                Value = "Normal"
            });
            list.Add(new SelectListItem
            {
                Text = "Overweight",
                Value = "Overweight"
            });
            list.Add(new SelectListItem
            {
                Text = "Obese",
                Value = "Obese"
            });
            return list.ToArray();
        }
        public SelectListItem[] getKPSChoices()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "10",
                Value = "10"
            }); list.Add(new SelectListItem
            {
                Text = "20",
                Value = "20"
            }); list.Add(new SelectListItem
            {
                Text = "30",
                Value = "30"
            }); list.Add(new SelectListItem
            {
                Text = "40",
                Value = "40"
            }); list.Add(new SelectListItem
            {
                Text = "50",
                Value = "50"
            }); list.Add(new SelectListItem
            {
                Text = "60",
                Value = "60"
            }); list.Add(new SelectListItem
            {
                Text = "70",
                Value = "70"
            }); list.Add(new SelectListItem
            {
                Text = "80",
                Value = "80"
            }); list.Add(new SelectListItem
            {
                Text = "90",
                Value = "90"
            }); list.Add(new SelectListItem
            {
                Text = "100",
                Value = "100"
            });
            return list.ToArray();
        }
        public SelectListItem[] getRespiratoryChoices()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Patient p in db.Patients.Where((item) => item.isCompare == false && item.Respiratory != "" && item.Respiratory != null))
            {
                var array = p.Respiratory.Split(',');
                for (int i = 0; i < array.Length; i++)
                {
                    if (list.Where((item) => item.Text == array[i]).Count() < 1 && array[i] != "")
                    {
                        SelectListItem sli = new SelectListItem
                        {
                            Text = array[i],
                            Value = array[i]
                        };
                        list.Add(sli);
                    }
                }
            }
            return list.ToArray();
        }
        public SelectListItem[] getCardiovascularChoices()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Patient p in db.Patients.Where((item) => item.isCompare == false && item.Cardiovascular != "" && item.Cardiovascular != null))
            {
                var array = p.Cardiovascular.Split(',');
                for (int i = 0; i < array.Length; i++)
                {
                    if (list.Where((item) => item.Text == array[i]).Count() < 1 && array[i] != "")
                    {
                        SelectListItem sli = new SelectListItem
                        {
                            Text = array[i],
                            Value = array[i]
                        };
                        list.Add(sli);
                    }
                }
            }
            return list.ToArray();
        }
        public SelectListItem[] getASAChoices()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "I",
                Value = "I"
            }); list.Add(new SelectListItem
            {
                Text = "II",
                Value = "II"
            }); list.Add(new SelectListItem
            {
                Text = "III",
                Value = "III"
            }); list.Add(new SelectListItem
            {
                Text = "IV",
                Value = "IV"
            }); list.Add(new SelectListItem
            {
                Text = "V",
                Value = "V"
            }); list.Add(new SelectListItem
            {
                Text = "VI",
                Value = "VI"
            });
            return list.ToArray();
        }
        public SelectListItem[] getNeurologicChoices()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Patient p in db.Patients.Where((item) => item.isCompare == false && item.Neurologic != "" && item.Neurologic != null))
            {
                var array = p.Neurologic.Split(',');
                for (int i = 0; i < array.Length; i++)
                {
                    if (list.Where((item) => item.Text == array[i]).Count() < 1 && array[i] != "")
                    {
                        SelectListItem sli = new SelectListItem
                        {
                            Text = array[i],
                            Value = array[i]
                        };
                        list.Add(sli);
                    }
                }
            }
            return list.ToArray();
        }
        public SelectListItem[] getRaceChoices()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "American Indian or Alaska Native",
                Value = "American Indian or Alaska Native"
            });
            list.Add(new SelectListItem
            {
                Text = "Asian",
                Value = "Asian"
            });
            list.Add(new SelectListItem
            {
                Text = "Black or African American",
                Value = "Black or African American"
            });
            list.Add(new SelectListItem
            {
                Text = "Native Hawaiian or Pacific Islander",
                Value = "Native Hawaiian or Pacific Islander"
            });
            list.Add(new SelectListItem
            {
                Text = "White",
                Value = "White"
            });
            list.Add(new SelectListItem
            {
                Text = "Hispanic or Latino",
                Value = "Hispanic or Latino"
            });
            return list.ToArray();
        }
        public SelectListItem[] getMusculoskeletalChoices()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Patient p in db.Patients.Where((item) => item.isCompare == false && item.Musculoskeletal != "" && item.Musculoskeletal != null))
            {
                var array = p.Musculoskeletal.Split(',');
                for (int i = 0; i < array.Length; i++)
                {
                    if (list.Where((item) => item.Text == array[i]).Count() < 1 && array[i]!="")
                    {
                        SelectListItem sli = new SelectListItem
                        {
                            Text = array[i],
                            Value = array[i]
                        };
                        list.Add(sli);
                    }
                }
            }
            return list.ToArray();
        }
        public SelectListItem[] getCCIChoices()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "Low",
                Value = "Low"
            }); list.Add(new SelectListItem
            {
                Text = "Moderate",
                Value = "Moderate"
            }); list.Add(new SelectListItem
            {
                Text = "High",
                Value = "High"
            }); list.Add(new SelectListItem
            {
                Text = "Very High",
                Value = "Very High"
            });
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
                x = await db.SaveChangesAsync();
                if (patient.isCompare)
                {
                    return RedirectToAction("Details", new { id });
                }  
                return RedirectToAction("Index");
            }
            ViewBag.Sex = new SelectList(getSexes(null), "Value", "Text", patient.Sex);
            ViewBag.HistologicalGrade = new SelectList(getGrades(null), "Value", "Text", patient.HistologicalGrade);
            ViewBag.HistologicalClassification = new SelectList(getTumorTypes(null), "Value", "Text",patient.HistologicalClassification);
            ViewBag.TumorLocation = new SelectList(getLocations(null), "Value", "Text", patient.TumorLocation);
            ViewBag.Diet = new MultiSelectList(getBMIChoices(), "Value", "Text");
            ViewBag.Neurologic = new MultiSelectList(getNeurologicChoices(), "Value", "Text");
            ViewBag.Musculoskeletal = new MultiSelectList(getMusculoskeletalChoices(), "Value", "Text");
            ViewBag.Gastrointestinal = new MultiSelectList(getCCIChoices(), "Value", "Text");
            ViewBag.Cardiovascular = new MultiSelectList(getCardiovascularChoices(), "Value", "Text");
            ViewBag.Exercize = new MultiSelectList(getASAChoices(), "Value", "Text");
            ViewBag.Integumentary = new MultiSelectList(getRaceChoices(), "Value", "Text");
            ViewBag.Respiratory = new MultiSelectList(getRespiratoryChoices(), "Value", "Text");
            ViewBag.Constitutional = new MultiSelectList(getKPSChoices(), "Value", "Text");
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
            ViewBag.Sex = new SelectList(getSexes(null), "Value", "Text", patient.Sex);
            ViewBag.HistologicalGrade = new SelectList(getGrades(null), "Value", "Text", patient.HistologicalGrade);
            ViewBag.HistologicalClassification = new SelectList(getTumorTypes(null), "Value", "Text", patient.HistologicalClassification);
            ViewBag.TumorLocation = new SelectList(getLocations(null), "Value", "Text", patient.TumorLocation);
            ViewBag.Diet = new MultiSelectList(getBMIChoices(), "Value", "Text");
            ViewBag.Neurologic = new MultiSelectList(getNeurologicChoices(), "Value", "Text");
            ViewBag.Musculoskeletal = new MultiSelectList(getMusculoskeletalChoices(), "Value", "Text");
            ViewBag.Gastrointestinal = new MultiSelectList(getCCIChoices(), "Value", "Text");
            ViewBag.Cardiovascular = new MultiSelectList(getCardiovascularChoices(), "Value", "Text");
            ViewBag.Exercize = new MultiSelectList(getASAChoices(), "Value", "Text");
            ViewBag.Integumentary = new MultiSelectList(getRaceChoices(), "Value", "Text");
            ViewBag.Respiratory = new MultiSelectList(getRespiratoryChoices(), "Value", "Text");
            ViewBag.Constitutional = new MultiSelectList(getKPSChoices(), "Value", "Text");
            ViewBag.curDiet = patient.Diet;
            ViewBag.curNeurologic = patient.Neurologic;
            ViewBag.curMusculoskeletal = patient.Musculoskeletal;
            ViewBag.curGastrointestinal = patient.Gastrointestinal;
            ViewBag.curCardiovascular = patient.Cardiovascular;
            ViewBag.curExercize = patient.Exercize;
            ViewBag.curIntegumentary = patient.Integumentary;
            ViewBag.curRespiratory = patient.Respiratory;
            ViewBag.curConstitutional = patient.Constitutional;
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
            ViewBag.Sex =new SelectList(getSexes(null), "Value", "Text", patient.Sex);
            ViewBag.HistologicalGrade = new SelectList(getGrades(null), "Value", "Text", patient.HistologicalGrade);
            ViewBag.HistologicalClassification = new SelectList(getTumorTypes(null), "Value", "Text", patient.HistologicalClassification);
            ViewBag.TumorLocation = new SelectList(getLocations(null), "Value", "Text", patient.TumorLocation);
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
                return RedirectToAction("Index", "Manage");
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
