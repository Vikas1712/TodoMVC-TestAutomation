Feature: Board
As a user I want to create new board in Trello

Background:
   Given The user is on a Trello board

@Board
Scenario: User can create a new board
   When User create a new board with name "Testing1" in the page
   Then The new Board is successfully created
   And User delete the active board too