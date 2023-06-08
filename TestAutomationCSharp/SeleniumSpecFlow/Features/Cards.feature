Feature: Cards_Feature
As a user I want to perform CRUD operation card in Trello Board

Background:
    Given The user is on a Trello board

@Card
Scenario: User can add new card in ToDo Lane
     Given Register User is on the Card page
     When User create a new card with title "Adding title into To Do Card Lane" in ToDo Lane
     Then That new to do card is added successfully on the board
     And User delete the active board too

@Card
Scenario: User can add new card in Doing Lane
    Given Register User is on the Card page
    When User create a new card with title "Adding title into Doing Card Lane" in Doing Lane
    Then That new to do card is added successfully on the board
    And User delete the active board too

@Card
Scenario: User can add new card in Done Lane
    Given Register User is on the Card page
    When User create a new card with title "Adding title into Done Card Lane" in Done Lane
    Then That new to do card is added successfully on the board
    And User delete the active board too

@Card
Scenario: User can delete new card in the board
    Given User can view the Card on the board
    When User deletes all the cards
    And User delete the active board too
    Then That cards are no longer visible on board