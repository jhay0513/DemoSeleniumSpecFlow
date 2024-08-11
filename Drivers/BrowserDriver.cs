using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using DemoSeleniumSpecFlow.Support;
using NUnit.Framework;

namespace DemoSeleniumSpecFlow.Drivers
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly Lazy<IWebDriver> _currentWebDriverLazy;
        private bool _isDisposed;

        public BrowserDriver()
        {
            _currentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver);
        }

        /// <summary>
        /// The Selenium IWebDriver instance
        /// </summary>
        public IWebDriver Current => _currentWebDriverLazy.Value;

        /// <summary>
        /// Creates the Selenium web driver (opens a browser)
        /// </summary>
        /// <returns></returns>
        private IWebDriver CreateWebDriver()
        {
            //We use the Chrome browser
            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            ChromeOptions options = new ChromeOptions();
            // Create instance for Chrome webdriver
            IWebDriver driver = new ChromeDriver(driverService, options);
            // Navigate to a website
            driver.Url = Util.GetAppSetting("Url");
            // Maximize window
            driver.Manage().Window.Maximize();
            // Implicit waits
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            return driver;
        }

        /// <summary>
        /// Disposes the Selenium web driver (closing the browser)
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentWebDriverLazy.IsValueCreated)
            {
                // close browser
                Current.Close();
                // destroy driver instance
                Current.Quit();
            }

            _isDisposed = true;
        }
    }
}
