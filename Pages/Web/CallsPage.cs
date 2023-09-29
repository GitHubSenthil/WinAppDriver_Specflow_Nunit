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
	public class CallsPage
	{
		private readonly WebTestDriver _Idriver;
		Reporting reportLine;
		private static ISpecFlowOutputHelper _outHelper;
		public CallsPage(WebTestDriver Idriver, ISpecFlowOutputHelper outHelper)
		{
			_Idriver = Idriver;
			_outHelper = outHelper;
			reportLine = new Reporting(_outHelper);
		}

		public String SearchUser(string sUserName)
        {
            try
            {
				_Idriver.Current.SwitchTo().ParentFrame();
				_Idriver.Current.SwitchTo().Frame(0);
				WaitUtil.WaitForVisible_bool(TeamsWebObjects.id_calls_usersearchbox, _Idriver.Current);
				_Idriver.Current.FindElement(By.Id(TeamsWebObjects.id_calls_usersearchbox)).Click();
				_Idriver.Current.FindElement(By.Id(TeamsWebObjects.id_calls_usersearchbox)).SendKeys(sUserName);
				WaitUtil.WaitForVisible_bool(TeamsWebObjects.id_calls_userpickeritem, _Idriver.Current);
				_Idriver.Current.FindElement(By.Id(TeamsWebObjects.id_calls_userpickeritem)).Click();
				WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_calls_userpicker, _Idriver.Current);
				return _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_calls_userpicker)).Text;
			}
			catch(Exception e)
            {
				reportLine.WriteLine("Exception: " + e);
				reportLine.TakeScreenShot(_Idriver.Current, "CallsView_SearchUser");
				return null;
			}
        }

		public void StartCall()
		{
			try
			{
				_Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_calls_callbutton)).Click();
				System.Threading.Thread.Sleep(5000);
				_Idriver.Current.SwitchTo().ParentFrame();
				if (_Idriver.Current.FindElement(By.Id(TeamsWebObjects.id_videocall_initial_alertmessage)).Displayed)
				{
					System.Threading.Thread.Sleep(5000);
				}
				if (WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_videocall_alertsettingbuton, _Idriver.Current))
				{
					Console.WriteLine("DISPLAY TEXT 2: {0}", _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_videocall_alertsettingbuton)).Text);
					_Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_videocall_alertsettingbuton)).Click();
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine("Exception: " + e);
				reportLine.TakeScreenShot(_Idriver.Current, "CallsView_StartCall");
			}
		}
	}
}
