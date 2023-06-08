Feature: Task Management - Negative Scenarios
	As a user
	I want to be aware of potential issues or limitations
	So that I can navigate them effectively and provide feedback

@negative	
Scenario: Create a task without entering any text
	Given I am on the TodoMVC application
	When I try to create a new task without entering any text
	Then I should see No Task created and task List is Empty

@negative
Scenario: Complete a non-existent task
	Given I am on the TodoMVC application
	When I try to mark a non-existent task as completed
	Then I should see No Task created and task List is Empty

@negative
Scenario: Delete a non-existent task
	Given I am on the TodoMVC application
	When I try to delete a non-existent task
	Then I should see No Task created and task List is Empty