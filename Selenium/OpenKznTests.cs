using System;
using System.Drawing;
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
            testUser = (JObject) testData["testUser2"];
            driver.Navigate().GoToUrl("https://old.kzn.opencity.pro/");
            Authorization(testUser["login"].ToString(), testUser["password"].ToString());
        }
        
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Close();
        }

        [Test]
        public void OpenKznAuthTesting()
        {
            var element = driver.FindElement(By.XPath("//div[@class='username']/span"));
            Assert.AreEqual(testUser["login"].ToString(), element.Text);
        }
        
        [Test]
        public void OpenKznCheckMyProfileTesting()
        { 
            driver.FindElement(By.XPath("//a[@class='btn_edit_profile itemMenu']")).Click();
            Assert.AreEqual("https://old.kzn.opencity.pro/cabinet/myprofile", driver.Url);
 
        }


        [Test]
        [TestCase(800, 600)]
        [TestCase( 300, 600)]
        [TestCase(740, 1280)]
        public void CheckSizeWindowsJSTesting(int height, int weight)
        {
            driver.Manage().Window.Size = new Size(height, weight);
            IJavaScriptExecutor executor = driver;
            driver.Navigate().GoToUrl("https://old.kzn.opencity.pro/cabinet/myuk");
            Boolean heightScroll =
                (Boolean) executor.ExecuteScript("return document.documentElement.scrollHeight > document.documentElement.clientHeight");
            Boolean weightScroll =
                (Boolean) executor.ExecuteScript(
                    "return document.documentElement.scrollWidth > document.documentElement.clientWidth");
            Assert.False(weightScroll);
            Assert.True(heightScroll);
        }

        [Test]
        public void jsExampleTesting()
        {
            IJavaScriptExecutor executor = driver;
           // executor.ExecuteScript("document.body.innerHTML = '<h1>OLOLOOLOLO!!!!!</h1>'");
           
           /*
           executor.ExecuteScript("document.body.innerHTML = '<h1>' + arguments[0] + arguments[1] + 'OLOLOOLOLO!!!!!</h1>'",
               "####", 47);
               */


            var element = driver.FindElement(By.XPath("//div[@class='username']/span"));
            executor.ExecuteScript("arguments[0].textContent = '7777777777'",
                element);
            Thread.Sleep(10000);
        }
        

        [Test]
        public void ScreenTesting()
        {
            Screenshot screenshot = driver.GetScreenshot();
            screenshot.SaveAsFile(@"D:\testing\Maxima_Aqa\ApiTesting\DemoTestFramework\Selenium\screens\exaple.png");

            IWebElement element = driver.FindElement(By.XPath(Locators.submitButton));
            Screenshot screenElement = ((ITakesScreenshot) element).GetScreenshot();
            screenElement.SaveAsFile(@"D:\testing\Maxima_Aqa\ApiTesting\DemoTestFramework\Selenium\screens\example1.png");
            Thread.Sleep(6000);
        }

    }
}