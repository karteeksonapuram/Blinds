# Blinds
###########################################################################################
prerequisite | Nuget packages
###########################################################################################
  INSTALL BELOW NUGET PACKAGES IN PACKAGE MANAGER CONSOLE
  
  Install-Package SpecFlow -Version 2.1.0
  Install-Package SpecRun.SpecFlow -Version 1.5.2
  Install-Package SpecRun.Runner -Version 1.5.2
  Install-Package Selenium.WebDriver -Version 3.12.1
  Install-Package Selenium.WebDriver.ChromeDriver -Version 2.36.0
  Install-Package Selenium.Support -Version 3.12.1
  Install-Package Selenium.Firefox.WebDriver -Version 0.19.1
  Install-Package Newtonsoft.Json -Version 4.5.9
  if VS2017, install https://marketplace.visualstudio.com/items?itemName=TechTalkSpecFlowTeam.SpecFlowforVisualStudio2017

  ##############################
  - Specflow tests are located under Tests folder
  - After the test run, customized specflow test report including screenshots can be located under Blinds.UIAutomation\TestResults.
