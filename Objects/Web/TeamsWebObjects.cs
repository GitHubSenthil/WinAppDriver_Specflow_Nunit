using System;
using System.Collections.Generic;
using System.Text;

namespace TeamsWindowsApp.Objects.Web
{
    class TeamsWebObjects
    {
        //Login
        //Login/views
        public static string xpath_login_activityview = "//button[@aria-label='Activity Toolbar']";
        public static string xpath_login_chatview = "//button[@aria-label='Chat Toolbar more options']";
        public static string xpath_login_teamsview = "//button[@aria-label='Teams Toolbar']";
        public static string xpath_login_calendarview = "//button[@aria-label='Calendar Toolbar']";
        public static string xpath_login_callsview = "//button[@aria-label='Calls Toolbar']";
        public static string id_login_othersview = "apps-button";
        //Login/views/verify
        public static string xpath_login_activityviewtitle = "//h1[@data-tid='leftRailHeaderTitle']";
        public static string xpath_login_chatviewtitle = "//button[@data-tid='dropdown-select-toggle-button-text']";
        public static string xpath_login_teamsviewtitle = "//h2[@data-tid='leftRailHeaderTitle']";
        public static string xpath_login_calendarviewtitle = "//span[@data-tid='calendar-app-header-title']";
        public static string xpath_login_callstitle = "//div[@class='middle-call-list-header-title']";
        public static string xpath_login_appflyout = "//div[@data-tid='app-flyout']";

        //Chat
        public static string xpath_chat_editfiled = "//div[contains(@id,'new-message')]/p";
        public static string xpath_chat_sendbutton = "//button[@name='send']";
        public static string xpath_chat_mentionuser_listitem = "//li[@data-tid=\"autocomplete-picker-item-#USER#\"]";
        public static string xpath_chat_eleReadMessage = "//div[@role='heading' and contains(text(), '#USER#')]/../div[2]//div[contains(@aria-label, '#MESSAGE#')]";
        public static string xpath_chat_PostedUserMentionTag = "//span[@itemtype='http://schema.skype.com/Mention']";

        public static string xpath_chat_TextOption_Format = "//button[@data-tid='newMessageCommands-expand-compose']";
        public static string xpath_chat_TextOption_Delivery = "//button[@data-tid='newMessageCommands-importance-picker']";
        public static string xpath_chat_TextOption_Attachment = "//button[@data-tid='newMessageCommands-FilePicker']";
        public static string xpath_chat_TextOption_Emoji = "//button[@data-tid='newMessageCommands-EmoticonPicker']";

        //Chat Tab
        public static string id_chat_Tab_OrgButton = "people.organization";
        public static string xpath_chat_SerchPicker = "//div[@data-tid='people-picker-search']";
        public static string xpath_chat_UserCard_OrgTab = "//button[contains(@aria-label, '#USER#')]";

        //Delivery option
        public static string xpath_chat_DeliveryOption_popup = "//div[@data-tid='newMessageCommands-popup-importance-picker-content']";
        public static string xpath_chat_StandardDelivery = "//li[@data-tid='standard-delivery-option']";
        public static string xpath_chat_UrgentDelivery = "//li[@data-tid='urgent-delivery-option']";
        public static string xpath_chat_ImportantDelivery = "//li[@data-tid='important-delivery-option']";

        public static string xpath_chat_DeliveryFlag_Textfield = "//div[@data-tid='new-message-formatting-toolbar']/following-sibling::div"; //[0]: delivery flag

        public static string xpath_chat_PostedImportantDeliveryText = "/parent::div[1]/parent::div[1]/preceding-sibling::div/div[@data-tid='important-message']";
        public static string xpath_chat_PostedUrgnetDeliveryText = "/parent::div[1]/parent::div[1]/preceding-sibling::div/div[@data-tid='urgent-message']";

        //Format text
        public static string xpath_chat_bold_option = "//button[@data-tid='newMessageCommands-Bold']";
        public static string xpath_chat_italic_option = "//button[@data-tid='newMessageCommands-Italic']";
        public static string xpath_chat_underline_option = "//button[@data-tid='newMessageCommands-Underline']";
        public static string xpath_chat_strike_option = "//button[@data-tid='newMessageCommands-Strike']";
        public static string xpath_chat_texthighlight_option = "//button[@data-tid='newMessageCommands-TextHighlightColor']";
        public static string xpath_chat_fontcolor_option = "//button[@data-tid='newMessageCommands-FontColor']";
        public static string xpath_chat_fontsize_option = "//button[@data-tid='newMessageCommands-FontSize']";
        public static string xpath_chat_richstyle_option = "//button[@data-tid='newMessageCommands-RichStyle']";
        public static string xpath_chat_insertlink_option = "//button[@data-tid='newMessageCommands-InsertHyperlink']";
        public static string xpath_chat_codesnippet_option = "//button[@data-tid='newMessageCommands-code-snippet']";
        // insert link
        public static string xpath_chat_insertlink_text = "//div[@data-tid='insertHyperlink-displayText']//input";
        public static string xpath_chat_insertlink_url = "//div[@data-tid='insertHyperlink-linkAddress']//input";
        public static string xpath_chat_insertbutton = "//button[@data-tid='insertHyperlink-insertButton']";

        //Video call
        public static string xpath_chat_videocall_button = "//button[@data-tid='dropdown-video-call-primary-btn']";
        public static string xpath_chat_phonecall_button = "//button[@data-tid='dropdown-audio-call-primary-btn']";
        public static string xpath_videocall_dismiss_button = "//button[@data-tid='calling-retry-cancelbutton']";
        public static string xpath_videocall_toolbar = "//div[@data-tid='ubar-toolbar-wrapper']";
        public static string xpath_videocall_mainscreen = "//div[@data-tid='modern-stage-wrapper']";
        public static string id_videocall_moreoption_button = "callingButtons-showMoreBtn";
        public static string id_videocall_mic_button = "microphone-button";
        public static string id_videocall_camera_button = "video-button";
        public static string id_videocall_sharescreen_button = "share-button";
        public static string id_videocall_hungup_button = "hangup-button";
        public static string id_videocall_initial_alertmessage = "ngdialog1-aria-labelledby";
        public static string xpath_videocall_alertsettingbuton = "//button[contains(@class, 'ts-btn-fluent-secondary-alternate')]";

        //Calls view
        public static string id_calls_usersearchbox = "people-picker-input";
        public static string id_calls_userpickeritem = "downshift-0-item-0";
        public static string xpath_calls_userpicker = "//span[contains(@class, 'ui-dropdown__selecteditem__header')]";
        public static string xpath_calls_callbutton = "//button[@data-tid='call-button']";
    }
}
