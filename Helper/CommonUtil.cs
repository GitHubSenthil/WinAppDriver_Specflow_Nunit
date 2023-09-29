using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using TeamsWindowsApp.Driver;
using Newtonsoft.Json;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using Microsoft.Extensions.Configuration;

namespace TeamsWindowsApp.Helper
{
    /// <summary>
    /// Generic helper class
    /// </summary>
    public class CommonUtil
    {
        
        public static Dictionary<string, string> _languageMap;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="processName"></param>
        /// 
       
        public static void KillProcess(string processName)
        {
            Process[] existProcess = Process.GetProcessesByName(processName);
            if (existProcess.Length > 0)
            {
                existProcess[0].Kill();
            }
        }

        public static Boolean checkProcessRunning(string processName)
        {
            Process[] existProcess = Process.GetProcessesByName(processName);
            return existProcess.Length > 0;
        }

        public static IntPtr GetMainWindowHandle(string processName)
        {
            //var existProcessList = null;
            //Process[] processCollection = Process.GetProcesses();
            //Console.WriteLine("=========@@@@@@@@@@@@@@@@@@========");
            //foreach (Process p in processCollection)
            //{
            //    if (p.ProcessName == processName)
            //    {
            //        Console.WriteLine(p.ProcessName);
            //        Console.WriteLine(p.BasePriority);
            //    }
            //}
            IntPtr mainWindowHandle = IntPtr.Zero;
            for (var i = 0; i < 5; i++)
            {
                String t = "";
                var existProcessList = Process.GetProcessesByName(processName);
                foreach (var process in existProcessList)
                {
                    process.WaitForInputIdle();
                    if (process.MainWindowHandle == IntPtr.Zero)
                    {
                        continue;
                    }
                    t = process.MainWindowTitle;
                    mainWindowHandle = process.MainWindowHandle;
                    break;
                }
                if (mainWindowHandle == IntPtr.Zero)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                else if(t == "")
                {
                    System.Threading.Thread.Sleep(10000);
                    Console.WriteLine("CONTENUE BY NO TITLE");
                }
                else
                {
                    break;
                }
            }

            return mainWindowHandle;
        }


        /// <summary>
        /// Converts plain text to secure string.
        /// </summary>
        public static SecureString PasswordToSecureString(string password)
        {
            if (password == null)
            {
                return null;
            }

            SecureString secureString = new SecureString();

            foreach (char c in password)
            {
                secureString.AppendChar(c);
            }

            return secureString;
        }


        public static void LoadKeyboardForCurrentForegroundWindow(String KeyboardType)
        {
            switch (KeyboardType.ToUpper()) 
            {
                case "US":
                    Win32ApiUtil.PostMessage(Win32ApiUtil.GetForegroundWindow(),
                    Win32ApiUtil.SendMessageFlags.WM_INPUTLANGCHANGEREQUEST, IntPtr.Zero,
                    Win32ApiUtil.LoadKeyboardLayout("00000409", Win32ApiUtil.KeyboardLayoutFlags.KLF_ACTIVATE));

                    Win32ApiUtil.ActivateKeyboardLayout(1, Win32ApiUtil.KeyboardLayoutFlags.KLF_SETFORPROCESS);
                    break;
                case "UK":
                    Win32ApiUtil.PostMessage(Win32ApiUtil.GetForegroundWindow(),
                    Win32ApiUtil.SendMessageFlags.WM_INPUTLANGCHANGEREQUEST, IntPtr.Zero,
                    Win32ApiUtil.LoadKeyboardLayout("00000809", Win32ApiUtil.KeyboardLayoutFlags.KLF_ACTIVATE));
                    break;
            }
            
        }

        public static uint GetCurrentKeybord()
        {
            Console.WriteLine(Win32ApiUtil.GetKeyboardLayout(0));
            return Win32ApiUtil.GetKeyboardLayout(0);
        }

        public static void RetryClickUntilTargetAvailable(String TargetValue, String ExpectValue, WindowsDriver<WindowsElement> driver, int Count = 3)
        {
            int i = 0;
            while (i < Count)
            {
                try
                {
                    if (TargetValue.StartsWith("//"))
                    {
                        int con = driver.FindElementsByXPath(TargetValue).Count;
                        driver.FindElementsByXPath(TargetValue)[con - 1].Click();
                    }
                    else
                    {
                        driver.FindElementByAccessibilityId(TargetValue).Click();
                    }
                    System.Threading.Thread.Sleep(2000);
                    if (WaitUtil.WaitForElementAvailability(ExpectValue, driver))
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                catch
                {
                    i += 1;
                    continue;
                }
            }
        }

        public static void RetryMouseOverUntilTargetAvailable(String TargetValue, String ExpectValue, WindowsDriver<WindowsElement> driver, int Xvalue = 10, int Yvalue = 10, int Count = 3)
        {
            int i = 0;
            while (i < Count)
            {
                try
                {
                    Actions action = new Actions(driver);
                    if (TargetValue.StartsWith("//"))
                    {
                        int con = driver.FindElementsByXPath(TargetValue).Count;
                        action.MoveToElement(driver.FindElementsByXPath(TargetValue)[con - 1]).Build().Perform();
                    }
                    else
                    {
                        action.MoveToElement(driver.FindElementByAccessibilityId(TargetValue), Xvalue, Yvalue).Build().Perform();
                    }
                    System.Threading.Thread.Sleep(2000);
                    if (WaitUtil.WaitForElementAvailability(ExpectValue, driver))
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                catch
                {
                    i += 1;
                    continue;
                }
            }
        }

        public static void RetryRightClickUntilTargetAvailable(String TargetValue, String ExpectValue, WindowsDriver<WindowsElement> driver, int Count = 3)
        {
            int i = 0;
            while (i < Count)
            {
                try
                {
                    Actions action = new Actions(driver);
                    if (TargetValue.StartsWith("//"))
                    {
                        int con = driver.FindElementsByXPath(TargetValue).Count;
                        //driver.FindElementsByXPath(TargetValue)[con - 1].Click();
                        action.ContextClick(driver.FindElementsByXPath(TargetValue)[con - 1]).Build().Perform();
                    }
                    else
                    {
                        action.ContextClick(driver.FindElementByAccessibilityId(TargetValue)).Build().Perform();
                    }
                    System.Threading.Thread.Sleep(2000);
                    if (WaitUtil.WaitForElementAvailability(ExpectValue, driver))
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                catch
                {
                    i += 1;
                    continue;
                }
            }
        }

        public static void ClipBoard_Copying(string text)
        {
            var powershell = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = $"-command \"Set-Clipboard -Value \\\"{text}\\\"\""
                }
            };
            //Process to paste the value using powerShell
            powershell.Start();
            powershell.WaitForExit();
            //Dispose the object 
            powershell.Dispose();
        }
        public static Dictionary<string, string> ReadLangConvertJson(string fileName = "LanguageMap.json")
        {
            string path = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location).Split("bin")[0];
            var jsonStr = File.ReadAllText(path + "\\TestData\\LanguageData\\" + fileName);
            var ConvertObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
            return ConvertObject;
        }

        public static void getObjectData()
        {
            _languageMap = ReadLangConvertJson();
        }

        public static void disposeObjectData()
        {
            _languageMap.Clear();
            //IDisposable(_languageMap);
        }


        public static string ConvertLang(string enString)
        {
            var _configurationDriver = new ConfigurationDriver();
            string text = null;
            try
            {
                switch (_configurationDriver.Configuration["Language"])
                {
                    case "EN":
                        text = enString;
                        break;
                    case "JP":
                        if (enString.StartsWith("//"))
                        {
                            string[] xpath = enString.Split("\"");
                            for (int i = 0; i < xpath.Length; i++)
                            {
                                if (xpath[i].Contains("(") && xpath[i].Contains(")"))
                                {
                                    string[] xpath2 = xpath[i].Split("\'");
                                    for (int n = 0; n < xpath2.Length; n++)
                                    {
                                        if (_languageMap.ContainsKey(xpath2[n]))
                                        {
                                            xpath2[n] = _languageMap[xpath2[n]];
                                        }
                                    }
                                    xpath[i] = string.Join("\'", xpath2);
                                }
                                else
                                {
                                    if (_languageMap.ContainsKey(xpath[i]))
                                    {
                                        xpath[i] = _languageMap[xpath[i]];
                                    }
                                }
                            }
                            text = string.Join("\"", xpath);
                            break;
                        }
                        else
                        {
                            text = _languageMap[enString];
                            break;
                        }
                }
            }
            catch (KeyNotFoundException e)
            {
                text = enString;
            }
            return text;
        }

        public static WindowsDriver<WindowsElement> NewDesktopSession()
        {
            try
            {
                var configurationDriver = new ConfigurationDriver();
                string UriString = configurationDriver.Configuration["winAppUri"];
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", "Root");
                WindowsDriver<WindowsElement> desktopSession = new WindowsDriver<WindowsElement>(new Uri(UriString), appiumOptions);
                return desktopSession;
            }
            catch
            {
                //To be updated with valid exception detail
                throw;
            }
        }

        public static String GenerateUserId(string address)
        {
            string[] sUserInfo = address.Split(".");
            string ID = sUserInfo[1].Split("@")[0];
            string surfix = null;
            switch (sUserInfo[0])
            {
                case "hkguser":
                    surfix = "(IT/HK)";
                    break;
                case "lonuser":
                    surfix = "(IT/UK)";
                    break;
                case "nycuser":
                    surfix = "(IT/US)";
                    break;
                case "jpruser":
                case "tokuser":
                    surfix = "(IT/JP)";
                    break;
            }
            //hkguser.m365ap02@rnd.nomura.com -> m365ap02, hkguser (IT/HK)
            string userID = ID + ", " + sUserInfo[0] + " " + surfix;
            return userID;
        }
    }

}

