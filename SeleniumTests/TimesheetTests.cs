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

        private string loginUrl = "http://localhost:58122/login";

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
        public void ViewSelectTimesheetTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl(loginUrl);
            AdminLogin(driver);

            driver.FindElement(By.XPath("//button[@id='timesheetArchiveButtonSmall']")).Submit();
            driver.FindElement(By.XPath("//button[@id='timesheetViewButton0']")).Submit();

            IWebElement input = driver.FindElement(By.XPath("//input[@id='endDateInput']"));

            Assert.IsTrue(input.GetAttribute("value").Equals("2018-02-02"));
        }

        [TestMethod]
        public void SearchTimesheetTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl(loginUrl);
            AdminLogin(driver);

            driver.FindElement(By.XPath("//input[@id='endDateInput']")).SendKeys("2018-03-16");
            driver.FindElement(By.XPath("//img[@alt='Load Timesheet']")).Submit();

            IWebElement input = driver.FindElement(By.XPath("//input[@id='totalInput0']"));
            
            Assert.IsTrue(input.GetAttribute("value").Equals("11"));
        }

        [TestMethod]
        public void UpdateTimesheetOverFourtyHours()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl(loginUrl);

            AdminLogin(driver);
            //Navigate to timesheet page
            driver.FindElement(By.XPath("//a[@id='timesheetsLink']")).Submit();
            //Add new row
            driver.FindElement(By.XPath("//button[@id='addTimesheetRowButton']")).Submit();
            //Enter hours into cells            
            driver.FindElement(By.XPath("//input[@id='saturdayInput0']")).SendKeys("16");
            driver.FindElement(By.XPath("//input[@id='tuesdayInput0']")).SendKeys("16");
            driver.FindElement(By.XPath("//input[@id='mondayInput0']")).SendKeys("16");

            //Select Project and workpackage
            driver.FindElement(By.XPath("//select[@id='projectNumberSelect0']")).FindElement(By.XPath("//option[@value='Cloud001']")).Click();
            driver.FindElement(By.XPath("//select[@id='workPackageNumberSelect0']")).FindElement(By.XPath("//option[@value='A']")).Click();

            //Click update
            driver.FindElement(By.XPath("//button[@id='updateTimesheetButton']")).Submit();

            //Check if alert is present
            Assert.IsTrue(IsAlertPresent(driver));
        }


        [TestMethod]
        public void OverDailyLimitTimesheetTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl(loginUrl);

            AdminLogin(driver);

            IWebElement input = driver.FindElement(By.XPath("//input[@id='saturdayInput0']"));
            input.SendKeys("25");

            Assert.IsTrue(input.GetAttribute("class").Equals("ng-valid ng-dirty ng-touched timesheet-input invalid-input"));
        }

        [TestMethod]
        public void AddRowTimesheetTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl(loginUrl);

            AdminLogin(driver);

            driver.FindElement(By.XPath("//input[@id='endDateInput']")).SendKeys("2000-03-16");
            driver.FindElement(By.XPath("//img[@alt='Load Timesheet']")).Submit();


            //Click new row button
            driver.FindElement(By.XPath("//button[@id='addTimesheetRowButton']")).Submit();

            var newRow = driver.FindElement(By.XPath("//select[@id='projectNumberSelect1']"));

            Assert.IsNotNull(newRow);
        }

        [TestMethod]
        public void SupervisorApproveTimesheetTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl(loginUrl);

            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("1000005");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("P@$$w0rd");
            driver.FindElement(By.XPath("//button")).Submit();

            driver.FindElement(By.XPath("//button[@id='timesheetArchiveButtonLarge']")).Submit();
            driver.FindElement(By.XPath("//button[text()='View Employee Timesheets']")).Submit();

            int i = 0;
            IWebElement status;
            while (true)
            {

                try
                {
                    status = driver.FindElement(By.XPath("//td[@id='timesheetStatus" + i + "']"));
                }
                catch (Exception)
                {
                    return;
                }
                
                if (status.GetAttribute("text").Equals("Submitted"))
                {
                    break;
                }
                i++;
            }

            driver.FindElement(By.XPath("//td[@id='timesheetViewButton" + i + "']")).Submit();
            driver.FindElement(By.XPath("//button[text()='Approve']")).Submit();

            driver.SwitchTo().Alert().Accept();

            IWebElement approveBtn = driver.FindElement(By.XPath("//button[text()='Approve']"));
            Assert.IsNull(approveBtn);
        }


    }
}
