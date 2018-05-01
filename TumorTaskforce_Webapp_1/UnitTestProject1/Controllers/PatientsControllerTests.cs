using NUnit.Framework;
using TumorTaskforce_Webapp_1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Principal;
using System.Web.Services;
using Moq;
using System.Linq.Expressions;

namespace TumorTaskforce_Webapp_1.Controllers.Tests
{
    [TestFixture]
    public class PatientsControllerTests : Controller
    {
        patientService _patientService;
        Mock<IList<Patient>> _patientMock;

        [TestInitialize]
        public void Setup()
        {
            _patientMock = new Mock<IList<Patient>>();
            _patientService = new patientService(_patientMock.Object);
        }
        [TestMethod]
        public void NoReturnWhenNotFound()
        {
            var Id = "001";
            var age = "45";
            var race = "white";
            var classification = "tumor";
            var location = "brain";

            _patientMock.Setup(Returns(new List<Patient>()));

            //Act
            List<Patient> actual = _patientService.GetPatients(Id, age, race, classification, location);

            NUnit.Framework.Assert.False(actual.Any());

            _patientMock.VerifyAll();
        }

        private Expression<Action<IList<Patient>>> Returns(List<Patient> list)
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GetIndexTest()
        {
            var patientList = new List<Patient>();
            //Arrange
            var Id = "001";
            var age = "45";
            var race = "white";
            var classification = "tumor";
            var location = "brain";

            //Act
            List<Patient> output = patientService.GetPatient(Id, age, race, classification, location);

            //Assert
            NUnit.Framework.Assert.IsNotNull(output);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(output, typeof(List<Patient>));

            //PatientsController patientController = new PatientsController();
            //ActionResult result = patientController.Index("a", "lobes", "tumor", "3", "M");
            //Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            //RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            //Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(routeResult.RouteValues["action"], "asd");
        }

        [Test]
        public void CompIndexTest()
        {
            PatientsController patientController = new PatientsController();
            ActionResult result = patientController.Index("a", "lobes", "tumor", "3", "M");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [Test]
        public void ResultsTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DetailsTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CreateTest()
        {
            throw new NotImplementedException();
        }

        private class patientService
        {
            private IList<Patient> @object;

            public patientService(IList<Patient> @object)
            {
                this.@object = @object;
            }

            internal static List<Patient> GetPatient(string id, string age, string race, string classification, string location)
            {
                throw new NotImplementedException();
            }

            internal List<Patient> GetPatients(string id, string age, string race, string classification, string location)
            {
                throw new NotImplementedException();
            }
        }
    }
}