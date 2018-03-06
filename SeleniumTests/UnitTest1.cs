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
            //***Configured to work with Travis but may not need this***
            //var chromeOptions = new ChromeOptions();
            //chromeOptions.BinaryLocation = "/usr/bin/google-chrome-stable";
            //chromeOptions.AddArgument("--headless");
            //IWebDriver driver = new ChromeDriver(chromeOptions);

            //***Original way to get driver and works locally***
            var driverDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("https://www.google.com");
            driver.FindElement(By.Name("q")).SendKeys("How to cook");
            driver.FindElement(By.Name("btnK")).Submit();

            Assert.IsNotNull(driver.FindElement(By.XPath("//div[@id='resultStats']")));
            driver.Close();
        }
     
    }
}
