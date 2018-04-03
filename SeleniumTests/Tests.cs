using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;

namespace SeleniumTests
{
    [TestClass]
    public class Tests
    {
        //Login Helper Function
        public void Login(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("a");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("P@$$W0rd");
            driver.FindElement(By.XPath("//button")).Submit();
        }

        [TestMethod]
        public void LoginTest()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl("http://localhost:58122/login");
            Login(driver);

            //Add an assert to see if login worked or not


        }

        

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
