using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Web.Mvc;
using NUnit.Framework;

namespace TumorTaskforce_Webapp_1.Controllers
{
    [TestFixture]
    public class TestPatientControllerClass
    { 
        /// <summary>
        /// THIS UNIT TEST IS STILL UNDER CONSTRUCTION
        /// </summary>

        [Test]
        public void TestPatientIndex()
        {
            //var obj = new PatientsController();
            //var actionResult = obj.Index("Patient") as ViewResult;

            //Assert.That(actionResult.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void TestPatientCreateRedirect()
        {

           // var obj = new PatientsController();
           // var ents = new tumorDBEntities();
           // RedirectToRouteResult result = obj.Create(new Patient()
           // { patientID = 3, Age = 36, Sex = "M" }) as RedirectToRouteResult;
           //Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));

        }
        [Test]
        public void TestPatientCreateErrorView()
        {
          //  var obj = new PatientsController();
          //  var ents = new tumorDBEntities();
          //  ViewResult result = obj.Create(new Patient()
          //  {
          //      patientID = 3,
          //      Age = 36,
          //      Sex = "M"
          //  }) as ViewResult;
          //Assert.That(result.ViewName, Is.EqualTo("Error"));

        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}