@MultiWebDriver
Feature: Chat_multibrowser


@browser1 @browser2
Scenario: Chat between two users
	Given the 'user1' creates a new chat session on 'browser1'
	When the 'user2' joins chat session on 'browser2'
	Then the 'has joined.' message from 'user2' appears on 'browser1'
	And the 'has joined.' message from 'user1' appears on 'browser2'
	When the user sends 'a test' message on 'browser1'
	Then the 'a test' message from 'user1' appears on all browsers
