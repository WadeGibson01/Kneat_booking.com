using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Configuration;

namespace Kneat_booking
{
    public class BaseClass
    {
        public IWebDriver driver;

        [SetUp]
        public void startTest() // Method executed before tests
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Url = ConfigurationManager.AppSettings["Test url"];
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void endTest() // Method executed after tests
        {
            driver.Quit();
        }

    }
}
