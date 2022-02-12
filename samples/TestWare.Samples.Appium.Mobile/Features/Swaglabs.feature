@Appiumdriver
Feature: Swaglabs


@IOSDriver @Androiddriver
Scenario: Login - Valid
	When login on SwagLabs with username 'standard_user' and password 'secret_sauce'
	Then the user can login - SwagLabs

@Androiddriver
Scenario: Add product to cart by button
	Given login on SwagLabs with username 'standard_user' and password 'secret_sauce'
	And the user can login - SwagLabs
	And the user click on toggle view - Products
	When the user adds product 'Sauce Labs Backpack' to cart clicking Add button
	And the user opens the Cart
	Then cart contains 'Sauce Labs Backpack' product

@Androiddriver
Scenario: Add product to cart by dragging
	Given login on SwagLabs with username 'standard_user' and password 'secret_sauce'
	And the user can login - SwagLabs
	And the user click on toggle view - Products
	When the user adds product 'Sauce Labs Backpack' to cart by dragging
	And the user opens the Cart
	Then cart contains 'Sauce Labs Backpack' product

@Androiddriver
Scenario: Remove product from cart by button
	Given login on SwagLabs with username 'standard_user' and password 'secret_sauce'
	And the user can login - SwagLabs
	And the user click on toggle view - Products
	When the user adds product 'Sauce Labs Backpack' to cart clicking Add button
	And the user opens the Cart
	Then cart contains 'Sauce Labs Backpack' product
	When the user removes product 'Sauce Labs Backpack' from cart clicking Remove button
	Then cart does not contain 'Sauce Labs Backpack' product

@Androiddriver
Scenario: Remove product from cart by dragging
	Given login on SwagLabs with username 'standard_user' and password 'secret_sauce'
	And the user can login - SwagLabs
	And the user click on toggle view - Products
	When the user adds product 'Sauce Labs Backpack' to cart clicking Add button
	And the user opens the Cart
	Then cart contains 'Sauce Labs Backpack' product
	When the user removes product 'Sauce Labs Backpack' from cart by dragging
	Then cart does not contain 'Sauce Labs Backpack' product
