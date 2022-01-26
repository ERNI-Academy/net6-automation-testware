@WebDriver
Feature: Login

@NominalTest
Scenario: Login 
    Given Cookies have been accepted
	And the user enters username 'mngr379661'
	And the user enters password 'rybepyq'
	When the user clicks submit
	Then the user can login

Scenario: Logout
    Given Cookies have been accepted
	And user 'mngr379661' is logged with 'rybepyq' into Guru99
	When the user clicks Logout button
	And user exit notification is accepted
	Then the user is at Login page