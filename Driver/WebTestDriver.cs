using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace TeamsWindowsApp.Driver
{
    public class WebTestDriver : IDisposable
    {
        private readonly BrowserSeleniumDriverFactory _browserSeleniumDriverFactory;
        private readonly Lazy<IWebDriver> _currentWebDriverLazy;
        private readonly Lazy<WebDriverWait> _waitLazy;
        private readonly TimeSpan _waitDuration = TimeSpan.FromSeconds(10);
        private bool _isDisposed;
        private readonly ConfigurationDriver _configurationDriver;

        public WebTestDriver(BrowserSeleniumDriverFactory browserSeleniumDriverFactory, ConfigurationDriver configurationDriver)
        {
            _browserSeleniumDriverFactory = browserSeleniumDriverFactory;
            _currentWebDriverLazy = new Lazy<IWebDriver>(GetWebDriver);
            _waitLazy = new Lazy<WebDriverWait>(GetWebDriverWait);
            _configurationDriver = configurationDriver;
        }

        public IWebDriver Current => _currentWebDriverLazy.Value;

        public WebDriverWait Wait => _waitLazy.Value;

        private WebDriverWait GetWebDriverWait()
        {
            return new WebDriverWait(Current, _waitDuration);
        }

        private IWebDriver GetWebDriver()
        {
            string testBrowserId = _configurationDriver.Configuration["browser"];
            return _browserSeleniumDriverFactory.GetForBrowser(testBrowserId);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentWebDriverLazy.IsValueCreated)
            {
                Current.Quit();
            }

            _isDisposed = true;
        }
    }
}
