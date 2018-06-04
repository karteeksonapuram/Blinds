using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Blinds.UIAutomation.TestData;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

namespace Blinds.UIAutomation.CommonClasses
{
    [Binding]
    public class TestInitialization
    {
        IWebDriver _driver;

        [BeforeScenario]
        public void BeforeScenario()
        {
            IWebDriver browser =  LaunchBrowser(TestProperties.Browser);
            Reporter.Browser = browser;
            Reporter.ScenarioName = ScenarioContext.Current.ScenarioInfo.Title;
            Reporter.InitalizeReporter();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Close();
            _driver.Quit();
            _driver.Dispose();
        }

        private IWebDriver LaunchBrowser(string browserName)
        {
            if (browserName.Equals("Firefox", StringComparison.CurrentCultureIgnoreCase))
            {
                _driver = new FirefoxDriver();
                _driver.Manage().Window.Maximize();
            }
            if (browserName.Equals("Chrome", StringComparison.CurrentCultureIgnoreCase))
            {
                _driver = new ChromeDriver();
                _driver.Manage().Window.Maximize();
            }
            ScenarioContext.Current.Set<IWebDriver>(_driver);
            return _driver;
        }
    }
}
