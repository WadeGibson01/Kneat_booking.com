using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Kneat_booking.Pages
{
    public class ResultsPage
    {

        private IWebDriver driver;

        public ResultsPage(IWebDriver driver) 
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public IList<IWebElement> hotelNameList
        {
            get
            {
                return driver.FindElements(By.CssSelector("a > span.sr-hotel__name"));
            }
        }

    }
}
