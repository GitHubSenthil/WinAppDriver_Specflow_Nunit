Feature: MSTeamWindowsAppRegulated
	MS Teams App Testing Features for Regulated user

@Test1
Scenario: MS Teams App and Web App
	Given Launch MS teams app and login with user2
	Given Launch MS teams Web app and login with user4
	Then Send a TestMessage message to user4
	When Verify the user2 message TestMessage is received  

@Ver @Test1
Scenario: MS Teams App verificaiton
	Given Launch MS teams app and login with user2
	#Then Verify the logged in user user1
	Then Logout successfully from application

@Ver1 @Nunit @IndividualChat
Scenario: Teams App to search user and chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Send a Message and verify the sent message
		| Messages                             |
		#| Hello                                |
		#| Testing                              |
		| Special characters !"£$%^&*()[];',./ |
		| Numbers 1234567890                   |
		#| senthilkumar@testing.com             |

@Ver1 @General
Scenario: All tabs verification
	Given Launch MS teams app and login with user2
	Then Open each apps from left tabs
		| Apps     |
        | Activity |
		| Chat     |
		| Teams    |
		| Files    |
		#| Training |
		| other    |

@Test1 @IndividualChat
Scenario: Send a message with user mention
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Send message with mention
		| Userfullname | Message                     |
		| user2        | Hello, this is test message |

@Nunit @IndividualChat
Scenario: Send a message with delivery option.
	Given Launch MS teams app and login with user2
	#Then Search the employee user2 and verify the search list
	Then Send a message with delivery option
		| DeliveryOption | Message                       |
		| Urgent         | Hello, Test urgent message    |
		| Standard       | Hello, Test standard message  |
		| Important      | Hello, Test important message |

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

@IndividualChat
Scenario: Upload file on chat
	Given Launch MS teams app and login with user2
	#Then Search the employee user4 and verify the search list
	Then Upload a TXT file from PC
	Then Send a file message and verify

@General
Scenario: Verify call button from Teams Calls
	Given Launch MS teams app and login with user1
	Then Open Calls app and verify
	Then Verify a call button as DISABLE
	Then Search user user2 from Calls app and verify
	Then Verify a call button as ENABLE

@GroupChat
Scenario: Add new user in chat
	Given Launch MS teams app and login with user1
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

@TeamsChannel
Scenario: Create a new Teams Channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	Then Create a new Teams group asn verify
		| GroupType | PublicationType | Title | Users |
		| Scratch   | PUBLIC          | check | user2 |

@TeamsChannel
Scenario: Send a message in Teams channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open check channel
	And  Send a New post in channel
		| Messages                |
		| Test message in channel |
	Then Verify the posted message in channel
		| Messages                |
		| Test message in channel |

@TeamsChannel
Scenario: Reply to Teams channel message
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open check channel
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
	Then Verify a phone call button is disabled
	Then Verify a video call button is disabled

@TeamsChannel @VideoCall
Scenario: Start channel MTG
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open check channel
	Then Verify video call button is disnable on channel top bar
	#And  Start video call from channel top bar
	#Then Verify video call dialog of general is displayed
	#Then Verify camera should be terned off
	#Then Verify background setting should be terned off on setting dialog
	#Then Verify computer audio setting available
	#When Join to MTG video call
	#When Open More Options
	#Then Verify buttons are available
	#   | Button        |
	#   | camera        |
	#   | sharescreen   |
	#   | livecaption   |
	#   | transcription |
	#   | recording     |
	#   | background    |
	#And  Verify buttons are not available
	#   | Button     |
	#   | mic        |
	#When End a MTG video call

## In order to check 100%, we need to interactive between 2 users
#@TeamsChannel @VideoCall
#Scenario: Lock MTG on Teams channel
#	Given Launch MS teams app and login with user1
#	Then Open Teams app and verify
#	When Open test channel
#	And  Start video call from channel top bar
#	Then Verify video call dialog of test schedule with attendees is displayed
#	When Join to MTG video call
#	#And  Set bypass lobby as Only me and co-organizers
#	#And  Save meeting setting
#	#Then Verify meeting setting is saved
#	#When Close metting setting view
#	And  Lock the MTG
#	Then Verify MTG is locked
#	When End a MTG video call
#
#@TeamsChannel @VideoCall
#Scenario: Screen sharing on Teams channel
#	Given Launch MS teams app and login with user1
#	Then Open Teams app and verify
#	When Open test channel
#	And  Start video call from channel top bar
#	Then Verify video call dialog of test schedule with attendees is displayed
#	When Join to MTG video call
#	When Open screen share view
#	And  Click share full screen
#	Then Verify Screen has shared
#	When Stop share screen
#	Then Verify screen is not shared
#	When End a MTG video call
#
#@TeamsChannel @VideoCall
#Scenario: Recording MTG on Teams channel
#	Given Launch MS teams app and login with user1
#	Then Open Teams app and verify
#	When Open test channel
#	And  Start video call from channel top bar
#	Then Verify video call dialog of test schedule with attendees is displayed
#	When Join to MTG video call
#	Then Verify screen is not shared
#	When Open More Options
#	And  Start record MTG
#	Then Verify transcript has started
#	And Verify recording MTG has started
#	When Open More Options
#	And  Stop record MTG
#	Then Verify transcript has stopped
#	And Verify recording MTG has stopped
#	When End a MTG video call
#
#@TeamsChannel @VideoCall
#Scenario: Background setting on setting dialog
#	Given Launch MS teams app and login with user1
#	Then Open Teams app and verify
#	When Open test channel
#	And  Start video call from channel top bar
#	Then Verify video call dialog of general is displayed
#	Then Verify camera should be terned off
#	When Camera setting turned on
#	And  Change background on setting dialog to Blur
#	Then Verify the background Blur is applied
#	When Close background setting view
#	Then Close video call window
#
#@TeamsChannel @VideoCall
#Scenario: Background setting on call
#	Given Launch MS teams app and login with user1
#	Then Open Teams app and verify
#	When Open test channel
#	And  Start video call from channel top bar
#	Then Verify video call dialog of test schedule with attendees is displayed
#	When Join to MTG video call
#	When Change background setting to Blur
#	Then Verify the background Blur is applied
#	When Close background setting view
#	When End a MTG video call