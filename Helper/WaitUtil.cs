using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using TeamsWindowsApp.Driver;

namespace TeamsWindowsApp.Helper
{
    public static class WaitUtil
    {
        public static IWebElement WaitForEnabled(this IWebElement element, int timeSpan = 10000)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            while (watch.Elapsed.Milliseconds < timeSpan)
            {
                if (element.Enabled)
                    return element;
            }

            throw new ElementNotInteractableException();
        }

        internal static Boolean WaitForElementEnabled(String value, String ElementType, WindowsDriver<WindowsElement> driver, int timeSpan = 10000)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            int i = 1000;
            while (i < timeSpan)
            {
                try
                {
                    System.Threading.Thread.Sleep(1000);
                    switch (ElementType.ToLower())
                    {
                        case "xpath":
                            if (driver.FindElementByXPath(value).Enabled)
                            {
                                return true;
                            }
                            break;
                        case "xname":
                            if (driver.FindElementByName(value).Enabled)
                            {
                                return true;
                            }
                            break;
                        case "autoid":
                            if (driver.FindElementByAccessibilityId(value).Enabled)
                            {
                                return true;
                            }
                            break;
                    }
                }
                catch
                {
                    i += 1000;
                }
                i += 1000;
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            Console.WriteLine("TIME: " + i.ToString());
            return false;
        }

        public static IWebElement WaitForVisible(this IWebElement element, int timeSpan = 10000)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            while (watch.Elapsed.Milliseconds < timeSpan)
            {
                if (element.Displayed)
                    return element;
            }

            throw new ElementNotVisibleException();
        }

        public static Boolean  WaitForVisible_bool(String xpath, IWebDriver driver,int timeSpan = 10000)
        {
            //Stopwatch watch = new Stopwatch();
            int i = 1000;
            while (i < timeSpan)
            {
                try
                {
                    System.Threading.Thread.Sleep(1000);
                    if (xpath.StartsWith("//"))
                    {
                        if (driver.FindElement(By.XPath(xpath)).Displayed)
                            return true;
                    }
                    else
                    {
                        if (driver.FindElement(By.Id(xpath)).Displayed)
                            return true;
                    }
                }
                 catch
                {
                    i += 1000;
                }
                i += 1000;
            }
            return false;
        }

        public static Boolean WaitForVisible_bool(string property, string propertyValue, WindowsDriver<WindowsElement> driver, int timeSpan = 10000)
        {
            //Stopwatch watch = new Stopwatch();
            int i = 1000;
            while (i < timeSpan)
            {
                try
                {
                    switch (property.ToUpper())
                    {
                        case "ID":
                            if (driver.FindElementByAccessibilityId((propertyValue)).Displayed)
                            {
                                return true;
                            }
                            break;
                        case "NAME":
                            if (driver.FindElementByName(propertyValue).Displayed)
                            {
                                return true;
                            }
                            break;
                        default:
                            break;

                    }
                    System.Threading.Thread.Sleep(1000);
                }
                catch
                {
                    i += 1000;
                }
                i += 1000;
            }
            return false;
        }

        public static IWebElement WaitForText(this IWebElement element, int timeSpan = 10000)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            while (watch.Elapsed.Milliseconds < timeSpan)
            {
                if (element.Text.Length > 0)
                    return element;
            }

            throw new ElementNotVisibleException();
        }

        public static IWebElement WaitForText(this IWebElement element, string text, int timeSpan = 10000)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            while (watch.Elapsed.Seconds < timeSpan)
            {
                if (element.Text == text)
                    return element;
            }

            throw new NoSuchElementException();
        }

        public static Boolean WaitForVisible_Bool(this IWebElement element, int timeSpan = 10000)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            while (watch.Elapsed.Milliseconds < timeSpan)
            {
                if (element.Displayed)
                    return true;
            }

            return false;
        }

        internal static Boolean WaitUntilElementAvailability(string xPath, WindowsDriver<WindowsElement> driver, int timeSpan = 10000)
        {
            int i = 1000;
            while (i < timeSpan)
            {
                try
                {
                    System.Threading.Thread.Sleep(1000);
                    if (driver.FindElementByXPath(xPath).Displayed)
                    {
                        i += 1000;
                        continue;
                    }
                        
                }
                catch
                {
                    i += 1000;
                    return true;
                }
                i += 1000;
            }
            return true;
        }



        public static Boolean WaitForElementAvailability(String value, WindowsDriver<WindowsElement> driver, int timeSpan = 10000)
        {
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            //while (watch.Elapsed.Milliseconds < timeSpan)
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            int i = 1000;
            while (i < timeSpan)
            {
                try
                {
                    System.Threading.Thread.Sleep(1000);
                    if (value.StartsWith("//"))
                    {
                        if (driver.FindElementByXPath(value).Displayed)
                            return true;
                    }
                    else
                    {
                        if (driver.FindElementByAccessibilityId(value).Displayed)
                            return true;
                    }
                }
                catch
                {
                    i += 1000;
                }
                i += 1000;
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            return false;
        }

        public static void WaitForElementAvailabilityByScrollUp(String TargetXPath, String ScrollTarget, WindowsDriver<WindowsElement> driver, int loopCount = 5)
        {
            int i = 0;
            int TargetCount = driver.FindElementsByXPath(TargetXPath).Count;
            var element = driver.FindElementsByXPath(ScrollTarget)[0];
            while (i < loopCount)
            {
                try
                {
                    if (TargetCount > 0)
                    {
                        break;
                    }
                    else
                    {
                        element.Click();
                        element.SendKeys(Keys.PageUp);
                        i++;
                        continue;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                i++;
            }
        }

        public static void WaitUntilFileExist(String sFullFilePath)
        {
            int i = 0;
            while (!File.Exists(sFullFilePath) && i < 60) //limit the time to whait to 30 sec
            {
                System.Threading.Thread.Sleep(1000);
                i++;
            }
        }
    }
}
