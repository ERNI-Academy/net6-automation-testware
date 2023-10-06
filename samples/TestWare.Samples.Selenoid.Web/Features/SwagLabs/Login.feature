@RemoteDriver
Feature: Login

Scenario Outline: Login 
	Given the user enters username '<username>'
	And the user enters password '<password>'
	When the user clicks submit
	Then the user can login

	@Chrome
	Examples: 
	 | Example Description | username      | password     |
	 | standard            | standard_user | secret_sauce |
	 | problem             | problem_user  | secret_sauce |

	@Firefox
	Examples: 
	 | Example Description | username      | password     |
	 | standard            | standard_user | secret_sauce |
	 | problem             | problem_user  | secret_sauce |

	@Edge
	Examples: 
	 | Example Description | username      | password     |
	 | standard            | standard_user | secret_sauce |
	 | problem             | problem_user  | secret_sauce |

@Chrome
Scenario: Logout
	Given user 'standard_user' is logged with 'secret_sauce' into SwagLabs
	When the user clicks Logout button
	Then the user is at Login page
