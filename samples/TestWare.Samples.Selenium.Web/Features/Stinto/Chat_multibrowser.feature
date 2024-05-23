@MultiWebDriver
Feature: Chat_multibrowser


@browser1Chat @browser2Chat
Scenario: Chat between two users
	Given the 'user1' creates a new chat session on 'browser1Chat'
	When the 'user2' joins chat session on 'browser2Chat'
	Then the 'has joined.' message from 'user2' appears on 'browser1Chat'
	And the 'has joined.' message from 'user1' appears on 'browser2Chat'
	When the user sends 'a test' message on 'browser1Chat'
	Then the 'a test' message from 'user1' appears on all browsers
