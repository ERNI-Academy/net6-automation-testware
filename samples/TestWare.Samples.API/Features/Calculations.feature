@API
Feature: API


Scenario: Simplify formula
	When the formula '2^2+2(2)' is simplified
	Then the simplified result is '8'
	And the simplified response status code is '200'

Scenario: factor formula
	When the formula 'x^2+2x' is factorized
	Then factor response status should be '200'
	And factor response result is "x (x + 2)"

	
Scenario: derive formula
	When the formula 'x^2+2x' is derived
	Then derive response status should be '200'
	And derive response result is "2 x + 2"

Scenario: integrate formula
	When the formula 'x^2+2x' is integrated
	Then integrate response status should be '200'
	And integrate response result is "1/3 x^3 + x^2"

Scenario: wrong endpoint
	When inexistent endpoint is called
	Then unexpected response status should be '400'
	And unexpected response error is "Unknown operation"


Scenario: sine operation
	When the operation sine is invoked with "1/2pi"
	Then sine response status should be '200'
	And sine response result is "1"

Scenario: sine wrong parameters
	When the operation sine is invoked with "(pi"
	Then sine response status should be '500'
	And sine response error is "Unable to perform calculation"
