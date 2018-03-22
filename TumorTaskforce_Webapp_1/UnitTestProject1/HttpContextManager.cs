using System.Web.SessionState;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Web;


   public class HttpContextManager
    {
        [SetUp]
        public void TestSetup()
        {
            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new HttpRequest("", url: "http://localhost:52612/ ", queryString: "");

            // Step 2: Setup the HTTP Response
            var httpResponce = new HttpResponse(new StringWriter());

            // Step 3: Setup the Http Context
            var httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
            new HttpStaticObjectsCollection(), 10, true, HttpCookieMode.AutoDetect, SessionStateMode.InProc, false);



            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance,
                                null, CallingConventions.Standard, new[] { typeof(HttpSessionStateContainer) }, null).Invoke(new object[] { sessionContainer });



            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
        }

        [Test]
        public void BasicTest_Push_Item_Into_Session()
        {
            // Arrange
            var itemValue = "RandomItemValue";
            var itemKey = "RandomItemKey";

            // Act
            HttpContext.Current.Session.Add(itemKey, itemValue);

            // Assert
            Assert.AreEqual(HttpContext.Current.Session[itemKey], itemValue);
        }

    }




