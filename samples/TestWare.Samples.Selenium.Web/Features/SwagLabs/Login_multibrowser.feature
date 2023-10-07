@MultiWebDriver
Feature: Login_multibrowser

@browser1Swaglabs @browser2Swaglabs @browser3Swaglabs
Scenario: Login on multiple browsers
	Given the user enters username 'standard_user' on all
	And the user enters password 'secret_sauce' on all
	When the user clicks submit on all
	Then the user can login on all

@browser1Swaglabs @browser2Swaglabs
Scenario: Login on multiple browsers and logout browser2
	Given the user enters username 'standard_user' on all
	And the user enters password 'secret_sauce' on all
	And the user clicks submit on all
	And the user can login on all
	When the user clicks Logout button on 'browser2Swaglabs' 
	Then the user is at Login page on 'browser2Swaglabs'
