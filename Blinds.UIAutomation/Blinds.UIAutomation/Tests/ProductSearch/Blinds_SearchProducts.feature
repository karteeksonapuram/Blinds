Feature: Blinds_SearchProducts
	
@SearchProduct
Scenario: Search for room darkening blinds and choose the least priced product
	Given I launch blinds website
	And I should see blinds home page
	When I search for 'room darkening blinds'
	Then I should see results page with 'Blackout Shades & Room Darkening Shade'
	And I should see horizontal filtering options with "brand|category|available options"
	And I should see sort options
	When I select 'Low-High' option
	Then I should see the lowest priced product first
	When I click shop now button
	Then I should land on product description page