using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

//this a file that will house the multiple behav tests for our website

namespace SeleniumTests
{
    [TestFixture]
    public class MoveThroughWebsite
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.katalon.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        //log onto website and then logoff
        [Test]
        public void LogOnOff()
        {
            driver.Navigate().GoToUrl("http://tumor1.azurewebsites.net/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("Briangs");
            Thread.Sleep(3000);
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Password2018!");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.LinkText("Log off")).Click();
        }

        [Test]
        public void Register()
        {
            driver.Navigate().GoToUrl("http://tumor1.azurewebsites.net/");
            driver.FindElement(By.Id("registerLink")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("briangs@email.sc.edu");
            Thread.Sleep(3000);
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("Briangs");
            Thread.Sleep(3000);
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Password2018!");
            Thread.Sleep(3000);
            driver.FindElement(By.Id("ConfirmPassword")).Clear();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("Password2018!");
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//input[@value='Register']")).Click();
            Thread.Sleep(3000);
        }
        //move through databse using search fields 
        [Test]
        public void NavigateDatabase()
        {
            driver.Navigate().GoToUrl("http://tumor1.azurewebsites.net/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("Briangs");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Password2018!");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Patients")).Click();
            //Thread.Sleep(1500);
            driver.FindElement(By.Id("sex")).Click();
            new SelectElement(driver.FindElement(By.Id("sex"))).SelectByText("Any");
            driver.FindElement(By.XPath("//option[@value='']")).Click();
            driver.FindElement(By.Id("clss")).Click();
            new SelectElement(driver.FindElement(By.Id("clss"))).SelectByText("Any");
            driver.FindElement(By.XPath("(//option[@value=''])[2]")).Click();
            driver.FindElement(By.Id("tumLoc")).Click();
            new SelectElement(driver.FindElement(By.Id("tumLoc"))).SelectByText("Any");
            driver.FindElement(By.XPath("(//option[@value=''])[3]")).Click();
            driver.FindElement(By.Id("grade")).Click();
            new SelectElement(driver.FindElement(By.Id("grade"))).SelectByText("Any");
            driver.FindElement(By.XPath("(//option[@value=''])[4]")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.Id("q")).Click();
            driver.FindElement(By.Id("q")).Clear();
            driver.FindElement(By.Id("q")).SendKeys("6");
            driver.FindElement(By.Id("buttonSubmit")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.Id("q")).Click();
            driver.FindElement(By.Id("q")).Clear();
            driver.FindElement(By.Id("q")).SendKeys("");
            driver.FindElement(By.Id("sex")).Click();
            new SelectElement(driver.FindElement(By.Id("sex"))).SelectByText("Male");
            driver.FindElement(By.XPath("//option[@value='M']")).Click();
            driver.FindElement(By.Id("buttonSubmit")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.Id("sex")).Click();
            new SelectElement(driver.FindElement(By.Id("sex"))).SelectByText("Any");
            driver.FindElement(By.XPath("//option[@value='']")).Click();
            driver.FindElement(By.Id("clss")).Click();
            new SelectElement(driver.FindElement(By.Id("clss"))).SelectByText("Metastatic Squamous Cell Carcinoma");
            driver.FindElement(By.XPath("//option[@value='Metastatic Squamous Cell Carcinoma ']")).Click();
            driver.FindElement(By.Id("buttonSubmit")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.Id("clss")).Click();
            new SelectElement(driver.FindElement(By.Id("clss"))).SelectByText("Any");
            driver.FindElement(By.XPath("(//option[@value=''])[2]")).Click();
            driver.FindElement(By.Id("tumLoc")).Click();
            new SelectElement(driver.FindElement(By.Id("tumLoc"))).SelectByText("Right Intraaxial Parietal");
            driver.FindElement(By.XPath("//option[@value='Right Intraaxial Parietal']")).Click();
            driver.FindElement(By.Id("buttonSubmit")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.Id("tumLoc")).Click();
            new SelectElement(driver.FindElement(By.Id("tumLoc"))).SelectByText("Any");
            driver.FindElement(By.XPath("(//option[@value=''])[3]")).Click();
            driver.FindElement(By.Id("grade")).Click();
            new SelectElement(driver.FindElement(By.Id("grade"))).SelectByText("1");
            driver.FindElement(By.XPath("//option[@value='1']")).Click();
            driver.FindElement(By.Id("buttonSubmit")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.Id("grade")).Click();
            new SelectElement(driver.FindElement(By.Id("grade"))).SelectByText("Any");
            driver.FindElement(By.XPath("(//option[@value=''])[4]")).Click();
        }

        //creates a dummy patient ID just for testing purposes; uses the 11th patient in database
        [Test]
        public void CreatePatient()
        {
            driver.Navigate().GoToUrl("http://tumor1.azurewebsites.net/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("Briangs");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Password2018!");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Patients")).Click();
            driver.FindElement(By.LinkText("Create New Patient")).Click();
            driver.FindElement(By.Id("Sex")).Click();
            new SelectElement(driver.FindElement(By.Id("Sex"))).SelectByText("Female");
            driver.FindElement(By.XPath("//option[@value='F']")).Click();
            driver.FindElement(By.Id("Age")).Click();
            driver.FindElement(By.Id("Age")).Clear();
            driver.FindElement(By.Id("Age")).SendKeys("25");
            driver.FindElement(By.Id("Integumentary")).Click();
            new SelectElement(driver.FindElement(By.Id("Integumentary"))).SelectByText("Asian");
            driver.FindElement(By.XPath("//option[@value='Asian']")).Click();
            driver.FindElement(By.Id("Diet")).Click();
            new SelectElement(driver.FindElement(By.Id("Diet"))).SelectByText("Normal");
            driver.FindElement(By.XPath("//option[@value='Normal']")).Click();
            driver.FindElement(By.Id("Exercize")).Click();
            new SelectElement(driver.FindElement(By.Id("Exercize"))).SelectByText("III");
            driver.FindElement(By.XPath("//option[@value='III']")).Click();
            driver.FindElement(By.Id("Gastrointestinal")).Click();
            new SelectElement(driver.FindElement(By.Id("Gastrointestinal"))).SelectByText("Moderate");
            driver.FindElement(By.XPath("//option[@value='Moderate']")).Click();
            driver.FindElement(By.Id("Constitutional")).Click();
            new SelectElement(driver.FindElement(By.Id("Constitutional"))).SelectByText("50");
            driver.FindElement(By.XPath("//option[@value='50']")).Click();
            driver.FindElement(By.Id("HistologicalClassification")).Click();
            new SelectElement(driver.FindElement(By.Id("HistologicalClassification"))).SelectByText("Hemangioblastoma");
            driver.FindElement(By.XPath("//option[@value='Hemangioblastoma']")).Click();
            driver.FindElement(By.Id("HistologicalGrade")).Click();
            new SelectElement(driver.FindElement(By.Id("HistologicalGrade"))).SelectByText("2");
            driver.FindElement(By.XPath("//option[@value='2']")).Click();
            driver.FindElement(By.Id("TumorWidth")).Click();
            driver.FindElement(By.Id("TumorWidth")).Clear();
            driver.FindElement(By.Id("TumorWidth")).SendKeys("5");
            driver.FindElement(By.Id("TumorHeight")).Clear();
            driver.FindElement(By.Id("TumorHeight")).SendKeys("5");
            driver.FindElement(By.Id("TumorLength")).Clear();
            driver.FindElement(By.Id("TumorLength")).SendKeys("5");
            driver.FindElement(By.Id("TumorLocation")).Click();
            new SelectElement(driver.FindElement(By.Id("TumorLocation"))).SelectByText("Extraaxial Anterior Fossa");
            driver.FindElement(By.XPath("//option[@value='Extraaxial Anterior Fossa']")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [addSelection | //select[@id='Respiratory'] | label=Lung Cancer]]
            driver.FindElement(By.XPath("//option[@value='Lung Cancer ']")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [addSelection | //select[@id='Respiratory'] | label=Chronic Pulmonary Disease]]
            driver.FindElement(By.XPath("//option[@value='Chronic Pulmonary Disease ']")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [addSelection | //select[@id='Cardiovascular'] | label=Hypertension]]
            driver.FindElement(By.XPath("//option[@value='Hypertension']")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [addSelection | //select[@id='Musculoskeletal'] | label=Weakness]]
            driver.FindElement(By.XPath("//option[@value='Weakness ']")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [addSelection | //select[@id='Neurologic'] | label=Dizziness]]
            driver.FindElement(By.XPath("//option[@value='Dizziness ']")).Click();
            driver.FindElement(By.XPath("//input[@value='Create']")).Click();
        }

        //use of dummy patient ID to show deletion of a patient inside database; uses 11th patient
        [Test]
        public void DeletePatient()
        {
            driver.Navigate().GoToUrl("http://tumor1.azurewebsites.net/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("Briangs");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Password2018!");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Patients")).Click();
            Thread.Sleep(2500);
            driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[11]")).Click();
            Thread.Sleep(2500);
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Thread.Sleep(4000);
        }

        [Test]
        public void EditPatient()
        {
            driver.Navigate().GoToUrl("http://tumor1.azurewebsites.net/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("Briangs");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Password2018!");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Patients")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'View Patient')])[11]")).Click();
            driver.FindElement(By.LinkText("(Edit)")).Click();
            driver.FindElement(By.Id("HistologicalGrade")).Click();
            driver.FindElement(By.Id("HistologicalGrade")).SendKeys("1");
            driver.FindElement(By.Id("Cardiovascular")).Click();
            driver.FindElement(By.Id("Cardiovascular")).Clear();
            driver.FindElement(By.Id("Cardiovascular")).SendKeys("poor");
            driver.FindElement(By.Id("Cardiovascular")).SendKeys(Keys.Enter);
            driver.FindElement(By.LinkText("(Edit)")).Click();
            driver.FindElement(By.Id("HistologicalGrade")).Click();
            driver.FindElement(By.Id("HistologicalGrade")).SendKeys("3");
            driver.FindElement(By.Id("Cardiovascular")).Click();
            driver.FindElement(By.Id("Cardiovascular")).Clear();
            driver.FindElement(By.Id("Cardiovascular")).SendKeys("normal");
            driver.FindElement(By.XPath("//div[13]/div")).Click();
            driver.FindElement(By.XPath("//input[@value='Save']")).Click();
        }

        [Test]
        public void ComparePatient()
        {
            driver.Navigate().GoToUrl("http://tumor1.azurewebsites.net/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("Briangs");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Password2018!");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Manage")).Click();
            driver.FindElement(By.LinkText("Comparison Patients")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'View')])[5]")).Click();
            driver.FindElement(By.XPath("//input[@value='Compare Now']")).Click();
        }

        [Test]
        public void TravManageData ()
        {
            driver.Navigate().GoToUrl("http://tumor1.azurewebsites.net/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("Briangs");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Password2018!");
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            driver.FindElement(By.LinkText("Manage")).Click();
            driver.FindElement(By.LinkText("Possible Health Factors")).Click();
            driver.FindElement(By.LinkText("Manage")).Click();
            driver.FindElement(By.LinkText("Possible Treatments")).Click();
            driver.FindElement(By.LinkText("Manage")).Click();
            driver.FindElement(By.LinkText("Possible Other Meds")).Click();
            driver.FindElement(By.LinkText("Manage")).Click();
            driver.FindElement(By.LinkText("Possible Symptoms")).Click();
            driver.FindElement(By.LinkText("Manage")).Click();
            driver.FindElement(By.LinkText("Users")).Click();
            driver.FindElement(By.LinkText("Manage")).Click();
            driver.FindElement(By.LinkText("Comparison Patients")).Click();
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }

    internal class SelectElement
    {
        private IWebElement webElement;

        public SelectElement(IWebElement webElement)
        {
            this.webElement = webElement;
        }

        internal void SelectByText(string v)
        {
            throw new NotImplementedException();
        }
    }
}