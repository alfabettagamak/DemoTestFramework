﻿using System;
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


        [Test]
        public void allertTesting()
        {
            executor.ExecuteScript("setTimeout(function() {alert('FROM C#!')}, 5000)");
            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            var elements = driver.FindElements(By.XPath("//ul"));
            foreach (var element in elements)
            {
                Console.WriteLine(element.Text);
            }
        }

        [Test]
        public void allertConfirmTesting()
        {
            executor.ExecuteScript("document.check = confirm('YES OR NOT')");
            driver.SwitchTo().Alert().Dismiss();
            bool answer = (bool) executor.ExecuteScript("return document.check");
            Assert.False(answer);
        }
        
        
        [Test]
        public void allertPromtTesting()
        {
            executor.ExecuteScript("document.check = prompt('Input text')");
            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().SendKeys("SOME VALUE!");
            driver.SwitchTo().Alert().Accept();
            driver.SwitchTo().Frame("");
            string answer = (string) executor.ExecuteScript("return document.check");
            Console.WriteLine(answer);
        }


        [Test]
        public void newTabSwitchTesting()
        {
            driver.SwitchTo().NewWindow(WindowType.Tab);
            driver.Navigate().GoToUrl("https://google.com");
            driver.SwitchTo().NewWindow(WindowType.Tab);
            driver.Navigate().GoToUrl("https://ya.ru");
            driver.SwitchTo().NewWindow(WindowType.Window);
            driver.Url = "https://ngs.ru";
            Thread.Sleep(1000);
            var tabs = driver.WindowHandles;
            foreach (var tab in tabs)
            {
                driver.SwitchTo().Window(tab);
                Thread.Sleep(1000);
                if (driver.Url != "https://www.google.com/") driver.Close();
            }
            Thread.Sleep(3000);
        }

        [Test]
        public void cookieTesting()
        {
            var cookies = driver.Manage().Cookies.AllCookies;
            foreach (var cookie in cookies)
            {
                Console.WriteLine(cookie);
            }
            
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Url = "https://old.kzn.opencity.pro/cabinet/myprofile";
            Thread.Sleep(6000);

            string cookiesJS = (string)executor.ExecuteScript("return document.cookie");
            Console.WriteLine("!!! " + cookiesJS);
        }

        [Test]
        public void jSExampleTesting()
        {
            IWebElement element = driver.FindElement(By.XPath("//div[@data-ui='selected']"));
            executor.ExecuteScript("arguments[0].innerText = '666666666666666666664'", element);
            Thread.Sleep(10000);
        }
        
        
        [Test]
        public void jSExampleParentTesting()
        {
            IWebElement element = driver.FindElement(By.XPath("//div[@data-ui='selected']"));
            var elementParent = executor.ExecuteScript("return arguments[0].parentElement", element);
            Console.WriteLine(elementParent.GetType());
            Thread.Sleep(10000);
        }
        
    }
}