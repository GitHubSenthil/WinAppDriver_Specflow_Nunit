using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsWindowsApp.Driver;
using TeamsWindowsApp.Helper;
using TeamsWindowsApp.Pages;
using TeamsWindowsApp.Pages.Web;
using TechTalk.SpecFlow;
using static TeamsWindowsApp.Helper.DataReader;

namespace TeamsWindowsApp.Steps.MIB
{
    [Binding]
    public class MIB
    {
        private readonly TeamsLogin _loginPage;
        private readonly ChatWindow _chatWindow;
        private readonly ConfigurationDriver _configurationDriver;

        public MIB(TeamsLogin loginPage, ChatWindow chatWindow, ConfigurationDriver configurationDriver)
        {
            _configurationDriver = configurationDriver;
            _loginPage = loginPage;
            _chatWindow = chatWindow;
        }

        [Given(@"Launch MS teams app with MIB (.*) user")]
        public void LoginMSTeams_App(string sUser)
        {
            String executeResult = _loginPage.TeamsApp_LoginPage(sUser);
            executeResult.Should().Be("pass", "Launch and login status");
        }

        [When(@"(.*) Search user to verify access to MIB (.*) users")]
        public void TeamsMIBGroupAccessVerification(string sUser, string sUsers)
        {
            
            int iFlagValue = 0;
            string[] sSearchUsers = sUsers.Split(",");
            foreach (string sMIBUser in sSearchUsers) {
                if (_chatWindow.MIBSearchEmployee(sUser, sMIBUser, DataReader.getFullName(sMIBUser)) == false)
                {
                    iFlagValue = 1;
                }
            }
            iFlagValue.Should().Be(0);
        }

        [Then(@"(.*) Search user to verify not having access to MIB (.*) users")]
        public void TeamsMIBNonGroupAccessVerification(string sUser, string sUsers)
        {
            int iFlagValue = 0;
            string[] sSearchUsers = sUsers.Split(",");
            foreach (string sMIBUser in sSearchUsers)
            {
                if (_chatWindow.MIBSearchEmployee(sUser, sMIBUser, DataReader.getFullName(sMIBUser)) == true)
                {
                    iFlagValue = 1;
                }
            }
            iFlagValue.Should().Be(0);
        }


    }
}
