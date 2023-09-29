using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using TeamsWindowsApp.Driver;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow.Infrastructure;
using TeamsWindowsApp.Objects;


namespace TeamsWindowsApp.Helper
{

    public class ChatWindow
    {
        private readonly WinAppDriver _driver;
        private readonly ConfigurationDriver _configurationDriver;
        Reporting reportLine;
        private static ISpecFlowOutputHelper _outHelper;
        public static class ExecTime
        {
            public static string sExecTime;
        }

        public ChatWindow(WinAppDriver driver, ISpecFlowOutputHelper outHelper, ConfigurationDriver configurationDriver)
        {
            _driver = driver;
            _configurationDriver = configurationDriver;
            reportLine = new Reporting(outHelper);
        }

        public void ClickSendButton()
        {
            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_SendButton)).Click();
            System.Threading.Thread.Sleep(4000);
        }

        //v3.2
        public Boolean GoToChatOptions()
        {
            //Click the profile
            try
            {
                System.Threading.Thread.Sleep(2000);
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Toolbar)).Click();
                System.Threading.Thread.Sleep(2000);
                return WaitUtil.WaitUntilElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Menu), _driver.Current, 30000);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "GoToChatOptions");
                return false;
            }
        }

        //v3.2
        public void SearchEmployee(String searchUser)
        {
            //Search the employee
            try
            {
                _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_SearchToolbar).Click();
                //Commenting below code because no List item available - ############################ CODE CHANGE
                //CONFIGURATION CHANGES IN QA 
                //Date: 26 June 2023
                //_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_SearchField).SendKeys(searchUser);

                //New code - 26/06/2023
                ////############################ CODE CHANGE
                CommonUtil.ClipBoard_Copying(searchUser);
                _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_SearchField).SendKeys(Keys.Control + "v");
                //############################ CODE CHANGE

                System.Threading.Thread.Sleep(10000);
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_SearchAutolist).Replace("#USER#", searchUser.ToUpper())).Click();

                reportLine.WriteLine("User searching the user: " + searchUser);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "SearchEmployee");
            }
        }

        public void SearchFile(String sFile)
        {
            //Search the file
            try
            {
                _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_SearchToolbar).Click();
                _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_SearchField).SendKeys(sFile);

                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "SearchFile");
            }
        }

        public Boolean VerifySuggestedFile(String sFile)
        {
            try
            {
                Console.WriteLine(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_SearchFileSuggetion).Replace("#FILE#", sFile));
                return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_SearchFileSuggetion).Replace("#FILE#", sFile), _driver.Current);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "VerifySuggestedFile");
                return false;
            }
        }


        //v3.2
        public void SendMessage(String strMessage, String sUser)
        {
            try
            {
                //Enter message
                WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField), _driver.Current, 5 * 1000);
                _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField))[0].Click();
                System.Threading.Thread.Sleep(2000);

                //New code - 26/06/2023
                ////############################ CODE CHANGE
                CommonUtil.ClipBoard_Copying(strMessage);
                _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField))[0].SendKeys(Keys.Control + "v");
                //############################ CODE CHANGE
                System.Threading.Thread.Sleep(2000);
                reportLine.WriteLine("Chat: sent a message: " + strMessage + " to user " + sUser);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "ChatTypeMessage");
            }
        }

        //Following codes are made by Takuya
        /*
        * Method Name: verifyMentionItem
        * Desc: Post a message and Verify mentioned User name.
        * Created by: Takuya 24/02/2022
        * Updated by: Takuya 24/02/2022
        * Update desc: Change Xpath so that it uses TeamsAppObjects.
        */
        public string verifyMentionItem()
        {
            try
            {
                System.Threading.Thread.Sleep(500);
                //Console.WriteLine(_driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MentionedUserName))[0].Text.Trim(','));
                //New code - 26 June 2023
                var items = _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MentionedUserName));
                return items[items.Count-1].Text.Split(",")[0];
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "verifyMentionItem");
                return "Exception";
            }
        }


        /*
        * Method Name: sendMessageWithUserMention
        * Desc: Input both message and user mention into textfield.
        * Created by: Takuya 24/02/2022
        * Updated by: Takuya 24/02/2022
        * Update desc: Change Xpath so that it uses TeamsAppObjects.
        */
        public void sendMessageWithUserMention(string sUserName, string sSendMessage = "")
        {
            System.Threading.Thread.Sleep(1000);
            try
            {
                var textfield = _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField))[0];
                var sMentionText = "@" + sUserName;
                textfield.Click();
                textfield.Clear();

                //Commenting below code because no List item available - ############################ CODE CHANGE
                //CONFIGURATION CHANGES IN QA 
                //Date: 26 June 2023
                //foreach (char str in sMentionText)
                //textfield.SendKeys(Char.ToString(str));
                // ############################ CODE CHANGE
                
                //New Code - 26 June 2023
                CommonUtil.ClipBoard_Copying(sMentionText);
                textfield.SendKeys(Keys.Control + "v");
                textfield.SendKeys(" ");
                System.Threading.Thread.Sleep(500);
                textfield.SendKeys(Keys.Backspace);
                // ###

                WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MentionedUserSuggest), _driver.Current, 2 * 1000);
                _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MentionedUserSuggest))[0].Click();

                CommonUtil.ClipBoard_Copying(sSendMessage);
                textfield.SendKeys(Keys.Control + "v");
                //textfield.SendKeys(sSendMessage);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "ChatTypeMessage");
            }
        }

        /*
        * Method Name: OpenAndVerifyDeliveryOption
        * Desc: Open a delivery option window and verify it if it is displayed.
        * Created by: Takuya 24/02/2022
        * Updated by: Takuya 24/02/2022
        * Update desc: Change Xpath so that it uses TeamsAppObjects.
        */
        public Boolean OpenAndVerifyDeliveryOption()
        {
            try
            {
                WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_DeliveryOption_Button), _driver.Current, 5 * 1000);
                System.Threading.Thread.Sleep(1000);
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_DeliveryOption_Button)).Click();
                System.Threading.Thread.Sleep(1000);
                WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_DeliveryOption_Window), _driver.Current, 5 * 1000);
                return _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_DeliveryOption_Window)).Displayed;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "OpenDeliveryOption");
                return false;
            }

        }


        /*
        * Method Name: ClickAndVerifyDeliveryOption
        * Desc: Select one of the delivery option and verify a delivery flag on textfield.
        * Created by: Takuya 24/02/2022
        * Updated by: Takuya 24/02/2022
        * Update desc: Change Xpath so that it uses TeamsAppObjects.
        */

        public void SelectDeliveryOption(string sMessageType)
        {
            try
            {
                string sMessageTypeConvert = CommonUtil.ConvertLang(sMessageType);
                WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_DeliveryOption_Item).Replace("#MESSAGETYPE#", sMessageTypeConvert), _driver.Current, 5 * 1000);
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_DeliveryOption_Item).Replace("#MESSAGETYPE#", sMessageTypeConvert)).Click();
                System.Threading.Thread.Sleep(5000);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "SelectDeliveryOption");
            }
        }

        public string ClickAndVerifyDeliveryOption(string sMessageType)
        {
            try
            {
                string sMessageTypeText = null;
                SelectDeliveryOption(sMessageType);
                switch (sMessageType)
                {
                    case "Urgent":
                        sMessageTypeText = _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_DeliveryOption_FlagText))[1].Text;
                        break;
                    case "Important":
                        sMessageTypeText = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_DeliveryOption_FlagText)).Text;
                        break;
                }
                return sMessageTypeText;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "ClickAndVerifyDeliveryOption");
                return "ClickAndVerifyDeliveryOption";
            }
        }


        /*
        * Method Name: ClickSendAndVerifySendMessage
        * Desc: Post a message and verify the posted meessage text.
        * Created by: Takuya 24/02/2022
        * Updated by: Takuya 24/02/2022
        * Update desc: 
        *  - Change Xpath so that it uses TeamsAppObjects.
        *  - Integrate this method as we can use both standard post and delivery option.
        */
        public string ClickSendAndVerifySendMessage(string sMessageType = "Standard")
        {
            try
            {
                if (sMessageType.ToLower() == "format")
                {
                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_SendButton)).Click();
                }
                else
                {
                    Actions action = new Actions(_driver.Current);
                    action.SendKeys(Keys.Enter);
                    action.Perform();
                }
                System.Threading.Thread.Sleep(3000);

                //Take posted time
                ExecTime.sExecTime = DateTime.Now.ToString("t");

                //Commenting below code because no List item available - ############################ CODE CHANGE
                //CONFIGURATION CHANGES IN QA 
                //Date: 26 June 2023
                /*var items = _driver.Current.FindElementByXPath(
                    CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent)
                ).FindElementsByXPath("//ListItem");*/
                //############################ CODE CHANGE

                var items = _driver.Current.FindElementsByXPath("//Group[contains(@Name,\"Sent\") or contains(@Name,\"Seen\")]");
                if (sMessageType == "Important")
                {
                    //############################ CODE CHANGE
                    //Commenting below code because no List item available - ############################ CODE CHANGE
                    //CONFIGURATION CHANGES IN QA 
                    //Date: 26 June 2023
                    //return items[items.Count - 1].FindElementsByXPath(
                    //     CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_ImpDelivery_PostFlag.Replace("#MESSAGETYPE#", sMessageType + " message."))
                    //)[2].Text;
                    //return _driver.Current.FindElementByXPath(
                    //    CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent)
                    //).FindElementsByXPath(
                    //    CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_ImpDelivery_PostFlag.Replace("#MESSAGETYPE#", sMessageType + " message."))
                    //)[2].Text;
                    //############################ CODE CHANGE
                    return (items[items.Count - 1].Text).Substring(0, 9).ToUpper();
                }
                else if (sMessageType == "Urgent")
                {
                    //Commenting below code because no List item available - ############################ CODE CHANGE
                    //CONFIGURATION CHANGES IN QA 
                    //Date: 26 June 2023
                    //return items[items.Count - 1].FindElementsByXPath(
                    //    CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_SentMessgae)
                    //)[2].Text;
                    //############################ CODE CHANGE

                    //New Code - 26 June 2023
                    Console.WriteLine("Urgent msg: " + items[items.Count - 1].FindElementsByXPath("//Text")[0].Text);
                    return (items[items.Count - 1].Text).Substring(0, 6).ToUpper();

                    /*
                     * We need to keep following script until we can confirm our script on QA env
                     * Becaseu our script might be broken due to the version of Teams.
                    */

                    //return _driver.Current.FindElementByXPath(
                    //    CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent)
                    //).FindElementsByXPath(
                    //    CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_SentMessgae)
                    //)[2].Text;
                }
                else
                {
                    return (items[items.Count - 1].FindElementsByXPath("//Text")[0].Text);
                }
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "ClickSendAndVerifySendMessage");
                return "ClickSendAndVerifySendMessage";
            }
        }

        public string ClickSendAndVerifyMentionedMessage()
        {
            try
            {
                ClickSendButton();

                //Commenting below code because no List item available - ############################ CODE CHANGE
                //CONFIGURATION CHANGES IN QA 
                //Date: 26 June 2023
                //var items = _driver.Current.FindElementByXPath(
                //    CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent)
                //).FindElementsByXPath("//ListItem");
                //############################ CODE CHANGE

                //New code - 26 June 2023
                var items = _driver.Current.FindElementsByXPath(TeamsAppObjects.xpath_Chat_MessageContent);

                //Commenting below code because no List item available - ############################ CODE CHANGE
                //CONFIGURATION CHANGES IN QA 
                //Date: 26 June 2023
                //var messageList = items[items.Count - 1].FindElementsByXPath(
                //        CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_SentMessgae)
                //    );

                //return messageList[messageList.Count - 1].Text;
                //############################ CODE CHANGE

                //New code - 26 June 2023
                return items[items.Count - 1].FindElementsByXPath("//Text")[0].Text;

                //return _driver.Current.FindElementByXPath(
                //    CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent)
                //).FindElementsByXPath(
                //    CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_SentMessgae)
                //)[4].Text;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "ClickSendAndVerifyMentionedMessage");
                return "ClickSendAndVerifyMentionedMessage";
            }
        }

        /*
        * Method Name: UploadAFile
        * Desc: Post a message and verify the posted meessage text.
        * Created by: Takuya 24/02/2022
        * Updated by: Takuya 03/03/2022
        * Update desc: 
        *  - Change Xpath so that it uses TeamsAppObjects.
        *  - Developped file upload method
        */
        // https://github.com/microsoft/WinAppDriver/issues/194
        public Boolean UploadAFile(string sFileType, string sStorageType)
        {
            System.Threading.Thread.Sleep(2000);
            String path = System.Environment.CurrentDirectory.Split("bin")[0].TrimEnd('\\');
            Console.WriteLine(path);
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
                System.Threading.Thread.Sleep(2000);
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AttachmentFile_Button)).Click();
                System.Threading.Thread.Sleep(2000);
                if (sStorageType == "PC")
                {
                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AttachmentFile_Item.Replace("#TYPE#", "Upload from this device"))).Click();
                    //_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AttachmentFile_Item.Replace("#TYPE#", "Upload from my computer"))).Click();
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
                WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_FileReplace_Button), _driver.Current);

                if (_driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_FileReplace_Button)).Count > 0)
                {
                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_FileReplace_Button)).Click();
                }
                return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Uploaded_File), _driver.Current);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "Duplication_dialog");
                return false;
            }
        }


        /*
        * Method Name: RemoveUploadFileFromTextField
        * Desc: Cancel to post a uploaded file when user attached a file into text field.
        * Created by: Takuya 02/03/2022
        * Updated by: Takuya 03/03/2022
        * Update desc: 
        *  - Change Xpath so that it uses TeamsAppObjects.
        *  - Developped Remove file operation
        */
        public Boolean RemoveUploadFileFromTextField()
        {
            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_FileRemoveButton)).Click();
            try
            {
                int FileNum = _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Uploaded_File)).Count;
                if (FileNum == 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Attached file count is not 1 but " + FileNum.ToString());
                }
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "Uploaded_file_on_textfield");
                return false;
            }

        }


        /*
        * Method Name: SendFileAndVerify
        * Desc: Post a uploaded file and verify it has actually been posted.
        * Created by: Takuya 02/03/2022
        * Updated by: Takuya 03/03/2022
        * Update desc: 
        *  - Change Xpath so that it uses TeamsAppObjects.
        *  - Developped post and verify method for file.
        */
        public Boolean SendFileAndVerify()
        {
            ClickSendButton();
            try
            {
                var items = _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_SendAttachment));
                //return _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_SendAttachment)).Displayed;
                Console.WriteLine(items.Count);
                Console.WriteLine(items[items.Count - 1].Text);
                Console.WriteLine(items[items.Count - 1].GetAttribute("Name"));
                return items[items.Count - 1].Displayed;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "Posted_file");
                return false;
            }

        }


        /*
        * Method Name: OpenAddUserDialog
        * Desc: Open user add dialog and verify the exist.
        * Created by: Takuya 03/03/2022
        * Updated by: Takuya 03/03/2022
        * Update desc: 
        *  - Developped post and verify method for file.
        */
        public Boolean OpenAddUserDialog()
        {
            try
            {
                WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AddUserButton), _driver.Current);
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AddUserButton)).Click();
                return WaitUtil.WaitForElementAvailability(TeamsAppObjects.xpath_Chat_AddUserDialog, _driver.Current);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "OpenAddUserDialog");
                return false;
            }
        }


        /*
        * Method Name: AddButtonVerification
        * Desc: Verify Add button enaible.
        * Created by: Takuya 03/03/2022
        * Updated by: Takuya 03/03/2022
        * Update desc: 
        *  - Developped post and verify method for file.
        */
        public Boolean AddButtonVerification()
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                return _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AddUserDialog_AddButton)).Enabled;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "AddButtonVerification");
                return false;
            }
        }


        /*
        * Method Name: InputUserNameInAddUserDialog
        * Desc: Input user name on the user add dialog
        * Created by: Takuya 03/03/2022
        * Updated by: Takuya 03/03/2022
        * Update desc: 
        *  - Developped post and verify method for file.
        */
        public Boolean InputUserNameInAddUserDialog(string sUserName)
        {
            try
            {
                Console.WriteLine(sUserName);
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AddUser_Input)).SendKeys(sUserName);
                WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AddUser_SearchListItem).Replace("#USER#", sUserName.ToUpper()), _driver.Current);
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AddUser_SearchListItem).Replace("#USER#", sUserName.ToUpper())).Click();
                return _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AddUserInput_RemoveButton)).Enabled;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "Input_UserName_In_AddUserDialog");
                return false;
            }
        }

        /*
        * Method Name: AddUserInChatAndVerify
        * Desc: Verify wether the added user is displayed on the DM chat.
        * Created by: Takuya 03/03/2022
        * Updated by: Takuya 03/03/2022
        * Update desc: 
        *  - Developped post and verify method for file.
        */
        public string AddUserInChatAndVerify()
        {
            try
            {
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AddUserDialog_AddButton)).Click();
                System.Threading.Thread.Sleep(3000);

                if (WaitUtil.WaitForElementAvailability(TeamsAppObjects.xpath_Chat_NewGroupText, _driver.Current))
                {
                    Console.WriteLine(_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_ChatTreeNewGroupItemText)).Text);
                    return _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_ChatTreeNewGroupItemText)).Text;
                }
                else
                {
                    return "existing";
                }
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "AddUserInChatAndVerify");
                return "Exception";
            }
        }


        /*
        * Method Name: VerifyUserNumberOfDMGroupChat
        * Desc: Verify the number of users on the DM group chat.
        * Created by: Takuya 07/03/2022
        * Updated by: Takuya 07/03/2022
        * Update desc: 
        *  - Developped post and verify method for file.
        */
        public Boolean VerifyUserNumberOfDMGroupChat(string sNum)
        {
            try
            {
                return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AddUserButtonNum).Replace("#NUMBER#", sNum), _driver.Current);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "AddUserInChatAndVerify");
                return false;
            }
        }


        /*
        * Method Name: ClickReplyAndVerify
        * Desc: Click "Reply" button from more option popup of target message. And verify reply item on text field
        * Created by: Takuya 09/03/2022
        * Updated by: Takuya 09/03/2022
        * Update desc: 
        *  - Added some steps for replying.
        */
        public Boolean ClickReplyAndVerify(string[] sPostText, string Type = "text")
        {
            try
            {
                int Count = sPostText.Length;
                string targetArea = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent) + "//ListItem";
                string targetPost = "";
                string targetXpath = "";
                for (int i = 0; i < Count; i++)
                {
                    switch (Type.ToLower())
                    {
                        case "text":
                            targetXpath = TeamsAppObjects.xpath_Chat_TrgetPostedText.Replace("#TEXT#", sPostText[i]);
                            targetPost = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent) + targetXpath;
                            break;
                        case "emoji":
                            targetXpath = TeamsAppObjects.xpath_Chat_TrgetPostedEmoji.Replace("#EMOJINAME#", sPostText[i]) + "//Image";
                            targetPost = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent) + targetXpath;
                            break;
                        case "file":
                            targetXpath = TeamsAppObjects.xpath_Chat_TrgetPostedFile.Replace("#FILENAME#", sPostText[i]);
                            targetPost = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent) + targetXpath;
                            break;
                    }
                    Console.WriteLine("TARGET XPATH: {0}", targetXpath);
                    Console.WriteLine("TARGET POST: {0}", targetPost);
                    WaitUtil.WaitForElementAvailabilityByScrollUp(targetPost, targetArea, _driver.Current);

                    CommonUtil.RetryMouseOverUntilTargetAvailable(targetPost, CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Reaction_MoreOption), _driver.Current);

                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Reaction_MoreOption)).Click();
                    System.Threading.Thread.Sleep(2000);
                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Reaction_MoreOption_Reply)).Click();
                    System.Threading.Thread.Sleep(500);
                }
                return _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField_ReplyItem)).Count == Count;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "Click_Reply_And_Verify");
                return false;
            }
        }


        /*
        * Method Name: RemoveReplyItem
        * Desc: Click "Reply" button from more option popup of target message. And remove reply item on text field
        * Created by: Takuya 09/03/2022
        * Updated by: Takuya 09/03/2022
        * Update desc: 
        *  - Added some steps for replying.
        */
        public Boolean RemoveReplyItem()
        {
            try
            {
                int ItemCount = _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField_ReplyItem)).Count;
                if (ItemCount > 0)
                {
                    for (int i = 0; i < ItemCount; i++)
                    {
                        _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField_ReplyItem_RemoveButton)).Click();
                        System.Threading.Thread.Sleep(500);
                    }
                }
                else
                {
                    throw new Exception("Displayed item count is 0 although expected count is more than 1");
                }
                if (_driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField_ReplyItem)).Count == 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Displayed item count does't match 0");
                }
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "Remove_ReplyItem");
                return false;
            }
        }


        /*
        * Method Name: RemoveReplyItem
        * Desc: Verify reply item on posted message
        * Created by: Takuya 09/03/2022
        * Updated by: Takuya 09/03/2022
        * Update desc: 
        *  - Added some steps for replying.
        */
        //v3.2
        public Boolean VerifyReplyMessage()
        {
            try
            {
                return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_PostedItem_ReplyItem), _driver.Current);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "VerifyReplyMessage");
                return false;
            }
        }

        public Boolean VerifyVideoCallButton(string sUserType)
        {
            try
            {
                bool bValue = WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_VideoCall), _driver.Current, 4 * 1000);
                reportLine.WriteLine(sUserType + " user call button availability check. Actual Value: " + bValue);
                return bValue;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "VerifyVideoCallButtun");
                return false;
            }
        }

        public Boolean VerifyPhoneCallButton(string sUserType)
        {
            try
            {
                bool bValue = WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_AudioCall), _driver.Current, 4 * 1000);
                reportLine.WriteLine(sUserType + " user call button availability check. Actual Value: " + bValue);
                return bValue;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "VerifyPhoneCallButtun");
                return false;
            }
        }

        public void OpenEmojiOption()
        {
            try
            {
                WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EmojiOption_Button), _driver.Current);
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EmojiOption_Button)).Click();
                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "OpenEmojiOption");
            }
        }

        public void SelectEmoji(string type, string EmojiName)
        {
            try
            {
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EmojiOption_EmojiType.Replace("#TYPE#", type))).Click();
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EmojiOption_Emoji.Replace("#EMOJI#", EmojiName))).Click();
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "SelectEmoji");
            }
        }

        public void InputEmojiMessage(string strMessage)
        {
            try
            {
                _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField))[0].Click();
                _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField))[0].SendKeys(strMessage);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "InputEmojiMessage");
            }
        }

        public int VerifySentEmojiCount()
        {
            try
            {
                var items = _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent));

                return items[items.Count - 1].FindElementsByXPath(
                    CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_SentEmojiMessgae)
                ).Count;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "VerifySentEmoji");
                return 0;
            }
        }

        public int VerifySentEmojiCount(string sEmojiName, string sUser)
        {
            string sEmojiXpath = TeamsAppObjects.xpath_Chat_Emoji;
            sEmojiXpath = sEmojiXpath.Replace("#EMOJINAME#", sEmojiName).Replace("#USER#", sUser);
            try
            {
                var items3 = _driver.Current.FindElementsByXPath(sEmojiXpath);
                return items3.Count;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "VerifySentEmoji");
                return 0;
            }
        }

        public void CreateLoopComponent(string type, string format, string title, string text)
        {
            try
            {
                string[] formats = format.Split(",");
                string[] texts = text.Split(",");
                //Oprn option
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopCompOption_Button)).Click();
                System.Threading.Thread.Sleep(2000);
                //Select type
                if (type.ToLower() == "table")
                {
                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopCompOptions.Replace("#LOOPOPTION#", type))).Click();
                    // Version < 1.5.00.28361 (64-bit)
                    //_driver.Current.FindElementByXPath(TeamsAppObjects.xpath_Chat_LoopCompOption_Table.Replace("#XASIX#", formats[0]).Replace("#YASIX#", formats[1])).Click();
                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopCompOption_Table).Replace("#XASIX#", formats[0]).Replace("#YASIX#", formats[1])).Click();
                }
                else
                {
                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopCompOptions.Replace("#LOOPOPTION#", type))).Click();
                }
                WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopComp_EditPane), _driver.Current);
                //Input details
                var editField = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopComp_EditPane));
                Actions action = new Actions(_driver.Current);
                action.MoveToElement(editField, 10, 10).Click().SendKeys(title);
                if (type.ToLower() == "table")
                {
                    int iHeadCount = int.Parse(formats[0]);
                    int iItemCount = texts.Length - iHeadCount;
                    //Console.WriteLine("HEADER count");
                    var Headers = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopComp_EditPane)).FindElementsByXPath("//Table//Header//Edit");
                    for (int i = 0; i < iHeadCount; i++)
                    {
                        Headers[i].Click();
                        Headers[i].SendKeys(texts[i]);
                    }

                    //Console.WriteLine("ITEM count");
                    var Items = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopComp_EditPane)).FindElementsByXPath("//Table//DataItem//Edit");
                    for (int i = 0; i < iItemCount; i++)
                    {
                        Items[i].Click();
                        Items[i].SendKeys(texts[i + iHeadCount]);
                    }
                }
                else if (type.ToLower() == "task list")
                {
                    if (int.Parse(format) > 2)
                    {
                        for (int i = 0; i < int.Parse(format) - 2; i++)
                        {
                            editField.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopComp_TaskAddButton)).Click();
                        }
                    }
                    var taskItem = editField.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopComp_TaskItems));
                    for (int i = 0; i < int.Parse(format); i++)
                    {
                        taskItem[i].FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopComp_Edit)).Click();
                        taskItem[i].FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopComp_Edit)).SendKeys(texts[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < int.Parse(format); i++)
                    {
                        action.SendKeys(Keys.Enter).SendKeys(text + (i + 1).ToString());
                    }
                }
                action.Build().Perform();
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "CreateLoopComponent");
            }
        }

        public String VerifyLoopComponentEditViewLoaded()
        {
            try
            {
                return _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_LoopComp_DraftLink))[0].Text;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "VerifyLoopComponentEditViewLoaded");
                return "Exception";
            }
        }

        public String SendLoopComponentAndVerify()
        {
            try
            {
                _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_Chat_LoopComp_SendButton)).Click();
                System.Threading.Thread.Sleep(4000);
                var items = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent)).FindElementsByXPath("//ListItem");
                int targetCount = items[items.Count - 1].FindElementByName(CommonUtil.ConvertLang("Canvas")).FindElementsByXPath("//Text").Count;
                return items[items.Count - 1].FindElementByName(CommonUtil.ConvertLang("Canvas")).FindElementsByXPath(CommonUtil.ConvertLang("//Text"))[0].Text;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "SendLoopComponentAndVerify");
                return "Exception";
            }
        }

        public void OpenTextFormat()
        {
            if (WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_Button), _driver.Current))
            {
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_Button)).Click();
            }
        }

        public void FormatText(string format, string type, string text)
        {
            try
            {
                if (type != "")
                {
                    string sFormat = TeamsAppObjects.xpath_Chat_Format_Item.Replace("#ITEM#", format);
                    Console.WriteLine(CommonUtil.ConvertLang(sFormat));
                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_Toolbar)).FindElementByXPath(CommonUtil.ConvertLang(sFormat)).Click();
                    switch (format.ToLower())
                    {
                        case "text highlight color":
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_TextHighlight_Option.Replace("#TEXTHIGHLIGHT#", type))).Click();
                            break;
                        case "text highlight colour":
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_TextHighlight_Option.Replace("#TEXTHIGHLIGHT#", type))).Click();
                            break;
                        case "font color":
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_FontColor_Option.Replace("#FONTCOLOR#", type))).Click();
                            break;
                        case "font colour":
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_FontColor_Option.Replace("#FONTCOLOR#", type))).Click();
                            break;
                        case "font size":
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_FontSize_Option.Replace("#SIZE#", type))).Click();
                            break;
                        case "paragraph":
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_Paragraph_Option.Replace("#PARAOPTION#", type))).Click();
                            break;
                        case "insert link":
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_Insert_Text_Edit)).Click();
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_Insert_Text_Edit)).SendKeys(text);
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_Insert_Address_Edit)).Click();
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_Insert_Address_Edit)).SendKeys(type);
                            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_Insert_Button)).Click();
                            break;
                    }
                }
                else
                {
                    string sFormat = TeamsAppObjects.xpath_Chat_Format_Format.Replace("#FORMAT#", format);
                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_Toolbar)).FindElementByXPath(CommonUtil.ConvertLang(sFormat)).Click();
                }
                if (format.ToLower() != "insert link")
                {
                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField)).Click();
                    //_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField)).SendKeys(text);
                    CommonUtil.ClipBoard_Copying(text);
                    //Paste the value in the field
                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField)).SendKeys(Keys.Control + "v");
                }
                if (format.ToLower() == "insert horizontal rule")
                {
                    _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField)).SendKeys(Keys.Shift + Keys.Enter);
                }
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "FormatText");
            }
        }

        public Boolean VerifyFormat(string format, string text)
        {
            try
            {
                var items = _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent));

                Console.WriteLine(items[items.Count - 1].Text);
                reportLine.TakeScreenShot(_driver.Current, "VerifyFormat_" + format + "_" + text);
                if (format.ToLower() == "insert horizontal rule")
                {
                    return items[items.Count - 1].FindElementByXPath("//Separator").Displayed;
                }
                else if (format.ToLower() == "insert link")
                {
                    var imageresult = items[items.Count - 1].FindElementByXPath(TeamsAppObjects.xpath_Chat_Format_InsertLink);
                    var removeButtonResult = items[items.Count - 1].FindElementByXPath(TeamsAppObjects.xpath_Chat_Format_InsertLinkButton);
                    if (imageresult.Displayed && removeButtonResult.Displayed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return items[items.Count - 1].FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_SentMessgae))[2].Text == text;
                }
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "VerifyFormat");
                return false;
            }
        }

        public Boolean WaitUntilGettingMessage(string user)
        {
            //string sExecTime = DateTime.Now.ToString("t");
            if (ExecTime.sExecTime == null)
            {
                //Take posted time
                ExecTime.sExecTime = DateTime.Now.ToString("t");
            }
            string sExecTime = ExecTime.sExecTime;

            //To clarify this operation starts.
            _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_EditField))[0].Click();

            Console.WriteLine("INSIDE 'WaitUntil' METHOD: {0}", sExecTime);
            _driver.Current.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            int i = 1000;
            while (i < 30000)
            {
                try
                {
                    var items = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent)).FindElementsByXPath("//ListItem");
                    string sLastMessageTime = items[items.Count - 1].FindElementsByXPath("//Text")[1].Text;
                    string sLastPostedUser = items[items.Count - 1].FindElementsByXPath("//Text")[0].Text.Split(", ")[0];
                    if (DateTime.Parse(sLastMessageTime) >= DateTime.Parse(sExecTime) && sLastPostedUser == user)
                    {
                        Console.WriteLine("EXECUTION TIME: {0}, LATEST POST TIME: {1}", sExecTime, sLastMessageTime);
                        Console.WriteLine("LAST POSTED USER: {0}", sLastPostedUser);
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

        public Boolean WaitUntilGettingParticularMessage(string TargetText, string user)
        {
            string sUser = user;
            string ErrorText = "";
            Console.WriteLine("TARGET TEXT: {0}, USER: {1}", TargetText, sUser);
            //Take posted time
            if (ExecTime.sExecTime == null)
            {
                ExecTime.sExecTime = DateTime.Now.ToString("HH:mm");
            }
            string sExecTime = ExecTime.sExecTime;
            Console.WriteLine("INSIDE 'WaitUntil' METHOD: {0}", sExecTime);
            int CountForAttachment = TargetText.Split(".").Length;
            int EmojiTypeCount = TargetText.Split(":").Length;
            Console.WriteLine("CountForAttachment: {0}, EmojiTypeCount: {1}", CountForAttachment, EmojiTypeCount);
            int i = 1000;
            while (i < 30000)
            {
                System.Threading.Thread.Sleep(1000);
                try
                {
                    Console.WriteLine(TargetText);
                    string sLastMessage = "";
                    //var items = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent)).FindElementsByXPath("//ListItem");
                    var items = _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent));
                    string sText = items[items.Count - 1].Text;
                    Console.WriteLine("CHECK0: {0}", sText);
                    string sLastPostedUser = "";
                    string sLastMessageTime = "";
                    string[] allTexts = sText.Split(" ");
                    int iTextLength = allTexts.Length;
                    for (int n = 0; n < iTextLength; n++)
                    {
                        if(allTexts[n].Contains("m365"))
                        {
                            //sLastPostedUser = sText.Split(",")[n].Remove(sText.Split(",")[n].Length - 1);
                            sLastPostedUser = allTexts[n];
                            break;
                        }
                    }
                    Console.WriteLine("CHECK1: {0}", sLastPostedUser);
                    sLastMessageTime = allTexts[iTextLength - 2] + " " + allTexts[iTextLength - 1];
                    Console.WriteLine("CHECK2: {0}", sLastMessageTime);
                    //var itemTextXPath = items[items.Count - 1].FindElementsByXPath("//Text");
                    //string sLastMessageTime = itemTextXPath[1].Text;
                    ////string sLastPostedUser = itemTextXPath[0].Text.Split(", ")[0];
                    //string sLastPostedUser = itemTextXPath[0].Text;
                    if (CountForAttachment > 1)
                    {
                        sLastMessage = items[items.Count - 1].FindElementByXPath("//MenuItem").Text;
                    }
                    else if (EmojiTypeCount > 1)
                    {
                        if (TargetText.Contains(":"))
                        {
                            TargetText = TargetText.Split(":")[1];
                        }
                        Console.WriteLine(items[items.Count - 1].Text);
                        sLastMessage = items[items.Count - 1].Text;
                        //sLastMessage = items[items.Count - 1].FindElementByXPath("//Group[starts-with(@Name,\"" + TargetText.Split(":")[1] + "\")]").Text;
                        //TargetText = TargetText.Split(":")[1];
                    }
                    else
                    {
                        sLastMessage = items[items.Count - 1].FindElementByXPath("//Text").Text;
                        Console.WriteLine("sLastMessage: {0}", sLastMessage);
                        //sLastMessage = itemTextXPath[2].Text;
                    }
                    Console.WriteLine("LAST_MESSAGE_TIME: {0}, LAST_MESSAGE_USER: {1}, LAST_MESSAGE_TEXT: {2}", sLastMessageTime, sLastPostedUser, sLastMessage);
                    Console.WriteLine("VERIFY TIME:{0}, VERIFY USER:{1}, VERIFY TEXT:{2}", sExecTime, sUser, TargetText);
                    ErrorText = "LAST_MESSAGE_TIME: " + sLastMessageTime + ", LAST_MESSAGE_USER: " + sLastPostedUser + ", LAST_MESSAGE_TEXT: " + sLastMessage;
                    if (DateTime.Parse(sLastMessageTime) >= DateTime.Parse(sExecTime) && sLastPostedUser == sUser && sLastMessage.Contains(TargetText))
                    {
                        //Console.WriteLine("EXECUTION TIME: {0}, LATEST POST TIME: {1}", sExecTime, sLastMessageTime);
                        //Console.WriteLine("LAST POSTED USER: {0}", sLastPostedUser);
                        reportLine.WriteLine(ErrorText);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                i += 1000;
                int num = i / 1000;
                Console.WriteLine("{0} TIMES", num.ToString());
            }
            reportLine.WriteLine(ErrorText);
            reportLine.TakeScreenShot(_driver.Current, "WaitUntilGettingParticularMessage");
            return false;
        }

        public string VerifyLastPostedMessage(string user)
        {
            try
            {
                //System.Threading.Thread.Sleep(5000);
                //var items = _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent)).FindElementsByXPath("//ListItem");
                //string sPostedUser = items[items.Count - 1].FindElementsByXPath("//Text")[0].Text.Split(", ")[0];
                var items = _driver.Current.FindElementsByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent));
                string sPostedUser = items[items.Count - 1].Text.Split(" ")[3];
                Console.WriteLine("TARGET USER: {0}, POSTED USER: {1}", user, sPostedUser);
                if (sPostedUser == user)
                {
                    //return items[items.Count - 1].FindElementsByXPath("//Text")[2].Text;
                    string[] retruntTexts = items[items.Count - 1].Text.Split(" ");
                    Console.WriteLine($"{retruntTexts[0]} {retruntTexts[1]} {retruntTexts[2]}");
                    return $"{retruntTexts[0]} {retruntTexts[1]} {retruntTexts[2]}";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                return null;
            }
        }

        public void ClickSentMessageOptions(string sSentText)
        {
            //string targetArea = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent) + "//ListItem";
            //string sSentText_xpath = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent) + "//ListItem" + TeamsAppObjects.xpath_Chat_TrgetPostedText.Replace("#TEXT#", sSentText);
            string targetArea = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent);
            string sSentText_xpath = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent) + TeamsAppObjects.xpath_Chat_TrgetPostedText.Replace("#TEXT#", sSentText);
            Console.WriteLine(sSentText_xpath);
            WaitUtil.WaitForElementAvailabilityByScrollUp(sSentText_xpath, targetArea, _driver.Current);

            CommonUtil.RetryMouseOverUntilTargetAvailable(sSentText_xpath, CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Reaction_MoreOption), _driver.Current);

            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Reaction_MoreOption)).Click();

        }

        public Boolean VerifySentMessageOptions(string sOption)
        {
            var eMenuElement = _driver.Current.FindElementsByXPath("//MenuItem");
            int iMenuCount = eMenuElement.Count;
            foreach (WindowsElement menu in eMenuElement)
            {
                if (menu.Text.Equals(sOption))
                    return true;
            }
            return false;
        }

        public void RightClickUserChat(string sUser)
        {
            string sXpath_UserSelection = "//TreeItem[(contains(@Name,\"#USER#\")) and (starts-with(@Name,\"Profile picture\"))]";
            sXpath_UserSelection = sXpath_UserSelection.Replace("#USER#", sUser);
            var sUserElement = _driver.Current.FindElementByXPath(sXpath_UserSelection);
            Actions CtRightClick = new Actions(_driver.Current);
            CtRightClick.MoveToElement(sUserElement).ContextClick().Build().Perform();
        }

        public Boolean VerifyUserChatOptions(string sOption)
        {
            var eMenuElement = _driver.Current.FindElementsByXPath("//Button");
            int iMenuCount = eMenuElement.Count;
            foreach (WindowsElement menu in eMenuElement)
            {
                if (menu.Text.Equals(sOption))
                    return true;
            }
            return false;
        }

        public void ClickAnotherUserSentMessageOptions(string sUser)
        {
            string sSentMessages = CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_MessageContent);
            string sSentText_xpath = TeamsAppObjects.xpath_Chat_AnotherUserMessageId;
            sSentText_xpath = sSentText_xpath.Replace("#USER#", sUser);
            Console.WriteLine(sSentText_xpath);
            var eleSentText = _driver.Current.FindElementsByXPath(sSentText_xpath);
            WaitUtil.WaitForElementAvailabilityByScrollUp(sSentText_xpath, sSentMessages, _driver.Current);

            Actions actMouseOver = new Actions(_driver.Current);
            actMouseOver.MoveToElement(eleSentText[eleSentText.Count - 1]).Build().Perform();

            _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Reaction_MoreOption)).Click();

        }

        public Boolean OpenTabInChat(string sTabName)
        {
            try
            {
                WaitUtil.WaitForElementAvailability("//TabItem[@Name=\"" + sTabName + "\"]", _driver.Current);
                _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_Chat_ChatTab).FindElementByXPath("//TabItem[@Name=\"" + sTabName + "\"]").Click();
                return true;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "OpenTabInChat");
                return false;
            }
        }

        public Boolean VerifyUserPanelIsDisplayed(string userName)
        {
            try
            {
                string userID = DataReader.getUserName(userName);
                Console.WriteLine("EXPECTED USERID: {0}",userID);
                System.Threading.Thread.Sleep(3000);
                return WaitUtil.WaitForElementAvailability(TeamsAppObjects.xpath_Chat_UserCard.Replace("#USERID#", userID), _driver.Current);
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "VerifyUserPanelIsDisplayed");
                return false;
            }
        }

        //New for v6.2 Remove user
        public Boolean RemoveUsersFromGroupChat(string user)
        {
            try
            {
                string userID = DataReader.getUserName(user);
                _driver.Current.FindElementByXPath("//Button[starts-with(@Name, \"View and add participants\")]").Click();
                System.Threading.Thread.Sleep(1000);
                _driver.Current.FindElementByXPath("//MenuItem[starts-with(@Name, \"Opens card Available " + userID + "\")]").FindElementByXPath("//Button[@Name=\"chat_removeChatButton\"]").Click();
                if (WaitUtil.WaitUntilElementAvailability("//Custom[starts-with(@AutomationId,\"ngdialog\")]", _driver.Current))
                {
                    _driver.Current.FindElementByXPath("//Custom[starts-with(@AutomationId,\"ngdialog\")]").FindElementByXPath("//Button[@Name=\"Remove\"]").Click();
                }
                return true;
            }
            catch(Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "RemoveUsersFromGroupChat");
                return false;
            }
        }

        public int VerifyNumberOfGroupChatMember()
        {
            try
            {
                string sAddMemberButton = _driver.Current.FindElementByXPath("//Button[starts-with(@Name, \"View and add participants\")]").Text;
                string sMemberNum = sAddMemberButton.Split(" ")[4];
                _driver.Current.FindElementByXPath("//Button[starts-with(@Name, \"" + sAddMemberButton + "\")]").Click();
                System.Threading.Thread.Sleep(1000);
                int memberListNum = _driver.Current.FindElementsByXPath("//MenuItem[starts-with(@Name, \"Opens card Available m365\")]").Count;
                if(int.Parse(sMemberNum) == memberListNum)
                {
                    return memberListNum;
                }
                reportLine.WriteLine("WARNING: The number of \"add member button\" and \"list item inside the button\" is not match!!");
                return 0;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "VerifyNumberOfGroupChatMember");
                return 0;
            }
        }

        public Boolean VerifyNameOfGroupChatMember(string user)
        {
            try
            {
                string userID = DataReader.getUserName(user);
                _driver.Current.FindElementByXPath("//Button[starts-with(@Name, \"View and add participants\")]").Click();
                System.Threading.Thread.Sleep(1000);
                var memberList = _driver.Current.FindElementsByXPath("//MenuItem[starts-with(@Name, \"Opens card Available m365\")]");
                int memberListNum = memberList.Count;
                for (int i = 0; i < memberListNum; i++)
                {
                    Console.WriteLine(memberList[i].Text);
                    if (memberList[i].Text.Contains(userID))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "VerifyNameOfGroupChatMember");
                return false;
            }
        }
        // by here Remove User

        public bool MIBSearchEmployee(string sLoggedUser,string sMIBUser,string searchUser)
        {
            //Search the employee
            try
            {
                _driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Toolbar)).Click();
                if (WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_SearchToolbar, _driver.Current, 3000))
                    _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_SearchToolbar).Click();
                //Commenting below code because no List item available - ############################ CODE CHANGE
                //CONFIGURATION CHANGES IN QA 
                //Date: 26 June 2023
                //_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_SearchField).SendKeys(searchUser);
                //New code - 26/06/2023
                ////############################ CODE CHANGE
                CommonUtil.ClipBoard_Copying(searchUser);
                _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_SearchField).SendKeys(Keys.Control + "v");

                //############################ CODE CHANGE
                bool userExists = WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xpath_SearchAutolist).Replace("#USER#", searchUser.ToUpper()), _driver.Current, 5000);
                if (userExists == true)
                {
                    reportLine.WriteLine("User "+ sLoggedUser + " able to connect with user: " + sMIBUser);
                    reportLine.TakeScreenShot(_driver.Current, "U_MIB");
                }
                else
                {
                    reportLine.WriteLine("User " + sLoggedUser + " not able to connect with user: " + sMIBUser);
                    reportLine.TakeScreenShot(_driver.Current, "U_MIB");
                }
                _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_SearchField).Clear();

                return userExists;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                reportLine.TakeScreenShot(_driver.Current, "MIB_Search");
                return false;
            }
        }

    }
}