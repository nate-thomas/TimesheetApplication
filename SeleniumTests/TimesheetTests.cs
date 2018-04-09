using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Reflection;

namespace SeleniumTests
{
    [TestClass]
    public class TimesheetTests
    {

        //Login Helper Function
        public void AdminLogin(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("1000001");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("P@$$w0rd");
            driver.FindElement(By.XPath("//button")).Submit();
        }

        public Boolean IsAlertPresent(IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }   // try 
            catch (NoAlertPresentException exception)
            {
                return false;
            }
        }

        [TestMethod]
        public void SearchTimesheetTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("http://localhost:58122/login");
            AdminLogin(driver);

            driver.FindElement(By.XPath("//input[@id='']")).SendKeys("");
            driver.FindElement(By.XPath("//button[@id='']")).Submit();


        }

        [TestMethod]
        public void UpdateTimesheetOverFourtyHours()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("http://localhost:58122/login");

            AdminLogin(driver);
            //Navigate to timesheet page
            driver.FindElement(By.XPath("//a[@id='timesheetsLink']")).Submit();
            //Add new row
            driver.FindElement(By.XPath("//button[@id='addTimesheetRowButton']")).Submit();
            //Enter hours into cells            
            driver.FindElement(By.XPath("//input[@name='saturday']]")).SendKeys("16");
            driver.FindElement(By.XPath("//input[@name='monday']]")).SendKeys("16");
            driver.FindElement(By.XPath("//input[@name='friday']]")).SendKeys("16");

            //Click update
            driver.FindElement(By.XPath("//button[@id='updateTimesheetButton']")).Submit();

            //Check if alert is present
            Assert.IsTrue(IsAlertPresent(driver));
        }

        [TestMethod]
        public void ResetTimesheetTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("http://localhost:58122/login");




        }

        [TestMethod]
        public void SignTimesheetTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("http://localhost:58122/login");


        }

        [TestMethod]
        public void AddRowTimesheetTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("http://localhost:58122/login");


        }
    }
}
