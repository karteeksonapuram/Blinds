using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.IO;

namespace Blinds.UIAutomation.CommonClasses
{
    public class Reporter
    {
        public static IWebDriver Browser;
        public static string ScenarioName;
        
        private static readonly string ResultDirectory = Environment.CurrentDirectory;
        static string _screenshotDirectory = "./ScreenShots";

        public static void InitalizeReporter()
        {
            if (!File.Exists(ResultDirectory))
            {
                Directory.CreateDirectory(ResultDirectory);
            }
            if (!File.Exists(_screenshotDirectory))
            {
                Directory.CreateDirectory(_screenshotDirectory);
            }
        }
        /// <summary>
        /// Updates specflow report with the stepname and with the screenshot
        /// </summary>
        /// <param name="stepname">stepname</param>
        /// <param name="description">description</param>
        public static void Pass(string stepname, string description)
        {
            string screenShot = "/" + ScenarioName + DateTime.Now.ToString("ddMMyyyyhhmmssffff") + ".png";
            string screenshotName = _screenshotDirectory + screenShot;
            TakeScreenshot(screenshotName);
            ConsoleWriter(stepname, screenshotName);
        }

        /// <summary>
        /// Updates specflow report with the stepname and with the screenshot and fails the test
        /// </summary>
        /// <param name="stepname">stepname</param>
        /// <param name="description">description</param>
        public static void Fail(string stepname, string description)
        {
            string screenShot = "/" + ScenarioName + DateTime.Now.ToString("ddMMyyyyhhmmssffff") + ".png";
            string screenshotName = _screenshotDirectory + screenShot;
            TakeScreenshot(screenshotName);
            ConsoleWriter(stepname, screenShot);
            Assert.Fail(stepname + " :" + description);
        }
        /// <summary>
        /// This method captured the screenshot of the current browser
        /// </summary>
        /// <param name="screenshotPath">screenshot path</param>
        private static void TakeScreenshot(string screenshotPath)
        {
            try
            {
                if (Browser != null)
                {
                    var ss = ((ITakesScreenshot)Browser).GetScreenshot();

                    lock (ss)
                    {
                        ss.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
                    }
                }

            }
            catch (Exception exception)
            {
                Assert.Fail("Screenshot exception: "+ exception.Message);
            }
        }
        /// <summary>
        /// Consolewriter updates the stepdefinition in reporttemplate.cshtml
        /// </summary>
        /// <param name="stepdescription">step description</param>
        /// <param name="imageUrl">imageurl</param>
        private static void ConsoleWriter(string stepdescription, string imageUrl)
        {
            string writer = "- {STEPDESCRIPTION} [{IMAGEURL}]";
            writer = writer.Replace("{STEPDESCRIPTION}", stepdescription);
            writer = writer.Replace("{IMAGEURL}", imageUrl);
            Console.WriteLine(writer);
        }

    }
}
