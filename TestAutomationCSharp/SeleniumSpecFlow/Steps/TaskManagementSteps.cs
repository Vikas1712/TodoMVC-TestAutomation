using SeleniumSpecFlow.Base;
using SeleniumSpecFlow.Pages;

namespace SeleniumSpecFlow.Steps;

[Binding]
public class TaskManagementSteps : BasePage
{
    private readonly ScenarioContext _scenarioContext;
    
    public TaskManagementSteps(ScenarioContext scenarioContext)
    {
        this._scenarioContext = scenarioContext;
    }
    
    [Given(@"I am on the TodoMVC application")]
    public void GivenIAmOnTheTodoMvcApplication()
    {
        CurrentPage = GetInstance<HomePage>();
        CurrentPage.As<HomePage>().OpenToDoSite();
    }

    [When(@"I enter a new task ""(.*)""")]
    public void WhenIEnterANewTask(string taskName)
    {
        CurrentPage.As<HomePage>().AddTaskAndEnter(taskName);
    }
    

    [Then(@"the task ""(.*)"" should be added to the task list")]
    public void ThenTheTaskShouldBeAddedToTheTaskList(string taskName)
    {
        Assert.True(CurrentPage.As<HomePage>().VerifyTaskIsVisible(taskName),$"The task '{taskName}' is not found in the task list.");
    }

    [Given(@"I have an existing task ""(.*)""")]
    public void GivenIHaveAnExistingTask(string taskName)
    {
        GivenIAmOnTheTodoMvcApplication();
        WhenIEnterANewTask(taskName);
        _scenarioContext.Set(taskName,"TASKNAME");
    }

    [When(@"I mark the task as completed")]
    public void WhenIMarkTheTaskAsCompleted()
    {
        
        // Find the task checkbox and mark it as completed
        var strTaskName=_scenarioContext.Get<string>("TASKNAME");
        CurrentPage.As<HomePage>().MarkTaskAsComplete(strTaskName);
      
    }

    [Then(@"the task ""(.*)"" should be marked as completed")]
    public void ThenTheTaskShouldBeMarkedAsCompleted(string taskName)
    {
        CurrentPage.As<HomePage>().VerifyTaskIsCompleted(taskName);
    }

    [When(@"I delete the task ""(.*)""")]
    public void WhenIDeleteTheTask(string taskName)
    {
        CurrentPage.As<HomePage>().DeleteTask(taskName);
    }

    [Then(@"the task ""(.*)"" should be removed from the task list")]
    public void ThenTheTaskShouldBeRemovedFromTheTaskList(string taskName)
    {
        Assert.False(CurrentPage.As<HomePage>().VerifyTaskIsVisible(taskName),$"The task '{taskName}' is found in the task list.");
    }

    [When(@"I edit the task name to ""(.*)""")]
    public void WhenIEditTheTaskNameTo(string taskName)
    {
        var oldTaskName=_scenarioContext.Get<string>("TASKNAME");
        CurrentPage.As<HomePage>().EditTask(taskName,oldTaskName);
    }

    [Then(@"the task ""(.*)"" should be updated to ""(.*)""")]
    public void ThenTheTaskShouldBeUpdatedTo(string oldTaskName, string newTaskName)
    {
        Assert.True(CurrentPage.As<HomePage>().VerifyTaskIsVisible(newTaskName),$"The task '{oldTaskName}' is found in the task list.");
    }

    [When(@"I try to create a new task without entering any text")]
    public void WhenITryToCreateANewTaskWithoutEnteringAnyText()
    {
        CurrentPage.As<HomePage>().AddTaskWithoutEnteringAnyText();
    }

    [Then(@"I should see No Task created and task List is Empty")]
    public void ThenIShouldSeeNoTaskCreatedAndTaskListIsEmpty()
    {
        Assert.AreEqual(0,CurrentPage.As<HomePage>().VerifyTaskListIsEmpty());
    }

    [When(@"I try to mark a non-existent task as completed")]
    public void WhenITryToMarkANonExistentTaskAsCompleted()
    {
        CurrentPage.As<HomePage>().MarkANonExistentTaskAsCompleted();
    }

    [When(@"I try to edit the task ""(.*)"" with an empty name")]
    public void WhenITryToEditTheTaskWithAnEmptyName(string workout)
    {
        CurrentPage.As<HomePage>().EditTask(workout,"");
    }

    [When(@"I try to delete a non-existent task")]
    public void WhenITryToDeleteANonExistentTask()
    {
        CurrentPage.As<HomePage>().MarkANonExistentTaskAsCompleted();
    }
}