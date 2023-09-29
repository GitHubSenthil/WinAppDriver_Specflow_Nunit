using System;
using System.IO;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;


namespace TeamsWindowsApp.Driver
{
    public class BrowserSeleniumDriverFactory
    {
        private readonly ConfigurationDriver _configurationDriver;
        
        public BrowserSeleniumDriverFactory(ConfigurationDriver configurationDriver)
        {
            _configurationDriver = configurationDriver;
        }

        public IWebDriver GetForBrowser(string browserId)
        {
            string lowerBrowserId = browserId.ToUpper();
            switch (lowerBrowserId)
            {
                case "IE": return GetInternetExplorerDriver();
                case "CHROME": return GetChromeDriver();
                case "FIREFOX": return GetFirefoxDriver();
                case "EDGE": return GetEdgeDriver();
                case string browser: throw new NotSupportedException($"{browser} is not a supported browser");
                default: throw new NotSupportedException("not supported browser: <null>");
            }
        }

        private IWebDriver GetFirefoxDriver()
        {
            return new FirefoxDriver()
            {
                Url = _configurationDriver.baseUrl,

            };
        }

        private IWebDriver GetEdgeDriver()
        {


            EdgeOptions edgeOptions = new EdgeOptions();
            edgeOptions.UseChromium = true;
            edgeOptions.BinaryLocation = _configurationDriver.Configuration["EdgeBinaryPath"];
            //edgeOptions.AddArgument("-inprivate"); // Required for non-SSO login - Not enabled in Prod or QA

            //edgeOptions.AddAdditionalCapability("UseChromium", true);
            //edgeOptions.AddAdditionalCapability("BinaryLocation", _configurationDriver.Configuration["EdgeBinaryPath"]);
            //edgeOptions.UseInPrivateBrowsing = true;
            //edgeOptions.AddAdditionalCapability("InPrivate", true);
            //edgeOptions.AddAdditionalCapability("UseInPrivateBrowsing", true);
            //edgeOptions.AddArgument("-inprivate");
            //edgeOptions.AddArgument("headless");
            //edgeOptions.AddArgument("disable-gpu");

            string path = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location).Split("bin")[0];
            try
            {
                var msedgedriverDir = path + _configurationDriver.Configuration["EdgeDriverPath"];
                //var driver = new EdgeDriver(msedgedriverDir, edgeOptions);
                var driver =
                    new EdgeDriver(msedgedriverDir, edgeOptions)
                    {
                        Url = _configurationDriver.baseUrl
                    };
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                driver.Manage().Window.Maximize();
                return driver;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        private IWebDriver GetChromeDriver()
        {
            return new ChromeDriver()
            {
                Url = _configurationDriver.baseUrl
            };
        }

        private IWebDriver GetInternetExplorerDriver()
        {
            var internetExplorerOptions = new InternetExplorerOptions
            {
                IgnoreZoomLevel = true,


            };
            return new InternetExplorerDriver()
            {
                Url = _configurationDriver.baseUrl,


            };
        }
    }
}
