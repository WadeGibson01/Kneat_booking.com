using System;
using System.Threading;
using OpenQA.Selenium;

namespace Kneat_booking.Pages
{
    public class HomePage
    {

        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        private IWebElement acceptCookies
        {
            get
            {
                return driver.FindElement(By.XPath("//button[@data-gdpr-consent='accept']"));
            }
        }
        private IWebElement destinationInput
        {
            get
            {
                return driver.FindElement(By.Id("ss"));
            }
        }

        private IWebElement destinationListItem
        {
            get
            {
                return driver.FindElement(By.XPath("//li[@data-label='Limerick, Limerick County, Ireland']"));
            }
        }

        private IWebElement searchBtn
        {
            get
            {
                return driver.FindElement(By.XPath("//span[contains(text(), 'Search')]"));
            }
        }

        private IWebElement calendarNextMonth
        {
            get
            {
                return driver.FindElement(By.XPath("//*[@id='frm']/div[1]/div[2]/div[2]/div/div/div[2]"));
            }
        }

        private IWebElement fourStarCheckbox
        {
            get
            {
                return driver.FindElement(By.XPath("//a[@data-id='class-4']"));
            }
        }
        private IWebElement fiveStarCheckbox
        {
            get
            {
                return driver.FindElement(By.XPath("//a[@data-id='class-5']"));
            }
        }
        private IWebElement spaAndWellnessCheckbox
        {
            get
            {
                return driver.FindElement(By.XPath("//span[contains(text(), 'Spa and wellness centre')]"));
            }
        }
        private IWebElement saunaCheckbox
        {
            get
            {
                return driver.FindElement(By.XPath("//span[contains(text(), 'Sauna')]"));
            }
        }


        //Methods
        public void ClickCookiesPopUp()
        {
            Thread.Sleep(2000);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", acceptCookies);
        }

        public void ClickSearchBtn()
        {
            searchBtn.Click();
        }

        public void DestinationInputValue(string dest)
        {
            destinationInput.SendKeys(dest);
        }

        public void DestinationListItemClick()
        {
            destinationListItem.Click();
        }

        public void CalendarNextMonthClick()
        {
            calendarNextMonth.Click();
        }
        
        public void CalendarCheckinDate(string checkinDateXpath)
        {
            driver.FindElement(By.XPath(checkinDateXpath)).Click();
        }

        public void CalendarCheckoutDate(string checkoutDateXpath)
        {
            driver.FindElement(By.XPath(checkoutDateXpath)).Click();
        }

        public void FourStarCheckboxCheck()
        {
            fourStarCheckbox.Click();
        }

        public void FiveStarCheckboxCheck()
        {
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            fiveStarCheckbox.Click();
        }

        public void SpaAndWellnessCheckboxCheck()
        {
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", spaAndWellnessCheckbox);
        }

        public void SaunaCheckboxCheck()
        {
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", saunaCheckbox);
        }
    }
}

