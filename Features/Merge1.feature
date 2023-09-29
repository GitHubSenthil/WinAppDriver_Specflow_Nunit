Feature: Merge1 testing
	MS Teams App merge 1 Testing Features

# MS365-1316
@GroupChat
Scenario: Add new user in chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Add users in chat and verify
		| InvitedUserName  |
		| user3            | #EMEA
		| user4            | #US
		| user5            | #HK
		| user6            | #JP w/s
		| user7            | #JP retail

# MS365-1321
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

# MS365-1322
# MS365-1323
# MS365-1324
@Nunit @IndividualChat
Scenario: Send a message with delivery option.
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Send a message with delivery option
		| DeliveryOption | Message                       |
		| Urgent         | Hello, Test urgent message    |
		| Standard       | Hello, Test standard message  |
		| Important      | Hello, Test important message |

# MS365-1325
# MS365-1326
# MS365-1327
# MS365-1328
# MS365-1329
# MS365-1330
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

# MS365-1333
@IndividualChat
Scenario: Send Emoji messages
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Send Emoji messages with text and verify
 		| EmojiType | EmojiCharactor                                                        | Text         |
		| Smilies   | Grinning face with big eyes,Squinting face with tongue,Cool robot,Emo | Test message |
		| Smilies   | Grinning face with big eyes,Squinting face with tongue,Emo            | Test message |
		| Smilies   | Grinning face with big eyes                                           | Test message |
		| Smilies   | Grinning face with big eyes                                           |              |

# MS365-1336
@IndividualChat @Merge1
Scenario: Upload file on chat
	Given Launch MS teams app and login with user1
	Then Search the employee user2 and verify the search list
	Then Upload a Test_WordDocument file from PC
	Then Send a file message and verify

# MS365-1345
@TeamsChannel
Scenario: Send a message in Teams channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel
	And  Send a New post in channel
		| Messages                |
		| Special characters !"£$%^&*()[];',./ |
	Then Verify the posted message in channel
		| Messages                |
		| Special characters !"£$%^&*()[];',./ |

# MS365-1346
Scenario: Send a message with mention in Teams channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel
	Then  Send a New post with mention in channel and verify
		| Userfullname | Message                    |
		| user2        | mention message in channel |

# MS365-1351
Scenario: Add new member in Teams from option
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	Then Add new user from Teams option
		| TragetGroup | Users       |
		| test        | user3,user4 |

# MS365-1351
Scenario: Add new member in Teams from teams management
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	Then Add new user from Teams management
		| TragetGroup | Users       |
		| test        | user3,user4 |

# MS365-1317
# MS365-1398
# MS365-1399
# MS365-1401
@TeamsChannel
Scenario: Create a new Teams Channel
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	Then Create a new Teams group asn verify
		| GroupType | PublicationType | Title | Users |
		| Scratch   | PUBLIC          | check | user2 |

# MS365-1317
# MS365-1398
# MS365-1399
# MS365-1401
@TeamsChannel @Merge1
Scenario: Reply to Teams channel message
	Given Launch MS teams app and login with user1
	Then Open Teams app and verify
	When Open test channel
	And  Send a New post in channel
		| Messages                |
		| Test message in channel |
	And  Send a reply post in channel
		| Messages                      |
		| Reply Special characters !"£$%^&*()[];',./ |
	Then Verify the posted message in channel
		| Messages                      |
		| Reply Special characters !"£$%^&*()[];',./ |