Feature: Zscalar_Regression
	ZScalar Regression Test Scenarios

#@RerunZsc001 @Ver1 @General #Z #MS365-3561
#Scenario: Access of Learning Pathways App
#	Given Launch MS teams app and login with user4
#	Then Open each apps from left tabs
#		| Apps     |
#        | Training |
#		#| Chat     |


@RerunZsc002 @Jenkins @Test213 @IndividualChat @ZscalarRegression @C123 @MS365-3991
Scenario: Send a message with user mention
	Given Launch MS teams app and login with user4
	Then Search the employee user2 and verify the search list
	Then Send message with mention 
		| Userfullname | Message                     |
		| user2        | Hello, this is test message |

@RerunZsc003 @Nunit @IndividualChat @Test213 @ZscalarRegression @C111 @MS365-3987 
Scenario: Send a message with delivery option.
	Given Launch MS teams app and login with user4
	Then Search the employee user2 and verify the search list
	Then Send a message with delivery option
		| DeliveryOption | Message                           |
		| Urgent         | Hello, Its an Urgent Test Message |
		
#@RerunZsc004 @IndividualChat #Z #MS365-3945
#Scenario: Upload file on chat
#	Given Launch MS teams app and login with user4
#	Then Search the employee user2 and verify the search list
#	Then Upload a Test_WordDocument.docx file from PC
#	Then Send a file message and verify

@RerunZsc005 @VideoCall @ZscalarRegression #MS365-3931
Scenario: Verify direct video call from individual chat
	Given Launch MS teams app and login with user4
	Then Search the employee user2 and verify the search list
	Then Verify a video call button is enabled
	When Start video call on chat
	Then Verify chat call dialog is enabled
	When Open More Options
	Then Verify buttons are available
	   | Button        |
	   | camera        |
	   | livecaption   |
	   #| background    |
	When Hang up a MTG
	Then Verify chat call dialog is disabled

@RerunZsc006 @TeamsChannel @VideoCall @ZscalarRegression #MS365-3937
Scenario: Start channel MTG
	Given Launch MS teams app and login with user4
	Then Open Teams app and verify
	When Open Test Public Team channel and select General category
	Then Verify video call button is enable on channel top bar
	When Start video call from channel top bar
	Then Verify video call dialog of General is displayed
	Then Verify camera should be terned off
	#Then Verify background setting should be terned off on setting dialog
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
	   #| background    |
	   | mic        |
	When End a MTG video call

@RerunZsc007 @TeamsChannel @VideoCall @ZscalarRegression #MS365-3972
Scenario: Screen sharing on Teams channel
	Given Launch MS teams app and login with user4
	Then Open Teams app and verify
	When Open Test Public Team channel and select General category
	And  Start video call from channel top bar
	Then Verify video call dialog of General is displayed
	When Join to MTG video call
	When Open screen share view
	And  Click share full screen
	Then Verify Screen has shared
	When Stop share screen
	Then Verify screen is not shared
	When End a MTG video call

@RerunZsc008 @TeamsChannel @VideoCall @ZscalarRegression #MS365-3970
Scenario: Recording MTG on Teams channel
	Given Launch MS teams app and login with user4
	Then Open Teams app and verify
	When Open Test Public Team channel and select General category
	And  Start video call from channel top bar
	Then Verify video call dialog of General is displayed
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


@RerunZsc009 @IndividualChat @ZscalarRegression #MS365-3984
Scenario: Send URL and check user preview
	Given Launch MS teams app and login with user4
	Then Search the employee user2 and verify the search list
	Then Send format messages with text and verify
		| Format               | option					   | Text                |
		| Insert link          | https://www.google.co.uk/ | Insertlink - google |

#MS365-3951 #MS365-3961
@RerunZsc010 @ZscalarRegression
Scenario: Verify the options in individual chat
	Given Launch MS teams app and login with user4
	Then Search the employee user2 and verify the search list
	When Send a Message and verify the sent message
		| Messages        |
		| testing option2 |
	And Verify user option is enabled
		| TargetMsg       | Option | Enabled |
		| testing option2 | Edit   | false   |
		| testing option2 | Delete | false   |
		| testing option2 | Reply  | true    |

#MS365-3983
@RerunZsc011 @ZscalarRegression
Scenario: Verify the options in right chat pane
	Given Launch MS teams app and login with user4
	Then Search the employee user2 and verify the search list
	And Verify the left pane chat option for user2
		| Chat Option | Enabled |
		| Delete      | false   |
		| Mute        | true    |

#MS365-3958
@RerunZsc012 @ZscalarRegression
Scenario: Verify the options in individual chat from sent user
	Given Launch MS teams app and login with user4
	Then Search the employee user2 and verify the search list
	And Verify another user2 sent chat options
		| Option | Enabled |
		| Delete | false   |
		| Reply  | true    |

@RerunZsc013 @TeamsChannel @ZscalarRegression #MS365-3955
Scenario: Teams Channel same user chat message options
	Given Launch MS teams app and login with user4
	Then Open Teams app and verify
	When Open Test Public Team channel and select General category
	And  Send a New post in channel
		| Messages                |
		| Test message in channel |
	Then Verify user4 sent message options in channel
		| Option | Enabled |
		| Edit   | false   |
		| Delete | false   |
		| Pin    | true    |

# @TeamsChannel #MS365-3947
#Attachment function is not working now
#@RerunZsc014 @ZscalarRegression
#Scenario: Teams Channel user send message with attachment
#	Given Launch MS teams app and login with user4
#	Then Open Teams app and verify
#	When Open test - edit channel and select General category
#	And  Send a new post in channel by user4 with Attachment
#		| Messages                  | Attachment             |
#		| Channels1 Test2 Message3  | Test_WordDocument.docx |

#MS365-3936
@RerunZsc015 @ZscalarRegression
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
	   #| background    |
	When Hang up a MTG
	Then Verify chat call dialog is disabled

#MS365-3974
# It is not working in QA correctly at the moment even if we did by manual.
#@RerunZsc016
#Scenario: Allow PPT sharing
#	Given Launch MS teams app and login with user4
#	Then Open Teams app and verify
#	When Open test - edit channel and select General category
#	And  Start video call from channel top bar
#	Then Verify video call dialog of General is displayed
#	When Join to MTG video call
#	When Open screen share view
#	Then Verify PPT live sharing is displayed
#	When Start PPT live sharing from computer by TEST.pptx
#	Then Verify Live sharing top tool bar buttons available
#	    | Button       |
#	    | Stop share   |
#	    | Layout       |
#	    | Private View |
#		| Popout       |
#	And Verify main screen sharing is enabled
#	And Verify Live sharing action tool buttons available
#	    | Button       |
#	    | Grid view    |
#	    | More action  |
#	    | Cursor       |
#		| LaserPointer |
#		| Pen          |
#		| Highlighter  |
#		| Eraser       |
#	When Stop PPT live sharing
#	Then Verify main screen sharing is disabled
#	When End a MTG video call

#MS365-3969
@RerunZsc017 @ZscalarRegression
Scenario: Allow meeting transcription on MTG
	Given Launch MS teams app and login with user4
	Then Open Teams app and verify
	When Open Test Public Team channel and select General category
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
@RerunZsc018 @ZscalarRegression
Scenario: Allow Live caption on channel MTG
	Given Launch MS teams app and login with user4
	Then Open Teams app and verify
	When Open Test Public Team channel and select General category
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

#MS365-3968
@RerunZsc019 @ZscalarRegression
Scenario: Create a schedule for myself
	Given Launch MS teams app and login with user4
	Then Open Calendar app and verify
	When Open schedule edit
	And Input schedule title as test-schedule
	And Input description as test description
	Then Save and verify created schedule by title test-schedule

# Need to check the spell of Organization in QA env
@RerunZsc020 @IndividualChat @ZscalarRegression #MS365-3993
Scenario: Show Organization tab in chat
	Given Launch MS teams app and login with user4
	Then Search the employee user2 and verify the search list
	When Open Organization tab in chat
	Then Verify user2 panel is displayed
