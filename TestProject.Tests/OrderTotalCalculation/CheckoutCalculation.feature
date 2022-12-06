Feature: Calculate order total

Testing of restaurant checkout system with ability to have orders with and without discount
  
  Background: 
  Starters cost £4.00,
  Mains cost £7.00,
  Drinks cost £2.50
  Drinks have a 30% discount when ordered before 19:00
  
Scenario: Calculation Of Order Without Discount
  Given 4 guests in the restaurant make order with 4 starters, mains and drinks
  And All drinks were ordered after 19:00
  When I send request to calculate order
  Then the response total price should be 54
  
Scenario Outline: Calculation of Order Without Discount
  Given 4 guests added <Starters>, <Mains>, <Drinks W.o Disc>, in the order
  And Order contains no <Drinks W. Disc>
  When I send request to calculate order
  Then response total price should be <Total> 

Examples:
  | Starters | Mains | Drinks W.o Disc | Drinks W. Disc | Total |
  | 1        | 1     | 1               | 0              | 54    |
  
  
Scenario: Calculation Of Order With Discount
  Given 2 guests in the restaurant make order with 1 starter, 2 mains and 2 drinks ordered before 19:00
  And When I send request to calculate order, it returns total 21.50
  When 2 more guests joined, ordered 2 mains, 2 drinks at 20:00
  Then I send request for Total price calculation, and for all guests it should return Total 40.50

  Scenario Outline: Calculation Of Order With Discount
    Given 2 Guests added <Starters>, <Mains>, <Drinks W. Disc>, in the order
    And When I send request to calculate order, it returns <Total>
    When 2 more guests joined, ordered <More Mains> and <Drinks W.o Disc>
    Then I send request for Total price calculation, and for all guests it should return Total <Updated Total>
    Examples:
    | Starters | Mains | More Mains  | Drinks W.o Disc | Drinks W. Disc | Total | Updated Total |
    | 1        | 2     |   2         | 2               | 2              | 21.50 | 40.50         |

Scenario: Calculation Of Order With Cancellation
    Given 4 guests in the restaurant make order with 4 starters, 4 mains and 4 drinks ordered after 19:00
    And  I send request to calculate order, it returns total 54
    When 1 guest cancels his order, it removes 1 main, 1 starter and 1 drink from order
    Then I send request for Total price calculation, and for all guests it should return Total 40.50
  
  Scenario Outline: Calculation Of Order With Discount
    Given 4 guests in the restaurant make order with <Starters>, <Mains> and <Drinks W.o Disc> ordered after 19:00
    And When I send request to calculate order, it returns <Total>
    When 1 guest cancels his order, it removes <Cancelled Mains>, <Cancelled Starters>, <Cancelled Drinks W.o Disc.>
    Then I send request for Total price calculation, and for all guests it should return Total <Updated Total>
    Examples:
      | Starters | Mains | Drinks W.o Disc |  Cancelled Mains | Cancelled Starters | Cancelled Drinks W.o Disc. |  Total | Updated Total |
      | 4        | 4     | 4               |  1               | 1                  | 1                          |  54    | 40.50         |
  
  