using OpenQA.Selenium;
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
        _scenarioContext.Set("taskName","TASKNAME");
    }

    [When(@"I mark the task as completed")]
    public void WhenIMarkTheTaskAsCompleted()
    {
        
        // Find the task checkbox and mark it as completed
        var taskName=_scenarioContext.Get<string>("TASKNAME");
        CurrentPage.As<HomePage>().MarkTaskAsComplete(taskName);
      
    }

    [Then(@"the task ""(.*)"" should be marked as completed")]
    public void ThenTheTaskShouldBeMarkedAsCompleted(string p0)
    {
        ScenarioContext.StepIsPending();
    }
}