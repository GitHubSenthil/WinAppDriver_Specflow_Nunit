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


//v3.2
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
	public class TeamsWindow
	{
		private readonly WinAppDriver _driver;
		Reporting reportLine;
		private static ISpecFlowOutputHelper _outHelper;
		private readonly ConfigurationDriver _configurationDriver;
		//private readonly Action _action;

		public TeamsWindow(WinAppDriver driver, ISpecFlowOutputHelper outHelper, ConfigurationDriver configurationDriver)
		{
			// _driver = driver.Current;
			_driver = driver;
			reportLine = new Reporting(outHelper);

			_configurationDriver = configurationDriver;
		}

		public static class ExecTime
		{
			public static string sExecTime;
		}

		public void SendEnterKey()
		{
			Actions action = new Actions(_driver.Current);
			action.SendKeys(Keys.Enter);
			action.Perform();
		}

		public Boolean PrepAndOpenCustomDialog(string sGroupName)
		{
			try
			{
				Console.WriteLine(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItem).Replace("#GROUPNAME#", sGroupName));
				if (_driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItem).Replace("#GROUPNAME#", sGroupName)).Count > 0)
				{
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItemOptionButton).Replace("#GROUPNAME#", sGroupName)).Click();
					System.Threading.Thread.Sleep(1000);
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItemOptionDelete).Replace("#GROUPNAME#", sGroupName)).Click();
					WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItemDeleteDialog).Replace("#GROUPNAME#", sGroupName), _driver.Current);
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItemDeleteDialogCheckbox)).Click();
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Teams_ListItemDeleteDialogDeleteButton).Click();
					System.Threading.Thread.Sleep(3000);
				}
				CommonUtil.RetryClickUntilTargetAvailable(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ManageButton), CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_TeamCreateButton), _driver.Current);
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_TeamCreateButton)).Click();
				System.Threading.Thread.Sleep(1000);
				return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_CreateTeam), _driver.Current);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "Prep_And_Open_CustomDialog");
				return false;
			}
		}

		public Boolean SelectScratchOption(string sGroupType)
		{
			try
			{
				switch (sGroupType.ToUpper())
				{
					case "SCRATCH":
						_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Teams_CustomDialog_FromScratchButton).Click();
						break;
				}
				return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_GroupType), _driver.Current);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "SelectScratchOption");
				return false;
			}

		}

		public Boolean SelectPublication(string sPublicType)
		{
			try
			{
				string sType = sPublicType.ToLower();
				switch (sType)
				{
					case "private":
						_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Teams_CustomDialog_Private).Click();
						break;
					case "public":
						_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Teams_CustomDialog_Public).Click();
						break;
				}
				string text = CommonUtil.ConvertLang(sType);
				string path = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_TeamDetail);
				Console.WriteLine(path.Replace("#PUBLICTYPE#", text));
				return WaitUtil.WaitForElementAvailability(path.Replace("#PUBLICTYPE#", text), _driver.Current);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "SelectPublication");
				return false;
			}
		}

		public Boolean CustomizeTeamDetails(string sGroupName)
		{
			try
			{
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Teams_CustomDialog_TemaDetail_Title).Click();
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Teams_CustomDialog_TemaDetail_Title).SendKeys(sGroupName);
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_TeamDetail_Desc)).Click();
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_TeamDetail_Desc)).SendKeys("This is a [" + sGroupName + "] group.");
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_TeamDetail_CreateButton)).Click();
				return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_MemberEdit_Field).Replace("#GROUPNAME#", sGroupName), _driver.Current, 30000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "Customize_TeamDetails");
				return false;
			}
		}

		public string AddTeamsMember(string[] sGroupUsers, string sGroupName)
		{
			try
			{
				WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_MemberEdit_Field).Replace("#GROUPNAME#", sGroupName), _driver.Current);
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_MemberEdit_Field).Replace("#GROUPNAME#", sGroupName)).Click();
				for (int i = 0; i < sGroupUsers.Length; i++)
				{
					string sMember = sGroupUsers[i].Split("@")[0];
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_MemberEdit_Field).Replace("#GROUPNAME#", sGroupName)).SendKeys(sMember);
					System.Threading.Thread.Sleep(1000);
					Console.WriteLine(TeamsAppObjects.xpath_Teams_CustomDialog_MemberEdit_SearchAutolist.Replace("#USERNAME#", sMember.ToUpper()));
					_driver.Current.FindElementByXPath(TeamsAppObjects.xpath_Teams_CustomDialog_MemberEdit_SearchAutolist.Replace("#USERNAME#", sMember.ToUpper())).Click();
				}
				System.Threading.Thread.Sleep(3000);
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_MemberEdit_AddButton)).Click();
				System.Threading.Thread.Sleep(10000);
				return _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Teams_CustomDialog_MemberEdit_AddMemberList).FindElementsByXPath("//ListItem").Count.ToString();
				//return _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_MemberEdit_AddMemberList)).Count.ToString();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "Add_TemasMember");
				return "0";
			}
		}

		public Boolean CompleteCustomizeAndVerify(string sGroupName)
		{
			try
			{
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_CustomDialog_MemberEdit_CloseButton)).Click();
				return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItem).Replace("#GROUPNAME#", sGroupName), _driver.Current);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "Complete_CustomizeGroup_And_Verify");
				return false;
			}
		}

		public void OpenChannel(string sChannelName, string sCategory)
		{
			try
			{
				string channelXpath_parent = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItem).Replace("#GROUPNAME#", sChannelName) + "/parent::*";
				string channelXpath = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItem).Replace("#GROUPNAME#", sChannelName);
				string SubChannelXpath = "//following-sibling::TreeItem[@Name='#CATEGORY#']";
				SubChannelXpath = channelXpath_parent + SubChannelXpath.Replace("#CATEGORY#", sCategory);
				Console.WriteLine(channelXpath_parent);
				Console.WriteLine(channelXpath);
				Console.WriteLine(SubChannelXpath);
				if (WaitUtil.WaitForElementAvailability(SubChannelXpath, _driver.Current, 2 * 1000))
				{
					_driver.Current.FindElementByXPath(SubChannelXpath).Click();
				}
				else
				{
					_driver.Current.FindElementByXPath(channelXpath).Click();
					_driver.Current.FindElementByXPath(SubChannelXpath).Click();
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "Complete_CustomizeGroup_And_Verify");
			}
		}

		public void BeginConversation()
		{
			try
			{
				if (WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_Teams_NewConversationButton, _driver.Current))
				{
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Teams_NewConversationButton).Click();
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "BeginConversation");
			}
		}

		
		public void SendANewPostWithMention(string sUserName, string sMessageText)
		{
			try
			{
				var textfield = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_TextField));
				var sMentionText = "@" + sUserName;
				textfield.Click();
				textfield.SendKeys(sMentionText);
				System.Threading.Thread.Sleep(1000);
				textfield.SendKeys(" ");
				// in future, we need to improve this for dynamic way.
				System.Threading.Thread.Sleep(5000);
				textfield.SendKeys(sMessageText);
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Teams_SendButton).Click();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "SendANewPostWithMention");
			}
		}

		public void ReplyMessage(string text, string user = null, string time = null)
		{
			try
			{
				if (user == null || time == null)
				{
					var list = _driver.Current.FindElementsByXPath(TeamsAppObjects.xpath_Teams_TargetPost);
					user = list[list.Count - 1].FindElementsByXPath("//Text")[0].Text;
					time = list[list.Count - 1].FindElementsByXPath("//Text")[1].Text;
				}
				var target = _driver.Current.FindElementByXPath(TeamsAppObjects.xpath_Teams_TargetPost.Replace("#USERID#", user).Replace("#TIME#", time));

				try
				{
					target.FindElementByXPath(TeamsAppObjects.xpath_Teams_ReplyButton).Click();
				}
				catch (WebDriverException)
				{
					target.FindElementByXPath(TeamsAppObjects.xpath_Teams_ReplyField).Click();
				}
				System.Threading.Thread.Sleep(1000);
				target.FindElementByXPath(TeamsAppObjects.xpath_Teams_ReplyField).SendKeys(text);
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Teams_SendButton).Click();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "ReplyMessage");
			}
		}

		public string VerifyPostedMessage()
		{
			try
			{
				//FindElementsByXPath("//Text")[0].Text); //user
				//FindElementsByXPath("//Text")[1].Text); //Time
				var list = _driver.Current.FindElementsByXPath(TeamsAppObjects.xpath_Teams_TargetPost);
				return list[list.Count - 1].FindElementsByXPath("//Text")[2].Text;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyPostedMessage");
				return e.ToString();
			}
		}

		public string VerifyPostedMentionTag()
		{
			try
			{
				var list = _driver.Current.FindElementsByXPath(TeamsAppObjects.xpath_Teams_TargetMessageList);
				Console.WriteLine(list[list.Count - 1].FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_MentionTag)).Text);
				return list[list.Count - 1].FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_MentionTag)).Text;
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyPostedMessage");
				return e.ToString();
			}
		}

		public Boolean VerifyVideoCallButtonDisplay()
		{
			try
			{
				return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_MeetNow), _driver.Current);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyVideoCallButtonDisplay");
				return false;
			}
		}

		public void StartVideoCallFromTopBar()
		{
			try
			{
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_MeetNow)).Click();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "StartVideoCallFromTopBar");
			}
		}

		public void OpenAddNewUserOption(string sGroupName)
		{
			try
			{
				//Teams option select
				Console.WriteLine(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItem).Replace("#GROUPNAME#", sGroupName));
				if (_driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItem).Replace("#GROUPNAME#", sGroupName)).Count > 0)
				{
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItemOptionButton).Replace("#GROUPNAME#", sGroupName)).Click();
					System.Threading.Thread.Sleep(1000);
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItemOptionAddUser).Replace("#GROUPNAME#", sGroupName)).Click();
				}
				System.Threading.Thread.Sleep(2000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "OpenAddNewUserOption");
			}
		}

		public void OpenAddNewUserFromManagementView(string sGroupName)
		{
			try
			{
				//Teams option select
				Console.WriteLine(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItem).Replace("#GROUPNAME#", sGroupName));
				if (_driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItem).Replace("#GROUPNAME#", sGroupName)).Count > 0)
				{
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItemOptionButton).Replace("#GROUPNAME#", sGroupName)).Click();
					System.Threading.Thread.Sleep(1000);
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItemOptionManageTeam).Replace("#GROUPNAME#", sGroupName)).Click();
				}
				System.Threading.Thread.Sleep(2000);
				WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Temas_ManageTeams_AddUserButton), _driver.Current);
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Temas_ManageTeams_AddUserButton)).Click();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "OpenAddNewUserFromManagementView");
			}
		}

		public Boolean VerifyMemberInTeams(string[] sUsers, string sGroupName)
		{
			try
			{
				if (!WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Temas_ManageTeams_AddUserButton), _driver.Current))
				{
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItemOptionButton).Replace("#GROUPNAME#", sGroupName)).Click();
					System.Threading.Thread.Sleep(1000);
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ListItemOptionManageTeam).Replace("#GROUPNAME#", sGroupName)).Click();
				}
				WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ManageTeams_MemberlistButton), _driver.Current);
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ManageTeams_MemberlistButton)).Click();
				int count = _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ManageTeams_Menberlists)).Count;
				Boolean result = false;
				if (count > 0)
				{
					for (int i = 0; i < count; i++)
					{
						for (int n = 0; n < sUsers.Length; n++)
						{
							string[] texts = _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_ManageTeams_Menberlists))[i].Text.Split(", ");
							if (texts[0] == sUsers[n])
							{
								result = true;
								Console.WriteLine(texts[0] + "<<------->>" + sUsers[n] + " MATCH!!!!!!!");
								break;
							}
						}
					}
					return result;
				}
				else
				{
					return result;
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyMemberInTeams");
				return false;
			}
		}

		//File Attachment
		public Boolean UploadAFile(string sFileType, string sStorageType)
		{
			System.Threading.Thread.Sleep(2000);
			String path = System.Environment.CurrentDirectory.Split("bin")[0].TrimEnd('\\');
			Console.WriteLine(path);
			//var items = _driver.Current.FindElementsByXPath(TeamsAppObjects.xpath_Teams_TargetMessageList);
			string sFilePath = null;
			switch (sFileType)
			{
				case "TXT":
					sFilePath = path + _configurationDriver.Configuration["TestFileDatatxt"];
					break;
				case "PNG":
					sFilePath = path + _configurationDriver.Configuration["TestFileDatapng"];
					break;
				case "JPEG":
					sFilePath = path + _configurationDriver.Configuration["TestFileDatajpeg"];
					break;
				default:
					sFilePath = path + _configurationDriver.Configuration[sFileType];
					break;
			}
			try
			{
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_AttachmentFile_Button_Button)).Click();
				System.Threading.Thread.Sleep(2000);
				if (sStorageType == "PC")
				{
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AttachmentFile_Item.Replace("#TYPE#", "Upload from my computer"))).Click();
				}
				else
				{
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AttachmentFile_Item).Replace("#TYPE#", "OneDrive")).Click();
				}
				System.Threading.Thread.Sleep(5000);
				//Copy the value in Clipboard
				CommonUtil.ClipBoard_Copying(sFilePath);
				//Paste the value in the field
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_FilePath_TextField)).SendKeys(Keys.Control + "v");

				reportLine.TakeScreenShot(_driver.Current, "FilePath_Input");
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_OpenButton_FileDialog)).Click();
				//System.Threading.Thread.Sleep(3000);
				WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_FileReplace_Button), _driver.Current, 5 * 1000);
				if (_driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_FileReplace_Button)).Count > 0)
				{
					_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_FileReplace_Button)).Click();
				}
				System.Threading.Thread.Sleep(5000);
				//return items[items.Count - 1].FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_Uploaded_File)).Displayed;
				return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_Uploaded_File), _driver.Current, 5 * 1000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "Duplication_dialog");
				return false;
			}
		}

		public void ClickChannelMessgaeMoreOptions(string sUser)
		{
			string sSentMessages = "//Group[@Name=\"message list\"]//Text[contains(@Name,\"#USER#\")]";

			///Group[@Name=\"message list\"] Group[@AutomationId=\"messageBody\"]
			sSentMessages = sSentMessages.Replace("#USER#", sUser);
			var eleSentText = _driver.Current.FindElementsByXPath(sSentMessages);
			Console.WriteLine("Count" + eleSentText.Count);

			//WaitUtil.WaitForElementAvailabilityByScrollUp(sSentText_xpath, sSentMessages, _driver.Current);

			Actions actMouseOver = new Actions(_driver.Current);
			actMouseOver.MoveToElement(eleSentText[eleSentText.Count - 1]).Build().Perform();
			System.Threading.Thread.Sleep(6000);
			_driver.Current.FindElementByXPath("//Button[@Name=\"More options. Use left and right arrow keys to navigate.\"]//Image").Click();
			System.Threading.Thread.Sleep(2000);
			reportLine.TakeScreenShot(_driver.Current, "Channel_Clicked");

		}

		public void TypeMessageinChannel(string sMessage)
		{
			try
			{
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_TextField)).Click();
				_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Teams_TextField)).SendKeys(sMessage);
				System.Threading.Thread.Sleep(1000);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "TypeAMessage_Channel");
			}
		}

		public void ClickSendMessage()
		{
			try
			{
				System.Threading.Thread.Sleep(1000);
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Teams_SendButton).Click();
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "ClickSendMessage");
			}
		}

		public Boolean VerifyMessagewithAttachmentinChannel(string sUser, string sMessage, string attachment)
		{
			bool bMessageCheck = false;
			string sSentMessages = TeamsAppObjects.xpath_Channel_Message_Parent;
			sSentMessages = sSentMessages.Replace("#USER#", sUser);
			var eleList_SentText3 = _driver.Current.FindElementsByXPath(sSentMessages);
			var eleList_SentText = _driver.Current.FindElementsByXPath(sSentMessages);

			foreach (WindowsElement ele1 in eleList_SentText[eleList_SentText.Count-1].FindElementsByXPath("//child::Text"))
			{
				if (ele1.Text.Equals(sMessage))
                {
					bMessageCheck = true;
				}
			}
			if (bMessageCheck == true && eleList_SentText[eleList_SentText.Count - 1].FindElementByXPath("//child::DataItem").GetAttribute("Name").Contains(attachment))
				return true;
			return false;

		}
	}
}