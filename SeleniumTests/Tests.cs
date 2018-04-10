﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Reflection;

namespace SeleniumTests
{
    [TestClass]
    public class Tests
    {
        private string loginUrl = "http://localhost:58122/login";

        //Login Helper Function
        public void AdminLogin(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("1000001");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("P@$$W0rd");
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
        public void LoginTestValidCredentials()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl(loginUrl);
            AdminLogin(driver);

            var logo = driver.FindElement(By.XPath("//div[text()='TimeSheetApplication']"));
            Assert.IsNotNull(logo);
        }

        [TestMethod]
        public void LoginTestInvalidCredentials()
        {
            var driverDir = System.IO.Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = new ChromeDriver(driverDir);

            driver.Navigate().GoToUrl(loginUrl);

            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("12312312322");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("00000");
            driver.FindElement(By.XPath("//button")).Submit();

            Assert.IsTrue(IsAlertPresent(driver));
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
