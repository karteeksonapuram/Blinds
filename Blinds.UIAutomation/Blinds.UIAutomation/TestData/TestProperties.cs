namespace Blinds.UIAutomation.TestData
{
    public class TestProperties
    {
        public static string Url
        {
            get
            {
                return TestSettings.Default.url;
            }
        }

        public static string Browser
        {
            get
            {
                return TestSettings.Default.browser;
            }
        }
    }
}
