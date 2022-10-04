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

            // Отключение "Браузером управляет автоматизированное ПО"
            option.AddAdditionalChromeOption("useAutomationExtension", false);
            option.AddExcludedArgument("enable-automation");
            
            driver = new ChromeDriver(@"D:\testing\Maxima_Aqa\ApiTesting\chromedriver_win32", option);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var path = Utils.GetFilePathByFileName("testData.json");
            testData = JObject.Parse(File.ReadAllText(path));

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
            /*wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(
                By.XPath("//iframe[@title='reCAPTCHA']")));
            var check = wait.Until(e => e.FindElement(By.Id("recaptcha-anchor")));
            check.Click();

            driver.SwitchTo().ParentFrame();*/
            
            driver.Manage().Cookies.AddCookie(new Cookie("PHPSESSID", "4al1r6rinp8gmdq19rvlpjp2k3"));  
            driver.Manage().Cookies.AddCookie(new Cookie("token", "{\"expired\":\"2022-10-05+12:39:39\",\"token\":\"d695bd895da89a5f31735514b4b5cc9232e0c86c\",\"key\":\"OAuth\",\"refresh\":\"196c0b0fb2a97d998b8ea051636dee2008076bd0\",\"refreshExpired\":\"2022-12-03+12:39:39\"}"));
            driver.Manage().Cookies.AddCookie(new Cookie("_ym_uid", "1639659381772082907"));
            driver.Manage().Cookies.AddCookie(new Cookie("_ym_d", "1663948815"));
            driver.Manage().Cookies.AddCookie(new Cookie("slider", "6"));
            driver.Manage().Cookies.AddCookie(new Cookie("_ym_isad", "2"));
            driver.Manage().Cookies.AddCookie(new Cookie("YII_CSRF_TOKEN", "f6c3ce5ce28d64d0a9b5c95717c894a76f8dc101"));
            driver.Navigate().GoToUrl("https://old.kzn.opencity.pro/cabinet/");

        }

    }
}