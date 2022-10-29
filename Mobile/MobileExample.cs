using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace Mobile
{
    public class MobileExample
    {
        /*
         * Разные расширения и разные устройтсва
         * Разные ОС
         * Обновление приложения
         * Проверка карманом  - нельзя не специально перевести деньги
         * Ориентация
         * Локализация
         * сеть 1. Сеть есть  2. Сеть есть, потом пропала 3. Нет сети 4. Низкий уровень сигнала
         * Батарея
         * Производительность
         * При разработке учитывать контрастность приложения
         */
        
        /*
         * > brew install node      # get node.js
> npm install -g appium  # get appium
> npm install wd         # get appium client
> appium &               # start appium
> node your-appium-test.js
 npm install appium-chromedriver --chromedriver_version=“61” 
 appium --chromedriver-executable /path/to/chromedriver
ANDROID_HOME
         */
        private string path = "/Users/administrator/maxima/example.apk";

        [Test]
        public void mobileExampleTesting()
        {
            var options = new AppiumOptions();
            options.App = path;
            options.PlatformName = "Android";
            options.DeviceName = "Pixel 2 API 27";
            //   options.BrowserName = "chrome";
           // AppiumDriver driver = new AndroidDriver(options); либо порт 4724
            AppiumDriver driver = new AndroidDriver(new Uri("http://127.0.0.1:4723/wd/hub"), options);
            
            
            var listElements = driver.FindElements(By.XPath("//*"));
            AppiumElement findElement = null;
            foreach (var element in listElements)
            {
             //   Console.WriteLine("element.Text " + element.Text);
            //    Console.WriteLine("element.Location " + element.Location);
            //   Console.WriteLine("element.TagName " + element.TagName);
            if (element.Text == "2")
            {
                findElement = element;
            }
            }
            findElement.Click();
            Thread.Sleep(3000);
        }

        [Test]
        public void webMobileExample()
        {
            var options = new AppiumOptions();
            options.BrowserName = "chrome";
            options.BrowserVersion = "61";
            options.PlatformName = "Android";
            options.DeviceName = "Pixel 2 API 27";
            AppiumDriver driver = new AndroidDriver(new Uri("http://127.0.0.1:4723/wd/hub"), options);
            driver.Url = "https://wikipedia.org/";
            Thread.Sleep(5000);
        }
        
        
        private ReadOnlyCollection<IWebElement> WaitElements(AppiumDriver driver, By selector)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            ReadOnlyCollection<IWebElement> results = null;

            try
            {
                wait.Until(driver =>
                {
                    try
                    {
                        var elements = driver.FindElements(selector);

                        if (elements.Any())
                        {
                            results = elements;
                            return true;
                        }
                    }
                    catch (StaleElementReferenceException)
                    {
                        // ignore
                    }

                    return false;
                });
            }
            catch (WebDriverTimeoutException)
            {
                throw new NoSuchElementException("Elements not found");
            }

            return results;
        }
    }
}