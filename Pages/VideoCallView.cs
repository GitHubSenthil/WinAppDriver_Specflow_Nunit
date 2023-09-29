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
	public class VideoCallView
	{
		private readonly WinAppDriver _driver;
		Reporting reportLine;
		private static ISpecFlowOutputHelper _outHelper;
		private readonly ConfigurationDriver _configurationDriver;

		public VideoCallView(WinAppDriver driver, ISpecFlowOutputHelper outHelper, ConfigurationDriver configurationDriver)
		{
			_driver = driver;
			reportLine = new Reporting(outHelper);

			_configurationDriver = configurationDriver;
		}

		private void SwitchWindowHandler()
		{
			IntPtr windowHandler = CommonUtil.GetMainWindowHandle("Teams");
			Console.WriteLine(windowHandler);
			_driver.Current.SwitchTo().Window(windowHandler.ToString("X"));
		}

		//Followings are for chat call
		public void StartPhoneCallOnChat()
		{
			System.Threading.Thread.Sleep(5000);
			_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AudioCall)).Click();
			System.Threading.Thread.Sleep(5000);
			SwitchWindowHandler();
		}

		public void StartVideoCallOnChat()
		{

			_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_VideoCall)).Click();
			System.Threading.Thread.Sleep(5000);
			SwitchWindowHandler();
		}

		public Boolean VerifyChatCallDialog()
		{
			try
			{
				return WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_videoCall_HungUP_Button, _driver.Current);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyChatCallDialog");
				return false;
			}
		}

		//Need to update (10/10/2022)
		public void WaitVideoCallConnection()
		{
			try
			{
				if (WaitUtil.WaitForElementEnabled(TeamsAppObjects.autoId_videoCall_Option_shareButton, "autoid", _driver.Current)) ;
				{
					Console.WriteLine(_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_shareButton).Enabled);
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "WaitVideoCallConnection");
			}
		}

		//Followings are the calls Call
		public void StartCallWithByCallsButton()
		{
			_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Call_Button)).Click();
			System.Threading.Thread.Sleep(5000);
			SwitchWindowHandler();
		}

		//Followings are for Channel call and schedule call
		public String VerifyVideoCallDialogDisplay()
		{
			try
			{
				System.Threading.Thread.Sleep(7000);
				SwitchWindowHandler();
				string result = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_VideoCallDialog)).Text;
				Console.WriteLine("TEXT: {0}", result);
				WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_VideoCallDialog), _driver.Current);
				return result;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyVideoCallDialogDisplay");
				return "error";
			}
		}

		public void JoinMTG()
		{
			try
			{
				_driver.Current.FindElementByAccessibilityId(CommonUtil.ConvertLang(TeamsAppObjects.autoId_videoCall_JoinButton)).Click();
				System.Threading.Thread.Sleep(7000);
				if (WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_NotificationDialog), _driver.Current))
				{
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_NotificationCloseButton)).Click();
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "JoinMTG");
			}
		}

		public void CancelMTG()
		{
			try
			{
				_driver.Current.FindElementByName(CommonUtil.ConvertLang("Cancel")).Click();
				SwitchWindowHandler();
				System.Threading.Thread.Sleep(2000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "CancelMTG");
			}
		}

		public void HungUpMTGByMyself()
		{
			try
			{
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_Leave_Button)).Click();
				System.Threading.Thread.Sleep(2000);
				if (WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_Evaluation_Close_Button, _driver.Current))
				{
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Evaluation_Close_Button).Click();
				}
				System.Threading.Thread.Sleep(2000);
				SwitchWindowHandler();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "HungUpMTGByMyself");
			}
		}

		public void EndMTG()
		{
			try
			{
				System.Threading.Thread.Sleep(2000);
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_HungUP_moreOptions)).Click();
				System.Threading.Thread.Sleep(2000);
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_EndMTG_Button)).Click();
				System.Threading.Thread.Sleep(2000);
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_EndMTG_Dialog)).FindElementByName(CommonUtil.ConvertLang("End")).Click();
				System.Threading.Thread.Sleep(2000);
				if (WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_Evaluation_Close_Button, _driver.Current))
				{
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Evaluation_Close_Button).Click();
				}
				System.Threading.Thread.Sleep(2000);
				SwitchWindowHandler();
				System.Threading.Thread.Sleep(2000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "EndMTG");
			}
		}

		public Boolean VerifyCameraSetting()
		{
			try
			{
				if (WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_CameraSettingOff), _driver.Current))
				{
					return false; //Setting is turn off
				}
				else
				{
					return true; //Setting is turn on
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyCameraSetting");
				return true;
			}
		}

		public Boolean VerifyBackgroundDisabledSetting()
		{
			try
			{
				return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_BGButton_TuenedOff_settingDialog), _driver.Current);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyBackgroundDisabledSetting");
				return true;
			}
		}

		public Boolean VerifyBackgroundEnabledSetting()
		{
			try
			{
				return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_BGButton_TuenedOn_settingDialog), _driver.Current);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyBackgroundEnabledSetting");
				return true;
			}
		}

		public void CameraSettingChange()
		{
			try
			{
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_VideoOption)).FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_Camera)).Click();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "CameraSettingChange");
			}
		}

		public void ClickBakcGroundSettingButton()
		{
			try
			{
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_VideoOption)).FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_BGButton_TuenedOn_settingDialog)).Click();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "ClickBakcGroundSettingButton");
			}
		}

		public void SelectBakcGroundButtonOnSetting(string bgName)
		{
			try
			{
				var _configurationDriver = new ConfigurationDriver();
				System.Threading.Thread.Sleep(1000);
				if (_configurationDriver.Configuration["Language"] == "JP" && bgName == "Blur")
				{
					_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_JPBGName_Blur)).Click();
				}
				else
				{
					_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_BGName_settingDialog).Replace("#BGNAME#", bgName)).Click();
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "SelectBakcGroundButtonOnSetting");
			}
		}

		public Boolean VerifyAudioSetting(string audioType)
		{
			try
			{
				Boolean result = false;
				switch (audioType.ToLower())
				{
					case "computer":
						result = _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_ComputerAudio)).Selected;
						break;
					case "phone":
						result = _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_PhoneAudio)).Selected;
						break;
					case "room":
						result = _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_RoomAudio)).Selected;
						break;
					case "not use":
						result = _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_NotUse)).Selected;
						break;
				}
				return result;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyAudioSetting");
				return false;
			}
		}

		public void SelectAudioSetting(string audioType)
		{
			try
			{
				switch (audioType.ToLower())
				{
					case "computer":
						_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_ComputerAudio)).Click();
						break;
					case "phone":
						_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_PhoneAudio)).Click();
						break;
					case "room":
						_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_RoomAudio)).Click();
						break;
					case "not use":
						_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_NotUse)).Click();
						break;
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "SelectAudioSetting");
			}
		}

		public Boolean VerifyMicSpeakerSetting(string type)
		{
			try
			{
				string sButtonName = null;
				if (type == "mic")
				{
					sButtonName = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_MicSetting)).GetAttribute("Name");
				}
				else
				{
					sButtonName = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_AudioSetting)).GetAttribute("Name");
				}
				Console.WriteLine(sButtonName);
				if (sButtonName == CommonUtil.ConvertLang("Mic is not available") || sButtonName == "Speaker is off")
				{
					return false; //Setting is turn off
				}
				else
				{
					return true; //Setting is turn on
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyMicSpeakerSetting");
				return false;
			}
		}

		public void MicSpeakerSettingChange(string type)
		{
			try
			{
				if (type == "mic")
				{
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_MicSetting)).Click();
				}
				else
				{
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_AudioSetting)).Click();
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "MicSpeakerSettingChange");
			}
		}

		public void OpenMoreOption()
		{
			try
			{
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_MoreOptionButton).Click();
				System.Threading.Thread.Sleep(5000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "OpenMoreOption");
			}
		}

		public Boolean OptionButtonAvailable(string optionName)
		{
			try
			{

				bool result = false;
				string attrText = null;
				System.Threading.Thread.Sleep(2000);
				Console.WriteLine("OPTION NAME: {0}", optionName);
				switch (optionName.ToLower())
				{
					case "camera":
						result = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_videoButton).Enabled;
						break;
					case "mic":
						result = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_micButton).Enabled;
						attrText = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_micButton).GetAttribute("Name");
						if (attrText == CommonUtil.ConvertLang("No microphone was found among your devices.") || attrText == "Japanese")
						{
							result = false;
							Console.WriteLine(attrText);
						}
						break;
					case "sharescreen":
						result = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_shareButton).Enabled;
						break;
					case "livecaption":
						_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_LiveCaptionMenu).Click();
						System.Threading.Thread.Sleep(1000);
						result = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_livecaption).Enabled;
						reportLine.TakeScreenShot(_driver.Current, "OptionButtonAvailable_livecaption");
						_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_LiveCaptionMenu).Click();
						break;
					case "transcription":
						_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_RecordingMenu).Click();
						System.Threading.Thread.Sleep(1000);
						result = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_Transcription).Enabled;
						reportLine.TakeScreenShot(_driver.Current, "OptionButtonAvailable_transcription");
						_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_RecordingMenu).Click();
						break;
					case "recording":
						_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_RecordingMenu).Click();
						System.Threading.Thread.Sleep(1000);
						result = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_RecordingButton).Enabled;
						reportLine.TakeScreenShot(_driver.Current, "OptionButtonAvailable_recording");
						_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_RecordingMenu).Click();
						break;
					case "background":
						result = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_BackgroundButton).Enabled;
						break;
					case "people":
						result = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Attendee_Button).Enabled;
						break;
					case "chat":
						result = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Chat_Button).Enabled;
						break;
				}
				reportLine.WriteLine("OPTION NAME: {0}, RESULT: {1}", optionName, result);
				return result;
			}
			catch (Exception e)
			{
				reportLine.WriteLine("OPTION NAME: {0}, ERROR: {1}", optionName, e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "OptionButtonAvailable_");
				return false;
			}
		}

		public string VerifyButtunIsTurningOnOff(string optionName)
		{
			try
			{

				string result = null;
				string attrText = null;
				switch (optionName.ToLower())
				{
					case "camera":
						attrText = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_videoButton).GetAttribute("Name");
						if (attrText.Contains(CommonUtil.ConvertLang("Turn camera on")))
						{
							result = "off";
							Console.WriteLine(attrText);
						}
						else
						{
							result = "on";
							Console.WriteLine(attrText);
						}
						break;
					case "mic":
						attrText = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_micButton).GetAttribute("Name");
						if (attrText == CommonUtil.ConvertLang("No microphone was found among your devices.") || attrText.Contains(CommonUtil.ConvertLang("Turn audio on")))
						{
							result = "off";
							Console.WriteLine(attrText);
						}
						else
						{
							result = "on";
						}
						break;
				}
				return result;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyButtunIsTurningOnOff");
				return "Exception";
			}
		}

		public void MouseOverToCameraButton()
		{
			try
			{
				var element = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_videoButton);
				Actions action = new Actions(_driver.Current);
				action.MoveToElement(element, 10, 10).Build().Perform();
				System.Threading.Thread.Sleep(5000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "MouseOverToCameraButton");
			}
		}

		public void OpenBackgroundSetting()
		{
			try
			{
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_BackgroundSettingButton)).Click();
				System.Threading.Thread.Sleep(1000);
				var element = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_micButton);
				Actions action = new Actions(_driver.Current);
				action.MoveToElement(element).Build().Perform();
				Console.WriteLine("move the mouse to the audio button");
				System.Threading.Thread.Sleep(2000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "OpenBackgroundSetting");
			}
		}

		public void ApplyBackgroundSetting(string bgName)
		{
			try
			{
				string status = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_videoButton).GetAttribute("Name");
				Console.WriteLine(status);
				System.Threading.Thread.Sleep(1000);
				var _configurationDriver = new ConfigurationDriver();
				if (_configurationDriver.Configuration["Language"] == "JP" && bgName == "Blur")
				{
					_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_JPBGName_Blur)).Click();
				}
				else
				{
					_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_BGName_settingDialog).Replace("#BGNAME#", bgName)).Click();
				}
				if (status.Contains("Turn camera on"))
				{
					_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_BackgroundApplyWithVideoOn)).Click();
				}
				else
				{
					_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_BackgroundApplyButton)).Click();
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "ApplyBackgroundSetting");
			}
		}

		public Boolean VerifyBackgroundIsSelected(string bgName)
		{
			try
			{
				var _configurationDriver = new ConfigurationDriver();
				System.Threading.Thread.Sleep(1000);
				if (_configurationDriver.Configuration["Language"] == "JP" && bgName == "Blur")
				{
					return _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_JPBGName_Blur)).Selected;
				}
				else
				{
					return _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_BGName_settingDialog).Replace("#BGNAME#", bgName)).Selected;
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyBackgroundIsSelected");
				return false;
			}
		}

		//Meeting setting
		public void OpenMeetingSettingView()
		{
			try
			{
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_MeetingSettingButton).Click();
				System.Threading.Thread.Sleep(1000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "OpenMeetingSettingView");
			}
		}

		public void SelectBypassSetting(string setting)
		{
			try
			{
				WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_MTGOptionBypassSetting), _driver.Current);
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_MTGOptionBypassSetting)).Click();
				System.Threading.Thread.Sleep(1000);
				Console.WriteLine(CommonUtil.ConvertLang(setting));
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(setting)).Click();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "SelectBypassSetting");
			}
		}

		public void SaveMeetingSetting()
		{
			try
			{
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_MeetingOptions)).FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_MeetingOptionsSaveButton)).Click();
				System.Threading.Thread.Sleep(3000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "SaveMeetingSetting");
			}
		}

		public Boolean VerifyMeetingSetting()
		{
			try
			{
				return _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_MeetingOptions)).FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_MeetingOptionsSaved)).Displayed;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyMeetingSetting");
				return false;
			}
		}

		public void CloseRightPane()
		{
			try
			{
				System.Threading.Thread.Sleep(1000);
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_CloseRightPaneButton)).Click();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "CloseRightPane");
			}
		}

		public void OpenParticipantView()
		{
			try
			{
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_ParticipantsButton).Click();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "OpenParticipantView");
			}
		}

		public void OpenParticipantViewMoreAction()
		{
			try
			{
				WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_ParticipantsView), _driver.Current);
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_ParticipantsView)).FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_ParticipantsViewMoreActions)).Click();
				System.Threading.Thread.Sleep(1000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "OpenParticipantViewMoreAction");
			}
		}

		public void LockTheMTG()
		{
			try
			{
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_LockMTGButton)).Click();
				System.Threading.Thread.Sleep(1000);
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_LockDialogLockButton)).Click();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "LockTheMTG");
			}
		}

		public void UnlockAMTG()
		{
			try
			{
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_UnlockMTGButton)).Click();
				System.Threading.Thread.Sleep(1000);
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_videoCall_UnlockDialogUnlockButton)).Click();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "UnlockAMTG");
			}
		}

		public Boolean VerifyLockUnlockAlart()
		{
			try
			{
				var AlartPopup = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_AlartCloseButton);
				if (AlartPopup.Displayed)
				{
					if (AlartPopup.GetAttribute("Name") == CommonUtil.ConvertLang("No Microphone Make sure a mic is connected to your computer so others can hear you. Close"))
					{
						AlartPopup.Click();
					}
				}
				return AlartPopup.Displayed;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyLockUnlockAlart");
				return false;
			}
		}

		public string VerifyMTGLockUnlockText()
		{
			try
			{
				System.Threading.Thread.Sleep(1000);
				var AlartPopup = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_AlartCloseButton);
				string AlartPopupText = AlartPopup.GetAttribute("Name");
				if (AlartPopupText == "No Microphone Make sure a mic is connected to your computer so others can hear you. Close")
				{
					AlartPopup.Click();
				}
				return AlartPopupText;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyMTGLockUnlockText");
				return "fail";
			}
		}

		//ScreenShare
		public void OpenShareScreenView()
		{
			try
			{
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_shareButton).Click();
				System.Threading.Thread.Sleep(1000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "OpenShareScreenView");
			}
		}

		public void ShareFullScreen()
		{
			try
			{
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_ChooseScreen)).FindElementByName("Screen 1").Click();
				System.Threading.Thread.Sleep(5000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "ShareFullScreen");
			}
		}

		public Boolean VerifyScreenShareControlWindow()
		{
			try
			{
				WindowsDriver<WindowsElement> desktopSession = CommonUtil.NewDesktopSession();
				System.Threading.Thread.Sleep(3000);
				Boolean result = desktopSession.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_StopSharingButton).Displayed;
				desktopSession.Dispose();
				System.Threading.Thread.Sleep(1000);
				return result;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyScreenShareControlBar");
				return false;
			}
		}

		public void StopScreenSharing()
		{
			try
			{
				WindowsDriver<WindowsElement> desktopSession = CommonUtil.NewDesktopSession();
				desktopSession.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_StopSharingButton).Click();
				desktopSession.Dispose();
				System.Threading.Thread.Sleep(3000);
				SwitchWindowHandler();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "StopScreenSharing");
			}
		}

		public Boolean VerifyNotScreenSharing()
		{
			try
			{
				string text = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_shareButton).Text;
				Console.WriteLine(_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_shareButton).GetAttribute("Name"));
				if (text == CommonUtil.ConvertLang("Share content (Ctrl+Shift+E)"))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyNotScreenSharing");
				return false;
			}
		}

		public Boolean VerifyPPTLiveSharingIsDisplayed()
		{
			try
			{
				//System.Threading.Thread.Sleep(5000);
				//int Count = _driver.Current.FindElementsByXPath("//MenuItem").Count;
				//for (int i = 0; i < Count; i++)
				//{
				//	Console.WriteLine(_driver.Current.FindElementByName("Share content").FindElementsByXPath("//MenuItem")[i].Text);
				//}
				return WaitUtil.WaitForElementAvailability(TeamsAppObjects.xpath_videoCall_PPTLiveSharing_button, _driver.Current);
				//return _driver.Current.FindElementByXPath(TeamsAppObjects.xpath_videoCall_PPTLiveSharing_button).Displayed;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyPPTLiveSharingIsDisplayed");
				return false;
			}
		}

		public void StartPPTLiveSharingBy(string sFileName)
		{
			try
			{
				_driver.Current.FindElementByName(TeamsAppObjects.xname_videoCall_ShareScreen_dialog).FindElementByXPath(TeamsAppObjects.xpath_videoCall_PPTLiveSharing_buttonName.Replace("#FILENAME#", sFileName)).Click();
				//System.Threading.Thread.Sleep(3000);
				WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_videoCall_PPTSharing_MailSlideView, _driver.Current);
			}
			catch (Exception e)
			{
				reportLine.WriteLine("TARGET XPATH {0}", TeamsAppObjects.xpath_videoCall_PPTLiveSharing_buttonName.Replace("#FILENAME#", sFileName));
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyPPTLiveSharingBy");
			}
		}

		public void StartPPTLiveSharingFromComputerBy(string sFileName)
        {
            try
            {
				_driver.Current.FindElementByName(TeamsAppObjects.xname_videoCall_ShareScreen_dialog).FindElementByXPath(TeamsAppObjects.xpath_videoCall_PPTLiveSharing_FromBrowserButton).Click();
				System.Threading.Thread.Sleep(3000);
				String path = System.Environment.CurrentDirectory.Split("bin")[0].TrimEnd('\\');
				Console.WriteLine(path);
				string sFilePath = path + _configurationDriver.Configuration[sFileName];
				System.Threading.Thread.Sleep(2000);
				
				//Copy the value in Clipboard
				CommonUtil.ClipBoard_Copying(sFilePath);
				//Paste the value in the field
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_FilePath_TextField)).SendKeys(Keys.Control + "v");
				reportLine.TakeScreenShot(_driver.Current, "FilePath_Input_PPT_sharing");
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_OpenButton_FileDialog)).Click();
				if (WaitUtil.WaitForElementAvailability(TeamsAppObjects.xpath_Chat_FileReplace_Button, _driver.Current))
                {
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_FileReplace_Button)).Click();
				}
				//Wait and Verify the target view is shown
				WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_videoCall_PPTSharing_MailSlideView, _driver.Current);
			}
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyPPTLiveSharingFromComputerBy");
			}
        }

		public Boolean VerifyLiveSharingTopToolBarButtonsAvailable(string sButtonName)
		{
			try
			{
				switch (sButtonName.ToLower())
				{
					case "stop share":
						return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_StopSharingButton).Displayed;
					case "layout":
						return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_LayoutButton).Displayed;
					case "private view":
						return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_PrivateviewButton).Displayed;
					case "popout":
						return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_PopupButton).Displayed;
				}
				return false;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyLiveSharingTopToolBarButtonsAvailable");
				return false;
			}
		}

		public Boolean VerifyLiveSharingActionToolButtonsAvailable(string sButtonName)
		{
			try
			{
				Console.WriteLine(sButtonName);
				switch (sButtonName.ToLower())
				{
					case "grid view":
						return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_GridviewButton).Displayed;
					case "more action":
						return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_MoreActionButton).Displayed;
					case "cursor":
						return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_CursorButton).Displayed;
					case "laserpointer":
						return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_LaserPointerButton).Displayed;
					case "pen":
						return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_PenButton).Displayed;
					case "highlighter":
						return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_HighlighterButton).Displayed;
					case "eraser":
						return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_EraserButton).Displayed;
				}
				return false;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyLiveSharingTopToolBarButtonsAvailable");
				return false;
			}
		}

		public Boolean VerifyMainScreenSharingIs()
		{
			try
			{
				return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_MailSlideView).Displayed;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyMainScreenSharingIs");
				return false;
			}
		}

		public void StopPPTLiveSharing()
		{
			try
			{
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_PPTSharing_StopSharingButton).Click();
				System.Threading.Thread.Sleep(3000);
				WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_videoCall_PPTSharing_StopConfirmationDialog, _driver.Current);
				_driver.Current.FindElementByName(TeamsAppObjects.xname_videoCall_PPTSharing_StopConfirmation).FindElementByName(TeamsAppObjects.xname_videoCall_PPTSharing_StopConfirmation_Button).Click();
				System.Threading.Thread.Sleep(3000);
			}
			catch(Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "StopPPTLiveSharing");
			}
		}

		//Recording MTG
		public void StartStopRecording(string type)
		{
			try
			{
				if (type.ToLower() == "start")
				{
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_RecordingMenu).Click();
					System.Threading.Thread.Sleep(1000);
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_RecordingButton).Click();
					WaitUtil.WaitForElementAvailability(TeamsAppObjects.xname_videoCall_TranscriptView, _driver.Current);
					System.Threading.Thread.Sleep(1000);
				}
				else if (type.ToLower() == "stop")
				{
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_RecordingMenu).Click();
					System.Threading.Thread.Sleep(1000);
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_RecordingButton).Click();
					System.Threading.Thread.Sleep(1000);
					_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videocall_RecordingStopdialogStopButton)).FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_RecordingStopButton)).Click();
					WaitUtil.WaitForElementAvailability(TeamsAppObjects.xname_videoCall_TranscriptView, _driver.Current);
				}
				else
				{
					throw new Exception("Scenario keywords does't match properly");
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "StartRecording");
			}
		}

		public void StartStopTranscription(string type)
		{
			try
			{
				if (type.ToLower() == "start")
				{
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_RecordingMenu).Click();
					System.Threading.Thread.Sleep(1000);
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_Transcription).Click();
					WaitUtil.WaitForElementAvailability(TeamsAppObjects.xpath_videoCall_TranscriptSettingConfirmDialog, _driver.Current);
					_driver.Current.FindElementByXPath(TeamsAppObjects.xpath_videoCall_TranscriptSettingConfirmButton).Click();
					WaitUtil.WaitForElementAvailability(TeamsAppObjects.xname_videoCall_TranscriptView, _driver.Current);
					System.Threading.Thread.Sleep(1000);
				}
				else if (type.ToLower() == "stop")
				{
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_RecordingMenu).Click();
					System.Threading.Thread.Sleep(1000);
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_Transcription).Click();
					System.Threading.Thread.Sleep(1000);
					//_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videocall_RecordingStopdialogStopButton)).FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_RecordingStopButton)).Click();
					WaitUtil.WaitForElementAvailability(TeamsAppObjects.xname_videoCall_TranscriptView, _driver.Current);
				}
				else
				{
					throw new Exception("Scenario keywords does't match properly");
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "StartStopTranscription");
			}
		}

		public void StartStopLiveCaption(string type)
		{
			try
			{
				if (type.ToLower() == "start")
				{
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_LiveCaptionMenu).Click();
					System.Threading.Thread.Sleep(1000);
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_livecaption).Click();
					WaitUtil.WaitForElementAvailability(TeamsAppObjects.xpath_videoCall_TranscriptSettingConfirmDialog, _driver.Current);
					_driver.Current.FindElementByXPath(TeamsAppObjects.xpath_videoCall_TranscriptSettingConfirmButton).Click();
					WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_videoCall_LiveCaptionSettingButton, _driver.Current);
				}
				else if (type.ToLower() == "stop")
				{
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_LiveCaptionMenu).Click();
					System.Threading.Thread.Sleep(1000);
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_livecaption).Click();
					System.Threading.Thread.Sleep(3000);
				}
				else
				{
					throw new Exception("Scenario keywords does't match properly");
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "StartStopLiveCaption");
			}
		}

		public Boolean VerifyLiveCaptionHas()
		{
			try
			{
				var targetElem = CommonUtil.ConvertLang(TeamsAppObjects.autoId_videoCall_LiveCaptionSettingButton);
				return WaitUtil.WaitForElementAvailability(targetElem, _driver.Current);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "StartStopLiveCaption");
				return false;
			}
		}

		public string VerifyRecordingButton()
		{
			try
			{
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_MoreOptionButton).Click();
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_RecordingMenu).Click();
				System.Threading.Thread.Sleep(1000);
				string sButton = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Option_RecordingButton).Text;
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_MoreOptionButton).Click();
				return sButton;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyRecordingButton");
				return "Exception";
			}
		}

		public string VerifyTranscription(string typeText)
		{
			try
			{
				WaitUtil.WaitForElementAvailability(TeamsAppObjects.xname_videoCall_TranscriptView, _driver.Current);
				var recordItem = _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_videoCall_TranscriptView)).FindElementsByXPath("//ListItem");
				string sTeanscriptMessage = null;
				Console.WriteLine(recordItem.Count);
				if (typeText == "started")
                {
					sTeanscriptMessage = recordItem[0].FindElementsByXPath("//Text")[1].Text;
				}
				else
                {
					sTeanscriptMessage = recordItem[recordItem.Count - 1].FindElementsByXPath("//Text")[1].Text;
				}
				return sTeanscriptMessage;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyTranscription");
				return "Exception";
			}
		}


		// interactive2user
		public Boolean WaitUntilAttendeeJoin()
		{
			try
			{
				_driver.Current.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
				int i = 1000;
				while (i < 30000)
				{
					try
					{
						var sElemPeopleButton = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Attendee_Button);
						var sElemChatButton = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_videoCall_Chat_Button);
						if (sElemPeopleButton.Displayed && sElemChatButton.Displayed)
						{
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
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "WaitUntilAttendeeJoin");
				return false;
			}
		}

		public Boolean WaitUntilRinging()
		{
			try
			{
				_driver.Current.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
				int i = 1000;
				while (i < 10000)
				{
					try
					{
						System.Threading.Thread.Sleep(500);
						if (_driver.Current.FindElementByName("Join").Displayed)
						{
							Console.WriteLine("FINED THE ELEMENT!!!!!!!!!!!!!!!");
							return true;
						}
					}
					catch (Exception e)
					{
						Console.WriteLine("{0} TIMES: CATCH method", i.ToString());
						Console.WriteLine(e);
					}
					int num = i / 1000;
					Console.WriteLine("{0} TIMES", num.ToString());
					i += 1000;
				}
				return false;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "WaitUntilRinging");
				return false;
			}
		}

		public void JoinTheCallAsAttendeeBy(string sCallType)
		{
			try
			{
				if (sCallType.ToUpper() == "PHONE")
				{
					_driver.Current.FindElementByName("Join").Click();
				}
				else if (sCallType.ToUpper() == "VIDEO")
				{
					_driver.Current.FindElementByName("Join").Click();
				}
				else
				{
					throw new NotSupportedException("Valiable: '" + sCallType + "' is wrong format, please use 'phone' or 'video' as this valiable.");
				}
				System.Threading.Thread.Sleep(10000);
				SwitchWindowHandler();
			}
			catch (NotSupportedException e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "JoinTheCallAsAttendeeBy");
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "JoinTheCallAsAttendeeBy");
			}
		}

		public Boolean WaitUntilVideoCallIsTerminated()
		{
			try
			{
				int i = 1000;
				WindowsDriver<WindowsElement> desktopSession = CommonUtil.NewDesktopSession();
				while (i < 30000)
				{
					System.Threading.Thread.Sleep(1000);
					string TabElement = desktopSession.FindElementByAccessibilityId("com.squirrel.Teams.Teams").GetAttribute("Name");
					string[] TabWordss = TabElement.Split(" ");
					Console.WriteLine("Browser count: {0}", TabWordss[3]);
					if (TabWordss[3] == "1")
					{
						desktopSession.Dispose();
						return true;
					}
					else
					{
						i += 1000;
						continue;
					}
				}
				desktopSession.Dispose();
				return false;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "WaitUntilVideoCallIsTerminated");
				return false;
			}
		}
	}
}
