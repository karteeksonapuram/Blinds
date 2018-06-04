using Blinds.UIAutomation.PageClasses;
using TechTalk.SpecFlow;
using Blinds.UIAutomation.CommonClasses;

namespace Blinds.UIAutomation.Tests.StepDefinitions
{
    [Binding]
    public class HomePageSteps : TestStepsInitialization
    {
        [Given(@"I launch blinds website")]
        public void GivenILaunchBlindsWebsite()
        {
            HomePage = new HomePage(Browser);
            ScenarioContext.Current.Set<HomePage>(HomePage);
            HomePage.LaunchHomePageUrl();
        }

        [Given(@"I should see blinds home page")]
        public void GivenIShouldSeeBlindsHomePage()
        {
            HomePage.IsHomePageLaunched();
        }
        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string productname)
        {
            HomePage.SearchForaProduct(productname);
            SearchResultsPage = new SearchResultsPage(Browser);
            ScenarioContext.Current.Set<SearchResultsPage>(SearchResultsPage);
        }
    }
}
