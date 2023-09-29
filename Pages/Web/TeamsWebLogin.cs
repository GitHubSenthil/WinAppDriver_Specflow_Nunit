
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using TeamsWindowsApp.Driver;
using TeamsWindowsApp.Helper;
using TeamsWindowsApp.Objects.Web;
using TechTalk.SpecFlow.Infrastructure;


namespace TeamsWindowsApp.Pages.Web
{
    public class TeamsWebLogin
    {
        private readonly WebTestDriver _Idriver;
        Reporting reportLine;
        private static ISpecFlowOutputHelper _outHelper;
        public TeamsWebLogin(WebTestDriver Idriver, ISpecFlowOutputHelper outHelper)
        {
            //_driver = driver.Current;
            _Idriver = Idriver;
            _outHelper = outHelper;
            reportLine = new Reporting(_outHelper);
        }

        public String LoginPage(String sUserName)
        {
            //Verify the login page launched with windows logo
            WaitUtil.WaitForVisible_bool("//img[@class='logo']", _Idriver.Current, 5000);

            String sUser = DataReader.getUserName(sUserName);
            String sPassword = DataReader.getPassword(sUserName);
            String sEmail = DataReader.getEmail(sUserName);

            if (WaitUtil.WaitForVisible_bool("//div[@id='loginHeader']", _Idriver.Current, 5000) && !(WaitUtil.WaitForVisible_bool("//input[@name='loginfmt']", _Idriver.Current, 5000)))
            {
                if (WaitUtil.WaitForVisible_bool("//div[@data-test-id='#EMAIL#']".Replace("#EMAIL#", sEmail), _Idriver.Current, 5000))
                {
                    _Idriver.Current.FindElement(By.XPath("//div[@data-test-id='#EMAIL#']".Replace("#EMAIL#", sEmail))).Click();
                }
                else
                {
                    _Idriver.Current.FindElement(By.Id("otherTileText")).Click();
                }
            }

            //Verify if it asked for new user
            EnterNewUser(sEmail, sPassword);

            return VerifyLoggedInUser();

            /*String eleProfile_Xpath = "//small[contains(text(),'" + sUser + "')]";
            if (WaitUtil.WaitForVisible_bool(eleProfile_Xpath, _Idriver.Current, 1000))
            {
                _Idriver.Current.FindElement(By.XPath(eleProfile_Xpath)).Click();
            }
            else
            {
                _Idriver.Current.FindElement(By.XPath("//a[text()='Use a different account']")).Click();
                EnterNewUser(sUser, sPassword);
            }*/

            /*String eleSkip_Xpath = "//a[contains(text(), 'Skip for now')]";
            if (WaitUtil.WaitForVisible_bool(eleSkip_Xpath, _Idriver.Current))
            {
                reportLine.WriteLine("Application_Launched_Successfully");
                reportLine.TakeScreenShot(_Idriver.Current, "BrowserLaunch");
                _Idriver.Current.FindElement(By.XPath(eleSkip_Xpath)).Click();
                eleSkip_Xpath = "//input[@id='idSIButton9']";
                if (WaitUtil.WaitForVisible_bool(eleSkip_Xpath, _Idriver.Current, 2000))
                {
                    _Idriver.Current.FindElement(By.XPath(eleSkip_Xpath)).Click();
                }
                eleSkip_Xpath = "//button[@id='personDropdown']";
                if (WaitUtil.WaitForVisible_bool(eleSkip_Xpath, _Idriver.Current, 20000))
                {
                    reportLine.WriteLine("User logged in: " + sUser);
                    return true;
                }
                else
                {
                    reportLine.TakeScreenShot(_Idriver.Current, "LoginFailed");
                    return false;
                }
            }
            else
            {
                reportLine.WriteLine("Application_Launch Failed");
                return false;
            }
            */
        }

        public void EnterNewUser(String sUserName, String sPassword)
        {
            
            sPassword = PasswordEncryptor.DecodeBase64(sPassword);
            String eleNewLogin = "//input[@name='loginfmt']";
            if (WaitUtil.WaitForVisible_bool(eleNewLogin, _Idriver.Current, 3 * 1000))
            {
                try
                {
                    //do
                    //{
                        System.Threading.Thread.Sleep(5000);
                        //Enter user name 
                        _Idriver.Current.FindElement(By.XPath(eleNewLogin)).SendKeys(sUserName);
                        WaitUtil.WaitForVisible_bool("//input[@id='idSIButton9']", _Idriver.Current, 5 * 1000);
                        System.Threading.Thread.Sleep(5000);
                        //Click Next button
                        _Idriver.Current.FindElement(By.XPath("//input[@id='idSIButton9']")).Click();
                    //}
                    //while (WaitUtil.WaitForVisible_bool(eleNewLogin, _Idriver.Current, 3 * 1000));

                    System.Threading.Thread.Sleep(5000);
                    //Retrieve the encrypted URL into the string
                    String sRedirectedURL = _Idriver.Current.Url;
                    //Prefix the URL
                    //Sample: https://<username>:<password>@URL
                    String sAutheticatedURL = sRedirectedURL.Substring(0, 8) + sUserName + ":" + sPassword + "@" + sRedirectedURL.Substring(8);
                    Console.WriteLine(sAutheticatedURL);
                    System.Threading.Thread.Sleep(10000);
                    //Resubmit with autheticated URL
                    _Idriver.Current.Navigate().GoToUrl(sAutheticatedURL);

                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    reportLine.TakeScreenShot(_Idriver.Current, "ExceptionLoggedInUser");
                    reportLine.WriteLine("Exception: " + e);
                }
            }
        }

        public void HandleNotification()
        {
            String eleDimissButton_xpath = "//button[@title=\"Dismiss\"]";

            if (WaitUtil.WaitForVisible_bool(eleDimissButton_xpath, _Idriver.Current))
            {
                _Idriver.Current.FindElement(By.XPath(eleDimissButton_xpath)).Click();
            }
        }

        public String VerifyLoggedInUser()
        {
            try
            {
                String eleProfile_Xpath = "//button[@id='personDropdown']";

                if (WaitUtil.WaitForVisible_bool(eleProfile_Xpath, _Idriver.Current, 10000))
                {
                    //Click the Profile
                    _Idriver.Current.FindElement(By.XPath(eleProfile_Xpath)).Click();

                    //Get the user value
                    eleProfile_Xpath = "//div[@class='profile-details-container']//span[contains(@ng-bind,'authenticatedUserEmail')]";
                    String sRetrieve = _Idriver.Current.FindElement(By.XPath(eleProfile_Xpath)).Text;

                    eleProfile_Xpath = "//button[@id='personDropdown']";
                    _Idriver.Current.FindElement(By.XPath(eleProfile_Xpath)).Click();

                    reportLine.WriteLine("User "+ sRetrieve + " logged in -> Web Application");
                    return sRetrieve;
                }
                else
                {
                    reportLine.TakeScreenShot(_Idriver.Current, "LogInFailed");
                    reportLine.WriteLine("Log in failed");
                    return "";
                }
            }
            catch (Exception e)
            {
                reportLine.TakeScreenShot(_Idriver.Current, "ExceptionLoggedInUser");
                reportLine.WriteLine("Exception: " + e);
                return "";
            }
            

        }

        public String Logout(String sUserName)
        {
            String eleProfile_Xpath = "//button[@id='personDropdown']";
            //Click the Profile
            _Idriver.Current.FindElement(By.XPath(eleProfile_Xpath)).Click();

            //Clicking logout button
            eleProfile_Xpath = "//button[@id='logout-button']";
            _Idriver.Current.FindElement(By.XPath(eleProfile_Xpath)).Click();

            _Idriver.Current.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

            WaitUtil.WaitForVisible_bool("//img[@class='logo']", _Idriver.Current);
            String sTitle = _Idriver.Current.Title;
            
            eleProfile_Xpath = "//small[contains(text(),'"+ sUserName + "')]";
            _Idriver.Current.FindElement(By.XPath(eleProfile_Xpath)).Click();
            System.Threading.Thread.Sleep(5000);
            
            return sTitle;
        }

        public Boolean OpenViewOnWeb(string sAppName)
        {
            try
            {
                Boolean result = false;
                switch (sAppName.ToLower())
                {
                    case "activity":
                        _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_login_activityview)).Click();
                        result = WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_login_activityviewtitle, _Idriver.Current);
                        break;
                    case "chat":
                        _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_login_chatview)).Click();
                        result = WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_login_chatviewtitle, _Idriver.Current);
                        break;
                    case "teams":
                        _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_login_teamsview)).Click();
                        result = WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_login_teamsviewtitle, _Idriver.Current);
                        break;
                    case "calendar":
                        _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_login_calendarview)).Click();
                        result = WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_login_calendarviewtitle, _Idriver.Current);
                        break;
                    case "calls":
                        _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_login_callsview)).Click();
                        result = WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_login_callstitle, _Idriver.Current);
                        break;
                    case "others":
                        _Idriver.Current.FindElement(By.Id(TeamsWebObjects.id_login_othersview)).Click();
                        result = WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_login_appflyout, _Idriver.Current);
                        break;
                }
                return result;
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                reportLine.TakeScreenShot(_Idriver.Current, "OpenViewOnWeb");
                return false;
            }
        }
    }
}
