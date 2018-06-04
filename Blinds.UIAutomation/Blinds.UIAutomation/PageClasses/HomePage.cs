using System.Collections.Generic;
using OpenQA.Selenium;
using Blinds.UIAutomation.TestData;
using OpenQA.Selenium.Support.PageObjects;
using Blinds.UIAutomation.CommonClasses;

namespace Blinds.UIAutomation.PageClasses
{
    public class HomePage : CommonMethods
    {

        [FindsBy(How = How.Id, Using = "gcc-inline-search")]
        public IWebElement SearchBox { get; set; }

        [FindsBy(How = How.Id, Using = "gcc-inline-search-submit")]
        public IWebElement SearchBoxSubmit { get; set; }

        public string HomePageId = "home-body";
        public string AutoCompleteSuggestions = "div.bdc-typeahead-dataset.bdc-typeahead-dataset-label";


        public HomePage(IWebDriver browser)
            : base(browser)
        {

        }

        public void LaunchHomePageUrl()
        {
            string url = TestProperties.Url;
            LaunchUrl(url);
            CloseOverlayIfExists();
            CloseSignonOverlayIfExists();
        }

        public bool IsHomePageLaunched()
        {
            CloseOverlayIfExists();
            if (Browser.Url.ToLower().Contains(TestProperties.Url.ToLower()))
            {
                if (Browser.FindElement(By.Id(HomePageId)).DoesExistsOnPage())
                {
                    Reporter.Pass("IsHomePageLaunched", "Home page launched succesfully");
                    return true;
                }
            }
            else
            {
                Reporter.Fail("IsHomePageLaunched", "Home page launch unsuccesful");
                return false;
            }
            return false;
        }

        public void SearchForaProduct(string productname)
        {
            try
            {
                if (SearchBox.DoesExistsOnPage())
                {
                    SearchBox.SendKeys(productname);
                    IList<IWebElement> autocompleteItems = Browser.FindElements(By.CssSelector(AutoCompleteSuggestions));
                    if (autocompleteItems.Count > 0)
                    {
                        autocompleteItems[0].Click();
                    }
                    else
                    {
                        SearchBoxSubmit.Click();
                    }
                    Reporter.Pass("SearchForaProduct", "Product search successful");
                    WaitForPageLoad();
                    WaitForJqueryAjax();
                }
                else
                {
                    Reporter.Fail("SearchForaProduct", "Search box not found");
                }
            }
            catch (WebDriverException e)
            {
                Reporter.Fail("SearchForaProduct", e.Message);
            }
        }
    }
}
