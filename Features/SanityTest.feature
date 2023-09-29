Feature: Sanity testing
	MS Teams App Sanity Testing Features
	 
# MS365-668
Scenario: MS Teams App verificaiton
	Given Launch MS teams app and login with user2
	#Then Verify the logged in user user1
	Then Logout successfully from application

# MS365-669
# MS365-713
Scenario: Teams App to search user and chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Send a Message and verify the sent message
		| Messages                             |
		| Hello, world                         |
		| Special characters !"£$%^&*()[];',./ |
		| Numbers 1234567890                   |
		| senthilkumar@testing.com             |

# MS365-670
Scenario: Search a File
	Given Launch MS teams app and login with user1
	Then Search the file TEST.pptx and verify the search list

# MS365-671
Scenario: Create a new Teams Channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	Then Create a new Teams group asn verify
		| GroupType | PublicationType | Title             | Users |
		| Scratch   | PUBLIC          | AutoUnreg_public  | user2 |
		| Scratch   | PRIVATE         | AutoUnreg_private | user2 |


#@GroupChat
#Scenario: Add new user in chat
#	Given Launch MS teams app and login with user1
#	Then Search the employee user2 and verify the search list
#	Then Add users in chat and verify
#		| InvitedUserName  |
#		| user3            |
#

#@GroupChat
#Scenario: Send a message in group chat
#	Given Launch MS teams app and login with user1
#	Then Search the employee user2 and verify the search list
#	Then Add users in chat and verify
#		| InvitedUserName  |
#		| user3            |
#	Then Send a Message and verify the sent message
#		| Messages                |
#		| Test group chat message |

# MS365-679 
# MS365-690 
# MS365-691
# MS365-714
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

#MS365-697
Scenario: Upload file on chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Upload a Test_WordDocument file from PC
	Then Send a file message and verify

#MS365-720
# Need to update for regulated users becuase it will bisconnected straight after user started a call
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

#MS365-733
Scenario: Screen sharing on Teams channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel
	And  Start video call from channel top bar
	Then Verify video call dialog of test schedule with attendees is displayed
	When Join to MTG video call
	When Open screen share view
	And  Click share full screen
	Then Verify Screen has shared
	When Stop share screen
	Then Verify screen is not shared
	When End a MTG video call

#MS365-1102
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

#MS365-1210
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

#MS365-1210
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