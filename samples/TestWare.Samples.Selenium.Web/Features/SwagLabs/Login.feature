@WebDriver
Feature: Login

@NominalTest
Scenario Outline: Login 
	Given the user enters username '<username>'
	And the user enters password '<password>'
	When the user clicks submit
	Then the user can login

	Examples: 
	 | username      | password     |
	 | standard_user | secret_sauce |
	 | standard_user | secret_sauce |

Scenario: Logout
	Given user 'standard_user' is logged with 'secret_sauce' into SwagLabs
	When the user clicks Logout button
	Then the user is at Login page
