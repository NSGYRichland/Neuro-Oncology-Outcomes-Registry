
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace StringLibraryTest
{
    [TestClass]
    public class UnitTest1
    {

        //ChromeDriver driver = new ChromeDriver();
        FirefoxDriver driver = new FirefoxDriver();

        [TestMethod]
        public void SearchPatient()
        {

            try
            {
                string url = "http://localhost:52612/";
                //ChromeDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("txtBox")).SendKeys("3");
                driver.FindElement(By.Id("buttonSubmit")).Click();
                driver.Close();
                driver.Dispose();
            }
            catch
            {
                ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile("D:/test.png", ScreenshotImageFormat.Png);
                driver.Quit();
            }

		}

    }
}
