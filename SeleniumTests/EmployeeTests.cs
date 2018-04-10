using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;

namespace SeleniumTests
{
    [TestClass]
    public class EmployeeTests
    {

        public void HRLogin(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("1000003");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("P@$$w0rd");
            driver.FindElement(By.XPath("//button")).Submit();
        }


        [TestMethod]
        public void SearchEmployeeTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("http://localhost:58122/login");

            HRLogin(driver);

            driver.FindElement(By.XPath("//a[@id='employeesLink']")).Submit();

            driver.FindElement(By.XPath("//input[@id='employeeNumberInput']")).SendKeys("1000005");
            driver.FindElement(By.XPath("//a[@id='employeesLink']")).Submit();

            driver.FindElement(By.XPath("//img[@alt='Load Timesheet']")).Submit();

            IWebElement empNum = driver.FindElement(By.XPath("//td[@id='eployeeNumberOutput0']"));

            Assert.IsTrue(empNum.GetAttribute("value").Equals("1000001"));
        }


        [TestMethod]
        public void UpdateEmployeeTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("http://localhost:58122/login");
            HRLogin(driver);
            driver.FindElement(By.XPath("//a[@id='employeesLink']")).Submit();




        }




        [TestMethod]
        public void AddEmployeeTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("http://localhost:58122/login");
            HRLogin(driver);
            driver.FindElement(By.XPath("//a[@id='employeesLink']")).Submit();


        }
    }
}
