Feature: Interactive 2 user simple chat testing
	MS Teams App for simple chat by 2 user Testing Features

@2user-chat @DEMOReceiverSimpleChat
Scenario: Multiple communication receiver
	Given Launch MS teams app and login with user2
	#Then Search the employee user2 and verify the search list
	Then Receiver user Send a Message from user4 and verify the sent message
		| SenderMessageType | SenderMessage                | ReceiverMessageType | ReceiverMessage  |
		| EMOJI             | Smilies:Crying with laughter | EMOJI               | Smilies:Angel    |
		| TEXT              | sendermessage1               | TEXT                | receivermessage1 |
		#| TEXT_EMOJI        | sendermessage2               | TEXT_Reply          | receivermessage2 |
		#| FILE              | Test_WordDocument.docx       | TEXT_EMOJI          | ReceiverMessage3 |

@2user-chat @Rec @DEMOSenderSimpleChat
Scenario: Multiple communication Sender
	Given Launch MS teams app and login with user4
	#Then Search the employee user4 and verify the search list
	Then Sender user Send a Message from user2 and verify the sent message
		| SenderMessageType | SenderMessage                | ReceiverMessageType | ReceiverMessage  |
		| EMOJI             | Smilies:Crying with laughter | EMOJI               | Smilies:Angel    |
		| TEXT              | sendermessage1               | TEXT                | receivermessage1 |
		#| TEXT_EMOJI        | sendermessage2               | TEXT_Reply          | receivermessage2 |
		#| FILE              | Test_WordDocument.docx       | TEXT_EMOJI          | ReceiverMessage3 |