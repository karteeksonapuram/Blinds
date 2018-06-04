using Blinds.UIAutomation.CommonClasses;
using OpenQA.Selenium;

namespace Blinds.UIAutomation.PageClasses
{
    public class ProductDescriptionPage : CommonMethods
    {
        public ProductDescriptionPage(IWebDriver browser)
            : base(browser)
        {

        }

        public void VerifyProductDescriptionPageofSelectedProduct()
        {
            CloseOverlayIfExists();
            if(GetCurrentUrl().Contains(OfferId))
            {
                Reporter.Pass("VerifyProductDescriptionPageofSelectedProduct", OfferId+" :landed on description page");
            }
            else
            {
                Reporter.Fail("VerifyProductDescriptionPageofSelectedProduct", OfferId + " :failed to land on description pagee");
            }
        }
    }
}
