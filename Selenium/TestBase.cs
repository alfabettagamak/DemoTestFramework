using System;
using System.IO;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Selenium
{
    public class TestBase
    {
        protected WebDriver driver;
        protected WebDriverWait wait;
        protected JObject testData;
        
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var option = new ChromeOptions();
            option.AddArguments("user-agent=Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
            //option.AddArgument("--window-position=-32000,-32000");
            
            /*
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;*/
            
            driver = new ChromeDriver(@"D:\testing\Maxima_Aqa\ApiTesting\chromedriver_win32", option);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var path = Utils.GetFilePathByFileName("testData.json");
            testData = JObject.Parse(File.ReadAllText(path));

        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        [OneTimeTearDown]
        public void OneTearDown()
        {
            driver.Quit();
        }
        
        protected void Authorization(string login,  string password)
        {
            driver.FindElement(By.XPath("//a[@data-ui='auth']")).Click();
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(password);
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(
                By.XPath("//iframe[@title='reCAPTCHA']")));
            var check = wait.Until(e => e.FindElement(By.Id("recaptcha-anchor")));
            check.Click();

            driver.SwitchTo().ParentFrame();
            wait.Until(e => e.FindElement(By.XPath(Locators.submitButton))).Click();
        }

    }
}