@MongoDB
Feature: Database


Scenario: Insert at database
	When the following document is inserted in 'collection-example' collection at 'database-example' database
	| NAME  |
	| Didac |
	Then the following document is saved in 'collection-example' collection at 'database-example' database
	| NAME  |
	| Didac |


Scenario: Delete at database
	Given the following document is saved in 'collection-example' collection at 'database-example' database
	| NAME  |
	| Diego |
	When the following document is deleted in 'collection-example' collection at 'database-example' database
	| NAME  |
	| Diego |
	Then no documents are saved in 'collection-example' collection at 'database-example' database with values
	| NAME  |
	| Diego |


