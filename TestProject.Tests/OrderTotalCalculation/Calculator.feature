Feature: Calculate order total

Testing of restaurant checkout system with ability to have orders with and without discount

@First
Scenario: Calculation Of Order Without Discount
Given 4 guests in the restaurant make order with 4 starters(£4 each), mains(£7 each) and drinks (£2.50 each)
And All drinks were ordered after 19:00
When I send request to calculate order
Then the response total price should be 54

@First
Scenario Outline: Calculation of Order Without Discount
Given I've 4 Guests added <Starters>, <Mains>, <Drinks W.o Disc>, in the order
And Order contains <Drinks W. Disc>
When I send request to calculate order
Then response total price should be <Total> 

Examples:
  | Starters | Mains | Mains | Drinks W.o Disc |Drinks W. Disc | Total |
  | 1        | 1     | 1     | 1               | 0             | 54    |
  | 1        | 2     | 2     | 2               | 0             | 108   |
  
  