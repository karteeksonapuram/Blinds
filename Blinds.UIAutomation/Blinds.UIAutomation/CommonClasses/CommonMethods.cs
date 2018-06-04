using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace Blinds.UIAutomation.CommonClasses
{
    public abstract class CommonMethods
    {
        protected IWebDriver Browser;
        protected static string OfferId;
        
        [FindsBy(How = How.CssSelector, Using = "div.acsClassicInner")]
        public IWebElement Overlay { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.frame-container")]
        public IWebElement SignonOverlay { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a.acsInviteButton.acsDeclineButton")]
        public IWebElement DecilneButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.layer-wiziwig")]
        public IWebElement SigoncloseButton { get; set; }

        
        protected CommonMethods(IWebDriver browser)
        {
            try
            {
                Browser = browser;
                PageFactory.InitElements(browser, this);
            }
            catch (WebDriverException ex)
            {
                throw ex;
            }
           
        }
        protected string GetCurrentUrl()
        {
            string url = Browser.Url;
            return String.IsNullOrWhiteSpace(url) ? string.Empty : url;
        }
        /// <summary>
        /// Method to launch given URL
        /// </summary>
        /// <param name="url">URL</param>
        protected void LaunchUrl(string url)
        {
            if(url!=null)
            {
                Browser.Navigate().GoToUrl(url);
            }
        }
        /// <summary>
        /// wait till the page loads
        /// </summary>
        protected void WaitForPageLoad()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Browser;
            WebDriverWait wait = new WebDriverWait(Browser, new TimeSpan(0, 1, 0));
            wait.Until(driver => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }
        protected void CloseOverlayIfExists()
        {
            if(Overlay.DoesExistsOnPage())
            {
                DecilneButton.Click();
            }
        }

        protected void CloseSignonOverlayIfExists()
        {
            if (SignonOverlay.DoesExistsOnPage())
            {
                SigoncloseButton.Click();
            }
        }
        protected void WaitUntilElementLoads(IWebElement webElement, int waitTimeInSeconds)
        {
            try
            {
                new WebDriverWait(Browser, TimeSpan.FromSeconds(waitTimeInSeconds)).Until(webDriver =>
                    webElement.Displayed);
            }
            catch (WebDriverException e)
            {
               Reporter.Fail("Wait For WebElement " + webElement.Text + " To Be Present", "Webelement is not found : " + e.InnerException);
            }
        }
        /// <summary>
        /// Wait for ajax call
        /// </summary>
        public void WaitForJqueryAjax()
        {
            int waitTime = 90;
            while (waitTime > 0)
            {
                Thread.Sleep(1000);
                var javaScriptExecutor = Browser as IJavaScriptExecutor;
                var jquery = javaScriptExecutor != null && (bool)javaScriptExecutor.ExecuteScript("return window.jQuery == undefined");
                if (jquery)
                {
                    break;
                }
                var scriptExecutor = Browser as IJavaScriptExecutor;
                var ajaxIsComplete = scriptExecutor != null && (bool)scriptExecutor.ExecuteScript("return window.jQuery.active == 0");
                if (ajaxIsComplete)
                {
                    break;
                }
                waitTime--;
            }
        }
    }
}