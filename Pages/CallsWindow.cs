using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TeamsWindowsApp.Driver;
using FluentAssertions;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow.Infrastructure;
using TeamsWindowsApp.Objects;


/*
 * Method Name: CallsWindow
 * Desc: All step action for Calls Window
 * Created by: Takuya 07/03/2022
 * Updated by: Takuya 07/03/2022
 * Update desc: 
 *  - Add a few steps for call window
 */

namespace TeamsWindowsApp.Helper
{
	public class CallsWindow
	{
		private readonly WinAppDriver _driver;
		//private readonly WindowsDriver<WindowsElement> _driver;
		Reporting reportLine;
		private static ISpecFlowOutputHelper _outHelper;
		private readonly ConfigurationDriver _configurationDriver;

		public CallsWindow(WinAppDriver driver, ISpecFlowOutputHelper outHelper, ConfigurationDriver configurationDriver)
		{
			_driver = driver;
			reportLine = new Reporting(outHelper);

			_configurationDriver = configurationDriver;
		}

		public Boolean SearchUserOnCallsWindowAndVerify(string sUserName)
		{
			try
			{
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Calls_SearchField).Click();
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Calls_SearchField).SendKeys(DataReader.getUserName(sUserName));
				System.Threading.Thread.Sleep(2000);
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Calls_SearchAutolist).Click();
				return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Calls_UserBadge), _driver.Current);
			}
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "Search_User_On_CallsWindow_And_Verify");
				return false;
            }
		}


		public Boolean VerifyCallButton()
        {
			try
            {
				return _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Call_Button)).Enabled;
            }
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "Verify_Call_Button");
				return false;
            }
        }

		public Boolean SearchTheUserFromCallsSearchAndVerify(string username)
		{
			Console.WriteLine("INPUT USER");
			var inputField = _driver.Current.FindElementByAccessibilityId("people-picker-input");
			inputField.Click();
			inputField.SendKeys(DataReader.getFullName(username));
			Console.WriteLine("SELECT USER");
			System.Threading.Thread.Sleep(3000);
			_driver.Current.FindElementByAccessibilityId("downshift-0-item-0").Click();
			string Xpath = "//ListItem[starts-with(@Name,\"" + DataReader.getUserName(username) + ". Use backspace or delete to remove.\")]";
			Console.WriteLine(Xpath);
			return _driver.Current.FindElementByXPath(Xpath).Displayed;
		}
	}
}
