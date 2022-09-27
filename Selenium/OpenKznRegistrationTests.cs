using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
        

        [Test]
        public void OpenKznRegistrationTesting()
        {
            IWebElement registration = driver.FindElement(By.XPath("//a[@data-ui='registration']"));
            registration.Click();

            string email = Utils.GetRandomEmail();
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.CssSelector(Locators.submitButton)).Click();

            bool isDisplayed = wait.Until( e => e.FindElement(By.XPath("//h3[text()='Вы зарегистрированы!']"))
                .Displayed);
            Assert.IsTrue(isDisplayed);
        }

    }
}