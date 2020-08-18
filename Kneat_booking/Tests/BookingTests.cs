using System;
using System.Threading;
using Kneat_booking.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Kneat_booking.Tests
{

    public class BookingTests : BaseClass
    {

        // Sample data
        string destination = "Limerick";
        string fiveStarDestination = "The Savoy Hotel";
        string hotelWithSauna = "No.1 Pery Square Hotel & Spa";
        string fourStarDestination = "George Limerick Hotel";
        string hotelWithoutSauna = "George Limerick Hotel";

        class GetBookingDates
        {
            public string GetArrivalDate()
            {
                DateTime datePlus3Months = DateTime.Now.AddMonths(3);
                string checkinDateonly = String.Format("{0:yyyy-MM-dd}", datePlus3Months);
                string checkinDateXpath = "//td[@data-date='" + checkinDateonly + "']";
                return checkinDateXpath;
            }

            public string GetDepartureDate()
            {
                DateTime datePlus3Months = DateTime.Now.AddMonths(3);
                DateTime datePlus3Months1Day = datePlus3Months.AddDays(1);
                string checkoutDateonly = String.Format("{0:yyyy-MM-dd}", datePlus3Months1Day);
                string checkoutDateXpath = "//td[@data-date='" + checkoutDateonly + "']";
                return checkoutDateXpath;
            }

        }

        [Test]
        public void SearchbyDestinationAndValidateFiveStarFilter()
        {

            HomePage home = new HomePage(driver);
            GetBookingDates dateXpaths = new GetBookingDates();
            string checkinDateXpath = dateXpaths.GetArrivalDate();
            string checkoutDateXpath = dateXpaths.GetDepartureDate();

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            // Wait until apage is fully loaded via JavaScript
            WebDriverWait wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(30));
            wait.Until((x) =>
            {
                return ((IJavaScriptExecutor)this.driver).ExecuteScript("return document.readyState").Equals("complete");
            });

            home.ClickCookiesPopUp();
            home.DestinationInputValue(destination);
            home.DestinationListItemClick();
            home.CalendarNextMonthClick();
            home.CalendarNextMonthClick();
            home.CalendarCheckinDate(checkinDateXpath);
            home.CalendarCheckinDate(checkoutDateXpath);
            home.ClickSearchBtn();

            //Validate 5 star filter search
            home.FiveStarCheckboxCheck();
            
            ResultsPage results = new ResultsPage(driver);

            Boolean confirmPropertyIsInFiveStarSearch = CheckDestinationFilterSearchResults(fiveStarDestination);
            Assert.IsTrue(confirmPropertyIsInFiveStarSearch);

            Boolean confirmPropertyIsNotInFiveStarSearch = CheckDestinationFilterSearchResults(fourStarDestination);
            Assert.IsFalse(confirmPropertyIsNotInFiveStarSearch);

        }

        [Test]
        public void SearchbyDestinationAndValidateSpaWellnessSauna()
        {

            HomePage home = new HomePage(driver);
            GetBookingDates dateXpaths = new GetBookingDates();
            string checkinDateXpath = dateXpaths.GetArrivalDate();
            string checkoutDateXpath = dateXpaths.GetDepartureDate();

            // Wait until apage is fully loaded via JavaScript
            WebDriverWait wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(30));
            wait.Until((x) =>
            {
                return ((IJavaScriptExecutor)this.driver).ExecuteScript("return document.readyState").Equals("complete");
            });
            home.ClickCookiesPopUp();
            home.DestinationInputValue(destination);
            home.DestinationListItemClick();
            home.CalendarNextMonthClick();
            home.CalendarNextMonthClick();
            home.CalendarCheckinDate(checkinDateXpath);
            home.CalendarCheckinDate(checkoutDateXpath);
            home.ClickSearchBtn();

            //Validate spa and wellness with suana search
            home.FiveStarCheckboxCheck();
  
            // ** Waits and custom extension did not work so for stability, reluctantly using Thread.Sleep**
            //TimeSpan T = TimeSpan.FromSeconds(30);
            //wait = new WebDriverWait(driver, T);
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(), 'Spa and wellness centre')]")));
            //IWebElement swc = driver.FindElement(By.XPath("//span[contains(text(), 'Spa and wellness centre')]"));
            //swc.Click();
            
            Thread.Sleep(1000);
            home.SpaAndWellnessCheckboxCheck();

            // remove 5 star filter and
            // get element again as it is stale
            //wait = new WebDriverWait(driver, T);
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[@data-id='class-5']"))).Click();
            Thread.Sleep(1000);
            var fiveStar = driver.FindElement(By.XPath("//a[@data-id='class-5']"));
            fiveStar.Click();

            //wait = new WebDriverWait(driver, T);
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(), 'Sauna')]"))).Click();
            //IWebElement s = driver.FindElement(By.XPath("//span[contains(text(), 'Sauna')]"));
            //s.Click();
            Thread.Sleep(1000);
            home.SaunaCheckboxCheck();

            ResultsPage results = new ResultsPage(driver);

            Boolean confirmPropertyIsInSpaWellnessSaunaSearch = CheckDestinationFilterSearchResults(hotelWithSauna);
            Assert.IsTrue(confirmPropertyIsInSpaWellnessSaunaSearch);

            Boolean confirmPropertyIsNotInSpaWellnessSaunaSearch = CheckDestinationFilterSearchResults(hotelWithoutSauna);
            Assert.IsFalse(confirmPropertyIsNotInSpaWellnessSaunaSearch);

        }

        private bool CheckDestinationFilterSearchResults(string hotelDestination)
        {
            ResultsPage results = new ResultsPage(driver);

            int a = 0;
            foreach (IWebElement address in results.hotelNameList)
            {
                if (address.Text.Contains(hotelDestination))
                {
                    a++;
                }
            }
            if (a == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
