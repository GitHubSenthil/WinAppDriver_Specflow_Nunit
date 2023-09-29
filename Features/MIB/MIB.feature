Feature: MIB Testing

@RerunMIB001 @MIB
Scenario Outline: Verify MIB Undefined user group access
	Given Launch MS teams app with MIB <UserCategory> user
	When <UserCategory> Search user to verify access to MIB <GroupAccess> users
	Then <UserCategory> Search user to verify not having access to MIB <NonGroupAccess> users
Examples: 
	| UserCategory   | GroupAccess                                                           | NonGroupAccess     |
	| UndefinedUser1 | UndefinedUser2,JPIBDOUT1,GlobalIBDExJP1,JPEQTResearch1,GlobalEQTExJP1 | JPIBDIN1,JPIBDNCI1 |
	