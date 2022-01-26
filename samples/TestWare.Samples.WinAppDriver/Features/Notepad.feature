@WinAppDriver
@Notepad
Feature: Notepad

Scenario: Write text
	Given user writes 'text example'
	When user deletes '8' characters
	Then file text is 'text'
