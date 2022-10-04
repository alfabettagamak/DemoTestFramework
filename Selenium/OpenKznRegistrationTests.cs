using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium.pages;
using SeleniumExtras.WaitHelpers;

namespace Selenium
{

    [TestFixture]
    public class OpenKznRegistrationTests : TestBase
    {

        private bool isNeedAuth = true;


        [SetUp]
        public void OneSetup()
        {    
            driver.Navigate().GoToUrl("https://old.kzn.opencity.pro/");
        }
        
        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
        

        [Test]
        public void OpenKznRegistrationTesting()
        {
            var page = new MainPage(driver, wait).GetRegistrationPage();
            page.SetEmail(Utils.GetRandomEmail());
            page.SubmitRegistration();
            
            bool isDisplayed = wait.Until( e => e.FindElement(By.XPath("//h3[text()='Вы зарегистрированы!']"))
                .Displayed);
            Assert.IsTrue(isDisplayed);
        }

    }
}