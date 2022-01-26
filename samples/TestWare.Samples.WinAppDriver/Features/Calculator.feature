@WinAppDriver
@Calculator
Feature: Calculator

Scenario: Add numbers
	Given select number '50'
	And select operation '+'
	And select number '70'
	When click on operation equals button
	Then the result should be '120'

Scenario: Substract numbers
	Given select number '50'
	And select operation '-'
	And select number '70'
	When click on operation equals button
	Then the result should be '-20'

Scenario: Multiply numbers
	Given select number '12'
	And select operation '*'
	And select number '5'
	When click on operation equals button
	Then the result should be '60'

Scenario: Divide numbers
	Given select number '100'
	And select operation '/'
	And select number '20'
	When click on operation equals button
	Then the result should be '5'
