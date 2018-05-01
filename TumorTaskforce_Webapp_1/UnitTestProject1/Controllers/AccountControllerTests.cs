using NUnit.Framework;
using TumorTaskforce_Webapp_1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TumorTaskforce_Webapp_1.Models;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Net;
using System.Web.Routing;

namespace TumorTaskforce_Webapp_1.Controllers.Tests
{
    [TestFixture]
    public class AccountControllerTests : Controller
    {
         private AccountController _accountController;
        [Test]
        public void AccountController_Register_User()
        {
            
            var registerViewModel = new RegisterViewModel
            {
                Email = "test@test.com",
                Password = "abc123"
            };
            var result = _accountController.Register(registerViewModel).Result;
            NUnit.Framework.Assert.IsTrue(condition: result is RedirectToRouteResult);
            NUnit.Framework.Assert.IsTrue(_accountController.ModelState.All(KeyValuePair => KeyValuePair.Key != ""));
        }
        

        [TestInitialize]
        public void Initialization()
        {
            // mocking HttpContext
            var request = new Mock<HttpRequestBase>();
#pragma warning disable CS0618 // Type or member is obsolete
            request.Expect(r => r.HttpMethod).Returns("GET");
#pragma warning restore CS0618 // Type or member is obsolete
            var mockHttpContext = new Mock<HttpContextBase>();
#pragma warning disable CS0618 // Type or member is obsolete
            mockHttpContext.Expect(c => c.Request).Returns(request.Object);
#pragma warning restore CS0618 // Type or member is obsolete
            var mockControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), new Mock<ControllerBase>().Object);

            // mocking IAuthenticationManager
            var authDbContext = new ApplicationDbContext();
            var mockAuthenticationManager = new Mock<AuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.GetType());
            mockAuthenticationManager.Setup(am => am.GetType());

            var mockUrl = new Mock<UrlHelper>();

            var manager = new AccountManager<ApplicationUserManager, ApplicationDbContext, ApplicationUser>(authDbContext, mockAuthenticationManager.Object);
            _accountController =MockingMethod(mockControllerContext, mockUrl, manager);

            // using our mocked HttpContext
           // _accountController.AccountManager.Equals(_accountController.HttpContext);
        }
        [Test]
        private static AccountController MockingMethod(ControllerContext mockControllerContext, Mock<UrlHelper> mockUrl, AccountManager<ApplicationUserManager, ApplicationDbContext, ApplicationUser> manager)
        {
            return MockingMethod(mockControllerContext, mockUrl, manager);
        }

        //private static AccountController NewMethod1(ControllerContext mockControllerContext, Mock<UrlHelper> mockUrl, AccountManager<ApplicationUserManager, ApplicationDbContext, ApplicationUser> manager)
        //{
        //    return new AccountController(manager)
        //    {
        //        Url = mockUrl.Object,
        //        ControllerContext = mockControllerContext
        //    };
        //}
    }
    [TestClass]
    internal class AccountManager<T1, T2, T3>
    {
        private ApplicationDbContext authDbContext;
        private AuthenticationManager @object;

        public AccountManager(ApplicationDbContext authDbContext, AuthenticationManager @object)
        {
            this.authDbContext = authDbContext;
            this.@object = @object;
        }
    }
   
    //[TestMethod, Isolated]
    //public async Task TestWhenLoginIsBad_ErrorMessageIsShown()
    //{
    //    // Arrange
    //    // Create the wanted controller for testing 
    //    var controller = new AccountController();
    //    var loginData = new LoginViewModel { Email = "support@typemock.com", Password = "password", RememberMe = false };

    //    // Fake the ModelState
    //    Isolate.WhenCalled(() => controller.ModelState.IsValid).WillReturn(true);

    //    // Ignore AddModelError (should be called when login fails)
    //    Isolate.WhenCalled(() => controller.ModelState.AddModelError("", "")).IgnoreCall();

    //    // Fake HttpContext to return a fake ApplicationSignInManager
    //    var fakeASIM = Isolate.WhenCalled(() => controller.HttpContext.GetOwinContext().Get<ApplicationSignInManager>()).ReturnRecursiveFake();

    //    // When password checked it will fail. Note we are faking an async method
    //    Isolate.WhenCalled(() => fakeASIM.PasswordSignInAsync(null, null, true, true)).WillReturn(Task.FromResult(SignInStatus.Failure));

    //    // Act
    //   var result = await controller.Login(loginData, "http://www.typemock.com/");

    //    // Assert
    //    // The result contains login data, doesn’t redirect
    //    Assert.IsInstanceOfType(result, typeof(ViewResult));
    //    Assert.AreSame(loginData, (result as ViewResult).Model);
    //    // Make sure that the code added an error
    //    Isolate.Verify.WasCalledWithExactArguments(() => controller.ModelState.AddModelError("", "Invalid login attempt."));

   
}