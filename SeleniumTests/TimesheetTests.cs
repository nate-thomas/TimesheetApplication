using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;

namespace SeleniumTests
{
    [TestClass]
    public class TimesheetTests
    {

        //Login Helper Function
        public void Login(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("1000001");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("P@$$W0rd");
            driver.FindElement(By.XPath("//button")).Submit();
        }

        [TestMethod]
        public void SearchTimesheetTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("http://localhost:58122/login");
            Login(driver);

            driver.FindElement(By.XPath("//input[@id='']")).SendKeys("");
            driver.FindElement(By.XPath("//button[@id='']")).Submit();


        }

        [TestMethod]
        public void UpdateTimesheetTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("http://localhost:58122/login");




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
