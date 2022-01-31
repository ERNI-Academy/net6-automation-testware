@WebDriver
Feature: Login

@NominalTest
Scenario: Login 
	Given the user enters username 'standard_user'
	And the user enters password 'secret_sauce'
	When the user clicks submit
	Then the user can login

Scenario: Logout
	Given user 'standard_user' is logged with 'secret_sauce' into SwagLabs
	When the user clicks Logout button
	Then the user is at Login page