Feature: TaskManagement
	As a user
	I want to be able to manage my tasks
	So that I can stay organized and productive

@mytag
Scenario: Create a new task
	Given I am on the TodoMVC application
	When I enter a new task "Buy groceries"
	Then the task "Buy groceries" should be added to the task list
	
Scenario: Complete a task
	Given I have an existing task "Clean the house"
	When I mark the task as completed
	Then the task "Clean the house" should be marked as completed
		
Scenario: Edit an existing task
	Given I have an existing task "Pay bills"
	And I am on the TodoMVC application
	When I click on the task "Pay bills"
	And I edit the task name to "Pay utility bills"
	Then the task "Pay bills" should be updated to "Pay utility bills"
		
Scenario: Delete a task
	Given I have an existing task "Call a friend"
	And I am on the TodoMVC application
	When I delete the task "Call a friend"
	Then the task "Call a friend" should be removed from the task list
	
	