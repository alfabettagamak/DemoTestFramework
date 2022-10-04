using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.pages
{
    public class MainPage
    {
        private WebDriver _driver;
        private WebDriverWait _wait;

        public MainPage(WebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        public RegistrationPage GetRegistrationPage()
        {
            IWebElement registration = _wait.Until(e => e.FindElement(By.XPath("//a[@data-ui='registration']")));
            registration.Click();
            return new RegistrationPage(_driver, _wait);
        }
    }
}