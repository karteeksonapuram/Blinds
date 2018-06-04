using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Blinds.UIAutomation.PageClasses;

namespace Blinds.UIAutomation.CommonClasses
{
    public abstract class TestStepsInitialization
    {
        protected IWebDriver Browser;
        protected HomePage HomePage;
        protected SearchResultsPage SearchResultsPage;
        protected ProductDescriptionPage ProductDescriptionPage;

        protected TestStepsInitialization()
        {
            ScenarioContext.Current.TryGetValue<IWebDriver>(out Browser);
            ScenarioContext.Current.TryGetValue<HomePage>(out HomePage);
            ScenarioContext.Current.TryGetValue<SearchResultsPage>(out SearchResultsPage);
            ScenarioContext.Current.TryGetValue<ProductDescriptionPage>(out ProductDescriptionPage);
        }
    }
}
