using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TumorTaskforce_Webapp_1;
using TumorTaskforce_Webapp_1.Controllers;

namespace UnitTestProject1
{
    [TestClass]
   public class HomeControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {   //Arrange
            HomeController controller = new HomeController();
            //Act
            ViewResult result = controller.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TestCompare()
        {
            HomeController controllerUnderTest = new HomeController();
            var result = controllerUnderTest.Compare() as ViewResult;
            Assert.AreEqual("Compare Page.", result.ViewData["Message"]);
        }
        [TestMethod]
        public void TestAbout()
        {

        }

    }
}
