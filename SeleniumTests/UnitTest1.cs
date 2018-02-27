using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Reflection;

namespace SeleniumTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GoogleSearchTest()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.BinaryLocation = "/usr/bin/google-chrome-stable";
            chromeOptions.AddArgument("--headless");

            IWebDriver driver = new ChromeDriver(chromeOptions);

            driver.Navigate().GoToUrl("https://www.google.com");
            driver.FindElement(By.Name("q")).SendKeys("How to cook");
            driver.FindElement(By.Name("btnK")).Submit();

            Assert.IsNotNull(driver.FindElement(By.XPath("//div[@id='resultStats']")));
            driver.Close();
        }

        [TestMethod]
        public void TestLogin()
        {

            var driverDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("https://lab03-v2.azurewebsites.net");
            driver.FindElement(By.XPath("//*[@id='registerLink']")).Click();
            driver.FindElement(By.XPath("//*[@id='Email']")).SendKeys("aasddd@ad.af");

            driver.FindElement(By.XPath("//*[@id='Password']")).SendKeys("123qwe!@#QWE");
            driver.FindElement(By.XPath("//*[@id='ConfirmPassword']")).SendKeys("123qwe!@#QWE");
            driver.FindElement(By.XPath("//*[@value='Register']")).Submit();

            var check = driver.FindElement(By.XPath("//*[@title='Manage']"));

            Assert.AreEqual(check.Text, "Hello aasddd@ad.af!");


            driver.Close();
        }

        [TestMethod]
        public void CounterTest()
        {

            var driverDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("https://timesheetapplication.azurewebsites.net/counter");

            var check = driver.FindElement(By.XPath("//strong[text()='0']"));
            for (var x = 1; x <= 5; x++)
            {
                driver.FindElement(By.XPath("//button[text()='Increment']")).Click();

            }

            

            Assert.AreEqual(check.Text, "5");


            driver.Close();
        }
    }
}
