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
            driver.Manage().Cookies.AddCookie(new Cookie("PHPSESSID", "g1q629njfnm4sr1a63n0l2q075"));  
            driver.Manage().Cookies.AddCookie(new Cookie("token", "{\"expired\":\"2022-10-08+18:46:35\",\"token\":\"8df3f496783a08d2e6634f3d8c845d80b2c4e27d\",\"key\":\"OAuth\",\"refresh\":\"688b042cd82de8cf6ed4d624a63e57a8d05e58d7\",\"refreshExpired\":\"2022-12-06+18:46:35\"}"));
            driver.Manage().Cookies.AddCookie(new Cookie("_ym_uid", "1639659381772082907"));
            driver.Manage().Cookies.AddCookie(new Cookie("_ym_d", "1663948815"));
            driver.Manage().Cookies.AddCookie(new Cookie("slider", "6"));
            driver.Manage().Cookies.AddCookie(new Cookie("_ym_isad", "2"));
            driver.Manage().Cookies.AddCookie(new Cookie("YII_CSRF_TOKEN", "f6c3ce5ce28d64d0a9b5c95717c894a76f8dc101"));
            driver.Navigate().GoToUrl("https://old.kzn.opencity.pro/cabinet/");

        }

    }
}