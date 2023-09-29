Feature: MSTeamWindowsApp
	MS Teams App Testing Features

#senthilkumar
#automation_2022 
# try login with sign in process
@Web @Ver2
Scenario: MS Teams Web App verificaiton
	Given Launch MS teams Web app and login with user4
	Then Verify the webapp logged user
	#Then Logout successfully from web application