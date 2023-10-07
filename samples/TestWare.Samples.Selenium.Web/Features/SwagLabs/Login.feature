@WebDriver
Feature: Login

@critical
Scenario Outline: Login 
	Given the user enters username '<username>'
	And the user enters password '<password>'
	When the user clicks submit
	Then the user can login

	Examples: 
	 | Example Description | username      | password     |
	 | standard            | standard_user | secret_sauce |
	 | problem             | problem_user  | secret_sauce |

@blocker
@tms:5
@issue:3
Scenario: Logout
	Given user 'standard_user' is logged with 'secret_sauce' into SwagLabs
	When the user clicks Logout button
	Then the user is at Login page
