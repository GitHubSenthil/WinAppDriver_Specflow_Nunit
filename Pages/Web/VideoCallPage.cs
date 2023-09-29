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
	public class VideoCallPage
	{
		private readonly WebTestDriver _Idriver;
		Reporting reportLine;
		private static ISpecFlowOutputHelper _outHelper;
		public VideoCallPage(WebTestDriver Idriver, ISpecFlowOutputHelper outHelper)
		{
			_Idriver = Idriver;
			_outHelper = outHelper;
			reportLine = new Reporting(_outHelper);
		}

		public void StartPhoneCallOnChat()
		{
            try
            {
				_Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_phonecall_button)).Click();
				System.Threading.Thread.Sleep(5000);
				IAlert Alert = _Idriver.Current.SwitchTo().Alert();
				if (_Idriver.Current.FindElement(By.Id("ngdialog1-aria-labelledby")).Displayed)
                {
					//IAlert Alert = _Idriver.Current.SwitchTo().Alert();
					Console.WriteLine("DISPLAY TEXT 1: {0}", _Idriver.Current.FindElement(By.Id("ngdialog1-aria-labelledby")).Text);
					Console.WriteLine("ALERT TEXT 1: {0}", Alert.Text);
					//Alert.Accept();
					//System.Threading.Thread.Sleep(5000);
					_Idriver.Current.SwitchTo().ParentFrame();
				}
				else if (WaitUtil.WaitForVisible_bool("//button[contains(@class, 'ts-btn-fluent-secondary-alternate')]", _Idriver.Current))
                {
					//IAlert Alert = _Idriver.Current.SwitchTo().Alert();
					Console.WriteLine("DISPLAY TEXT 2: {0}", _Idriver.Current.FindElement(By.Id("//button[contains(@class, 'ts-btn-fluent-secondary-alternate')]")).Text);
					Console.WriteLine("ALERT TEXT 2: {0}", Alert.Text);
					//Alert.Accept();
					//System.Threading.Thread.Sleep(5000);
					_Idriver.Current.SwitchTo().ParentFrame();
				}
				Console.WriteLine("ALERT TEXT 3: {0}", Alert.Text);
			}
			catch (Exception e)
            {
				reportLine.WriteLine("Exception: " + e);
				reportLine.TakeScreenShot(_Idriver.Current, "StartPhoneCallOnChat_onWeb");
			}
		}

		public void StartVideoCallOnChat()
		{
			try
			{
				_Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_videocall_button)).Click();
				System.Threading.Thread.Sleep(5000);
				_Idriver.Current.SwitchTo().ParentFrame();
				if (_Idriver.Current.FindElement(By.Id(TeamsWebObjects.id_videocall_initial_alertmessage)).Displayed)
				{
					//IAlert Alert = _Idriver.Current.SwitchTo().Alert();
					//Console.WriteLine("DISPLAY TEXT 1: {0}", _Idriver.Current.FindElement(By.Id("ngdialog1-aria-labelledby")).Text);
					//Console.WriteLine("ALERT TEXT 1: {0}", _Idriver.Current.SwitchTo().Alert().Text);
					//Alert.Accept();
					System.Threading.Thread.Sleep(5000);
				}
				if (WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_videocall_alertsettingbuton, _Idriver.Current))
				{
					//IAlert Alert = _Idriver.Current.SwitchTo().Alert();
					Console.WriteLine("DISPLAY TEXT 2: {0}", _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_videocall_alertsettingbuton)).Text);
					_Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_videocall_alertsettingbuton)).Click();
					//Console.WriteLine("ALERT TEXT 2: {0}", _Idriver.Current.SwitchTo().Alert().Text);
					//Alert.Accept();
					
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine("Exception: " + e);
				reportLine.TakeScreenShot(_Idriver.Current, "StartVideoCallOnChat_onWeb");
			}
		}

		public Boolean VerifyChatCallDialogIsOnWeb()
        {
            try
            {
				_Idriver.Current.SwitchTo().ParentFrame();
				_Idriver.Current.SwitchTo().Frame(0);
				return WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_videocall_mainscreen, _Idriver.Current, 5000);
				////Boolean bResultToolBar = WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_videocall_toolbar, _Idriver.Current, 5000);
				//if (bResultMainView.Equals(true) && bResultToolBar.Equals(true))
                //{
				//	return true;
                //}
                //else
                //{
				//	return false;
                //}
			}
			catch (Exception e)
            {
				reportLine.WriteLine("Exception: " + e);
				reportLine.TakeScreenShot(_Idriver.Current, "VerifyChatCallDialogIs_onWeb");
				return false;
			}
        }

		public void OpenMoreOptions()
        {
            try
            {
				_Idriver.Current.FindElement(By.Id(TeamsWebObjects.id_videocall_moreoption_button)).Click();
			}
			catch (Exception e)
            {
				reportLine.WriteLine("Exception: " + e);
				reportLine.TakeScreenShot(_Idriver.Current, "OpenMoreOptions_onWeb");
			}
        }

		public Boolean VerifyButtonsAreAvailability(string sButtonName)
        {
			try
			{
				Boolean returnResult = false;
                switch (sButtonName.ToLower())
                {
					case "camera":
						returnResult = _Idriver.Current.FindElement(By.Id(TeamsWebObjects.id_videocall_camera_button)).Enabled;
						break;
					case "sharescreen":
						returnResult = _Idriver.Current.FindElement(By.Id(TeamsWebObjects.id_videocall_sharescreen_button)).Enabled;
						break;
					//case "livecaption":
					//	_Idriver.Current.FindElement(By.XPath()).Click();
					//	returnResult = _Idriver.Current.FindElement(By.XPath()).Enabled;
					//	_Idriver.Current.FindElement(By.XPath()).Click();
					//	break;
					//case "transcription":
					//	_Idriver.Current.FindElement(By.XPath()).Click();
					//	returnResult = _Idriver.Current.FindElement(By.XPath()).Enabled;
					//	_Idriver.Current.FindElement(By.XPath()).Click();
					//	break;
					//case "recording":
					//	_Idriver.Current.FindElement(By.XPath()).Click();
					//	returnResult = _Idriver.Current.FindElement(By.XPath()).Enabled;
					//	_Idriver.Current.FindElement(By.XPath()).Click();
					//	break;
					//case "background":
					//	returnResult = _Idriver.Current.FindElement(By.XPath()).Enabled;
					//	break;
					case "mic":
						returnResult = _Idriver.Current.FindElement(By.Id(TeamsWebObjects.id_videocall_mic_button)).Enabled;
						break;
                }
				return returnResult;
			}
			catch (Exception e)
			{
				reportLine.WriteLine("Exception: " + e);
				reportLine.TakeScreenShot(_Idriver.Current, "VerifyButtonsAreAvailability_onWeb");
				return false;
			}
		}

		public void HangUpMTG()
        {
			try
			{
				_Idriver.Current.FindElement(By.Id(TeamsWebObjects.id_videocall_hungup_button)).Click();
				if (WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_videocall_dismiss_button, _Idriver.Current))
                {
					_Idriver.Current.FindElement(By.Id(TeamsWebObjects.xpath_videocall_dismiss_button)).Click();
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine("Exception: " + e);
				reportLine.TakeScreenShot(_Idriver.Current, "HangUpMTG_onWeb");
			}
		}
	}
}

