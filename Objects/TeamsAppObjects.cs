using System;
using System.Collections.Generic;
using System.Text;

namespace TeamsWindowsApp.Objects
{
    public static class TeamsAppObjects
    {

        //v3.2
        /*
         * Info: Object Repository
         * Description: Applicaiton related elements to be stored here in variables
         * Naming convention:Variable to be declared as <typeofElementIdentificaiton>_<preferedObjectName> EX:id_testObject
        */

        /* Page: TeamLogin Page
         * Added by: Senthil (23/02/2022)
         * Updated by:
         * Update description:
         * AutomationId/AccessibilityID - Key: autoId
         */


        //Login Page - List Item to select the user
        public static string xpath_UserSelection = "//ListItem[contains(@Name,'#USER#')]";
        public static string xpath_LoadingPane = "//Pane[@Name=\"Loading Microsoft Teams\"]";
        //Login page - Other user login
        //public static string xname_MSLoginPage_InputField = "Email, phone, or Skype";
        public static string autoId_SSO_LoginForm = "loginForm";
        public static string autoId_SSO_LoginForm_PasswordInput = "passwordInput";
        public static string autoId_SSO_SigninButton = "submitButton";
        public static string autoId_SSO_Confirmation_OkButton = "next_button";
        public static string autoId_SSO_CloseErrorButton = "button_close";
        public static string xname_MSLoginPage_NextButton = "Next";
        public static string xpath_MSLoginPage_InputField = "//Edit[contains(@Name,\"Email\")]";
        public static string xpath_SSO_TempErroDialog = "//ListItem[starts-with(@Name, \"Error code:\")]";
        


        //Team App - Profile Icon
        public static string autoId_profileIcon = "personDropdown";
        //Teams App - Toolbar
        public static string autoId_SignOut = "logout-button";
        public static string autoId_SignOutConfirm = "confirmButton";
        public static string autoId_settingDiarog_lefOption = "options-dialog-focus-default";
        public static string name_SignOut = "Sign out of Microsoft Teams";
        public static string autoId_UseAnotherAccountLink = "useAnotherAccountLink";
        public static string autoId_loginSubmitButton = "submitButton";
        public static string autoId_accountSetting_Button = "settings-manage-account-button";
        public static string xname_accountSetting_Dialog = "Settings";
        public static string xname_accountSetting_CloseButton = "Close Settings";
        public static string xpath_settingOption_General = "//TabItem[@Name=\"General\"]";
        public static string xpath_settingOption_Account = "//TabItem[@Name=\"Accounts\"]";
        public static string xpath_settingOption_Appsettings_close_text = "//Text[@Name=\"On close, keep the application running\"]";
        public static string xpath_settingOption_Appsettings_close_check = "//CheckBox[@Name=\"On close, keep the application running\"]";
        // version < 1.5.00.28361 (64-bit). It was last updated on 10/18/2022.
        public static string xname_settingOption_Appsettings_close = "On close, keep the application running";
        //public static string xpath_LogoutConfirmationDialog = "//Group[starts-with(@Name,\"Signing out of\")]";
        //public static string autoId_LogoutButtonOnConfirmationDialog = "confirmButton";


        /* Page: ChatWindow
         * Added by: Senthil (23/02/2022)
         * Updated by:
         * Update description:
         */

        //Search toolbar
        //v3.2 Need to cahnge Japanese ver
        public static string autoId_SearchToolbar = "control-input";
        public static string autoId_SearchField = "searchInputField";
        public static string xpath_SearchAutolist = "//ListItem[contains(@Name,'Person') and contains(@Name,'#USER#')][starts-with(@AutomationId,\"autosuggest-id-\")]/Button";        

        //Need to look into the seen message
        //Clicking the buttons in toolbar dynamically Ex: Chat, Activity, calls, etc
        public static string autoId_MoreApp = "apps-button";
        public static string autoId_MoreApp_SearchButton = "app-search-input";
        public static string xpath_Toolbar_MenuItem = "//Button[@Name=\"#FunctionTitle# Toolbar\"]";
        public static string xpath_Toolbar_WithNotification = "//Button[contains(@Name, \"teams with new messages\")]";
        public static string xpath_Activity_MenuItem_Text = "//MenuItem[@Name=\"Select type: #text#\"]//Text[@Name=\"#text#\"]";
        public static string xpath_MenuItem_Text = "//MenuItem[@Name=\"#text#\"]//Text[@Name =\"#text#\"]";
        public static string xpath_MenuItem_Group = "//Group[@Name=\"#text#\"]";

        //Activity
        public static string xpath_LeftActivityToolbarSelection = "//List/ListItem/Button[@Name=\"Activity Toolbar\"]";
        public static string xpath_Chat_with_Activity_Notification_Toolbar = "//Button[contains(@Name, \"new activity\")]";

        //Chat
        public static string xpath_Chat_Toolbar = "//Button[contains(@Name,\"Chat Toolbar more options\") or contains(@Name,\"chat with new messages more options\")]";///Button[@Name=\"1 chat with new messages more options\"]"
        public static string xpath_Chat_With_Notification_Toolbar = "//Button[contains(@Name, \"chat with new messages more options\")]";
        public static string xpath_Chat_EditField = "//Group[@Name=\"Compose\"]//Edit";
        public static string xpath_Chat_SendButton = "//Group[@Name=\"Compose\"]//Button[@Name=\"Send\"]";
        //public static string xpath_Chat_MessageContent = "//Group[contains(@Name,\"Sent\") or contains(@Name,\"Seen\")]";
        public static string xpath_Chat_MessageContent = "//Group[@Name=\"Message List\"]//Group";
        public static string xpath_Chat_SentMessgae = "//Group[contains(@Name,\"Sent\") or contains(@Name,\"Seen\")]//Text";
        public static string xpath_Chat_SearchFileSuggetion = "//ListItem[starts-with(@Name,\"File #FILE#\")]//Button";
        public static string xpath_Chat_Menu = "//MenuItem[@Name =\"Chat list\"]";
        public static string xpath_Chat_AnotherUserMessageId = "//Group[@Name=\"Message List\"]//Group[contains(@Name,\"#USER#\")]";
        //Chat General
        public static string xpath_Chat_MentionedUserName = "//Group[contains(@Name,\"Sent\") or contains(@Name,\"Seen\")]//MenuItem";
        public static string xpath_Chat_MentionedUserSuggest = "//List[@Name=\"Suggestions\"]//ListItem";
        public static string xpath_Chat_TrgetPostedText = "//Text[@Name=\"#TEXT#\"]";
        public static string xpath_Chat_TrgetPostedEmoji = "//Group[starts-with(@Name, \"#EMOJINAME#\")]";
        //public static string xpath_Chat_TrgetPostedFile = "//Group[contains(@Name,\"The message has an attachment\")]";
        public static string xpath_Chat_TrgetPostedFile = "//Group[contains(@Name,\"The message has an attachment\") and //MenuItem[starts-with(@Name,\"File attachment #FILENAME#\")]]";
        //Chat General > Reactions

        
        public static string xpath_Chat_TrgetPostedItem = "//Text[@Name=\"#TEXT#\"]";
        //General > Reactions
        public static string xpath_Chat_Reaction_MoreOption = "//MenuItem[@Name=\"More options\"]";
        //Chat General > Reactions > More options
        public static string xpath_Chat_Reaction_MoreOption_Reply = "//MenuItem[@Name=\"Reply\"]";
        public static string xpath_Chat_EditField_ReplyItem = "//Edit//Group[starts-with(@Name,\"Begin Reference\")]";
        public static string xpath_Chat_EditField_ReplyItem_RemoveButton = xpath_Chat_EditField_ReplyItem + "//Button[@Name=\"Remove Reference\"]";
        //public static string xpath_Chat_PostedItem_ReplyItem = "//Group[starts-with(@Name,\"Sent\")]//Group[starts-with(@Name,\"Begin Reference\")]";
        public static string xpath_Chat_PostedItem_ReplyItem = "//Group[contains(@Name,\"Sent\") or contains(@Name,\"Seen\")]//Group[starts-with(@Name,\"Begin Reference\")]";

        // Add user in Group chat
        public static string xpath_Chat_ChatTreeNewGroupItemText = "//TreeItem[contains(@Name, \"Draft\")]";
        public static string xpath_Chat_NewGroupText = "//Text[@Name=\"Type your first message below.\"]";

        //Top Tool Bar Items
        //public static string xpath_Chat_ToolFilesButton = "//TabItem[@Name=\"Files\"]";
        public static string xpath_Chat_AddUserButton = "//Button[@Name=\"Add people\"]";
        public static string xpath_Chat_AddUserButtonNum = "//Button[@Name=\"View and add participants, #NUMBER# participants.\"]";
        public static string xpath_Chat_AddUserDialog = "//Custom[starts-with(@AutomationId,\"ngdialog\")]/Document/Custom";
        public static string xpath_Chat_AddUser_Input = "//Edit[@Name=\"Add someone to the chat.\"]";
        public static string xpath_Chat_AddUserDialog_CancelButton = "//Group[@Name=\"Add someone to the chat.\"]//Button[@Name=\"Cancel\"]";
        public static string xpath_Chat_AddUserDialog_AddButton = "//Button[@Name=\"Add\"]";
        public static string xpath_Chat_AddUser_SearchListItem = "//MenuItem[contains(@Name,'#USER#')]";
        public static string xpath_Chat_AddUserInput_RemoveButton = "//Button[contains(@Name,\"Remove\")]";

        //Tabs
        public static string autoId_Chat_ChatTab = "messages-header-v2-tab-group";
        public static string xpath_Chat_UserCard = "//List[@Name=\"Direct reports list\"]//MenuItem[contains(@Name, \"#USERID#\")]";

        //Delivery Options
        public static string xpath_Chat_DeliveryOption_Button = "//MenuItem[@Name=\"Set delivery options\"]";
        public static string xpath_Chat_DeliveryOption_Window = "//Document//List";
        public static string xpath_Chat_DeliveryOption_Item = "//Text[@Name=\"#MESSAGETYPE#\"]";
        public static string xpath_Chat_DeliveryOption_FlagText = "//Group[@Name=\"Compose\"]//Text";
        public static string xpath_Chat_ImpDelivery_PostFlag = "//Group[starts-with(@Name,\"#MESSAGETYPE#\")]//Text";

        //Emoji Options
        public static string xpath_Chat_EmojiOption_Button = "//MenuItem[@Name=\"Emoji\"]";
        //public static string xpath_Chat_EmojiOption_EmojiType = "//MenuItem[@Name=\"#TYPE#\"]";
        public static string xpath_Chat_EmojiOption_EmojiType = "//RadioButton[@Name=\"#TYPE#\"]";
        public static string xpath_Chat_EmojiOption_Emoji = "//MenuItem[starts-with(@Name,\"#EMOJI#\")]";
        public static string xpath_Chat_SentEmojiMessgae = "//Group[contains(@Name,\"Sent\") or contains(@Name,\"Seen\")]//Image";
        public static string xpath_Chat_Emoji = "//Group[contains(@Name,'#EMOJINAME#') and contains(@Name,'#USER#')]";


        //Loop Component
        public static string xname_Chat_LoopComp_SendButton = "Send Loop component";
        public static string xpath_Chat_LoopCompOption_Button = "//MenuItem[@Name=\"Loop components\"]";
        public static string xpath_Chat_LoopCompOptions = "//MenuItem[@Name=\"#LOOPOPTION#\"]";
        public static string xpath_Chat_LoopCompOption_Table = "//MenuItem[@Name=\"Insert #XASIX# by #YASIX# table\"]";
        public static string xpath_Chat_LoopCompOption_DiscardButton = "//Group[@Name=\"Compose\"]//Button[@Name=\"Discard\"]";
        public static string xpath_Chat_LoopCompOption_DiscardConfirm = "//Custom[@Name=\"Discard draft message\"]//Button[@Name=\"Discard\"]";
        public static string xpath_Chat_LoopComp_EditPane = "//Group[@Name=\"Compose\"]//Edit[@Name=\"Canvas\"]";
        public static string xpath_Chat_LoopComp_DraftLink = "//Group[@Name=\"Compose\"]//Text";
        public static string xpath_Chat_LoopComp_TaskAddButton = "//Group[@Name=\"Add a task\"]//Button[@Name=\"Add a task\"]";
        public static string xpath_Chat_LoopComp_TaskItems = "//Group[@Name=\"Add a task\"]//DataItem[@Name=\"Checkbox, unchecked, disabled Task name\"]";
        public static string xpath_Chat_LoopComp_Edit = "//Edit[@Name=\"Canvas\"]";

        //Formatting
        public static string xpath_Chat_Format_Button = "//Button[@Name=\"Format\"]";
        public static string xpath_Chat_Format_Opened = "//Button[@Name=\"Collapse compose box\"]";
        public static string xpath_Chat_Format_Toolbar = "//ToolBar[@Name=\"Message formatting\"]";
        public static string xpath_Chat_Format_Format = "//Button[@Name=\"#FORMAT#\"]";
        public static string xpath_Chat_Format_Item = "//MenuItem[@Name=\"#ITEM#\"]";
        public static string xpath_Chat_Format_TextHighlight_Option = "//Button[@Name=\"#TEXTHIGHLIGHT#\"]";
        public static string xpath_Chat_Format_FontColor_Option = "//Button[@Name=\"#FONTCOLOR#\"]";
        public static string xpath_Chat_Format_FontSize_Option = "//Menu[@Name=\"Font size\"]/MenuItem[@Name=\"#SIZE#\"]";
        public static string xpath_Chat_Format_Paragraph_Option = "//MenuItem[@Name=\"#PARAOPTION#\"]";
        public static string xpath_Chat_Format_Insert_Text_Edit = "//Edit[@Name=\"Text to display\"]";
        public static string xpath_Chat_Format_Insert_Address_Edit = "//Edit[@Name=\"Address\"]";
        public static string xpath_Chat_Format_Insert_Button = "//Button[@Name=\"Insert\"]";
        //public static string xpath_Chat_Format_InsertLink = "//Hyperlink[contains(@Name,\"Url Preview for\")]";
        public static string xpath_Chat_Format_InsertLink = "//Image[starts-with(@Name,\"Url Preview for\")]";
        public static string xpath_Chat_Format_InsertLinkButton = "//Button[@Name=\"Remove URL Preview\"]";
        //public static string xpath_Chat_Format_TextHighlight = "//MenuItem[@Name=\"Text highlight color\"]";
        //public static string xpath_Chat_Format_FontColor = "//MenuItem[@Name=\"Font color\"]";
        //public static string xpath_Chat_Format_FontSize = "//MenuItem[@Name=\"Font size\"]";
        //public static string xpath_Chat_Format_Paragraph = "//MenuItem[@Name=\"Paragraph\"]";

        //File Attachment
        public static string xpath_Chat_AttachmentFile_Button = "//MenuItem[@Name =\"Attach files\"]";
        public static string xpath_Chat_AttachmentFile_Item = "//MenuItem[@Name=\"#TYPE#\"]";
        public static string xpath_Chat_FilePath_TextField = "//Edit[@Name=\"File name:\"]";
        public static string xpath_Chat_OpenButton_FileDialog = "//Button[@ClassName=\"Button\"][@Name=\"Open\"]";
        public static string xpath_Chat_UploadCopy_Button = "//Button[@Name=\"Upload a copy\"]";
        public static string xpath_Chat_FileReplace_Button = "//Button[@Name=\"Replace\"]";
        public static string xpath_Chat_Uploaded_File = "//Group[contains(@Name,\"100 % uploaded of\")]";
        public static string xpath_Chat_FileRemoveButton = "//Button[@Name=\"Remove attachment\"]";
        public static string xpath_Chat_SendAttachment = "//Group[contains(@Name,\"Seen\") or contains(@Name,\"Sent\")]//MenuItem[starts-with(@Name,\"File attachment\")]";
        
        //public static string xpath_Chat_SendAttachment = "//Group[starts-with(@Name,\"Sent\")]//MenuItem[contians(@Name,\"File attachment\")]";


        //Video Calls on chat
        public static string xpath_Chat_AudioCall = "//Button[@Name=\"Audio call\"]";
        public static string xpath_Chat_VideoCall = "//Button[@Name=\"Video call\"]";

        //Teams Window
        public static string autoId_Teams_ListItemDeleteDialogDeleteButton = "confirmButton";
        public static string autoId_Teams_CustomDialog_FromScratchButton = "WizardBtnfromScratch";
        public static string autoId_Teams_CustomDialog_Private = "WizardBtnPrivate";
        public static string autoId_Teams_CustomDialog_Public = "WizardBtnPublic";
        public static string autoId_Teams_CustomDialog_TemaDetail_Title = "detailsTeamName";
        public static string autoId_Teams_CustomDialog_MemberEdit_AddMemberList = "member-table";
        public static string autoId_Teams_NewConversationButton = "new-post-button";
        public static string autoId_Teams_SendButton = "send-message-button";
        public static string autoId_Teams_PostedMessageBody = "messageBody";
        public static string autoId_Teams_MeetNowFromChatOption = "start-meetup-experience";
        public static string xpath_Teams_ListItem = "//Group[@Name=\"Teams and Channels list\"]//Text[@Name=\"Profile picture of #GROUPNAME#. #GROUPNAME# More options\"]";
        public static string xpath_Teams_ListItemOptionButton = xpath_Teams_ListItem + "//Button[@Name=\"More options\"]";
        public static string xpath_Teams_ListItemOptionManageTeam = "//Menu[@Name =\"Team #GROUPNAME# actions\"]//MenuItem[@Name=\"Manage team\"]";
        public static string xpath_Teams_ListItemOptionAddUser = "//Menu[@Name =\"Team #GROUPNAME# actions\"]//MenuItem[@Name=\"Add member\"]";
        public static string xpath_Teams_ListItemOptionDelete = "//Menu[@Name =\"Team #GROUPNAME# actions\"]//MenuItem[@Name=\"Delete the team\"]";
        public static string xpath_Teams_ListItemDeleteDialog = "//Group[starts-with(@Name,\"Are you sure you want to delete the #GROUPNAME# team?\")]"; //test should be valiable
        public static string xpath_Teams_ListItemDeleteDialogCheckbox = "//CheckBox[@Name =\"I understand that everything will be deleted\"]";
        public static string xpath_Teams_ManageButton = "//Button[@Name=\"Manage teams\"]";
        public static string xpath_Teams_TeamCreateButton = "//Button[@Name=\"Create a team\"]";
        public static string xpath_Teams_CustomDialog_CreateTeam = "//Custom[contains(@Name, \"Create a team\")]";
        public static string xpath_Teams_CustomDialog_GroupType = "//Custom[@Name =\"What kind of team will this be?\"]";
        public static string xpath_Teams_CustomDialog_TeamDetail = "//Custom[@Name =\"Some quick details about your #PUBLICTYPE# team\"]";
        public static string xpath_Teams_CustomDialog_TeamDetail_Desc = "//Edit[@Name =\"Enter team description\"]";
        public static string xpath_Teams_CustomDialog_TeamDetail_CreateButton = "//Button[@Name =\"Create\"]";
        public static string xpath_Teams_CustomDialog_MemberEdit = "//Custom[@Name =\"Add members to #GROUPNAME#\"]"; //it works but not used because of loading.
        public static string xpath_Teams_CustomDialog_MemberEdit_Field = "//Edit[starts-with(@Name,\"Add members to #GROUPNAME# Start typing a name\")]";
        public static string xpath_Teams_CustomDialog_MemberEdit_SearchAutolist = "//MenuItem[contains(@Name, \"#USERNAME#\")]";
        public static string xpath_Teams_CustomDialog_MemberEdit_BadgeRemove = xpath_Teams_CustomDialog_MemberEdit_Field + "//Button[starts-with(@Name,\"Remove\")]"; //not used yet
        //public static string xpath_Teams_CustomDialog_MemberEdit_AddMemberList = "//List[@AutomationId =\"member-table\"]";
        //public static string autoId_Teams_CustomDialog_MemberEdit_AddMemberList = "member-table";
        public static string xpath_Teams_CustomDialog_MemberEdit_AddMemberRemove = "//Button[start-with(@Name =\"Remove\")]"; //mpt used yet
        public static string xpath_Teams_CustomDialog_MemberEdit_AddButton = "//Button[@Name =\"Add members to team\"]";
        public static string xpath_Teams_CustomDialog_MemberEdit_SkipButton = "//Button[@Name =\"Skip\"]"; //not uesd yet
        public static string xpath_Teams_CustomDialog_MemberEdit_CloseButton = "//Button[@Name=\"Close\"]";
        public static string xpath_Temas_ManageTeams_AddUserButton = "//Button[@Name=\"Add member\"]";
        public static string xpath_Teams_ManageTeams_MemberlistButton = "//Button[contains(@Name,\"Members and guests\")]";
        public static string xpath_Teams_ManageTeams_Menberlists = "//Table[@Name=\"Members and guests\"]//Custom";
        public static string xpath_Teams_TextField = "//Edit[@Name=\"Start a new conversation. Type @ to mention someone.\"]";
        //public static string xpath_Teams_PostedMessage = "//Group[starts-with(@Name,\"Opens card Available #USER# #TIME# Toolbar w\")]";
        //public static string xpath_Temas_MessageList = "//Group[starts-with(@Name,\"Opens card Available\") and starts-with(@AutomationId,\"m\")]";
        public static string xpath_Teams_TargetPost = "//Group[starts-with(@Name,\"Opens card Available #USERID# #TIME# Toolbar \")]";
        //public static string xpath_Teams_TargetMessageList = "//Group[starts-with(@AutomationId,\"m\")]";
        public static string xpath_Teams_TargetMessageList = "Group[@Name=\"message list\"]/Group";
        public static string xpath_Teams_MentionTag = "//MenuItem[starts-with(@Name,\"Opens Profile Card for\")]";
        public static string xpath_Teams_ReplyButton = "//Button[@Name=\"Reply\"]";
        public static string xpath_Teams_ReplyField = "//Edit[@Name=\"Reply, editing\"]";
        public static string xpath_Teams_MeetNow = "//Button[@Name=\"Meet\"]";

        //Teams Window > emoji options
        public static string xpath_Teams_EmojiOption_Button = "//Button[starts-with(@Name,\"Choose an emoji. Press Enter key to activate and left or right a\")]";

        //Teams Window > Attachments
        public static string xpath_Teams_AttachmentFile_Button_Button = "//Button[@Name=\"Attach. Use left and right arrow keys to navigate.\"]";
        public static string xpath_Teams_Uploaded_File = "//MenuItem[contains(@Name, \"Press Enter to cancel upload for file\")]";

        //Video call
        public static string autoId_videoCall_JoinButton = "prejoin-join-button";
        public static string autoId_videoCall_ScreenShareButton = "share-button";
        public static string autoId_videoCall_MoreOptionButton = "callingButtons-showMoreBtn";
        public static string autoId_videoCall_HungUP_Button = "hangup-button";
        public static string autoId_videoCall_Attendee_Button = "roster-button"; //NEW
        public static string autoId_videoCall_Chat_Button = "chat-button"; //NEW
        public static string autoId_videoCall_Option_videoButton = "video-button";
        public static string autoId_videoCall_Option_micButton = "microphone-button";
        public static string autoId_videoCall_Option_shareButton = "share-button";
        public static string autoId_videoCall_Option_livecaption = "closed-captions-button";
        public static string autoId_videoCall_Option_Transcription = "call-transcript-button";
        public static string autoId_videoCall_Option_BackgroundButton = "custom-video-backgrounds-panel-button";
        public static string autoId_videoCall_MeetingSettingButton = "meeting-options-ubar";
        public static string autoId_videoCall_ParticipantsButton = "roster-button";
        public static string autoId_videoCall_AlartCloseButton = "close_button";
        public static string autoId_Evaluation_Close_Button = "cqf-dismiss-button";
        public static string xname_videoCall_HungUP_moreOptions = "More options";
        public static string xname_videoCall_Leave_Button = "Leave (Ctrl+Shift+H)";
        public static string xname_videoCall_Leave_Notification = "Are you sure you want to leave?";
        public static string xname_videoCall_Leave_Button_On_Notification = "Leave";
        public static string xname_videoCall_EndMTG_Button = "End meeting";
        public static string xname_videoCall_EndMTG_Dialog = "End the meeting?";
        public static string xname_videoCall_VideoOption = "Video options";
        public static string xname_videoCall_Camera = "Camera";
        public static string xname_videoCall_ComputerAudio = "Computer audio";
        public static string xname_videoCall_PhoneAudio = "Phone audio";
        public static string xname_videoCall_RoomAudio = "Room audio";
        public static string xname_videoCall_NotUse = "Don&apos;t use audio";
        public static string xname_videoCall_BackgroundSettingButton = "More background effects";
        //public static string xname_videoCall_BackgroundSetting = "Backgrounds";
        public static string xname_videoCall_BackgroundApplyWithVideoOn = "Apply and turn on video";
        public static string xname_videoCall_BackgroundApplyButton = "Apply";
        public static string xname_videoCall_MeetingOptions = "Meeting options";
        public static string xname_videoCall_MeetingOptionsSaveButton = "Save";
        public static string xname_videoCall_MeetingOptionsSaved = "Done!";
        public static string xname_videoCall_CloseRightPaneButton = "Close right pane";
        public static string xname_videoCall_ParticipantsView = "Participants";
        public static string xname_videoCall_ParticipantsViewMoreActions = "More actions";
        public static string xname_videoCall_LockMTGButton = "Lock the meeting";
        public static string xname_videoCall_UnlockMTGButton = "Unlock the meeting";
        public static string xname_videoCall_WarningAlart = "Warning alert"; // This might be old version
        public static string xpath_videoCall_NotificationDialog = "//Custom[starts-with(@Name,\"Invite people to join you Copy and share the link to invite some\")]";
        public static string xpath_videoCall_NotificationCloseButton = "//Custom[starts-with(@Name,\"Invite people to join you Copy and share the link to invite some\")]/Button[@Name=\"Close\"]";
        public static string xpath_videoCall_MicConnectionCaution = "//Button[@Name=\"No Microphone Make sure a mic is connected to your computer so others can hear you. Close\")]";
        //Video Call setting
        public static string xname_videoCall_BGButton_TuenedOn_settingDialog = "Background filters";
        public static string xname_videocall_BGsettingMenu_settingDialog = "Background settings";
        public static string xname_videoCall_BGName_settingDialog = "#BGNAME# background effect";
        public static string xname_videoCall_JPBGName_Blur = "JPBGNameBlur";
        public static string xpath_videocall_ChatCall_Dialog = "//Group[@Name=\"Meeting view\"]";
        //public static string xpath_videoCall_VideoCallDialog = "//Group[@Name=\"Choose your video and audio options for Meeting in &quot;#TITLE#&quot; \"]";
        public static string xpath_videoCall_VideoCallDialog = "//Group[contains(@Name,\"Choose your video and audio options for Meeting in\")]";
        public static string xpath_videoCall_CameraSettingOff = "//Text[@Name=\"Your camera is turned off\"]";
        public static string xpath_videoCall_MicSetting = "//CheckBox[contains(@Name,\"Mic is\")]";
        public static string xpath_videoCall_AudioSetting = "//Button[contains(@Name,\"Speaker is\")]";
        public static string xpath_videoCall_MTGOptionBypassSetting = "//MenuItem[starts-with(@AutomationId,\"dropdown-trigger-button-\")]";
        public static string xpath_videoCall_LockDialogLockButton = "//Button[@Name=\"Lock\"]";
        public static string xpath_videoCall_UnlockDialogUnlockButton = "//Button[@Name=\"Unlock\"]";
        public static string xpath_videoCall_BGButton_TuenedOff_settingDialog = "//Button[@Name=\"Turn on video to use background effects\"]";
        //ScreenSharing
        public static string autoId_videoCall_PPTSharing_StopSharingButton = "stopPresentingPptBtn";
        public static string autoId_videoCall_PPTSharing_LayoutButton = "ppt-sharing-layout-toolbar";
        public static string autoId_videoCall_PPTSharing_PrivateviewButton = "toggleEnablePrivateViewingButton";
        public static string autoId_videoCall_PPTSharing_PopupButton = "popout-content-button";
        public static string autoId_videoCall_PPTSharing_GridviewButton = "gridViewButton";
        public static string autoId_videoCall_PPTSharing_MoreActionButton = "moreSlideShowOptionsMenuButton";
        public static string autoId_videoCall_PPTSharing_CursorButton = "annotationSelectBtn";
        public static string autoId_videoCall_PPTSharing_LaserPointerButton = "annotationLaserBtn";
        public static string autoId_videoCall_PPTSharing_PenButton = "annotationPenBtn";
        public static string autoId_videoCall_PPTSharing_HighlighterButton = "annotationHighlighterBtn";
        public static string autoId_videoCall_PPTSharing_EraserButton = "annotationEraserBtn";
        public static string autoId_videoCall_PPTSharing_MailSlideView = "slideshow-app-container";
        public static string autoId_videoCall_PPTSharing_StopConfirmationDialog = "child-window-body";

        public static string xname_videoCall_ChooseScreen = "Choose screen";
        public static string xname_videoCall_StopSharingButton = "Stop sharing";
        public static string autoId_videoCall_StopSharingButton = "share-button";
        public static string xname_videoCall_ShareScreen_dialog = "Share content";
        public static string xname_videoCall_PPTSharing_StopConfirmation = "Stop presenting?";
        public static string xname_videoCall_PPTSharing_StopConfirmation_Button = "Stop presenting";
        public static string xpath_videoCall_PPTLiveSharing_button = "//MenuItem[starts-with(@Name, \"Share PowerPoint file\")]";
        public static string xpath_videoCall_PPTLiveSharing_buttonName = "//MenuItem[@Name=\"Share PowerPoint file #FILENAME#\"]";
        public static string xpath_videoCall_PPTLiveSharing_FromBrowserButton = "//MenuItem[@Name=\"Browse my computer\"]";
        //Recording MTG
        public static string autoId_videoCall_Option_RecordingButton = "recording-button";
        public static string autoId_videoCall_RecordingMenu = "RecordingMenuControl-id";
        public static string xname_videoCall_TranscriptView = "Transcript";
        public static string xname_videocall_RecordingStopdialogStopButton = "Stop recording and transcription?";
        public static string xname_videoCall_RecordingStopButton = "Stop";
        public static string xpath_videoCall_TranscroiptPane = "//Group[@Name=\"Transcript\"]";
        //LiveCaption
        public static string autoId_videoCall_LiveCaptionMenu = "LanguageSpeechMenuControl-id";
        public static string xpath_videoCall_LiveCaptionPane = "//Group[@Name=\"Live Captions\"]";
        public static string autoId_videoCall_LiveCaptionSettingButton = "captions-settings-menu-trigger-button";
        //Transcription
        public static string xpath_videoCall_TranscriptSettingConfirmDialog = "//Custom[@Name=\"What language is everyone speaking?\"]";
        public static string xpath_videoCall_TranscriptSettingConfirmButton = "//Button[@Name=\"Confirm\"]";
        //Phone catch
        //public static string xpath_videoCall_phonecatch_dilog = "//Group[@Name=\"Microsoft Teams Notification\"]";
        public static string xpath_videoCall_phonecallCatch_button = "//Button[@Name=\"Accept with audio Press ctrl+shift+s to Accept audio call.\"]"; //NEW
        public static string xpath_videoCall_videocallCatch_button = "//Button[@Name=\"Accept with video Press ctrl+shift+a to Accept video call.\"]"; //NEW
        public static string xpath_videoCall_declinecallCatch_button = "//Button[@Name=\"Decline call Press ctrl+shift+d to Decline call.\"]"; //NEW

        //Calls
        public static string autoId_Calls_SearchField = "people-picker-input";
        public static string xpath_Calls_Toolbar = "//Button[@Name=\"Calls Toolbar\"]";
        public static string xpath_Missed_Calls_Toolbar = "//Button[@Name=\"new voicemails and missed calls\"]";
        public static string xpath_Call_Button = "//Button[@Name=\"Call\"]";
        public static string xpath_Calls_SearchAutolist = "//Edit[@AutomationId=\"people-picker-input\"]/following-sibling::List";
        public static string autoId_Calls_SearchAutolist = "downshift-0-item-0";
        public static string xpath_Calls_UserBadge = "//ListItem[contains(@Name,\"Use backspace or delete to re\")]";

        //Training
        public static string xpath_Training_Title = "//Button[@Name=\"Training app icon\"]";

        //Quit Teams Application - via Notification toolbar
        public static string xname_NotificationArrow = "Notification Chevron";
        public static string xname_ContextQuit = "Quit";
        public static string xpath_NotificationIcon_TeamsApp = "//ToolBar[@Name='Overflow Notification Area']/Button[contains(@Name,'Microsoft Teams')]";

        //Calendar window
        //public static string autoId_cal_CreateNewButton = "header_new_meeting_button";
        //public static string xpath_cal_CreateNewMeetingButton = "//Button[@Name=\"New meeting\"]";

        // This is for alternative way in case the QA Teams is configured by different way from RND
        //public static string xpath_cal_CreateNewMeetingButton = "//Button[starts-with(@Name,\"Schedule a new meeting, Use alt+down to schedule different types\")]";
        public static string autoId_cal_CreateNewMeetingButton = "header_new_meeting_button";
        public static string autoId_cal_ViedoLink = "calv2-peek-copy-link";
        public static string xpath_cal_EventGridView = "Calendar grid view";
        public static string xpath_cal_EventCard = "//Button[starts-with(@Name,\"#TITLE#\")]";
        public static string xname_cal_JoinMTGButton = "Join";
        public static string xpath_cal_NextMonthViewButton = "//Button[starts-with(@Name,\"Go to next\")]";
        public static string xpath_cal_NextPreviousViewButton = "//Button[starts-with(@Name,\"Go to previous\")]";
        //Calendar Edit view
        public static string xpath_cal_MeetingDetailView = "//Group[@Name=\"Meeting details\"]";
        //// Title
        public static string autoId_cal_EditTitle = "cv2-sf-meeting-subject-input";
        //// Attendees
        public static string autoId_cal_EditAttendee = "cv2-sf-required-people-picker";
        public static string autoId_cal_EditAttendeelist_0 = "cv2-sf-people-result-0";
        //// DateTime
        public static string xname_cal_EditStartDate = "Start date";
        public static string xname_cal_EditStartTime = "Start time";
        public static string xname_cal_EditEndDate = "End date";
        public static string xname_cal_EditEndTime = "End time";
        public static string xpath_cal_DatePickerMonthTitle = "//Group[starts-with(@AutomationId,\"DatePickerDay-monthAndYear\")]//Text";
        //// Recurrence
        public static string xname_cal_EditRecurrence = "Recurrence";
        public static string autoId_cal_EditLocation = "cv2-sf-location-picker-input";
        public static string autoId_cal_EditOptionalAttendeeField = "cv2-sf-optional-people-picker";
        //// Recurrence Dialog
        public static string xname_cal_RecurrenceDialog = "Set recurrence";
        public static string xname_cal_RecurrenceDialog_Sdate = "Starts";
        public static string xname_cal_RecurrenceDialog_Edate = "End";
        public static string xname_cal_RecurrenceDialog_Tdate = "On day";
        public static string xname_cal_RecurrenceDialog_Tweek = "On";
        public static string xname_cal_Repeat = "#TYPE# to repeat";
        // Other options
        public static string xname_cal_AddOptionalButton = "Add optional attendees";
        public static string xname_cal_EditAllDay = "All day";
        public static string xname_cal_EditAddChannel = "Add channel";
        public static string xname_cal_EditDescription = "Type details for this new meeting";
        //// Save options
        public static string autoId_cal_EditHeader = "page-content-wrapper";
        public static string xname_cal_EditCloseButton = "Close scheduling form";
        public static string xname_cal_CloseDialogDiscardButton = "Discard";
        public static string xname_cal_CloseDialogContinueEdit = "Continue editing";
        public static string xname_cal_EditSaveButton = "Save";
        public static string xname_cal_EditSendButton = "Send";
        // Channels
        public static string xpath_Channel_Message_Parent = "//Text[contains(@Name,\"#USER#\")]/parent::*";
    }
}
