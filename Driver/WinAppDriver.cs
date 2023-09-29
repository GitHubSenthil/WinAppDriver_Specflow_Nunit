using System;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using TeamsWindowsApp.Helper;
using TeamsWindowsApp.Pages;
using TechTalk.SpecFlow.Infrastructure;

namespace TeamsWindowsApp.Driver
{
    public class WinAppDriver : IDisposable
    {
        
        private readonly ConfigurationDriver _configurationDriver;
        private readonly Lazy<WindowsDriver<WindowsElement>> _windowsDriverLazy= null;
        private bool _isDisposed;
        

        public WinAppDriver(ConfigurationDriver configurationDriver)
        {
            _configurationDriver = configurationDriver;
            _windowsDriverLazy = new Lazy<WindowsDriver<WindowsElement>>(GetCurrentDriver);
        }

        public WindowsDriver<WindowsElement> Current => _windowsDriverLazy.Value;

        private WindowsDriver<WindowsElement> GetCurrentDriver()
        {
            if (CommonUtil.checkProcessRunning("Teams"))
            {
                //TeamsLogin.QuitTeamsApp();
                CommonUtil.KillProcess("Teams");
                System.Threading.Thread.Sleep(5000);
            }
            string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var appiumOptions = new AppiumOptions();
            //Step the Environment from Configuration File
            string sEnvironment = _configurationDriver.Configuration["Env"];
            if (sEnvironment.Equals("RND"))
            {
                appiumOptions.AddAdditionalCapability("app", userProfilePath + _configurationDriver.Configuration[sEnvironment + "_TeamsAppPath"]);
            }
            else
            {
                appiumOptions.AddAdditionalCapability("app", _configurationDriver.Configuration[sEnvironment + "_TeamsAppPath"]);
            }
            appiumOptions.AddAdditionalCapability("platformName", "Windows");
            appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
            appiumOptions.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appiumOptions.AddAdditionalCapability("ms:experimental-webdriver", false);
            appiumOptions.AddAdditionalCapability("fullReset", true);
            string appSetting = _configurationDriver.Configuration["winAppUri"];
            var remoteAddress = new Uri(appSetting);
            WindowsDriver<WindowsElement> driver = null;
            try
            {
                driver = new WindowsDriver<WindowsElement>(remoteAddress, appiumOptions);
            }
            catch (Exception e)
            {
                Console.Write("Start Windows Driver, an error occurred:" + e.Message);
                Console.Write("Strack Trace is here: " + e.StackTrace);
                if (CommonUtil.checkProcessRunning("Teams"))
                {
                    CommonUtil.KillProcess("Teams");
                    System.Threading.Thread.Sleep(5000);
                }
                driver = new WindowsDriver<WindowsElement>(remoteAddress, appiumOptions);
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            return driver;
        }

        public void clearDriver()
        {
            if (!_windowsDriverLazy.IsValueCreated)
            {
                //_windowsDriverLazy.Value.Quit();
                _windowsDriverLazy.Value.Dispose();
                _isDisposed = true;
            }

        }
         
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (!_windowsDriverLazy.IsValueCreated)
            {
                return;
            }

            _windowsDriverLazy.Value.Quit();
            //_windowsDriverLazy.Value.Dispose();
            _isDisposed = true;
        }
    }
}
