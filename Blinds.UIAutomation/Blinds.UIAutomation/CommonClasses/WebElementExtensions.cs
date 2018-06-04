using OpenQA.Selenium;

namespace Blinds.UIAutomation.CommonClasses
{
    public static class WebElementExtensions
    {
        public static bool DoesExistsOnPage(this IWebElement webElement)
        {
            try
            {
                return (webElement.Displayed && webElement.Enabled);
            }
            catch (WebDriverException)
            {
                return false;
            }
        }
    }
}
