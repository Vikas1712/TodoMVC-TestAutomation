Feature: TaskManagement
	As a user
	I want to be able to manage my tasks
	So that I can stay organized and productive

@Positive
Scenario: Create a new task
	Given I am on the TodoMVC application
	When I enter a new task "Buy groceries"
	Then the task "Buy groceries" should be added to the task list

@Positive	
Scenario: Complete a task
	Given I have an existing task "Clean the house"
	When I mark the task as completed
	Then the task "Clean the house" should be marked as completed

@Positive	
Scenario: Edit an existing task
	Given I have an existing task "Pay"
	When I edit the task name to "utility bills"
	Then the task "Pay" should be updated to "Payutility bills"

@Positive				
Scenario: Delete a task
	Given I have an existing task "Call a friend"
	When I delete the task "Call a friend"
	Then the task "Call a friend" should be removed from the task list
	
	