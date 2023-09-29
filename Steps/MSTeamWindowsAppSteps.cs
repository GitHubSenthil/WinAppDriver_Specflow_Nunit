using System;
using System.Diagnostics;
using System.IO;
using TeamsWindowsApp.Driver;
using TeamsWindowsApp.Helper;
using FluentAssertions;
using TechTalk.SpecFlow;
using NUnit.Framework;
using TeamsWindowsApp.Pages;
using TechTalk.SpecFlow.Infrastructure;
using TeamsWindowsApp.Objects;
using System.Globalization;
using TeamsWindowsApp.Pages.Web;
using OpenQA.Selenium;

namespace TeamsWindowsApp.Steps
{
    [Binding]
    public class MSTeamWindowsAppSteps
    {

        private readonly TeamsLogin _loginPage;
        private readonly ChatWindow _chatWindow;
        private readonly ChatPage _chatPage;
        private readonly CallsWindow _callsWindow;
        private readonly TeamsWindow _teamsWindow;
        private readonly VideoCallView _videocallView;
        private readonly CalendarWindow _calendarWindow;
        private static Process _windriver;
        private static ISpecFlowOutputHelper _outHelper;
        private readonly ScenarioContext _scenarioContext;
        private readonly ConfigurationDriver _configurationDriver;

        public MSTeamWindowsAppSteps(TeamsLogin loginPage, ChatWindow chatWindow,
            CallsWindow callsWindow, TeamsWindow teamsWindow, VideoCallView videocallView,
            ChatPage chatPage, ScenarioContext scenarioContext, ConfigurationDriver configurationDriver,
            CalendarWindow calendarWindow)
        {
            _configurationDriver = configurationDriver;
            _loginPage = loginPage;
            _chatWindow = chatWindow;
            _callsWindow = callsWindow;
            _teamsWindow = teamsWindow;
            _videocallView = videocallView;
            _chatPage = chatPage;
            _scenarioContext = scenarioContext;
            _calendarWindow = calendarWindow;
        }

        [Given(@"Launch MS teams app and login with (.*)")]
        public void LoginMSTeams_App(string sUser)
        {
            _scenarioContext["Label1"] = sUser;
            _scenarioContext["User1"] = DataReader.getUserName(sUser);
            String executeResult = _loginPage.TeamsApp_LoginPage(sUser);
            executeResult.Should().Be("pass", "Launch and login status");
        }

        [Given(@"Launch MS teams app and login with1 (.*)")]
        public void LoginMSTeams_Ap1p(string sUser)
        {
            

        }

        [Given(@"Launch MS teams app andlogin with (.*) and userType (.*)")]
        public void LoginMSTeams_App1(string sUser, string sUsertype)
        {
            _scenarioContext["Label1"] = sUser;
            _scenarioContext["User1"] = DataReader.getUserName(sUser);
            String executeResult = _loginPage.TeamsApp_LoginPage(sUser);
            executeResult.Should().Be("pass", "Launch and login status");
        }

        [Then(@"Logout successfully from application")]
        public void LogoutMSTeams_App()
        {
            String executeResult = _loginPage.TeamsApp_Logout();
            executeResult.Should().Be("pass", "Launch and login status");
        }

        [Then(@"Send a (.*) message to (.*)")]
        public void ThenSendAMessageToUser(string sMessage, string sUser)
        {
            _chatWindow.SearchEmployee(DataReader.getUserName(sUser));
            _chatWindow.SendMessage(sMessage, _scenarioContext["User1"].ToString());
            _chatWindow.ClickSendAndVerifySendMessage().Should().Be(sMessage, "Message sent verification");
        }

        [Then(@"(.*) send messages to (.*) and both read and reply")]
        public void UsersReadAndReply(string sUser1, string sUser2, Table messageTable)
        {
            string user1 = DataReader.getUserName(sUser1);
            string user2 = DataReader.getUserName(sUser2);
            string fullName2 = DataReader.getFullName(sUser2);
            bool sStatus = _chatPage.SearchUser(user2);
            sStatus.Should().Be(true);

            foreach (TableRow row in messageTable.Rows)
            {

                string verifyMsg = _chatPage.ReadMessageFromUser(user1);
                verifyMsg.Should().Be(row[0]);
                _chatPage.TypeMessage(row[1]);
                verifyMsg = _chatPage.ClickSendAndVerify(row[1], user2); 
                verifyMsg.Should().Be(row[1]);
            }
        }


        [Then(@"Verify the logged in user (.*)")]
        public void VerifyUserValidation(string userLabel)
        {
            string email = DataReader.getEmail(userLabel);
            //_loginPage.VerifyLoggedinUser(userLabel).Should().Be(email, "Logged in user name validation");
            //String executeResult = _loginPage.VerifyLoggedinUser();
            //executeResult.Should().Be(email, "Logged in user name validation");

            _chatWindow.GoToChatOptions().Should().Be(true, "Chat option window opened");
        }


        [Then(@"Search the employee (.*) and verify the search list")]
        public void SearchEmployee(String user)
        {
            System.Threading.Thread.Sleep(3000);
            //_chatWindow.SearchEmployee(DataReader.getEmail(user));
            _chatWindow.SearchEmployee(DataReader.getFullName(user));
        }

        [Then(@"Search the file (.*) and verify the search list")]
        public void SearchFile(String file)
        {
            System.Threading.Thread.Sleep(3000);
            _chatWindow.SearchFile(file);
            _chatWindow.VerifySuggestedFile(file).Should().Be(true);
        }


        [StepDefinition(@"Send a Message and verify the sent message")]
        public void SendMessage(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _chatWindow.SendMessage(row[0], _scenarioContext["User1"].ToString());
                _chatWindow.ClickSendAndVerifySendMessage().Should().Be(row[0], "Message sent verification");
                
            }
        }

        //New for latest version
        [Then(@"Send a Message from file and verify the sent message")]
        public void SendMessageFromFileAndVerifyTheSentMessage(Table tableFileData)
        {
            foreach (TableRow row in tableFileData.Rows)
            {
                //string[] texts = new string[0];
                string path = System.Environment.CurrentDirectory.Split("bin")[0].TrimEnd('\\');
                string sFilePath = path + _configurationDriver.Configuration[row[0]];
                string[] lines = System.IO.File.ReadAllLines(sFilePath);
                foreach (string line in lines)
                {
                    Console.WriteLine("SENT MESSAGE: {0}", line);
                    _chatWindow.SendMessage(line.Replace("\\", Keys.Alt + Keys.NumberPad9 + Keys.NumberPad2 + Keys.Alt), _scenarioContext["User1"].ToString());
                    Console.WriteLine("EXPECTED MESSAGE: {0}", line);
                    _chatWindow.ClickSendAndVerifySendMessage().Should().Be(line, "Message sent verification");
                }
            }
        }

        [Then(@"Open (.*) app and verify")]
        public void openAppAndVerify(string app)
        {
            _loginPage.openApp(app);
            switch (app)
            {
                case "Activity":
                    _loginPage.VerifyOpenApp(app).Should().Be(CommonUtil.ConvertLang("Feed"));
                    break;
                case "Chat":
                    _loginPage.VerifyOpenApp(app).Should().Be(CommonUtil.ConvertLang("Chat"));
                    break;
                case "Teams":
                    _loginPage.VerifyOpenApp(app).Should().Be(CommonUtil.ConvertLang("Teams and Channels list"));
                    break;
                case "Calender":
                    _loginPage.VerifyOpenApp(app).Should().Be(CommonUtil.ConvertLang("Calender"));
                    break;
                case "Calls":
                    _loginPage.VerifyOpenApp(app).Should().Be(CommonUtil.ConvertLang("Call"));
                    break;
                case "Files":
                    _loginPage.VerifyOpenApp(app).Should().Be(CommonUtil.ConvertLang("Files list"));
                    break;
                case "Training":
                    _loginPage.VerifyOpenApp(app).Should().Be(CommonUtil.ConvertLang("Training app icon"));
                    _loginPage.VerifyLearningPathwayContents("Modern workplace").Should().Be(true);
                    break;
                case "other":
                    _loginPage.verifyOtherAppsWindow().Should().Be(true);
                    break;
            }
        }

        //v3.2
        [Then(@"Open each apps from left tabs")]
        public void openTabs(Table tableAppData)
        {
            foreach (TableRow row in tableAppData.Rows)
            {
                this.openAppAndVerify(row[0]);
            }
        }

        [Then(@"Send message with mention")]
        public void sendMessageWithMention(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _chatWindow.sendMessageWithUserMention(DataReader.getFullName(row[0]), row[1]);
                _chatWindow.ClickSendAndVerifyMentionedMessage().Should().Be(row[1], "Mention with message");
                _chatWindow.verifyMentionItem().Should().Be(DataReader.getUserName(row[0]), "Mention with message");
            }
        }

        [Then(@"Send a message with delivery option")]
        public void sendMessageWithUrgent(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _chatWindow.OpenAndVerifyDeliveryOption().Should().Be(true);
                if (row[0] == "Standard")
                {
                    _chatWindow.SelectDeliveryOption(row[0]);
                    _chatWindow.SendMessage(row[1], _scenarioContext["User1"].ToString());
                    _chatWindow.ClickSendAndVerifySendMessage(row[0]).Should().Be(row[1]);
                }
                else
                {
                    _chatWindow.ClickAndVerifyDeliveryOption(row[0]).Should().Be(CommonUtil.ConvertLang(row[0].ToUpper()) + "!");
                    _chatWindow.SendMessage(row[1], _scenarioContext["User1"].ToString());
                    //_chatWindow.ClickSendAndVerifySendMessage(row[0]).Should().Be(CommonUtil.ConvertLang(row[0].ToUpper()) + "!");
                    _chatWindow.ClickSendAndVerifySendMessage(row[0]).Should().Be(CommonUtil.ConvertLang(row[0].ToUpper()));
                }
            }
        }

        [Then(@"Send Emoji messages with text and verify")]
        public void SendEmojiMessagesWithTextAndVerify(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                string[] EmojiNames = row[1].Split(",");
                int EmojiNum = EmojiNames.Length;
                if (EmojiNum > 1)
                {
                    for (int i = 0; i < EmojiNum; i++)
                    {
                        _chatWindow.OpenEmojiOption();
                        _chatWindow.SelectEmoji(row[0], EmojiNames[i]);
                    }
                }
                else
                {
                    _chatWindow.OpenEmojiOption();
                    _chatWindow.SelectEmoji(row[0], row[1]);
                }
                if (row[2] == "")
                {
                    _chatWindow.ClickSendButton();
                }
                else
                {
                    _chatWindow.InputEmojiMessage(row[2]);
                    _chatWindow.ClickSendAndVerifySendMessage().Should().Be(row[2]);
                }
                _chatWindow.VerifySentEmojiCount().Should().Be(EmojiNum);
            }
        }

        [Then(@"Create Loop Component document and verify")]
        public void CreateLoopComponentDocumentAndVerify(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _chatWindow.CreateLoopComponent(row[0], row[1], row[2], row[3]);
                switch (row[0].ToLower())
                {
                    case "bulleted list":
                        _chatWindow.VerifyLoopComponentEditViewLoaded().Should().Be(CommonUtil.ConvertLang("Loop list (Draft)"));
                        break;
                    case "checklist":
                        _chatWindow.VerifyLoopComponentEditViewLoaded().Should().Be(CommonUtil.ConvertLang("Loop list (Draft)"));
                        break;
                    case "numbered list":
                        _chatWindow.VerifyLoopComponentEditViewLoaded().Should().Be(CommonUtil.ConvertLang("Loop list (Draft)"));
                        break;
                    case "paragraph":
                        _chatWindow.VerifyLoopComponentEditViewLoaded().Should().Be(CommonUtil.ConvertLang("Loop paragraph (Draft)"));
                        break;
                    case "table":
                        _chatWindow.VerifyLoopComponentEditViewLoaded().Should().Be(CommonUtil.ConvertLang("Loop table (Draft)"));
                        break;
                    case "task list":
                        _chatWindow.VerifyLoopComponentEditViewLoaded().Should().Be(CommonUtil.ConvertLang("Loop task list (Draft)"));
                        break;
                }
                _chatWindow.SendLoopComponentAndVerify().Should().Be(row[2]);
            }
        }

        [Then(@"Send format messages with text and verify")]
        public void SendFormatMessagesWithTextAndVerify(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _chatWindow.OpenTextFormat();
                _chatWindow.FormatText(row[0], row[1], row[2]);
                _chatWindow.ClickSendAndVerifySendMessage("format").Should().Be(row[2], "Message sent verification");
                _chatWindow.VerifyFormat(row[0], row[2]).Should().Be(true);
            }
        }

        [Then(@"Send format messages from file text and verify")]
        public void SendFormatMessagesFromFileTextAndVerify(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                string formatName = row[0];
                string formatOption = row[1];
                string fileName = row[2];
                string path = System.Environment.CurrentDirectory.Split("bin")[0].TrimEnd('\\');
                string sFilePath = path + _configurationDriver.Configuration[fileName];
                string[] lines = System.IO.File.ReadAllLines(sFilePath);
                foreach (string line in lines)
                {
                    _chatWindow.OpenTextFormat();
                    _chatWindow.FormatText(formatName, formatOption, line.Replace("\\", Keys.Alt + Keys.NumberPad9 + Keys.NumberPad2 + Keys.Alt));
                    _chatWindow.ClickSendAndVerifySendMessage("format").Should().Be(line, "Message sent verification");
                    _chatWindow.VerifyFormat(formatName, line);
                }
            }
        }

        [Then(@"Upload a (.*) file from PC")]
        public void uploadAFileFromPC(string sFileType)
        {
            _chatWindow.UploadAFile(sFileType, "PC").Should().Be(true);
        }

        [Then(@"Upload a (.*) file from Drive")]
        public void UploadAFileOnChatFromDrive(string sFileType)
        {
            _chatWindow.UploadAFile(sFileType, "Drive").Should().Be(true);
        }

        [Then(@"Remove a uploaded file from Chat text field")]
        public void RemoveUploadedFile()
        {
            _chatWindow.RemoveUploadFileFromTextField().Should().Be(true);
        }

        [Then(@"Send a file message and verify")]
        public void SendAFileMessageAndVerify()
        {
            _chatWindow.SendFileAndVerify().Should().Be(true);
        }


        // v3.2 - Need to merge
        [Then(@"Add users in chat and verify")]
        public void AddNewUserInChatAndVerify(Table tableMessageData)
        {
            _chatWindow.OpenAddUserDialog().Should().Be(true);
            _chatWindow.AddButtonVerification().Should().Be(false);
            foreach (TableRow row in tableMessageData.Rows)
            {
                _chatWindow.InputUserNameInAddUserDialog(DataReader.getUserName(row[0])).Should().Be(true);
            }
            _chatWindow.AddButtonVerification().Should().Be(true);
            int sUserCountOnAddButton = tableMessageData.Rows.Count + 2;
            _chatWindow.AddUserInChatAndVerify().Should().ContainAny(CommonUtil.ConvertLang("Draft More options"), CommonUtil.ConvertLang("existing"));
            _chatWindow.VerifyUserNumberOfDMGroupChat(sUserCountOnAddButton.ToString()).Should().Be(true);
        }

        [Then(@"Verify a (.*) call button is enabled")]
        public void VerifyCallButtonEnable(string type)
        {
            if (type.ToUpper() == "PHONE")
            {
                _chatWindow.VerifyPhoneCallButton("Unregulated").Should().Be(true);
            }
            else
            {
                _chatWindow.VerifyVideoCallButton("Unregulated").Should().Be(true);
            }
        }

        [Then(@"Verify a (.*) call button is disabled")]
        public void VerifyCallButtonDisable(string type)
        {
            if (type.ToUpper() == "PHONE")
            {
                _chatWindow.VerifyPhoneCallButton("regulated").Should().Be(false);
            }
            else
            {
                _chatWindow.VerifyVideoCallButton("regulated").Should().Be(false);
            }
        }

        [When(@"Start (.*) call on chat")]
        public void StartCallOnChat(string type)
        {
            if (type.ToUpper() == "PHONE")
            {
                _videocallView.StartPhoneCallOnChat();
            }
            else
            {
                _videocallView.StartVideoCallOnChat();
            }
        }

        [Then(@"Verify chat call dialog is (.*)")]
        public void VerifyChatCallDialog(string text)
        {
            if (text.ToLower() == "enabled")
            {
                _videocallView.VerifyChatCallDialog().Should().Be(true);
            }
            else
            {
                _videocallView.VerifyChatCallDialog().Should().Be(false);
            }
        }

        /*
        * Method Name: ReplyToMessage
        * Desc: Reply a message to one target message
        * Created by: Takuya 09/03/2022
        * Updated by: Takuya 09/03/2022
        * Update desc: 
        *  - Add a few steps for Reply operations
        * Changed files:
        *  - MSTeamsWindowAppSteps.cs
        *  - ChatWindow.cs
        *  - TeamsObjects.cs
        */
        [Then(@"Send reply message")]
        public void ReplyMessage(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                string[] ReplyComments = row[0].Split(", ");
                _chatWindow.SendMessage(row[1], _scenarioContext["User1"].ToString());
                _chatWindow.ClickReplyAndVerify(ReplyComments).Should().Be(true);
                _chatWindow.ClickSendAndVerifySendMessage().Should().Be(row[1]);
                _chatWindow.VerifyReplyMessage().Should().Be(true);
            }
        }

        [Then(@"Add reply item and remove reply item")]
        public void RemoveReplyItem(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                string[] ReplyComments = row[0].Split(", ");
                
                    _chatWindow.ClickReplyAndVerify(ReplyComments).Should().Be(true);
                
                _chatWindow.RemoveReplyItem().Should().Be(true);
            }
        }




        //Calls app steps (Need to be separate in future)
        /*
        * Method Name: -
        * Desc: All step action for Calls Window
        * Created by: Takuya 07/03/2022
        * Updated by: Takuya 07/03/2022
        * Update desc: 
        *  - Add a few steps for call window
        */
        [Then(@"Verify a call button as (.*)")]
        public void VerifyACallButtonAs(string sType)
        {

            switch (sType)
            {
                case "ENABLE":
                    _callsWindow.VerifyCallButton().Should().Be(true);
                    break;
                case "DISABLE":
                    _callsWindow.VerifyCallButton().Should().Be(false);
                    break;
            }
        }

        [Then(@"Search user (.*) from Calls app and verify")]
        public void SearchUserFromCallsAppAndVerify(string sUserName)
        {
            _callsWindow.SearchUserOnCallsWindowAndVerify(sUserName).Should().Be(true);
        }


        //Teams app steps (Need to be separate in future)
        /*
        * Method Name: -
        * Desc: All step action for Teams Window
        * Created by: Takuya 06/10/2022
        * Updated by: Takuya 06/10/2022
        * Update desc: 
        *  - Add a sending message step
        */
        [Then(@"Create a new Teams group asn verify")]
        public void CreateANewGroupAndVerify(Table tableProperties)
        {
            foreach (TableRow row in tableProperties.Rows)
            {
                string sGroupType = row[0];
                string sPublicType = row[1];
                string sGroupName = row[2];
                string[] sGroupUsers = DataReader.getEmail(row[3]).Split(",");
                _teamsWindow.PrepAndOpenCustomDialog(sGroupName).Should().Be(true);
                _teamsWindow.SelectScratchOption(sGroupType).Should().Be(true);
                _teamsWindow.SelectPublication(sPublicType).Should().Be(true);
                _teamsWindow.CustomizeTeamDetails(sGroupName).Should().Be(true);
                _teamsWindow.AddTeamsMember(sGroupUsers, sGroupName).Should().Be("1");
                _teamsWindow.CompleteCustomizeAndVerify(sGroupName).Should().Be(true);
            }
        }

        [Then(@"Add new user from Teams option")]
        public void AddNewUserFromTeamsOption(Table tableProperties)
        {
            foreach (TableRow row in tableProperties.Rows)
            {
                string sGroupName = row[0];
                string[] sGroupUsers = row[1].Split(",");
                string[] sUserNames = new string [sGroupUsers.Length];
                string[] sUserEmails = new string[sGroupUsers.Length];
                for (int i = 0; i < sGroupUsers.Length; i++)
                {
                    sUserEmails[i] = DataReader.getEmail(sGroupUsers[i]);
                    sUserNames[i] = DataReader.getUserName(sGroupUsers[i]);
                }
                _teamsWindow.OpenAddNewUserOption(sGroupName);
                _teamsWindow.AddTeamsMember(sUserEmails, sGroupName).Should().Be(sGroupUsers.Length.ToString());
                _teamsWindow.CompleteCustomizeAndVerify(sGroupName);
                _teamsWindow.VerifyMemberInTeams(sUserNames, sGroupName).Should().Be(true);
            }
            
        }

        [Then(@"Add new user from Teams management")]
        public void AddNewUserFromTeamsManagement(Table tableProperties)
        {
            foreach (TableRow row in tableProperties.Rows)
            {
                string sGroupName = row[0];
                string[] sGroupUsers = row[1].Split(",");
                string[] sUserNames = new string[sGroupUsers.Length];
                string[] sUserEmails = new string[sGroupUsers.Length];
                for (int i = 0; i < sGroupUsers.Length; i++)
                {
                    sUserEmails[i] = DataReader.getEmail(sGroupUsers[i]);
                    sUserNames[i] = DataReader.getUserName(sGroupUsers[i]);
                }
                _teamsWindow.OpenAddNewUserFromManagementView(sGroupName);
                _teamsWindow.AddTeamsMember(sUserEmails, sGroupName).Should().Be(sGroupUsers.Length.ToString());
                _teamsWindow.CompleteCustomizeAndVerify(sGroupName);
                _teamsWindow.VerifyMemberInTeams(sUserNames, sGroupName).Should().Be(true);
            }

        }

        [When(@"Open (.*) channel and select (.*) category")]
        public void OpenChannel(string channelName, string category)
        {
            _teamsWindow.OpenChannel(channelName, category);
        }

        [When(@"Send a New post in channel")]
        public void SendANewPostInChannel(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _teamsWindow.BeginConversation();
                _teamsWindow.TypeMessageinChannel(row[0]);
                _teamsWindow.ClickSendMessage();
            }
        }

        [Then(@"Send a New post with mention in channel and verify")]
        public void SendNewPostWithMentionInChannelAndVerify(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _teamsWindow.BeginConversation();
                _teamsWindow.SendANewPostWithMention(DataReader.getFullName(row[0]), row[1]);
                _teamsWindow.VerifyPostedMessage().Should().Be(row[1]);
                _teamsWindow.VerifyPostedMentionTag().Should().Contain(DataReader.getUserName(row[0]));
            }
        }

        [When(@"Send a reply post in channel")]
        public void SendAReplyPostInChannel(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _teamsWindow.ReplyMessage(row[0]);
            }
        }

        [Then(@"Verify the posted message in channel")]
        public void VerifyThePostedMessageInChannel(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _teamsWindow.VerifyPostedMessage().Should().Be(row[0], "Message sent verification in channel");
            }
        }


        [Then(@"Verify video call button is (.*) on channel top bar")]
        public void VerifyVideoCallButtonOnChannelTopBar(string condition)
        {
            if (condition.ToLower() == "enable")
            {
                _teamsWindow.VerifyVideoCallButtonDisplay().Should().Be(true);
            }
            else
            {
                _teamsWindow.VerifyVideoCallButtonDisplay().Should().Be(false);
            }
        }

        [When(@"Start video call from channel top bar")]
        public void StartVideoCallFromChannelTopBar()
        {
            _teamsWindow.StartVideoCallFromTopBar();
        }

        //Video call
        /*
        * Method Name: -
        * Desc: All steps for Video call Window
        * Created by: Takuya 06/10/2022
        * Updated by: Takuya 06/10/2022
        * Update desc: 
        *  - Add basic video call steps
        */
        [Then(@"Verify video call dialog of (.*) is displayed")]
        public void VerifyVideoCallDialogDisplayed(string title)
        {
            string verifyText = CommonUtil.ConvertLang("Choose your video and audio options for Meeting in \"" + title + "\" ");
            _videocallView.VerifyVideoCallDialogDisplay().Should().Be(verifyText);
        }

        [When(@"Join to MTG video call")]
        public void JoinToMTG()
        {
            _videocallView.JoinMTG();
        }

        [When(@"End a MTG video call")]
        public void FinishAMTG()
        {
            _videocallView.EndMTG();
        }

        [When(@"Hang up a MTG")]
        public void HangUpMTG()
        {
            _videocallView.HungUpMTGByMyself();
        }

        [Then(@"Close video call window")]
        public void CloseVideoCallWindow()
        {
            _videocallView.CancelMTG();
        }

        [Then(@"Verify camera should be terned (.*)")]
        public void CameraShouldBeTerned(string setting)
        {
            //When we test actually, we need to switch the result as true.
            if (setting.ToUpper() == "ON")
            {
                _videocallView.VerifyCameraSetting().Should().Be(true);
            }
            else
            {
                _videocallView.VerifyCameraSetting().Should().Be(false);
            }
        }

        [Then(@"Verify background setting should be terned (.*) on setting dialog")]
        public void BackgroundSettingShouldBeTerned(string setting)
        {
            //When we test actually, we need to switch the result as true.
            if (setting.ToUpper() == "ON")
            {
                _videocallView.VerifyBackgroundEnabledSetting().Should().Be(true);
            }
            else
            {
                _videocallView.VerifyBackgroundDisabledSetting().Should().Be(true);
            }
        }

        [When(@"Change background on setting dialog to (.*)")]
        public void ChangeBackgroundOnSettingDialog(string bgName)
        {
            _videocallView.ClickBakcGroundSettingButton();
            _videocallView.SelectBakcGroundButtonOnSetting(bgName);
        }

        [When(@"Camera setting turned (.*)")]
        public void CareraTurnedONOFF(string setting)
        {
            if (setting.ToUpper() == "ON")
            {
                if (!_videocallView.VerifyCameraSetting())
                {
                    _videocallView.CameraSettingChange();
                }
                _videocallView.VerifyCameraSetting().Should().Be(true);
            }
            else
            {
                if (_videocallView.VerifyCameraSetting())
                {
                    _videocallView.CameraSettingChange();
                }
                _videocallView.VerifyCameraSetting().Should().Be(false);
            }
        }

        [Then(@"Verify (.*) audio setting available")]
        public void VerifyAudioSettingAvailable(string type)
        {
            _videocallView.SelectAudioSetting(type);
            _videocallView.VerifyAudioSetting(type).Should().Be(true);
            _videocallView.VerifyMicSpeakerSetting("mic").Should().Be(false); // need to change
            _videocallView.VerifyMicSpeakerSetting("speaker").Should().Be(true);
        }

        //Open menus
        [When(@"Open More Options")]
        public void OpenMoreOptions()
        {
            _videocallView.OpenMoreOption();
        }

        [When(@"Open participant view")]
        public void OpenParticipantView()
        {

        }

        [Then(@"Verify buttons are available")]
        public void VerifyButtonIsAvailable(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _videocallView.OptionButtonAvailable(row[0]).Should().Be(true);
            }
        }


        [Then(@"Verify buttons are not available")]
        public void VerifyButtonIsNotAvailable(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _videocallView.OptionButtonAvailable(row[0]).Should().Be(false);
            }
        }

        [Then(@"Verify buttuns are turnning on/off")]
        public void VerifyButtunIsTurnningOff(Table tableData)
        {
            foreach (TableRow row in tableData.Rows)
            {
                _videocallView.VerifyButtunIsTurningOnOff(row[0]).Should().Be(row[1]);
            }
        }

        [When(@"Wait video call connection")]
        public void WaitVideoCallConnection()
        {
            _videocallView.WaitVideoCallConnection();
        }


        [When(@"Change background setting to (.*)")]
        public void ChangeBackgroundSetting(string bgName)
        {
            _videocallView.MouseOverToCameraButton();
            _videocallView.OpenBackgroundSetting();
            _videocallView.ApplyBackgroundSetting(bgName);
        }

        [Then(@"Verify the background (.*) is applied")]
        public void VerifyTheBackgroundIsApplied(string bgName)
        {
            _videocallView.VerifyBackgroundIsSelected(bgName).Should().Be(true);
        }

        [When(@"Close background setting view")]
        public void CloseBackgroundSettingView()
        {
            _videocallView.CloseRightPane();
        }

        //Bypass lobby
        [When(@"Set bypass lobby as (.*)")]
        public void SetBypassLobbyAs(string setting)
        {
            _videocallView.OpenMoreOption();
            _videocallView.OpenMeetingSettingView();
            _videocallView.SelectBypassSetting(setting);
        }

        [When(@"Save meeting setting")]
        public void SaveMeetingSetting()
        {
            _videocallView.SaveMeetingSetting();
        }

        [When(@"Close metting setting view")]
        public void CloseMettingSettingView()
        {
            _videocallView.CloseRightPane();
        }

        [Then(@"Verify meeting setting is saved")]
        public void VerifyMeetingSettingIsSaved()
        {
            _videocallView.VerifyMeetingSetting().Should().Be(true);
        }

        //MTG lock
        [When(@"Lock the MTG")]
        public void LockTheMTG()
        {
            _videocallView.OpenParticipantView();
            _videocallView.OpenParticipantViewMoreAction();
            _videocallView.LockTheMTG();
        }

        [Then(@"Verify MTG is locked")]
        public void VerifyMTGIsLocked()
        {
            _videocallView.VerifyLockUnlockAlart().Should().Be(true);
            _videocallView.VerifyMTGLockUnlockText().Should().Be(CommonUtil.ConvertLang("This meeting is locked. No one else can join. Close"));
        }

        [When(@"Unlock the MTG")]
        public void UnlockTheMTG()
        {
            _videocallView.OpenParticipantView();
            _videocallView.OpenParticipantViewMoreAction();
            _videocallView.UnlockAMTG();
        }

        [Then(@"Verify MTG is unlocked")]
        public void VerifyMTGIsUnlocked()
        {
            _videocallView.VerifyLockUnlockAlart().Should().Be(true);
            _videocallView.VerifyMTGLockUnlockText().Should().Be(CommonUtil.ConvertLang("This meeting is unlocked. People can join. Close"));
        }

        //ScreenShare
        [When(@"Open screen share view")]
        public void OpenScreenShareView()
        {
            _videocallView.OpenShareScreenView();
        }

        [When(@"Click share full screen")]
        public void ClickShareFullScreen()
        {
            _videocallView.ShareFullScreen();
        }

        [Then(@"Verify Screen has shared")]
        public void VerifyScreenHasShared()
        {
            _videocallView.VerifyScreenShareControlWindow().Should().Be(true);
        }

        [When(@"Stop share screen")]
        public void StopSahreScreen()
        {
            _videocallView.StopScreenSharing();
        }

        [Then(@"Verify screen is not shared")]
        public void VerifyScreenIsNotShared()
        {
            _videocallView.VerifyNotScreenSharing().Should().Be(true);
        }

        //Recording MTG
        [When(@"(.*) record MTG")]
        public void StartStopRecordingMTG(string type)
        {
            _videocallView.StartStopRecording(type);
        }


        [Then(@"Verify recording MTG has (.*)")]
        public void VerifyRecordingMTGHas(string type)
        {
            if (type.ToLower() == "started")
            {
                _videocallView.VerifyRecordingButton().Should().Be(CommonUtil.ConvertLang("Stop recording"));
            }
            else
            {
                _videocallView.VerifyRecordingButton().Should().Be(CommonUtil.ConvertLang("Start recording"));
            }
        }

        [Then(@"Verify transcript has (.*)")]
        public void VerifyTranscriptHas(string type)
        {
            if (type.ToLower() == "started")
            {
                _videocallView.VerifyTranscription(type.ToLower()).Should().Contain(CommonUtil.ConvertLang("started transcription"));
            }
            else
            {
                _videocallView.VerifyTranscription(type.ToLower()).Should().Contain(CommonUtil.ConvertLang("stopped transcription"));
            }

        }

        [When(@"User search for (.*) (.*) user and send message with devlivery option")]
        public void sendMessageToDifferentUsers(string sSearchUser, string sSearchType, Table sTableData)
        {
            //Search User
            _chatWindow.SearchEmployee(DataReader.getFullName(sSearchUser));

            foreach (TableRow row in sTableData.Rows)
            {
                _chatWindow.OpenAndVerifyDeliveryOption().Should().Be(true);
                if (row[0] == "Standard")
                {
                    _chatWindow.ClickAndVerifyDeliveryOption(row[0]).Should().Be(CommonUtil.ConvertLang(""));
                    _chatWindow.SendMessage(row[1], DataReader.getUserName(sSearchUser));
                    _chatWindow.ClickSendAndVerifySendMessage(row[0]).Should().Be(row[1]);
                }
                else
                {
                    _chatWindow.ClickAndVerifyDeliveryOption(row[0]).Should().Be(CommonUtil.ConvertLang(row[0].ToUpper()) + "!");
                    _chatWindow.SendMessage(row[1], DataReader.getUserName(sSearchUser));
                    _chatWindow.ClickSendAndVerifySendMessage(row[0]).Should().Be(CommonUtil.ConvertLang(row[0].ToUpper()) + "!");
                }
            }

        }

        [Then(@"User search for (.*) (.*) user and Upload a (.*) file from PC")]
        public void FileShare(string sSearchUser, string sSearchType, string sFileType)
        {
            _chatWindow.UploadAFile(sFileType, "PC").Should().Be(true);
            _chatWindow.SendFileAndVerify().Should().Be(true);
        }


        [Then(@"User (.*) check with other (.*) user check call, video call and screenshare call options are (.*)")]
        public void VerifyUserOption(string sUserType, string sSearchUser, string sOptions)
        {
            if ((sUserType.ToUpper().Equals("REGULATED") && sSearchUser.ToUpper().Equals("REGULATED")) ||
                (sUserType.ToUpper().Equals("REGULATED") && sSearchUser.ToUpper().Equals("UNREGULATED")))
            {
                _chatWindow.VerifyPhoneCallButton(sUserType + " to Call " + sSearchUser).Should().Be(false);
                _chatWindow.VerifyVideoCallButton(sUserType + " to Video call " + sSearchUser).Should().Be(false);
            }
            else if (sUserType.ToUpper().Equals("UNREGULATED") && sSearchUser.ToUpper().Equals("UNREGULATED"))
            {
                _chatWindow.VerifyPhoneCallButton(sUserType + " to Call " + sSearchUser).Should().Be(true);
                _chatWindow.VerifyVideoCallButton(sUserType + " to Video call " + sSearchUser).Should().Be(true);
            }
            else
            {
                _chatWindow.VerifyPhoneCallButton(sUserType + " to Call " + sSearchUser).Should().Be(true);
                _chatWindow.VerifyVideoCallButton(sUserType + " to Video call " + sSearchUser).Should().Be(true);
            }
        }



        //Pilot version testing
        [When(@"User check the chat features of (.*) check icons, chat, file share and delivery option")]
        public void Pilot_CheckChatFeatures(string userType)
        {
            _chatWindow.SearchEmployee(DataReader.getFullName("user4"));
            string sData_UserType = DataReader.getUserType("user4");

            if (userType.ToUpper().Equals(sData_UserType.ToUpper()))
            {
                if (userType.ToUpper().Equals("REGULATED"))
                {
                    _chatWindow.VerifyPhoneCallButton(userType).Should().Be(false);
                    _chatWindow.VerifyVideoCallButton(userType).Should().Be(false);
                }
                else
                {
                    _chatWindow.VerifyPhoneCallButton(userType).Should().Be(true);
                    _chatWindow.VerifyVideoCallButton(userType).Should().Be(true);
                }
            }
            else
            {

            }
        }


        /// For interactive 2 user scripts
        /// Chat
        ///
        [When(@"Wait until the message (.*) receive from (.*)")]
        public void WaitUntilTheMessageReceive(string targetText, string user)
        {
            _chatWindow.WaitUntilGettingParticularMessage(targetText, DataReader.getUserName(user)).Should().Be(true);
        }

        [When(@"Wait until a new message receive from (.*)")]
        public void WaitUntilNewMessageReceive(string user)
        {
            _chatWindow.WaitUntilGettingMessage(DataReader.getUserName(user)).Should().Be(true);
        }

        [Then(@"Verify new message (.*) is displayed on (.*) chat")]
        public void VerifyNewMessageIsDisplayedOnChat(string text, string user)
        {
            _chatWindow.VerifyLastPostedMessage(DataReader.getUserName(user)).Should().Be(text);
        }

        [Then(@"(.*) Send a message and (.*) Receiver replies back")]
        public void UserWindowsAndWeb(string sender, string receiver, Table sTableData)
        {
            string sDateString, sTestData;
            string senderName = DataReader.getUserName(sender);
            string receiverName = DataReader.getFullName(receiver);
            _chatWindow.SearchEmployee(receiverName);
            _chatPage.SearchUser(senderName);
            foreach (TableRow row in sTableData.Rows)
            {
                //Sender switch case
                switch (row[0].ToUpper())
                {
                    case "TEXT":
                        //Send and Verify in Windows App
                        sDateString = DateTime.Now.ToString("HH:mm:ss dd");
                        sTestData = row[1] + " " + sDateString;
                        _chatWindow.SendMessage(sTestData, receiverName);
                        _chatWindow.ClickSendAndVerifySendMessage().Should().Be(sTestData, "Message sent verification");
                        System.Threading.Thread.Sleep(5000);
                        //Verify in Web Page
                        _chatPage.ReadMessageFromUserwithTime(sTestData, senderName).Should().Be(true);
                        break;
                    case "WORD_DOCUMENT":
                        //Send Word document in Windows App
                        sDateString = DateTime.Now.ToString("HH:mm:ss dd");
                        sTestData =  "File Attached " + sDateString;
                        _chatWindow.SendMessage(sTestData, receiverName);
                        _chatWindow.UploadAFile(row[1], "PC").Should().Be(true);
                        _chatWindow.SendFileAndVerify().Should().Be(true);
                        System.Threading.Thread.Sleep(5000);
                        //Verify Document in WebPage
                        _chatPage.ReadMessageFileFromUserwithTime(sTestData, row[1], senderName).Should().Be(true);
                        break;
                    case "EMOJI":
                        //Send Emoji in Windows App
                        _chatWindow.OpenEmojiOption();
                        _chatWindow.SelectEmoji(row[1].Split(":")[0], row[1].Split(":")[1]);
                        _chatWindow.ClickSendButton();
                        //Verify the Emoji in Web
                        _chatPage.ReadSingleEmojiFromUser(row[1].Split(":")[1], senderName);
                        break;
                    default:
                        break;
                }

                //Receiver switch case
                switch (row[2].ToUpper())
                {
                    case "TEXT":
                        //Sending text in Web app
                        sDateString = DateTime.Now.ToString("HH:mm:ss dd");
                        sTestData = row[3] + " " + sDateString;
                        _chatPage.TypeMessage(sTestData);
                        _chatPage.ClickSendAndVerify(sTestData, DataReader.getUserName(receiver));
                        System.Threading.Thread.Sleep(5000);
                        //Verify in Windows App
                        _chatWindow.VerifyLastPostedMessage(DataReader.getUserName(receiver)).Should().Be(sTestData);
                        break;
                    case "EMOJI":
                        int iEmojiCount = _chatWindow.VerifySentEmojiCount(row[3].Split(":")[1], DataReader.getUserName(receiver));
                        //Sending EMOJI in Web app
                        _chatPage.SendEmojiAndVerify(row[3].Split(":")[0], row[3].Split(":")[1]);
                        _chatPage.ClickSendButton().Should().Be(true);
                        //Verify the Emoji in Windows App
                        _chatWindow.VerifySentEmojiCount(row[3].Split(":")[1], DataReader.getUserName(receiver)).Should().Be(iEmojiCount);
                        break;
                }
            }
        }



        [Then(@"(.*) user Send a Message from (.*) and verify the sent message")]
        public void UserSendAMessageFromAndverifyTheSentMessage(string userType, string targetUser, Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                string SenderMessageType = row[0];
                string senderMessage = row[1];
                string ReceiverMessageType = row[2];
                string receiverMessage = row[3];
                string SenderEmojiType = "Smilies";
                string SenderEmojiName = "Grinning face with big eyes";
                string ReceiverEmojiType = "Smilies";
                string ReceiverEmojiName = "Grinning face with big eyes";

                void SingleTypeMethod(string type, string Message, string sEmojiType, string sEmojiName, int EmojiCount = 1)
                {
                    switch (type.ToUpper())
                    {
                        case "TEXT":
                            _chatWindow.SendMessage(Message, _scenarioContext["User1"].ToString());
                            _chatWindow.ClickSendAndVerifySendMessage().Should().Be(Message);
                            break;
                        case "EMOJI":
                            if (EmojiCount > 1)
                            {
                                for (int i = 0; i < EmojiCount; i++)
                                {
                                    _chatWindow.OpenEmojiOption();
                                    _chatWindow.SelectEmoji(sEmojiType, sEmojiName);
                                }
                            }
                            else
                            {
                                _chatWindow.OpenEmojiOption();
                                _chatWindow.SelectEmoji(sEmojiType, sEmojiName);
                            }
                            _chatWindow.ClickSendButton();
                            _chatWindow.VerifySentEmojiCount().Should().Be(EmojiCount);
                            break;
                        case "FILE":
                            _chatWindow.UploadAFile(Message, "PC").Should().Be(true);
                            _chatWindow.SendFileAndVerify().Should().Be(true);
                            break;
                        case "REPLY":
                            _chatWindow.ClickReplyAndVerify(Message.Split(", ")).Should().Be(true);
                            _chatWindow.ClickSendButton();
                            _chatWindow.VerifyReplyMessage().Should().Be(true);
                            break;
                    }
                }

                void InputOptionCommon(string option, string Message, string sEmojiType, string sEmojiName, string replyTarget = "")
                {
                    switch (option.ToUpper())
                    {
                        case "TEXT":
                            _chatWindow.SendMessage(Message, _scenarioContext["User1"].ToString());
                            break;
                        case "EMOJI":
                            _chatWindow.OpenEmojiOption();
                            _chatWindow.SelectEmoji(sEmojiType, sEmojiName);
                            break;
                        case "FILE":
                            _chatWindow.UploadAFile(Message, "PC").Should().Be(true);
                            break;
                        case "REPLY":
                            _chatWindow.ClickReplyAndVerify(replyTarget.Split(",")).Should().Be(true);
                            break;
                    }
                }

                void CommonOperation(string MessageType, string targetText, string UserEmojiType, string UserEmojiName, string replyTarget = "")
                {
                    string[] MessageTypes = MessageType.Split("_");
                    string MainOption = MessageTypes[0];
                    int MessageTypeaLength = MessageTypes.Length;
                    string sEmojiType = UserEmojiType;
                    string sEmojiName = UserEmojiName;
                    if (MainOption.ToUpper() == "EMOJI")
                    {
                        sEmojiType = targetText.Split(":")[0];
                        sEmojiName = targetText.Split(":")[1];
                    }

                    if (MessageTypeaLength == 1)
                    {
                        SingleTypeMethod(MainOption, targetText, sEmojiType, sEmojiName, sEmojiName.Split(",").Length);
                    }
                    else if (MessageTypeaLength > 1)
                    {
                        //Input main option
                        InputOptionCommon(MainOption, targetText, sEmojiType, sEmojiName);
                        //Input Sub option
                        for (int i = 0; i < MessageTypeaLength; i++)
                        {
                            if (i != 0 && MessageTypes[i].ToUpper() == "REPLY")
                            {
                                InputOptionCommon(MessageTypes[i], targetText, sEmojiType, sEmojiName, replyTarget);
                            }
                            else if (i != 0)
                            {
                                InputOptionCommon(MessageTypes[i], targetText, sEmojiType, sEmojiName);
                            }
                        }
                        //Send
                        switch (MainOption.ToUpper())
                        {
                            case "TEXT":
                                _chatWindow.ClickSendAndVerifySendMessage().Should().Be(targetText);
                                break;
                            case "EMOJI":
                                _chatWindow.ClickSendButton();
                                _chatWindow.VerifySentEmojiCount().Should().Be(sEmojiName.Split(":").Length);
                                break;
                            case "FILE":
                                _chatWindow.SendFileAndVerify().Should().Be(true);
                                break;
                        }
                        if (MessageTypes[MessageTypeaLength - 1].ToUpper() == "REPLY")
                        {
                            _chatWindow.VerifyReplyMessage().Should().Be(true);
                        }
                    }
                }

                ///Actual operation
                switch (userType.ToUpper())
                {
                    case "SENDER":
                        CommonOperation(SenderMessageType, senderMessage, SenderEmojiType, SenderEmojiName);
                        //Wait sender message
                        WaitUntilTheMessageReceive(receiverMessage, targetUser);
                        break;
                    case "RECEIVER":
                        WaitUntilTheMessageReceive(senderMessage, targetUser);
                        /// Reply message toward the sender message.
                        string[] messageTypes = ReceiverMessageType.Split("_");
                        if (Array.Exists(messageTypes, elem => elem.ToUpper() == "REPLY"))
                        {
                            CommonOperation(ReceiverMessageType, receiverMessage, ReceiverEmojiType, ReceiverEmojiName, senderMessage);
                        }
                        else
                        {
                            CommonOperation(ReceiverMessageType, receiverMessage, ReceiverEmojiType, ReceiverEmojiName);
                        }
                        break;
                }
            }
        }

        /// Interactive 2 user
        /// 
        /// Video call
        [When(@"Wait until attendee join")]
        public void WaitUntilAttendeeJoin()
        {
            _videocallView.WaitUntilAttendeeJoin().Should().Be(true);
        }

        [When(@"Wait until ringing the call")]
        public void WaitUntilRingingTheCallAndJoin()
        {
            _videocallView.WaitUntilRinging().Should().Be(true);
        }

        [When(@"Join the call as attendee by (.*) call")]
        public void JoinTheCallAsAttendeeBy(string callType)
        {
            _videocallView.JoinTheCallAsAttendeeBy(callType);
        }

        [Then(@"Wait Until video call is terminated")]
        public void WaitUntilVideoCallIsTerminated()
        {
            _videocallView.WaitUntilVideoCallIsTerminated().Should().Be(true);
        }

        [Then(@"Verify buttons availavility")]
        public void VerifyButtonsAvailavility(Table tableButtonData)
        {
            foreach (TableRow row in tableButtonData.Rows)
            {
                string buttonName = row[0];
                string buttonStatus = row[1];
                string buttonConfig = row[2];
                switch (buttonStatus.ToLower())
                {
                    case "enable":
                        _videocallView.OptionButtonAvailable(buttonName).Should().Be(true);
                        break;
                    case "disable":
                        _videocallView.OptionButtonAvailable(buttonName).Should().Be(false);
                        break;
                }
                if (buttonConfig != "")
                {
                    _videocallView.VerifyButtunIsTurningOnOff(buttonName).Should().Be(buttonConfig);
                }
            }
            System.Threading.Thread.Sleep(3000);
        }

        [When(@"Verify user option is enabled")]
        public void WhenVerifyUserOptionIsEnabled(Table sOptions)
        {
           _chatWindow.ClickSentMessageOptions(sOptions.Rows[0][0]);
            foreach (TableRow row in sOptions.Rows)
            {   
                _chatWindow.VerifySentMessageOptions(row[1]).Should().Be(Convert.ToBoolean(row[2]));
            }
        }

        [StepDefinition(@"Verify the left pane chat option for (.*)")]
        public void VerifyChatOptions(string sUser, Table sOptions)
        {
            _chatWindow.RightClickUserChat(DataReader.getUserName(sUser));
            foreach (TableRow row in sOptions.Rows)
            {
                _chatWindow.VerifyUserChatOptions(row[0]).Should().Be(Convert.ToBoolean(row[1]));
                
            }
        }

        [StepDefinition(@"Verify another (.*) sent chat options")]
        public void VerifySentUserChatOptions(string sUser, Table sOptions)
        {
            _chatWindow.ClickAnotherUserSentMessageOptions(DataReader.getUserName(sUser));
            foreach (TableRow row in sOptions.Rows)
            {
                _chatWindow.VerifySentMessageOptions(row[0]).Should().Be(Convert.ToBoolean(row[1]));

            }
        }

        [StepDefinition(@"Verify (.*) sent message options in channel")]
        public void VerifyTeamsChannelMessageOptions(string sUser, Table sOptions)
        {
            _teamsWindow.ClickChannelMessgaeMoreOptions(DataReader.getUserName(sUser));
            foreach (TableRow row in sOptions.Rows)
            {
                _chatWindow.VerifySentMessageOptions(row[0]).Should().Be(Convert.ToBoolean(row[1]));

            }
        }

        [StepDefinition(@"Send a new post in channel by (.*) with Attachment")]
        public void SendMessagewithAttachmentChannel(string sUser, Table sData)
        {
            foreach (TableRow row in sData.Rows)
            {
                _teamsWindow.BeginConversation();
                _teamsWindow.TypeMessageinChannel(row[0]);
                _teamsWindow.UploadAFile(row[1], "PC").Should().Be(true);
                _teamsWindow.ClickSendMessage();
                _teamsWindow.VerifyMessagewithAttachmentinChannel(DataReader.getUserName(sUser), row[0], row[1]).Should().Be(true);
            }
        }

        [When(@"Search the user (.*) from calls serach")]
        public void SearchTheUserFromCallsSearch(string user)
        {
            _callsWindow.SearchTheUserFromCallsSearchAndVerify(user).Should().Be(true);
        }

        [When(@"Start call with (.*) by calls button")]
        public void StartCallWithByCallsButton(string user)
        {
            SearchTheUserFromCallsSearch(user);
            _videocallView.StartCallWithByCallsButton();
        }

        [Then(@"Verify PPT live sharing is displayed")]
        public void VerifyPPTLiveSharingIsDisplayed()
        {
            _videocallView.VerifyPPTLiveSharingIsDisplayed().Should().Be(true);
        }

        [When(@"Start PPT live sharing by (.*)")]
        public void StartPPTLiveSharingBy(string sFileName)
        {
            _videocallView.StartPPTLiveSharingBy(sFileName);
        }

        [When(@"Start PPT live sharing from computer by (.*)")]
        public void StartsPPTLiveSharingFromComputerBy(string sFileName)
        {
            _videocallView.StartPPTLiveSharingFromComputerBy(sFileName);
        }

        [Then(@"Verify Live sharing top tool bar buttons available")]
        public void VerifyLiveSharingTopToolBarButtonsAvailable(Table itemTable)
        {
            foreach (TableRow row in itemTable.Rows)
            {
                _videocallView.VerifyLiveSharingTopToolBarButtonsAvailable(row[0]).Should().Be(true);
            }
        }

        [Then(@"Verify Live sharing action tool buttons available")]
        public void VerifyLiveSharingActionToolButtonsAvailable(Table itemTable)
        {
            foreach (TableRow row in itemTable.Rows)
            {
                _videocallView.VerifyLiveSharingActionToolButtonsAvailable(row[0]).Should().Be(true);
            }
        }

        [Then(@"Verify main screen sharing is (.*)")]
        public void VerifyMainScreenSharingIs(string sCondition)
        {
            if (sCondition.ToLower() == "enable")
            {
                _videocallView.VerifyMainScreenSharingIs().Should().Be(true);
            }
            else if (sCondition.ToLower() == "disable")
            {
                _videocallView.VerifyMainScreenSharingIs().Should().Be(false);
            }
        }

        [When(@"Stop PPT live sharing")]
        public void StopPPTLiveSharing()
        {
            _videocallView.StopPPTLiveSharing();
        }

        [When(@"(.*) transcription")]
        public void StartStopTranscription(string type)
        {
            _videocallView.StartStopTranscription(type);
        }

        [When(@"(.*) Live caption")]
        public void StartStopLiveCaption(string type)
        {
            _videocallView.StartStopLiveCaption(type);
        }

        [Then(@"Verify live caption has (.*)")]
        public void VerifyLiveCaptionHas(string sCondition)
        {
            if (sCondition.ToLower() == "started")
            {
                _videocallView.VerifyLiveCaptionHas().Should().Be(true);
            }
            else
            {
                _videocallView.VerifyLiveCaptionHas().Should().Be(false);
            }
        }

        // Calendar
        [When(@"Open schedule edit")]
        public void OpenScheduleEdit()
        {
            //if (_calendarWindow.CkeckEditViewOpen())
            //{
            //    _calendarWindow.ClickScheduleEditCancel("discard");
            //}
            _calendarWindow.OpenCalendarEditView().Should().Be(true);
        }

        [When(@"Input schedule title as (.*)")]
        public void InputScheduleTitleAs(string title)
        {
            _calendarWindow.InputTitle(title);
        }

        [When(@"Input attendees and optional attendees")]
        public void InputAttendeesAndOptionalAttendees(Table tableProperties)
        {
            foreach (TableRow row in tableProperties.Rows)
            {
                if (row.Count > 1)
                {
                    string[] attendees = row[0].Split(",");
                    string[] optAttendees = row[1].Split(",");
                    _calendarWindow.InputAttendees(attendees);
                    _calendarWindow.InputOptionalAttendees(optAttendees);
                }
                else
                {
                    string[] attendees = row[0].Split(",");
                    _calendarWindow.InputAttendees(attendees);
                }
            }
        }

        [When(@"Input start and end datetime from today")]
        public void InputDatetimeFromToday(Table tableProperties)
        {
            foreach (TableRow row in tableProperties.Rows)
            {
                string startDate = row[0];
                string endDate = row[1];
                string startTime = row[2];
                string endTime = row[3];
                _calendarWindow.InputStartDateTime(startDate, startTime);
                _calendarWindow.InputEndDateTime(endDate, endTime);
            }
        }

        [When(@"Input description as (.*)")]
        public void InputDescrioptionAs(string text)
        {
            _calendarWindow.InputDescription(text);
        }

        [Then(@"Save and verify created schedule by title (.*)")]
        public void SaveAndVerifyCreatedSchedule(string title)
        {
            _calendarWindow.ClickScheduleEditSave();
            _calendarWindow.VerifyCreatedSchedule(title).Should().Be(true);
        }

        [Then(@"Send invitation and verify created schedule by title (.*)")]
        public void SendInvitationAndVerifyCreatedSchedule(string title)
        {
            _calendarWindow.ClickScheduleEditSend();
            _calendarWindow.VerifyCreatedSchedule(title).Should().Be(true);
        }

        [When(@"Open (.*) tab in chat")]
        public void OpenTabInChat(string sTabName)
        {
            _chatWindow.OpenTabInChat(sTabName).Should().Be(true);
        }

        [Then(@"Verify (.*) panel is displayed")]
        public void VerifyUserPanelIsDisplayed(string user)
        {
            _chatWindow.VerifyUserPanelIsDisplayed(user).Should().Be(true);
        }

        //New for v6.2
        [When(@"Remove users from group chat")]
        public void RemoveUsersFromGroupChat(Table itemTable)
        {
            foreach (TableRow row in itemTable.Rows)
            {
                _chatWindow.RemoveUsersFromGroupChat(row[0]).Should().Be(true);
            }
        }

        [Then(@"Verify number of group chat member shuld be (.*)")]
        public void VerifyNumberOfGroupChatMember(string sNum)
        {
            int num = int.Parse(sNum);
            _chatWindow.VerifyNumberOfGroupChatMember().Should().Be(num);
        }

        [Then(@"Verify name of group chat member")]
        public void VerifyNameOfGroupChatMember(Table itemTable)
        {
            foreach (TableRow row in itemTable.Rows)
            {
                _chatWindow.VerifyNameOfGroupChatMember(row[0]).Should().Be(true);
            }
        }
        // by here
    }
}
