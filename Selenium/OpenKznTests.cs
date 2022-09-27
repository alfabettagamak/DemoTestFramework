using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Selenium
{

    [TestFixture]
    public class OpenKznTests : TestBase
    {

        private bool isNeedAuth = true;
        private JObject testUser;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //driver = new ChromeDriver();
            testUser = (JObject) testData["testUser1"];
            driver.Navigate().GoToUrl("https://old.kzn.opencity.pro/");
            Authorization(testUser["login"].ToString(), testUser["password"].ToString());
        }
        

        [Test]
        public void OpenKznAuthTesting()
        {
            Thread.Sleep(6000);
            Assert.True(true);
        }
        
        [Test]
        public void OpenKznCheckMyProfileTesting()
        {
            Assert.AreEqual("https://old.kzn.opencity.pro/#", driver.Url);
            Assert.Pass();
        }

        [Test]
        public void ScreenTesting()
        {
            Screenshot screenshot = driver.GetScreenshot();
            screenshot.SaveAsFile(@"D:\testing\Maxima_Aqa\ApiTesting\DemoTestFramework\Selenium\screens\exaple.png");

            IWebElement element = driver.FindElement(By.XPath(Locators.submitButton));
            Screenshot screenElement = ((ITakesScreenshot) element).GetScreenshot();
            screenElement.SaveAsFile(@"D:\testing\Maxima_Aqa\ApiTesting\DemoTestFramework\Selenium\screens\example!.png");
            Thread.Sleep(6000);
        }

    }
}