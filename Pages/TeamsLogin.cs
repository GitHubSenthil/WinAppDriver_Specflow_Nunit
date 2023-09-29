using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TeamsWindowsApp.Driver;
using FluentAssertions;
using OpenQA.Selenium.Interactions;
using TeamsWindowsApp.Helper;
using TeamsWindowsApp.Objects;
using TechTalk.SpecFlow.Infrastructure;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using TeamsWindowsApp.Driver;

namespace TeamsWindowsApp.Pages
{

    public class TeamsLogin
    {
        private readonly WinAppDriver _driver;
        private readonly ConfigurationDriver _configurationDriver;
        Reporting reportLine;
        private static ISpecFlowOutputHelper _outHelper;


        public TeamsLogin(WinAppDriver driver, ISpecFlowOutputHelper outHelper, ConfigurationDriver configurationDriver)
        {
            _driver = driver;
            reportLine = new Reporting(outHelper);
            _configurationDriver = configurationDriver;
        }


        /*
         * Method Name: TeamsApp_LoginPage
         * Desc: Login to Teams app
         * Created by: Senthil 23/02/2022
         * Updated by:
         * Update desc:
         */
        public String TeamsApp_LoginPage(String sUserName)
        {
            //Dispose

            //Test
            
            Console.WriteLine(_driver.Current.Title);

            String sUser = DataReader.getUserName(sUserName);

            //Check the user 
            UserNameSelection(sUserName);
            reportLine.WriteLine(sUser + " logged in -> Windows App successfully");

            //Verify the Window Launched
            if (VerifyLoggedinUser(sUserName))
            {
                reportLine.TakeScreenShot(_driver.Current, "logged_in");
                return "pass";
            }
            Console.WriteLine(" {0} user name is already exists, No need to create user", sUser);
            reportLine.TakeScreenShot(_driver.Current, "Not_logged_in");
            return "fail";

        }
        /*
         * Method Name: TeamsApp_logout
         * Desc: Logout fromm Teams app and Quit the running teams app
         * Created by: Senthil 23/02/2022
         * Updated by:
         * Update desc:
         */
        public string TeamsApp_Logout()
        {
            //Click the profile
            System.Threading.Thread.Sleep(2000);
            _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_profileIcon).Click();

            //Click Logout
            System.Threading.Thread.Sleep(2000);
            _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_SignOut).Click();

            //Confirm the sign out
            if (WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_SignOutConfirm, _driver.Current, 2 * 1000))
                _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_SignOutConfirm).Click();


            //Waiting time to load the application
            System.Threading.Thread.Sleep(10000);

            WindowsDriver<WindowsElement> desktopSession = CommonUtil.NewDesktopSession();

            if (WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_UseAnotherAccountLink, desktopSession, 2 * 1000))
            {
               desktopSession.Dispose();
                return "pass";
            }
            desktopSession.Dispose();
            return "fail";

        }

        public void maximizeWindow()
        {
            WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_profileIcon, _driver.Current, 5 * 1000);
            IntPtr windowHandler = GetExistingProcessWindowHandle("Teams");

            _driver.Current.SwitchTo().Window(GetWindowHandleString(windowHandler));

            //Show in original position & size
            Win32ApiUtil.ShowWindow(windowHandler, Win32ApiUtil.ShowWindowFlags.SW_SHOWMAXIMIZED);
        }

        public bool VerifyLoggedinUser(string sUser)
        {

            int i = 0;
            bool bResult = false;
            string email = DataReader.getEmail(sUser);
            maximizeWindow();
            try
            {
                do
                {
                    maximizeWindow();
                    //Click the profile
                    WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_profileIcon, _driver.Current, 5 * 1000);
                    _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_profileIcon).Click();
                    _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_accountSetting_Button).Click();
                    //Verify the logged in User
                    string verifiedEmail = _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_accountSetting_Dialog)).FindElementsByXPath("//Text")[1].Text;

                    //Check application setting
                    _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_settingDiarog_lefOption).FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_settingOption_General)).Click();
                    System.Threading.Thread.Sleep(3000);
                    if (_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_settingOption_Appsettings_close_check)).Selected)
                    {
                        _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_settingOption_Appsettings_close_text)).Click();
                    }
                    _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_accountSetting_CloseButton)).Click();
                    if (verifiedEmail.ToUpper().Equals(email.ToUpper()))
                    {
                        reportLine.WriteLine("Logged in User: " + email);
                        bResult = true;
                        i = 0;
                    }
                    else
                    {
                        if (i == 1)
                        {
                            bResult = false;
                            break;
                        }
                        //Invalid user logged in - Logout
                        TeamsApp_Logout();
                        //Quit Teams
                        QuitTeamsApp();
                        //Relaunch
                        System.Threading.Thread.Sleep(10000);
                        _driver.Current.LaunchApp();
                        //Relogin with valid user
                        UserNameSelection(sUser);
                        bResult = true;
                        i = i + 1;
                    }
                }
                while (i == 1);

            }
            
            catch (Exception e)
            {
                Console.WriteLine(e);
                reportLine.WriteLine("Logged in failure: " + email);
            }
            return bResult;
        }


        // TeamsAppObjects.cs
        public void UserNameSelection(String userLabel)
        {

            string email = DataReader.getEmail(userLabel);
            string userName = DataReader.getUserName(userLabel);
            string profileObject = CommonUtil.ConvertLang(TeamsAppObjects.xpath_UserSelection).Replace("#USER#", userName);

            //Verify the User name list
            if (WaitUtil.WaitForElementAvailability("appLogosImage", _driver.Current, 5*1000))
            {
                if (WaitUtil.WaitForElementAvailability(profileObject, _driver.Current, 5 * 1000) == true)
                {
                    LoginByOtherUserAccount(userLabel, email);

                    //Clicking the user name selection button
                    Console.WriteLine(" {0} user name is already exists, No need to create user", userName);
                    _driver.Current.FindElementByXPath(profileObject).Click();

                    System.Threading.Thread.Sleep(4000);
                    IntPtr windowHandler = GetExistingProcessWindowHandle("Teams");
                    _driver.Current.SwitchTo().Window(GetWindowHandleString(windowHandler));

                    //Waiting the loading element
                    WaitUtil.WaitUntilElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_LoadingPane), _driver.Current, 20000);

                    System.Threading.Thread.Sleep(15 * 1000);
                }
                else if (WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_UseAnotherAccountLink, _driver.Current, 2 * 1000))
                {

                    reportLine.WriteLine("{0} user name is NOT listed on Teams login page, try to login by other ID/PASS", userName);
                    //Login 
                    LoginByOtherUserAccount(userLabel, email);                    
                }
            }
            else
            {
                System.Threading.Thread.Sleep(1 * 1000);
            }
        }


        private string GetWindowHandleString(IntPtr hWnd)
        {
            return hWnd.ToString("X");
        }

        private IntPtr GetExistingProcessWindowHandle(string processName)
        {
            return CommonUtil.GetMainWindowHandle(processName);
        }

        private void LoginByOtherUserAccount(string userLabel, string email)
        {
            _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_UseAnotherAccountLink).Click();

            WaitUtil.WaitForElementAvailability(TeamsAppObjects.xpath_MSLoginPage_InputField, _driver.Current, 10 * 1000);
            string textField = CommonUtil.ConvertLang(TeamsAppObjects.xpath_MSLoginPage_InputField);
            //_driver.Current.FindElementByXPath(textField).Click();
            System.Threading.Thread.Sleep(1000);
            _driver.Current.FindElementByXPath(textField).SendKeys(email);

            _driver.Current.FindElementByXPath(textField).SendKeys(Keys.Return);

            System.Threading.Thread.Sleep(5000);

            //Switch window to SSO
            WindowsDriver<WindowsElement> desktopSession = CommonUtil.NewDesktopSession();

            if (WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_SSO_LoginForm, desktopSession, 5000))
            {
                //Input SSO dialog Enter the Password
                reportLine.WriteLine("Nomura SSO login");
                var element = desktopSession.FindElementByAccessibilityId(
                    TeamsAppObjects.autoId_SSO_LoginForm
                ).FindElementByAccessibilityId(
                    TeamsAppObjects.autoId_SSO_LoginForm_PasswordInput
                );
                element.Click();
                string pass = PasswordEncryptor.DecodeBase64(DataReader.getPassword(userLabel));
                element.SendKeys(pass);
                // Click Next button
                desktopSession.FindElementByAccessibilityId(TeamsAppObjects.autoId_SSO_SigninButton).Click();

            }

            //WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_SSO_Confirmation_OkButton, desktopSession);
            if (WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_SSO_Confirmation_OkButton, desktopSession, 2* 1000))
                desktopSession.FindElementByAccessibilityId(TeamsAppObjects.autoId_SSO_Confirmation_OkButton).Click();

            //Skip temporal error page
            //if (WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_SSO_TempErroDialog), desktopSession))
           // {
               // reportLine.WriteLine("Tempral SSO Error page");
               // reportLine.TakeScreenShot(desktopSession, "Tempral_SSO_error");
               // desktopSession.FindElementByAccessibilityId(TeamsAppObjects.autoId_SSO_CloseErrorButton).Click();
           // }
            System.Threading.Thread.Sleep(2 * 1000);

            //Dispose temporal window
            desktopSession.Dispose();
            
        }

        
        public void openApp(string sMenuOption)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                string sOptionName = CommonUtil.ConvertLang(sMenuOption);
                switch (sMenuOption)
                {
                    case "Activity":
                        string sTabObject = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Toolbar_MenuItem).Replace("#FunctionTitle#", sOptionName);
                        if (WaitUtil.WaitForElementAvailability(sTabObject, _driver.Current))
                        {
                            _driver.Current.FindElementByXPath(sTabObject).Click();
                        }
                        else if (WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_with_Activity_Notification_Toolbar), _driver.Current))
                        {
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_with_Activity_Notification_Toolbar)).Click();
                        }
                        else
                        {
                            string multiActNotification = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_with_Activity_Notification_Toolbar).Replace("activity", "activities");
                            _driver.Current.FindElementByXPath(multiActNotification).Click();
                        }
                        break;
                    case "Chat":
                        if (WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Toolbar), _driver.Current))
                        {
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Toolbar)).Click();
                        }
                        else if (WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_With_Notification_Toolbar), _driver.Current))
                        {
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_With_Notification_Toolbar)).Click();
                        }
                        else
                        {
                            string multiChatNotification = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_With_Notification_Toolbar).Replace("chat", "chats");
                            Console.WriteLine(multiChatNotification);
                            _driver.Current.FindElementByXPath(multiChatNotification).Click();
                        }
                        break;
                    case "Teams":
                        if (WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Toolbar_MenuItem).Replace("#FunctionTitle#", sOptionName), _driver.Current))
                        {
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Toolbar_MenuItem).Replace("#FunctionTitle#", sOptionName)).Click();
                        }
                        else
                        {
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Toolbar_WithNotification)).Click();
                        }
                        break;
                    case "Calendar":
                        _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Toolbar_MenuItem).Replace("#FunctionTitle#", sOptionName)).Click();
                        break;
                    case "Calls":
                        if (WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Calls_Toolbar), _driver.Current))
                        {
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Calls_Toolbar)).Click();
                        }
                        else
                        {
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Missed_Calls_Toolbar)).Click();
                        }
                        break;
                    case "Files":
                        _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Toolbar_MenuItem).Replace("#FunctionTitle#", sOptionName)).Click();
                        break;
                    case "Training":
                        _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Toolbar_MenuItem).Replace("#FunctionTitle#", sOptionName)).Click();
                        break;
                    case "other":
                        _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_MoreApp).Click();
                        break;
                }
                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "openApp");
            }
        }

        /* Updated by: Takuya 23/02/2022
         * Updated Description: REname the field to from app to sToolbarOption
         * updated by: Takuya 25/02/2022
         * DEsc: 
         * */
        public string VerifyOpenApp(string app)
        {
            System.Threading.Thread.Sleep(2000);
            string test = null;
            switch (app)
            {
                case "Activity":
                    string ActivityIdentifier = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Activity_MenuItem_Text).Replace("#text#", CommonUtil.ConvertLang("Feed"));
                    test = _driver.Current.FindElementByXPath(ActivityIdentifier).Text;
                    break;
                case "Chat":
                    string ChatIdentifier = CommonUtil.ConvertLang(TeamsAppObjects.xpath_MenuItem_Text.Replace("#text#", "Chat"));
                    test = _driver.Current.FindElementByXPath(ChatIdentifier).Text;
                    break;
                case "Teams":
                    string TeamsIdentifier = CommonUtil.ConvertLang(TeamsAppObjects.xpath_MenuItem_Group.Replace("#text#", "Teams and Channels list"));
                    test = _driver.Current.FindElementByXPath(TeamsIdentifier).Text;
                    break;
                case "Calendar":
                    string CalenderIdentifier = CommonUtil.ConvertLang(TeamsAppObjects.xpath_MenuItem_Group.Replace("#text#", "Calendar"));
                    test = _driver.Current.FindElementByXPath(CalenderIdentifier).Text;
                    break;
                case "Calls":
                    test = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Call_Button)).Text;
                    break;
                case "Files":
                    string FilesIdentifier = CommonUtil.ConvertLang(TeamsAppObjects.xpath_MenuItem_Group.Replace("#text#", "Files list"));
                    test = _driver.Current.FindElementByXPath(FilesIdentifier).Text;
                    break;
                case "Training":
                    test = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Training_Title)).Text;
                    Console.WriteLine(test);
                    break;
            }
            return test;
        }

        //v3.2
        public Boolean verifyOtherAppsWindow()
        {
            return WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_MoreApp_SearchButton, _driver.Current);
        }

        public Boolean VerifyLearningPathwayContents(string text)
        {
            try
            {
                return WaitUtil.WaitForElementAvailability("//Text[@Name=\"" + text + "\"]", _driver.Current);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "VerifyLearningPathwayContents");
                return false;
            }
        }


        /*
         * Method Name: TeamsApp_QuitTeams
         * Desc: Team App Quit from taskbar
         * Created by: Senthil 23/02/2022
         * Updated by:
         * Update desc:
         */
        public static void QuitTeamsApp()
        {
            try
            {
                if (CommonUtil.checkProcessRunning("Teams"))
                {
                    WindowsDriver<WindowsElement> desktopSession = CommonUtil.NewDesktopSession();
                    desktopSession.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_NotificationArrow)).Click();
                    var eleTeams = desktopSession.FindElementsByXPath(TeamsAppObjects.xpath_NotificationIcon_TeamsApp);
                    if (eleTeams.Count > 0)
                    {
                        Actions CtRightClick = new Actions(desktopSession);
                        CtRightClick.MoveToElement(eleTeams[0]).ContextClick().Build().Perform();
                        desktopSession.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_ContextQuit)).Click();
                        System.Threading.Thread.Sleep(6000);
                        desktopSession.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_NotificationArrow)).Click();
                    }
                    else
                    {
                        desktopSession.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_NotificationArrow)).Click();
                    }
                    desktopSession.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: "+e);
            }
        }

        public void CloseDriver()
        {
            if (_driver.Current != null)
                _driver.Current.IsIMEActive();

        }
    }
}