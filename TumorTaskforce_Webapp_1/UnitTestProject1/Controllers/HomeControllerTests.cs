
using TumorTaskforce_Webapp_1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;
using NUnit.Framework;
using Moq;
using System.Web.Mvc;
using NUnit.Engine.Services;
using System.Web;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TumorTaskforce_Webapp_1.Controllers.Tests
{
    [TestFixture]
    public class HomeControllerTests 

    {

        [TestMethod]
        public void IndexTest()
        {
            string expected = "Index";
            new Mock<IPrincipal>().Expect(p => p.IsInRole("admin")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.ExpectGet(ctx => ctx.User).Returns(new Mock<IPrincipal>().Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

            var homeController = new HomeController();
            homeController.ControllerContext = controllerContextMock.Object;
            var result = homeController.Index();
            new Mock<IPrincipal>().Verify(p => p.IsInRole("admin"));
            //Assert.AreEqual(((ViewResult)result).ViewName, "Index");
        }
    }

    //    [Test]
    //    public void ManageTest()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    [Test]
    //    public void CompareTest()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    [Test]
    //    public void isAdminUserTest()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal interface ISessionRepo
    //{
    //    //internal void ();
    //}
}