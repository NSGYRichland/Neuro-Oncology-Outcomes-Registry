using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using NUnit.Framework;
using System.Web.Services.Description;

namespace TumorTaskforce_UnitTests
{
    [TestFixture]
    class TestWebApp

    {
        IWebDriver driver;

        [SetUp]
        public void Initilize()
        {
            driver = new EdgeDriver();
        }
        [Test]
        public void OpenWebAppTest()
        {
            driver.Url = "http://tumor1.azurewebsites.net";
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }


    }
   

}

