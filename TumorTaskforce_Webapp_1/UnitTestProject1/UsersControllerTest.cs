using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TumorTaskforce_Webapp_1;
using TumorTaskforce_Webapp_1.Controllers;


namespace UnitTestProject1
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void ViewIndex()
        {
            Assert.AreEqual("UsersController", "UsersController");
        }
    }
}
