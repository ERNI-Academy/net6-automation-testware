@WinAppDriver
@WindowsCalculator
Feature: WindowsCalculator

Scenario: Add numbers
	Given number '50'
	And operation '+'
	And number '70'
	When click on equals button
	Then result should be '120'

Scenario: Substract numbers
	Given number '50'
	And operation '-'
	And  number '70'
	When click on equals button
	Then result should be '-20'

Scenario: Multiply numbers
	Given number '12'
	And operation '*'
	And number '5'
	When click on equals button
	Then result should be '60'

Scenario: Divide numbers
	Given number '100'
	And operation '/'
	And number '20'
	When click on equals button
	Then result should be '5'
