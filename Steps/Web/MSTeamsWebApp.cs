using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using NUnit.Framework;
using TeamsWindowsApp.Helper;
using TechTalk.SpecFlow.Infrastructure;
using TeamsWindowsApp.Pages.Web;
using FluentAssertions;

namespace TeamsWindowsApp.Steps.Web
{
    [Binding]
    public class MSTeamsWebApp
    {
        private static ISpecFlowOutputHelper _outHelper;
        private readonly TeamsWebLogin _loginPage;
        private readonly ChatPage _chatPage;
        private readonly VideoCallPage _videoCallPage;
        private readonly CallsPage _callsPage;
        private readonly ScenarioContext _scenarioContext;

        public MSTeamsWebApp(ScenarioContext scenarioContext, TeamsWebLogin loginPage, ChatPage ChatPage, VideoCallPage VideoCallPage, CallsPage CallsPage)
        {
            _loginPage = loginPage;
            _chatPage = ChatPage;
            _videoCallPage = VideoCallPage;
            _callsPage = CallsPage;
            _scenarioContext = scenarioContext;
        }

        [Given(@"Launch MS teams Web app and login with (.*)")]
        public void TeamWebLogin(String userName)
        {
            _scenarioContext["User1"] = DataReader.getUserName(userName);
            _scenarioContext["Email1"] = DataReader.getEmail(userName);
            _scenarioContext["FullName1"] = DataReader.getFullName(userName);
            string email = (string)_scenarioContext["Email1"];
            //string expectation = email.Replace("qa", "QA");
            //Console.WriteLine(sResult);
            Console.WriteLine(email);
            _loginPage.LoginPage(userName).Should().Be(email, "User log in check");
            //_loginPage.HandleNotification();
        }

        [When(@"Verify the (.*) message (.*) is received")]
        public void ThenVerifyTheUserMessageIsReceived(String sUser, string sMessage)
        {
            _scenarioContext["User2"] = DataReader.getUserName(sUser);
            _scenarioContext["FullName2"] = DataReader.getFullName(sUser);
            bool sStatus = _chatPage.SearchUser(_scenarioContext["FullName2"].ToString());
            sStatus.Should().Be(true);
            string verifyMsg = _chatPage.ReadMessageFromUser((string)_scenarioContext["User2"]);
            verifyMsg.Should().Be(sMessage);
            _chatPage.TypeMessage("Hi User");
            verifyMsg = _chatPage.ClickSendAndVerify("Hi User", (string)_scenarioContext["User1"]);
            verifyMsg.Should().Be("Hi User");

        }


        [Then(@"Verify the webapp logged user")]
        public void VerifyLoggedUser()
        {
            String sResult = _loginPage.VerifyLoggedInUser();
            string email = (string)_scenarioContext["Email1"];
            string expectation = email.Replace("qa", "QA");
            Console.WriteLine(sResult);
            Console.WriteLine(expectation);
            sResult.Should().Contain(expectation);
        }

        [Then(@"Logout successfully from web application")]
        public void Logout()
        {
            String sResult = _loginPage.Logout((string)_scenarioContext["User1"]);
            sResult.Should().Be("Sign out");
        }

        [When(@"Search the employee (.*)")]
        public void SearchUser(string sUser)
        {
            _chatPage.SearchUser(DataReader.getFullName(sUser));
        }

        [Then(@"Send message with mention on web")]
        public void SendMessageWithMentionOnWeb(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                string sMentionUserName = DataReader.getUserName(row[0]);
                string targetAriaLabel = sMentionUserName + " " + row[1];
                string targetText = sMentionUserName + row[1];
                Console.WriteLine("STEPSTEP: {0}", targetAriaLabel);
                Console.WriteLine("STEPSTEP: {0}", targetText);
                _chatPage.TypeMentionTagandText(sMentionUserName, row[1]);
                _chatPage.ClickSendAndVerify(targetAriaLabel, (string)_scenarioContext["User1"]).Should().Be(targetText, "Mention with message");
                _chatPage.verifyMentionItem((string)_scenarioContext["User1"], sMentionUserName).Should().Be(true);
            }
        }

        [Then("Open each view on web")]
        public void OpenViewOnWeb(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _loginPage.OpenViewOnWeb(row[0]).Should().Be(true);
            }
        }

        [When("Open (.*) tab in chat on web")]
        public void OpenTabInChat(string sTabName)
        {
            _chatPage.OpenTabInChat(sTabName);
        }

        [Then("Verify (.*) panel is displayed on web")]
        public void VerifyUserPanelIsDisplayed(string sUser)
        {
            _chatPage.VerifyUserPanelIsDisplayed(DataReader.getUserName(sUser));
        }

        [Then(@"Send a message with delivery option on web")]
        public void SendMessageWithUrgentOnWeb(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                string sDeliveryOptionText = row[0];
                string sTestMessage = row[1];
                _chatPage.TypeMessage(sTestMessage);
                _chatPage.OpenAndVerifyDeliveryOption().Should().Be(true);
                if (sDeliveryOptionText == "Standard")
                {
                    _chatPage.SelectDeliveryOption(sDeliveryOptionText);
                    _chatPage.ClickSendAndVerify(sTestMessage, (string)_scenarioContext["User1"]).Should().Be(sTestMessage);
                }
                else
                {
                    _chatPage.ClickAndVerifyDeliveryOption(sDeliveryOptionText).Should().Be(CommonUtil.ConvertLang(row[0].ToUpper()) + "!");
                    _chatPage.ClickSendAndVerifySendMessage((string)_scenarioContext["User1"], sTestMessage, sDeliveryOptionText).Should().Be(CommonUtil.ConvertLang(row[0].ToUpper()));
                    _chatPage.VerifySentTextByText((string)_scenarioContext["User1"], sTestMessage);
                }
            }
        }

        [Then(@"Send format messages with text and verify on web")]
        public void SendFormatMessagesWithTexAndVerifyOnWeb(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                _chatPage.OpenTextFormat();
                _chatPage.FormatText(row[0], row[1], row[2]);
                _chatPage.ClickSendAndVerify(row[2], (string)_scenarioContext["User1"]).Should().Be(row[2], "Message sent verification");
                _chatPage.VerifyFormat(row[2], (string)_scenarioContext["User1"], row[0]).Should().Be(true);
            }
        }

        //Video Call Related
        [Then(@"Verify call buttons availability")]
        public void VerifyCallButtonsAvailability(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                string sButtonName = row[0];
                string sResult = row[1];
                if (sResult.ToLower() == "enabled")
                {
                    _chatPage.VerifyCallButtonsAvailability(sButtonName).Should().Be(true);
                }
                else
                {
                    _chatPage.VerifyCallButtonsAvailability(sButtonName).Should().Be(false);
                }
            }
        }

        [When(@"Start (.*) call on chat on web")]
        public void StartVideoCallOnChat(string sCallType)
        {
            if (sCallType.ToUpper() == "PHONE")
            {
                _videoCallPage.StartPhoneCallOnChat();
            }
            else
            {
                _videoCallPage.StartVideoCallOnChat();
            }
        }

        [Then(@"Verify call dialog on web is (.*)")]
        public void VerifyChatCallDialogIsOnWeb(string sExpect)
        {
            if (sExpect.ToUpper() == "ENABLED")
            {
                _videoCallPage.VerifyChatCallDialogIsOnWeb().Should().Be(true);
            }
            else
            {
                _videoCallPage.VerifyChatCallDialogIsOnWeb().Should().Be(false);
            }
        }

        [When(@"Open More Options on web")]
        public void OpenMoreOptions() => _videoCallPage.OpenMoreOptions();

        [Then(@"Verify buttons availability")]
        public void VerifyButtonsAreAvailability(Table tableMessageData)
        {
            foreach (TableRow row in tableMessageData.Rows)
            {
                string sButtonName = row[0];
                string sResult = row[1];
                if (sResult.ToLower() == "enabled")
                {
                    _videoCallPage.VerifyButtonsAreAvailability(sButtonName).Should().Be(true);
                }
                else
                {
                    _videoCallPage.VerifyButtonsAreAvailability(sButtonName).Should().Be(false);
                }
            }
        }

        [When(@"Hang up a MTG on web")]
        public void HangUpMTG() => _videoCallPage.HangUpMTG();

        [When(@"Start call with (.*) by calls view")]
        public void StartCallWithByCallsView(string sUser)
        {
            string username = DataReader.getUserName(sUser);
            _callsPage.SearchUser(username).Should().Be(username);
            _callsPage.StartCall();
        }
    }
}
