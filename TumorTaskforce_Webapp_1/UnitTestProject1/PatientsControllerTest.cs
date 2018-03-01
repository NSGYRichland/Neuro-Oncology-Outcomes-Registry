using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TumorTaskforce_Webapp_1;
using TumorTaskforce_Webapp_1.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
   public class PatientsControllerTest
    {
        [TestMethod]
        public void TestPatientViewDetails()
        {
            var controller = new PatientsController();
            var result = (RedirectToRouteResult)controller.Details(-1);
           // var patients = (Patient) result.ViewData.Model;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        [TestMethod]
        public void TestPatientCreate()
        {

        }
    }
}
