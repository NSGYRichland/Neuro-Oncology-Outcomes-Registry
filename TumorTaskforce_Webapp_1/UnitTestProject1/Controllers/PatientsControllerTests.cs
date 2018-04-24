using TumorTaskforce_Webapp_1.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Runtime.Remoting.Contexts;

namespace TumorTaskforce_Webapp_1.Controllers.Tests
{
    [TestFixture()]
    public class PatientsControllerTests
    {
        

        [Test()]
        public void IndexTest()
        {
            using(var conn = context.NewConnection())
            {
                var table = conn.GetSchema("Tables");
                var tableNames = table.Rows.Cast<System.Data.DataRow>().Select(xe => xe["TABLE_NAME"].ToString).ToArray();
                Assert.That(tableNames.Contains("NewTable"), Is.True);
            }
            string expected = "Index";
            PatientsController controller = new PatientsController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.AreEqual(expected, result.ViewBag.Message);
            Assert.Fail();
        }

        [Test()]
        public void CompIndexTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ResultsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DetailsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getSexesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getGradesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getLocationsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getTumorTypesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getDietChoicesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getConstitutionalChoicesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getRespiratoryChoicesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getCardiovascularChoicesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getExercizeChoicesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getNeurologicChoicesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getIntegumentaryChoicesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getMusculoskeletalChoicesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getGastrointestinalChoicesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EditTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteConfirmedTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void isAdminUserTest()
        {
            Assert.Fail();
        }

    
    }
}