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
    public class ChatPage
    {
        private readonly WebTestDriver _Idriver;
        Reporting reportLine;
        private static ISpecFlowOutputHelper _outHelper;
        public ChatPage(WebTestDriver Idriver, ISpecFlowOutputHelper outHelper)
        {
            //_driver = driver.Current;
            _Idriver = Idriver;
            _outHelper = outHelper;
            reportLine = new Reporting(_outHelper);
        }

        public bool SearchUser(String sUser)
        {
            //Searching user  
            //searchInputField
            _Idriver.Current.SwitchTo().Window(_Idriver.Current.CurrentWindowHandle);

            _Idriver.Current.FindElement(By.Id("control-input")).Click();

            _Idriver.Current.FindElement(By.Id("searchInputField")).Click();

            _Idriver.Current.FindElement(By.Id("searchInputField")).SendKeys(sUser);

            //click the user
            ////span[text()='HKGUSER.M365AP01' and @class='highlighted']
            String eleText = "//span[text()='#USER#' and @class='highlighted']";
            eleText = eleText.Replace("#USER#", sUser);

            if (WaitUtil.WaitForVisible_bool(eleText, _Idriver.Current, 5000))
            {
                _Idriver.Current.FindElement(By.XPath(eleText)).Click();
                System.Threading.Thread.Sleep(5000);
                return true;
            }
            else
            {
                reportLine.WriteLine("User logged in: " + sUser);
                reportLine.TakeScreenShot(_Idriver.Current, "UserNotAva");
                return false;
            }
        }

        public String VerifySentTextByText(string sSentUser, string sTargetText)
        {
            Console.WriteLine("USER: {0}, TEXT: {1}", sSentUser, sTargetText);
            string eleReadMessage = TeamsWebObjects.xpath_chat_eleReadMessage.Replace("#USER#", sSentUser).Replace("#MESSAGE#", sTargetText);
            Console.WriteLine(eleReadMessage);
            if (WaitUtil.WaitForVisible_bool("(" + eleReadMessage + ")[1]", _Idriver.Current, 5000))
            {
                int countReceivesMessage = _Idriver.Current.FindElements(By.XPath(eleReadMessage)).Count;
                string sentMessage = _Idriver.Current.FindElement(By.XPath("(" + eleReadMessage + ")[" + countReceivesMessage + "]")).Text;
                Console.WriteLine("GOT TEXT: {0}", sentMessage);
                Console.WriteLine("INPUT TEXT: {0}", sTargetText);
                return sentMessage;
            }
            else
            {
                return null;
            }
        }

        public string ReadMessageFromUser(String sUser)
        {
            try
            {
                //Sent user name should be passed in parameter
                //Switching the frame
                _Idriver.Current.SwitchTo().ParentFrame();
                _Idriver.Current.SwitchTo().Frame(0);
                string receivedMessage = "";
                //Message list
                String eleReadMessage = "//div[@role='heading' and contains(text(), '#USER#')]/../div[2]//div[contains(@class,'ui-chat__messagecontent')]/div";
                eleReadMessage = eleReadMessage.Replace("#USER#", sUser);
                if (WaitUtil.WaitForVisible_bool("(" + eleReadMessage + ")[1]", _Idriver.Current, 5000))
                {
                    int countReceivesMessage = _Idriver.Current.FindElements(By.XPath(eleReadMessage)).Count;
                    receivedMessage = _Idriver.Current.FindElement(By.XPath("(" + eleReadMessage + ")[" + countReceivesMessage + "]")).Text;
                    reportLine.WriteLine("User received message: " + receivedMessage + " from " + sUser);
                    return receivedMessage;
                }
                Console.WriteLine("Read Message: " + receivedMessage);
                return receivedMessage;
            }
            catch (Exception e)
            {
                reportLine.WriteLine(e.ToString());
                return "error";
            }
        }

        public Boolean ReadMessageFromUserwithTime(string sTextMessage, string sUser)
        {
            try
            {
                //Sent user name should be passed in parameter
                //Switching the frame
                _Idriver.Current.SwitchTo().ParentFrame();
                _Idriver.Current.SwitchTo().Frame(0);
                string receivedMessage = "";
                //Message list
                string eleReadMessage = "//div[@role='heading' and contains(text(), '#USER#')]/../div[2]//div[contains(@aria-label, '#MESSAGE#')]";
                eleReadMessage = eleReadMessage.Replace("#USER#", sUser).Replace("#MESSAGE#", sTextMessage);
                if (WaitUtil.WaitForVisible_bool("(" + eleReadMessage + ")[1]", _Idriver.Current, 5000))
                {
                    for (int iCheck = 0; iCheck <= 5; iCheck++)
                    {
                        int countReceivesMessage = _Idriver.Current.FindElements(By.XPath(eleReadMessage)).Count;
                        receivedMessage = _Idriver.Current.FindElement(By.XPath("(" + eleReadMessage + ")[" + countReceivesMessage + "]")).Text;
                        if (receivedMessage.Equals(sTextMessage))
                        {
                            reportLine.WriteLine("User received message(in Web): " + receivedMessage + " from " + sUser);
                            return true;
                        }
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                Console.WriteLine("Read Message: " + receivedMessage + " NOT RECEVIED EXPECTED MESSAGE FROM USER1 - FAILED");
                reportLine.WriteLine("Read Message: " + receivedMessage + " NOT RECEVIED EXPECTED MESSAGE FROM USER - FAILED");
                return false;
            }
            catch(Exception e)
            {
                reportLine.WriteLine(e.ToString());
                return false;
            }
            
        }

        public Boolean ReadMessageFileFromUserwithTime(string sTextMessage, string sFileName, string sUser)
        {
            //Sent user name should be passed in parameter
            //Switching the frame
            _Idriver.Current.SwitchTo().ParentFrame();
            _Idriver.Current.SwitchTo().Frame(0);
            string receivedMessage = "";
            //Message list
            String eleReadMessage = "//div[@role='heading' and contains(text(), '#USER#')]/../div[2]//div[contains(@class,'ui-chat__messagecontent')]/div";
            eleReadMessage = eleReadMessage.Replace("#USER#", sUser);
            if (WaitUtil.WaitForVisible_bool("(" + eleReadMessage + ")[1]", _Idriver.Current, 5000))
            {
                for (int iCheck = 0; iCheck <= 5; iCheck++)
                {
                    int countReceivesMessage = _Idriver.Current.FindElements(By.XPath(eleReadMessage)).Count;
                    receivedMessage = _Idriver.Current.FindElement(By.XPath("(" + eleReadMessage + ")[" + countReceivesMessage + "]")).Text;
                    if (receivedMessage.Equals(sTextMessage))
                    {
                        string eleFileRead = eleReadMessage + "/following::div[contains(@title,'#FILENAME#')]";
                        eleFileRead = eleFileRead.Replace("#FILENAME#", sFileName);
                        if (_Idriver.Current.FindElement(By.XPath(eleFileRead)).Displayed)
                        {
                            reportLine.WriteLine("User received message and file(in Web): " + receivedMessage + " and file: " + sFileName + " from " + sUser);
                            return true;
                        }
                        else
                        {
                            reportLine.WriteLine("User received message and file(in Web): " + receivedMessage + " and file: " + sFileName + " from " + sUser + " FILE NOT RECEIVED");
                            return false;
                        }
                    }
                    System.Threading.Thread.Sleep(2000);
                }
            }
            reportLine.WriteLine("Read Message: " + receivedMessage + " NOT RECEVIED EXPECTED MESSAGE FROM USER2 - FAILED");
            return false;
        }

        public Boolean ReadSingleEmojiFromUser(string sEmojiName, string sUser)
        {
            //Sent user name should be passed in parameter
            //Switching the frame
            _Idriver.Current.SwitchTo().ParentFrame();
            _Idriver.Current.SwitchTo().Frame(0);
            string receivedMessage = "";
            //Message list
            string eleReadMessage = "//div[@role='heading' and contains(text(), '#USER#')]/../div[2]//div[contains(@aria-label, '#MESSAGE#')]";
            eleReadMessage = eleReadMessage.Replace("#USER#", sUser).Replace("#MESSAGE#", sEmojiName);
            if (WaitUtil.WaitForVisible_bool("(" + eleReadMessage + ")[1]", _Idriver.Current, 5000))
            {
                for (int iCheck = 0; iCheck <= 5; iCheck++)
                {
                    int countReceivesMessage = _Idriver.Current.FindElements(By.XPath(eleReadMessage)).Count;
                    if (countReceivesMessage > 0)
                    {
                        reportLine.WriteLine("User received emoji(in Web): " + sEmojiName + " from " + sUser);
                        reportLine.TakeScreenShot(_Idriver.Current, "Emoji_" + sEmojiName);
                        return true;
                    }
                    System.Threading.Thread.Sleep(2000);
                }
            }
            reportLine.WriteLine("Read Message: " + receivedMessage + " NOT RECEVIED EXPECTED MESSAGE FROM USER3 - FAILED");
            return false;
        }

        public Boolean TypeMessage(string sMessage)
        {
            _Idriver.Current.SwitchTo().Window(_Idriver.Current.CurrentWindowHandle);
            string sentMessage = "";
            try
            {
                //Switching the frame
                _Idriver.Current.SwitchTo().ParentFrame();
                _Idriver.Current.SwitchTo().Frame(0);
                //Sent user name should be passed in parameter
                //Switching the frame
                _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_editfiled)).Click();
                _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_editfiled)).SendKeys(sMessage);
                
                return true;
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                return false;
            }
        }

        public string ClickSendAndVerify(string sMessage, string sSentUser)
        {
            string sentMessage = "";
            try
            {
                //Click the Send Message
                _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_sendbutton)).Click();
                System.Threading.Thread.Sleep(10000);
                return VerifySentTextByText(sSentUser, sMessage);
                //string eleReadMessage = "//div[@role='heading' and contains(text(), '#USER#')]/../div[2]//div[contains(@aria-label, '#MESSAGE#')]";
                //eleReadMessage = eleReadMessage.Replace("#USER#", sSentUser).Replace("#MESSAGE#", sMessage);
                //
                //if (WaitUtil.WaitForVisible_bool("(" + eleReadMessage + ")[1]", _Idriver.Current, 5000))
                //{
                //    int countReceivesMessage = _Idriver.Current.FindElements(By.XPath(eleReadMessage)).Count;
                //    sentMessage = _Idriver.Current.FindElement(By.XPath("(" + eleReadMessage + ")[" + countReceivesMessage + "]")).Text;
                //    if (sMessage.Equals(sentMessage))
                //        reportLine.WriteLine("Chat(WEB): sent a message: " + sentMessage + " To " + sSentUser);
                //    return sentMessage;
                //}

                //return sentMessage;
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                return sentMessage;
            }
        
        }

        public Boolean ClickSendButton()
        {
            string sSendButton = "//button[@name='send']";
            try
            {
                WaitUtil.WaitForVisible_bool(sSendButton, _Idriver.Current, 5000);
                //Click the Send Message
                _Idriver.Current.FindElement(By.XPath(sSendButton)).Click();
                System.Threading.Thread.Sleep(5000);
                return true;
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Send button click Failed (WEB)");
                reportLine.WriteLine("Exception: " + e);
                return false;
            }
        }


        
        public Boolean SendEmojiAndVerify(string sEmojiType, string sEmojiName)
        {
            _Idriver.Current.SwitchTo().Window(_Idriver.Current.CurrentWindowHandle);
            string sentMessage = "";
            try
            {
                _Idriver.Current.SwitchTo().ParentFrame();
                _Idriver.Current.SwitchTo().Frame(0);
                //Emot Icon object - XPATH
                string eleEmotIcon = "//button[@name='EmoticonPicker']";
                //Type of Emot - XPATH
                string eleEmotType = "//a[@title='#EMOTTYPE#']";
                eleEmotType = eleEmotType.Replace("#EMOTTYPE#", sEmojiType);
                //Emot Selection - XPATH
                string eleEmotName = "//button[@data-tid='emoticon-button-#EMOTNAME#']";
                eleEmotName = eleEmotName.Replace("#EMOTNAME#", sEmojiName.ToLower().Replace(" ",""));

                //Click Emot Icon
                _Idriver.Current.FindElement(By.XPath(eleEmotIcon)).Click();
                //Click Emot Type
                WaitUtil.WaitForVisible_bool(eleEmotType, _Idriver.Current, 5000);
                _Idriver.Current.FindElement(By.XPath(eleEmotType)).Click();
                //Select the Emot from the pane
                WaitUtil.WaitForVisible_bool(eleEmotName, _Idriver.Current, 5000);
                _Idriver.Current.FindElement(By.XPath(eleEmotName)).Click();
                reportLine.WriteLine("Chat(WEB): sent a Emoji: " + sEmojiName);

                return true;
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                return false;
            }
        }

        public Boolean OpenTabInChat(string sTabName)
        {
            try
            {
                WaitUtil.WaitForVisible_bool(TeamsWebObjects.id_chat_Tab_OrgButton, _Idriver.Current);
                _Idriver.Current.FindElement(By.Id(TeamsWebObjects.id_chat_Tab_OrgButton)).Click();
                if (WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_chat_SerchPicker, _Idriver.Current))
                {
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                return false;
            }
        }

        public Boolean VerifyUserPanelIsDisplayed(string sUser)
        {
            try
            {
                string xpath = TeamsWebObjects.xpath_chat_UserCard_OrgTab.Replace("#USER#", sUser);
                if (WaitUtil.WaitForVisible_bool(xpath, _Idriver.Current))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                return false;
            }
        }

        public Boolean TypeMentionTagandText(string sUser, string sMessage)
        {
            try
            {
                //Switching the frame
                _Idriver.Current.SwitchTo().ParentFrame();
                _Idriver.Current.SwitchTo().Frame(0);

                _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_editfiled)).Click();
                //Mention
                string sMentionItemXpath = TeamsWebObjects.xpath_chat_mentionuser_listitem.Replace("#USER#", sUser);
                string sMentionText = "@" + sUser;
                CommonUtil.ClipBoard_Copying(sMentionText);
                _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_editfiled)).SendKeys(Keys.Control + "v");
                _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_editfiled)).SendKeys(" ");
                System.Threading.Thread.Sleep(500);
                _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_editfiled)).SendKeys(Keys.Backspace);
                WaitUtil.WaitForVisible_bool(sMentionItemXpath, _Idriver.Current, 5000);
                _Idriver.Current.FindElement(By.XPath(sMentionItemXpath)).Click();
                //Type sending message
                _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_editfiled)).SendKeys(sMessage);
                return true;
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                return false;
            }
        }

        public Boolean verifyMentionItem(string sMentionedUser, string MentioneText)
        {
            try
            {
                //return VerifySentTextByBoolean(sMentionedUser, MentioneText);
                //Console.WriteLine("USER: {0}, TEXT: {1}", sMentionedUser, MentioneText);
                string eleReadMessage = TeamsWebObjects.xpath_chat_eleReadMessage.Replace("#USER#", sMentionedUser).Replace("#MESSAGE#", MentioneText);
                string eleMentionTag = eleReadMessage + TeamsWebObjects.xpath_chat_PostedUserMentionTag;
                //Console.WriteLine(eleMentionTag);
                if (WaitUtil.WaitForVisible_bool("(" + eleReadMessage + ")[1]", _Idriver.Current, 5000))
                {
                    int countReceivesMessage = _Idriver.Current.FindElements(By.XPath(eleMentionTag)).Count;
                    string sentMessage = _Idriver.Current.FindElement(By.XPath("(" + eleMentionTag + ")[" + countReceivesMessage + "]")).Text;
                    //Console.WriteLine("TEXT: {0}", sentMessage);
                    if (MentioneText.Equals(sentMessage))
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
                    return false;
                }
            }
            catch(Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                return false;
            }
        }

        public Boolean OpenAndVerifyDeliveryOption()
        {
            try
            {
                _Idriver.Current.SwitchTo().ParentFrame();
                _Idriver.Current.SwitchTo().Frame(0);
                _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_TextOption_Delivery)).Click();
                if (WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_chat_DeliveryOption_popup, _Idriver.Current, 5000))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                return false;
            }
        }

        public void SelectDeliveryOption(string optionName)
        {
            try
            {
                switch (optionName.ToLower())
                {
                    case "standard":
                        _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_StandardDelivery)).Click();
                        break;
                    case "important":
                        _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_ImportantDelivery)).Click();
                        break;
                    case "urgent":
                        _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_UrgentDelivery)).Click();
                        break;
                }
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
            }
        }

        public String ClickAndVerifyDeliveryOption(string OptionName)
        {
            try
            {
                //string eleFlagText = "(" + TeamsWebObjects.xpath_chat_DeliveryFlag_Textfield + ")[0]";
                //Console.WriteLine(eleFlagText);
                SelectDeliveryOption(OptionName);
                System.Threading.Thread.Sleep(2000);
                if (WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_chat_DeliveryFlag_Textfield, _Idriver.Current, 5000))
                {
                    Console.WriteLine(_Idriver.Current.FindElements(By.XPath(TeamsWebObjects.xpath_chat_DeliveryFlag_Textfield))[0].Text);
                    return _Idriver.Current.FindElements(By.XPath(TeamsWebObjects.xpath_chat_DeliveryFlag_Textfield))[0].Text.ToUpper();
                }
                return null;
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                return null;
            }
        }

        public String ClickSendAndVerifySendMessage(string sPostedUser, string sPostedText, string sDeliveryOptionName="standard")
        {
            try
            {
                _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_sendbutton)).Click();
                System.Threading.Thread.Sleep(10000);
                string eleReadMessage = TeamsWebObjects.xpath_chat_eleReadMessage.Replace("#USER#", sPostedUser).Replace("#MESSAGE#", sPostedText);
                string elePostedDeliveryText = "";
                switch (sDeliveryOptionName.ToLower())
                {
                    case "important":
                        elePostedDeliveryText = eleReadMessage + TeamsWebObjects.xpath_chat_PostedImportantDeliveryText;
                        break;
                    case "urgent":
                        elePostedDeliveryText = eleReadMessage + TeamsWebObjects.xpath_chat_PostedUrgnetDeliveryText;
                        break;
                }
                if (WaitUtil.WaitForVisible_bool("(" + eleReadMessage + ")[1]", _Idriver.Current, 5000))
                {
                    int countReceivesMessage = _Idriver.Current.FindElements(By.XPath(elePostedDeliveryText)).Count;
                    string sPostedDeliveryText = _Idriver.Current.FindElement(By.XPath("(" + elePostedDeliveryText + ")[" + countReceivesMessage + "]")).Text;
                    Console.WriteLine("POSTED DELIVERY TEXT: {0}", sPostedDeliveryText);
                    return sPostedDeliveryText;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                return null;
            }
        }

        public void OpenTextFormat()
        {
            try
            {
                _Idriver.Current.SwitchTo().ParentFrame();
                _Idriver.Current.SwitchTo().Frame(0);
                _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_TextOption_Format)).Click();
                System.Threading.Thread.Sleep(1000);
            }
            catch(Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
            }
        }

        public void FormatText(string format, string type, string text)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                if (type != "")
                {
                    //string sFormat = TeamsAppObjects.xpath_Chat_Format_Item.Replace("#ITEM#", format);
                    //Console.WriteLine(CommonUtil.ConvertLang(sFormat));
                    //_driver.Current.FindElementByXPath(CommonUtil.ConvertLang(TeamsAppObjects.xpath_Chat_Format_Toolbar)).FindElementByXPath(CommonUtil.ConvertLang(sFormat)).Click();
                    //_Idriver.Current.SwitchTo().ParentFrame();
                    //_Idriver.Current.SwitchTo().Frame(0);
                    switch (format.ToLower())
                    {
                        case "bold":
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_bold_option)).Click();
                            break;
                        case "italic":
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_italic_option)).Click();
                            break;
                        case "underline":
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_underline_option)).Click();
                            break;
                        case "strike":
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_strike_option)).Click();
                            break;
                        case "text highlight color":
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_texthighlight_option)).Click();
                            break;
                        case "text highlight colour":
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_texthighlight_option)).Click();
                            break;
                        case "font color":
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_fontcolor_option)).Click();
                            break;
                        case "font colour":
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_fontcolor_option)).Click();
                            break;
                        case "font size":
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_fontsize_option)).Click();
                            break;
                        case "paragraph":
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_richstyle_option)).Click();
                            break;
                        case "insert link":
                            WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_chat_insertlink_option, _Idriver.Current);
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_insertlink_option)).Click();
                            System.Threading.Thread.Sleep(5000);
                            WaitUtil.WaitForVisible_bool("//form[@aria-label='Insert link']", _Idriver.Current);
                            System.Threading.Thread.Sleep(5000);
                            WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_chat_insertlink_text, _Idriver.Current);
                            System.Threading.Thread.Sleep(5000);
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_insertlink_text)).SendKeys(text);
                            System.Threading.Thread.Sleep(2000);
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_insertlink_url)).Click();
                            System.Threading.Thread.Sleep(2000);
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_insertlink_url)).SendKeys(type);
                            Console.WriteLine(_Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_insertbutton)).Text);
                            _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_insertbutton)).Click();
                            System.Threading.Thread.Sleep(3000);
                            break;
                    }
                }
                if (format.ToLower() != "insert link")
                {
                    _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_editfiled)).Click();
                    CommonUtil.ClipBoard_Copying(text);
                    //Paste the value in the field
                    _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_editfiled)).SendKeys(Keys.Control + "v");
                }
                if (format.ToLower() == "insert horizontal rule")
                {
                    _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_codesnippet_option)).Click();
                    _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_codesnippet_option)).SendKeys(Keys.Shift + Keys.Enter);
                }
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
            }
        }

        public Boolean VerifyFormat(string sTargetText, string sSentUser, string sFormat)
        {
            try
            {
                string eleReadMessage = TeamsWebObjects.xpath_chat_eleReadMessage.Replace("#USER#", sSentUser).Replace("#MESSAGE#", sTargetText);
                Console.WriteLine(eleReadMessage);
                if (WaitUtil.WaitForVisible_bool("(" + eleReadMessage + ")[1]", _Idriver.Current, 5000))
                {
                    int countReceivesMessage = _Idriver.Current.FindElements(By.XPath(eleReadMessage)).Count;
                    var eleTargetPost = _Idriver.Current.FindElement(By.XPath("(" + eleReadMessage + ")[" + countReceivesMessage + "]"));
                    Boolean result = false;
                    switch (sFormat.ToLower())
                    {
                        case "bold":
                            result = eleTargetPost.FindElement(By.XPath("//strong")).Displayed;
                            break;
                        case "italic":
                            result = eleTargetPost.FindElement(By.XPath("//i")).Displayed;
                            break;
                        case "underline":
                            result = eleTargetPost.FindElement(By.XPath("//u")).Displayed;
                            break;
                        case "strike":
                            result = eleTargetPost.FindElement(By.XPath("//s")).Displayed;
                            break;
                        case "text highlight color" or "text highlight colour":
                            result = eleTargetPost.FindElement(By.XPath("//span[contains(@style,'background-color')]")).Displayed;
                            break;
                        case "font color" or "font colour":
                            result = eleTargetPost.FindElement(By.XPath("//span[contains(@style,'color')]")).Displayed;
                            break;
                        case "font size":
                            result = eleTargetPost.FindElement(By.XPath("//span[contains(@style,'font-size')]")).Displayed;
                            break;
                        case "paragraph":
                            result = eleTargetPost.FindElement(By.XPath("//h.*")).Displayed;
                            break;
                        case "insert link":
                            string sentMessage = eleTargetPost.Text;
                            Boolean displayResult = eleTargetPost.FindElement(By.XPath("//img[@data-tid='url-preview-image']")).Displayed;
                            if (sentMessage.Equals(sTargetText) && displayResult)
                            {
                                result = true;
                            }
                            break;
                    }
                    return result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                return false;
            }
        }

        // Video Call related
        public Boolean VerifyCallButtonsAvailability(string sButtonName)
        {
            try
            {
                if(sButtonName.ToLower() == "video")
                {
                    WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_chat_videocall_button, _Idriver.Current);
                    return _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_videocall_button)).Enabled;
                }
                else if (sButtonName.ToLower() == "phone")
                {
                    WaitUtil.WaitForVisible_bool(TeamsWebObjects.xpath_chat_phonecall_button, _Idriver.Current);
                    return _Idriver.Current.FindElement(By.XPath(TeamsWebObjects.xpath_chat_phonecall_button)).Enabled;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                reportLine.WriteLine("Exception: " + e);
                return false;
            }
        }
    }

}
    