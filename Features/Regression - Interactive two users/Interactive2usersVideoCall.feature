Feature: Interactive 2 user video call testing
	MS Teams App for simple video call by 2 user Testing Features

@DEMOSender1
### Mic config should be ON when we test on proper env
### QA environment doesn't have the background setting on the direct call
Scenario: Start video call as organizer
	Given Launch MS teams app and login with user2
	#Then Search the employee user2 and verify the search list
	When Start phone call on chat
	Then Verify chat call dialog is enabled
	When Wait until attendee join
	When Open More Options
	Then Verify buttons availavility
	    | Button        | availability | default config |
		| camera        | enable       | off            |
		| people        | enable       |                |
		| chat          | enable       |                |
	    | livecaption   | enable       |                |
	    #| background    | disable      |                |
		| sharescreen   | enable       |                |
		| mic           | disable      | off            |
		| recording     | disable      |                |
		| transcription | enable       |                |
	When Hang up a MTG
	Then Verify chat call dialog is disabled

@DEMOReceiver1
Scenario: Join video call as attendee
	Given Launch MS teams app and login with user2
	When Wait until ringing the call
	And Join the call as attendee by phone call
	Then Verify chat call dialog is enabled
	When Open More Options
	Then Verify buttons availavility
	    | Button        | availability | default config |
		| camera        | enable       | off            |
		| people        | enable       |                |
		| chat          | enable       |                |
	    | livecaption   | enable       |                |
	    #| background    | enable       |                |
		| sharescreen   | enable       |                |
		| mic           | disable      | off            |
		| recording     | disable      |                |
		| transcription | enable       |                |
	Then Wait Until video call is terminated
	Then Verify chat call dialog is disabled