Feature: MSTeamWindowsApp
	MS Teams App Testing Features

Scenario: 2 user intaractive receiver
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	When Wait until a new message receive from user2
	Then Verify new message TestMessageFromSender is displayed on user2 chat
	And  Send reply message
		| TargetMessageText      | SendText					   |
		| TestMessageFromSender  | TestMessageFromReceiver     |

Scenario: 2 user intaractive sender
	Given Launch MS teams app and login with user4
	#Then Search the employee user2 and verify the search list
	Then Send a Message and verify the sent message
		| Messages                |
		| TestMessageFromSender11 |  
	When Wait until a new message receive from user2
	Then Verify new message TestMessageFromReceiver11 is displayed on user2 chat

	#Sender
#Scenario: Chat between Two Users
#	Given Launch MS teams app and login with user4
#	Then Search the employee user2 and verify the search list
#	Then Send a Message and verify the sent message
#		| Sender Message          | Receiver Message          |
#		| TestMessageFromSender11 | TestMessageFromReceiver12 |
#		| Send Word Document      | Document Received         |
#		| Send Emojis             | Reply Emojis              |

Scenario: Multiple communication receiver2
	Given Launch MS teams app and login with user2
	#Then Search the employee user4 and verify the search list
	Then Receiver user Send a Message from user4 and verify the sent message
		| SenderMessageType | SenderMessage                | ReceiverMessageType | ReceiverMessage  |
		| EMOJI             | Smilies:Crying with laughter | EMOJI               | Smilies:Angel    |
		| TEXT              | SenderMessage1               | TEXT                | ReceiverMessage1 |
		| TEXT_EMOJI        | SenderMessage2               | TEXT_Reply          | ReceiverMessage2 |
		| FILE              | Test_WordDocument.docx       | TEXT_EMOJI          | ReceiverMessage3 |

@TESTING1
Scenario: Multiple communication Sender2-1
	Given Launch MS teams app and login with user2
	#Then Search the employee user4 and verify the search list
	Then Sender user Send a Message from user4 and verify the sent message
		| SenderMessageType | SenderMessage                | ReceiverMessageType | ReceiverMessage  |
		| EMOJI             | Smilies:Crying with laughter | EMOJI               | Smilies:Angel    |
		#| TEXT              | SenderMessage1               | TEXT                | ReceiverMessage1 |
		#| TEXT_EMOJI        | SenderMessage2               | TEXT_Reply          | ReceiverMessage2 |
		#| FILE              | Test_WordDocument.docx       | TEXT_EMOJI          | ReceiverMessage3 |

#@TESTING1
#Scenario: Multiple communication Sender2-2
#	Given Launch MS teams app and login with user4
#	#Then Search the employee user2 and verify the search list
#	Then Sender user Send a Message from user2 and verify the sent message
#		| SenderMessageType | SenderMessage                | ReceiverMessageType | ReceiverMessage  |
#		| TEXT              | SenderMessage1               | TEXT                | ReceiverMessage1 |

@Test1
Scenario: MS Teams App and Web App
	Given Launch MS teams app and login with user2
	Given Launch MS teams Web app and login with user4
	Then Send a TestMessage message to user4
	When Verify the user1 message TestMessage is received

@WindowsWeb
Scenario: MS Teams App and Web App - multiple scenario
	Given Launch MS teams app and login with user2
	Given Launch MS teams Web app and login with user4
	Then user2 Send a message and user4 Receiver replies back
		| Sender Type   | Sender Message          | Receiver type | Receiver Message          |
		| Text          | TestMessageFromSender11 | Text          | TestMessageFromReceiver12 |
		#| Word_Document | Test_WordDocument.docx  | Text          | Document Received         |
		| Emoji         | Smilies:Happy face      | Emoji         | Smilies:Smile             |
		#| Emoji         | Smilies:Smile           | Emoji         | Smilies:Happy face        |

@Ver @Test1
Scenario: MS Teams App verificaiton
	Given Launch MS teams app and login with user2
	#Then Verify the logged in user user1
	Then Logout successfully from application

@Ver1 @Nunit @IndividualChat @testA
Scenario: Teams App to search user and chat
	Given Launch MS teams app and login with user4
	Then Search the employee user2 and verify the search list
	Then Send a Message and verify the sent message
		| Messages                             |
		#| Hello                                |
		#| Testing                              |
		| Special characters !"£$%^&*()[];',./ |
		| Numbers 1234567890                   |
		#| senthilkumar@testing.com             |

@Ver1 @General #Z #MS365-3557
Scenario: All tabs verification
	Given Launch MS teams app and login with user1
	Then Open each apps from left tabs
		| Apps     |
        | Activity |
		| Chat     |
		| Teams    |
		#| Calender |
		| Calls    |
		| Files    |
		#| Training |
		| other    |

@Test1 @IndividualChat #Z
Scenario: Send a message with user mention
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Send message with mention
		| Userfullname | Message                     |
		| user2        | Hello, this is test message |

@IndividualChat #Z
Scenario: Send a message with delivery option.
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Send a message with delivery option
		| DeliveryOption | Message                       |
		| Urgent         | Hello, Test urgent message    |
		| Standard       | Hello, Test standard message  |
		| Important      | Hello, Test important message |

@IndividualChat
Scenario: Send Emoji messages
	Given Launch MS teams app and login with user4
	#Then Search the employee user4 and verify the search list
	Then Send Emoji messages with text and verify
 		| EmojiType | EmojiCharactor                                                        | Text         |
		#| Smilies   | Grinning face with big eyes,Squinting face with tongue,Cool robot,Emo | Test message |
		#| Smilies   | Grinning face with big eyes,Squinting face with tongue,Emo            | Test message |
		| Smilies   | Grinning face with big eyes                                           | Test message |
		| Smilies   | Grinning face with big eyes                                           |              |

@IndividualChat
Scenario: Send Loop Component
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Create Loop Component document and verify
		| ComponentType | Format  | Title                    | Text                        |
		#| Bulleted list | 2       | Test title bullet Check  | Test bullet message        |
		#| Checklist     | 2       | Test title Checklist     | Test Checklist message     |
		#| Numbered list | 2       | Test title Numbered list | Test Numbered list message |
		#| Paragraph     | 2       | Test title Paragraph     | Test Paragraph message     |
		| Table         | 2,3     | Test title table2         | HEAD A,HEAD B,C,D,E,F  |
		#| Task list     | 3       | Test title task         | TASK 1,TASK 2,TASK 3  |

@IndividualChat
Scenario: Send Format texts
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Send format messages with text and verify
		| Format               | option     | Text                        |
		#| Bold                 |            | Bold |
		#| Italic               |            | Italic |
		#| Underline            |            | Underline |
		#| Strikethrough        |            | Strikethrough |
		#| Text highlight color | Rose bud   | Text highlight color - Rose bud |
		#| Font color           | Sunglow    | Font color - Sunglow |
		#| Font size            | Large      | Font size - Large |
		#| Paragraph            | Monospaced | Paragraph - Monospaced |
		#| Quote |  | Quote |
		| Insert link          | https://www.google.co.uk/ | Insertlink - google3 |
		#| Insert horizontal rule |  | Insert horizontal rule |

# Status: Pending to improve
# Reason: Test users has issue on the login process.
#         Once user logout from Teams App, SSO login dialog will be shown whenever user will login again.
@IndividualChat
Scenario: Reply to a chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Send a Message and verify the sent message
		| Messages                            |
		| ReplyTarget                         |
	Then Send reply message
		| TargetMessageText | SendText      |
		| ReplyTarget       | Thank you     |

# Status: Pending to improve
# Reason: Test users has issue on the login process.
#         Once user logout from Teams App, SSO login dialog will be shown whenever user will login again.
@IndividualChat
Scenario: Reply to a chat and cancel
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Send a Message and verify the sent message
		| Messages |
		| Done     |
	Then Add reply item and remove reply item
		| TargetMessageText |
		| Done              |

@IndividualChat
Scenario: Search a File
	Given Launch MS teams app and login with user1
	Then Search the file TEST.pptx and verify the search list

@IndividualChat
Scenario: Upload file and remove file on chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Upload a TXT file from PC
	Then Remove a uploaded file from Chat text field 

@IndividualChat #Z #MS365-3945
Scenario: Upload file on chat
	Given Launch MS teams app and login with user4
	Then Search the employee user2 and verify the search list
	Then Upload a Test_WordDocument.docx file from PC
	Then Send a file message and verify

# Need to update for regulated users becuase it will bisconnected straight after user started a call
@VideoCall #Z #MS365-3933
Scenario: Verify direct phone call from individual chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Verify a phone call button is enabled
	When Start phone call on chat
	Then Verify chat call dialog is enabled
	When Open More Options
	Then Verify buttons are available
	   | Button        |
	   | camera        |
	   | livecaption   |
	   | background    |
	And  Verify buttons are not available
	   | Button        |
	   | mic           | # this should be ON when we test on proper env
	   | sharescreen   | # this should be ON when users make a connection
	   | transcription |
	   | recording     |
	And Verify buttuns are turnning on/off
	   | Button | config |
	   | camera | off    |
	   | mic    | off    | # this should be ON when we test on proper env
	Then Verify buttons are available
	   | Button        |
	   | sharescreen   |
	When Hang up a MTG
	Then Verify chat call dialog is disabled

@VideoCall #Z #MS365-3931
Scenario: Verify direct video call from individual chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Verify a phone call button is enabled
	When Start video call on chat
	Then Verify chat call dialog is enabled
	When Open More Options
	Then Verify buttons are available
	   | Button        |
	   | camera        |
	   | livecaption   |
	   | background    |
	And  Verify buttons are not available
	   | Button        |
	   | mic           | # this should be ON when we test on proper env
	   | sharescreen   | # this should be ON when users make a connection
	   | transcription |
	   | recording     |
	And Verify buttuns are turnning on/off
	   | Button | config |
	   | camera | on     |
	   | mic    | off    | # this should be ON when we test on proper env
	When Wait video call connection
	Then Verify buttons are available
	   | Button        |
	   | sharescreen   |
	When Hang up a MTG
	Then Verify chat call dialog is disabled

@GroupChat
Scenario: Add new user in chat
	Given Launch MS teams app and login with user4
	Then Search the employee user2 and verify the search list
	Then Add users in chat and verify
		| InvitedUserName  |
		| user3            |

@GroupChat
Scenario: Send a message in group chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Add users in chat and verify
		| InvitedUserName  |
		| user3            |
	Then Send a Message and verify the sent message
		| Messages                |
		| Test group chat message |

@GroupChat
Scenario: Reply a message in group chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Add users in chat and verify
		| InvitedUserName  |
		| user3            |
	Then Send a Message and verify the sent message
		| Messages               |
		| Test GroupChat Message3 |
	Then Send reply message
		| TargetMessageText       | SendText      |
		| Test GroupChat Message3  | Thank you     |

# Need to get one more unregulated user otherwise we cannot check this testing.
# Because the call will be terminated if regulated user is in the group chat.
@VideoCall @GroupChat #Z #MS365-3934
Scenario: Group chat phone call 
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Add users in chat and verify
		| InvitedUserName  |
		| user3            |
	When Start phone call on chat
	Then Verify chat call dialog is enabled
	When Open More Options
	Then Verify buttons are available
	   | Button        |
	   | camera        |
	   | livecaption   |
	   | background    |
	And  Verify buttons are not available
	   | Button        |
	   | mic           | # this should be ON when we test on proper env
	   | sharescreen   | # this should be ON when users make a connection
	   | transcription |
	   | recording     |
	And Verify buttuns are turnning on/off
	   | Button | config |
	   | camera | off    |
	   | mic    | off    | # this should be ON when we test on proper env
	When Wait video call connection
	Then Verify buttons are available
	   | Button        |
	   | sharescreen   |
	When Hang up a MTG
	Then Verify chat call dialog is disabled

@VideoCall @GroupChat #MS365-3932 #Z
Scenario: Group chat video call 
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Add users in chat and verify
		| InvitedUserName  |
		| user3            |
	When Start phone call on chat
	Then Verify chat call dialog is enabled
	When Open More Options
	Then Verify buttons are available
	   | Button        |
	   | camera        |
	   | livecaption   |
	   | background    |
	And  Verify buttons are not available
	   | Button        |
	   | mic           | # this should be ON when we test on proper env
	   | sharescreen   | # this should be ON when users make a connection
	   | transcription |
	   | recording     |
	And Verify buttuns are turnning on/off
	   | Button | config |
	   | camera | on     |
	   | mic    | off    | # this should be ON when we test on proper env
	When Wait video call connection
	Then Verify buttons are available
	   | Button        |
	   | sharescreen   |
	When Hang up a MTG
	Then Verify chat call dialog is disabled

@General
Scenario: Verify call button from Teams Calls
	Given Launch MS teams app and login with user1
	Then Open Calls app and verify
	Then Verify a call button as DISABLE
	Then Search user user2 from Calls app and verify
	Then Verify a call button as ENABLE

@TeamsChannel
Scenario: Create a new Teams Channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	Then Create a new Teams group asn verify
		| GroupType | PublicationType | Title | Users |
		| Scratch   | PUBLIC          | check | user2 |

@TeamsChannel
Scenario: Add new member in Teams from option
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	Then Add new user from Teams option
		| TragetGroup | Users       |
		| test        | user3,user4 |

@TeamsChannel
Scenario: Add new member in Teams from teams management
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	Then Add new user from Teams management
		| TragetGroup | Users       |
		| test        | user3,user4 |

@TeamsChannel
Scenario: Send a message in Teams channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel and select General category
	And  Send a New post in channel
		| Messages                |
		| Test message in channel |
	Then Verify the posted message in channel
		| Messages                |
		| Test message in channel |

@TeamsChannel
Scenario: Send a message with mention in Teams channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel and select General category
	Then  Send a New post with mention in channel and verify
		| Userfullname | Message                    |
		| user2        | mention message in channel |

@TeamsChannel
Scenario: Reply to Teams channel message
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel and select General category
	And  Send a New post in channel
		| Messages                |
		| Test message in channel |
	And  Send a reply post in channel
		| Messages                      |
		| Test reply message in channel |
	Then Verify the posted message in channel
		| Messages                      |
		| Test reply message in channel |

@VideoCall
Scenario: Verify direct call button from Teams Chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Verify a phone call button is enabled
	Then Verify a video call button is enabled

@TeamsChannel @VideoCall #Z #MS365-3937
Scenario: Start channel MTG
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel and select General category
	Then Verify video call button is enable on channel top bar
	When Start video call from channel top bar
	Then Verify video call dialog of general is displayed
	Then Verify camera should be terned off
	Then Verify background setting should be terned off on setting dialog
	Then Verify computer audio setting available
	When Join to MTG video call
	When Open More Options
	Then Verify buttons are available
	   | Button        |
	   | camera        |
	   | sharescreen   |
	   | livecaption   |
	   | transcription |
	   | recording     |
	   | background    |
	And  Verify buttons are not available
	   | Button     |
	   | mic        |
	When End a MTG video call

# In order to check 100%, we need to interactive between 2 users
@TeamsChannel @VideoCall
Scenario: Lobby setting on Teams channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel and select General category
	And  Start video call from channel top bar
	Then Verify video call dialog of test schedule with attendees is displayed
	When Join to MTG video call
	And  Set bypass lobby as Only me and co-organizers
	And  Save meeting setting
	Then Verify meeting setting is saved
	When Close metting setting view
	#And  Lock the MTG
	#Then Verify MTG is locked
	When End a MTG video call

# In order to check 100%, we need to interactive between 2 users
@TeamsChannel @VideoCall
Scenario: Lock MTG on Teams channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel and select General category
	And  Start video call from channel top bar
	Then Verify video call dialog of test schedule with attendees is displayed
	When Join to MTG video call
	#And  Set bypass lobby as Only me and co-organizers
	#And  Save meeting setting
	#Then Verify meeting setting is saved
	#When Close metting setting view
	And  Lock the MTG
	Then Verify MTG is locked
	When End a MTG video call

@TeamsChannel @VideoCall #Z #MS365-3972
Scenario: Screen sharing on Teams channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel and select General category
	And  Start video call from channel top bar
	Then Verify video call dialog of test schedule with attendees is displayed
	When Join to MTG video call
	When Open screen share view
	And  Click share full screen
	Then Verify Screen has shared
	When Stop share screen
	Then Verify screen is not shared
	When End a MTG video call

@TeamsChannel @VideoCall #Z #MS365-3969
Scenario: Recording MTG on Teams channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel and select General category
	And  Start video call from channel top bar
	Then Verify video call dialog of test schedule with attendees is displayed
	When Join to MTG video call
	Then Verify screen is not shared
	When Open More Options
	And  Start record MTG
	Then Verify transcript has started
	And Verify recording MTG has started
	When Open More Options
	And  Stop record MTG
	Then Verify transcript has stopped
	And Verify recording MTG has stopped
	When End a MTG video call

@TeamsChannel @VideoCall
Scenario: Background setting on setting dialog
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel and select General category
	And  Start video call from channel top bar
	Then Verify video call dialog of general is displayed
	Then Verify camera should be terned off
	When Camera setting turned on
	And  Change background on setting dialog to Blur
	Then Verify the background Blur is applied
	When Close background setting view
	Then Close video call window      

@TeamsChannel @VideoCall
Scenario: Background setting on call
	Given Launch MS teams app and login with 'user1'
	Then Open Teams app and verify
	When Open test channel and select General category
	And  Start video call from channel top bar
	Then Verify video call dialog of test schedule with attendees is displayed
	When Join to MTG video call
	When Change background setting to Blur
	Then Verify the background Blur is applied
	When Close background setting view
	When End a MTG video call

@Sanity @Pilot @MS365-2873

Scenario Outline: Teams App Pilot flow
	Given Launch MS teams app andlogin with <users> and userType <usertype>
	When User search for <searchuser1> <searchtype1> user and send message with devlivery option
		| DeliveryOption | Message                    |
		| Urgent         | Hello, Test urgent message |
	Then User search for <searchuser1> <searchtype1> user and Upload a Test_WordDocument file from PC
	And User <usertype> check with other <searchtype1> user check call, video call and screenshare call options are <Options>
	When User search for <searchuser2> <searchtype2> user and send message with devlivery option
		| DeliveryOption | Message                       |
		| Important      | Hello, Test important message |
	Then User search for <searchuser2> <searchtype2> user and Upload a Word file from PC
	When User check the chat features of <usertype> check icons, chat, file share and delivery option
	And User <usertype> check with other <searchtype1> user check call, video call and screenshare call options are <Options>
	Then Logout successfully from application

Scenarios: 
		| users | usertype    | searchuser1 | searchtype1 | searchuser2 | searchtype2 | Options  |
		| user1 | Regulated   | user3       | Regulated   | user2       | Unregulated | disabled |
		| user2 | Unregulated | user1       | Regulated   | user4       | Unregulated | enabled  |

#MS365-3936
Scenario: Audio call from Calls function
	Given Launch MS teams app and login with user4
	Then Open each apps from left tabs
		| Apps     |
		| Calls    |
	When Start call with user2 by calls button
	Then Verify chat call dialog is enabled
	When Open More Options
	Then Verify buttons are available
	   | Button        |
	   | camera        |
	   | livecaption   |
	   | background    |
	When Hang up a MTG
	Then Verify chat call dialog is disabled

#MS365-3989
#Pending due to the permission
Scenario: Remove user from group chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Add users in chat and verify
		| InvitedUserName  |
		| user4            |
		| user3            |
	When Remove users from group chat
		| RemoveUsers  |
		| user3        |
	Then Verify number of group chat member shuld be 3

#MS365-3974
Scenario: Allow PPT sharing
	Given Launch MS teams app and login with user4
	Then Open Teams app and verify
	When Open test channel
	And  Start video call from channel top bar
	Then Verify video call dialog of test schedule with attendees is displayed
	When Join to MTG video call
	When Open screen share view
	Then Verify PPT live sharing is displayed
	When Start PPT live sharing by TEST.pptx
	Then Verify Live sharing top tool bar buttons available
	    | Button       |
	    | Stop share   |
	    | Layout       |
	    | Private View |
		| Popout       |
	And Verify main screen sharing is enabled
	And Verify Live sharing action tool buttons available
	    | Button       |
	    | Grid view    |
	    | More action  |
	    | Cursor       |
		| LaserPointer |
		| Pen          |
		| Highlighter  |
		| Eraser       |
	When Stop PPT live sharing
	Then Verify main screen sharing is disabled
	When End a MTG video call

#MS365-3969
Scenario: Allow meeting transcription on MTG
	Given Launch MS teams app and login with user4
	Then Open Teams app and verify
	When Open test channel
	And  Start video call from channel top bar
	Then Verify video call dialog of General is displayed
	When Join to MTG video call
	When Open More Options
	And  Start transcription
	Then Verify transcript has started
	When Open More Options
	And  Stop transcription
	Then Verify transcript has stopped
	When End a MTG video call

#MS365-3979
Scenario: Allow Live caption on channel MTG
	Given Launch MS teams app and login with user4
	Then Open Teams app and verify
	When Open test channel
	And  Start video call from channel top bar
	Then Verify video call dialog of General is displayed
	When Join to MTG video call
	When Open More Options
	And  Start Live caption
	Then Verify live caption has started
	When Open More Options
	And  Stop Live caption
	Then Verify live caption has stopped
	When End a MTG video call

#### Calendar ####
Scenario: Create a schedule for myself
	Given Launch MS teams app and login with user4
	Then Open Calendar app and verify
	When Open schedule edit
	And Input schedule title as test schedule
	#And Input start and end datetime from today
	#   | StartDaysAfter | EndDaysAfter | StartTime  | EndTime  |
	#   | 18/05/2022     | 0            | 19:00      | 20:00    |
	#And Input Every weekday (Mon - Fri) recurrence with parameters
	#	| StartDaysAfter    | EndDaysAfter     | RepeatCount |
	#	| 0                 | 7                | 1           |
	#And Input Daily recurrence with parameters
	#   | StartDaysAfter | EndDaysAfter | RepeatCount |
	#   | 0              | 2            | 2           |
	#And Input Weekly recurrence with parameters
	#	| StartDaysAfter  | EndDaysAfter | RepeatCount | TargetDay        |
	#	| 0               | 60           | 1           | Monday,Thursday  |
	#And Input Monthly recurrence with parameters
	#   | StartDaysAfter | EndDaysAfter  | RepeatCount | TargetDateType |
	#   | 0              | 60            | 1           | On the First Tuesday |
	#And Input Yearly recurrence with parameters
	#	| StartDaysAfter  | EndDaysAfter  | RepeatCount | TargetDateType |
	#	| 0               | 20           | 1           | On May 25    |
	#And Input Add Channel as test channel
	#And Inpit Add Location as test location
	And Input description as test description
	Then Save and verify created schedule by title test schedule

Scenario: Create a schedule with attendees
	Given Launch MS teams app and login with user2
	Then Open Calendar app and verify
	When Open schedule edit
	And Input schedule title as test schedule
	And Input attendees and optional attendees
	    | attendees | Optional Attendees |
		| user4     | user3              |
	And Input start and end datetime from today
	    | StartDaysAfter | EndDaysAfter | StartTime  | EndTime  |
		| 0             | 0           | 6:00 PM    | 6:30 PM  |
	#And Input Add Channel as test channel
	#And Inpit Add Location as test location
	And Input description as test description
	Then Send invitation and verify created schedule by title test schedule

Scenario: Start a call from Schedule
	Given Launch MS teams app and login with user1
	#Then Open Calendar app and verify
	#When Open schedule edit
	#And Input schedule title as test schedule with attendees
	#And Input attendees and optional attendees
	#    | attendees |
	#	| user2     |
	#And Input start and end datetime from today
	#    | StartDaysAfter | EndDaysAfter | StartTime  | EndTime  |
	#	| 0              | 0            | 20:00      | 20:30    |
	#And Input Add Channel as test channel
	#And Inpit Add Location as test location
	#And Input descrioption as test description
	#Then Send invitation and verify created schedule by title test schedule with attendees
	When Open event info of test schedule with attendees
	And  Start video call by Button
	#And  Start video call by Link
	Then Verify video call dialog of test schedule with attendees is displayed
	Then Verify camera should be terned off
	Then Verify computer audio setting available
	When Join to MTG video call
	When Open More Options
	Then Verify buttons are available
	   | button        |
	   | camera        |
	   | sharescreen   |
	   | transcription |
	   | recording     |
	And  Verify buttons are not available
	   | button     |
	   | mic        |
	When Change background setting to Blur
	Then Verify the background Blur is applied
	When Close background setting view
	When End a MTG video call
	#When Carera setting turned on
	#Then Verify camera should be terned on
	#When Carera setting turned off
	#Then Close video call window
	#When Change background setting to Dessert Island
	#Then Verify the background Dessert Island is applied

@IndividualChat #Z #MS365-3993
Scenario: Show Organization tab in chat
	Given Launch MS teams app and login with user4
	Then Search the employee user2 and verify the search list
	When Open Organisation tab in chat
	Then Verify user2 panel is displayed