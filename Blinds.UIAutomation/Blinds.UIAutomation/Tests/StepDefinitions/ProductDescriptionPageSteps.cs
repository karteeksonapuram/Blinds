using Blinds.UIAutomation.CommonClasses;
using TechTalk.SpecFlow;

namespace Blinds.UIAutomation.Tests.StepDefinitions
{
    [Binding]
    public class ProductDescriptionPageSteps : TestStepsInitialization
    {
        [Then(@"I should land on product description page")]
        public void ThenIShouldLandOnProductDescriptionPage()
        {
            ProductDescriptionPage.VerifyProductDescriptionPageofSelectedProduct();
        }
    }
}
