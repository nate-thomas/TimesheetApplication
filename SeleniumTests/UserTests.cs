using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Reflection;

namespace SeleniumTests
{
    [TestClass]
    public class UserTests
    {
        private string loginUrl = "http://localhost:58122/login";

        //Login Helper Function
        public void AdminLogin(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("1000001");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("P@$$W0rd");
            driver.FindElement(By.XPath("//button")).Submit();
        }

        [TestMethod]
        public void ChangePasswordTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl(loginUrl);
            AdminLogin(driver);

            driver.FindElement(By.XPath("//a[@id='userLink']")).Submit();

            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("P@$$W0rd12345");
            driver.FindElement(By.XPath("//input[@name='confirmPassword']")).SendKeys("P@$$W0rd12345");

            driver.FindElement(By.XPath("//button[text()='Update Password']")).Submit();

            driver.FindElement(By.XPath("//a[@id='logoutLink']")).Submit();

            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("1000001");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("P@$$W0rd12345");
            driver.FindElement(By.XPath("//button")).Submit();

            var logo = driver.FindElement(By.XPath("//div[text()='TimeSheetApplication']"));
            Assert.IsNotNull(logo);

            driver.FindElement(By.XPath("//a[@id='userLink']")).Submit();

            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("P@$$W0rd");
            driver.FindElement(By.XPath("//input[@name='confirmPassword']")).SendKeys("P@$$W0rd");

            driver.FindElement(By.XPath("//button[text()='Update Password']")).Submit();
        }






    }
}
