using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.pages
{
    public class RegistrationPage
    {
        private WebDriver _driver;
        private WebDriverWait _wait;

        public RegistrationPage(WebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        public void SetEmail(string email)
        {
            _wait.Until(e => e.FindElement(By.Name("email"))).SendKeys(email);
        }

        public void SubmitRegistration()
        {
           var element =  _driver.FindElement(By.XPath(Locators.submitButton));
           element.Click();
        }
    }
}