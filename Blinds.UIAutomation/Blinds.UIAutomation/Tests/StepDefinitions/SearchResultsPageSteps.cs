using TechTalk.SpecFlow;
using Blinds.UIAutomation.CommonClasses;

namespace Blinds.UIAutomation.Tests.StepDefinitions
{
    [Binding]
    public class SearchResultsPageSteps : TestStepsInitialization
    {
        private string _productIdToSelect = string.Empty;
        [Then(@"I should see results page with '(.*)'")]
        public void ThenIShouldSeeResultsPageWith(string searchString)
        {
            SearchResultsPage.VerifyValidSearchResultsPageHasBeenDisplayed(searchString);
        }

        [Then(@"I should see horizontal filtering options with ""(.*)""")]
        public void ThenIShouldSeeHorizontalFilteringOptionsWith(string filterningOptions)
        {
            SearchResultsPage.VerifyVerticalFilterOptionsHasBeenDisplayed(filterningOptions);
        }


        [Then(@"I should see the lowest priced product first")]
        public void ThenIShouldSeeTheLowestPricedProductFirst()
        {
            _productIdToSelect = SearchResultsPage.VerifyLowPriccedProductDisplayedOnTheTop();
        }


        [Then(@"I should see sort options")]
        public void ThenIShouldSeeSortOptions()
        {
            SearchResultsPage.VerifySortbyOptions();
        }

        [When(@"I select '(.*)' option")]
        public void WhenISelectOption(string sortOptionType)
        {
            SearchResultsPage.SelectSortByOption(sortOptionType);
        }

        [When(@"I click shop now button")]
        public void WhenIClickShopNowButton()
        {
            SearchResultsPage.AddProductTocart(_productIdToSelect);
        }

    }
}
