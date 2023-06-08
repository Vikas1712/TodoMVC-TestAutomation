using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumSpecFlow.Base;
using SeleniumSpecFlow.Config;
using SeleniumSpecFlow.Extensions;

namespace SeleniumSpecFlow.Pages;

public class HomePage : BasePage
{
    private By HeaderToDo => By.CssSelector("header[class='header'] h1");
    private By TaskInput => By.CssSelector("input[placeholder='What needs to be done?']");
    private By TaskList => By.CssSelector("div[class='view'] label");

    public void OpenToDoSite()
    {
        DriverContext.Driver.Navigate().GoToUrl(Settings.URL);
        DriverContext.Driver.Manage().Window.Maximize();
        DriverContext.Driver.IsDisplayed(HeaderToDo, Settings.DefaultWait);
    }

    public void AddTaskAndEnter(string taskName)
    {
        DriverContext.Driver.FindElement(TaskInput, Settings.DefaultWait).SendKeys(taskName);
        DriverContext.Driver.FindElement(TaskInput).SendKeys(Keys.Enter);
    }

    public void AddTaskWithoutEnteringAnyText()
    {
        DriverContext.Driver.FindElement(TaskInput, Settings.DefaultWait);
        // Submit the task without entering any text
        DriverContext.Driver.FindElement(TaskInput).SendKeys(Keys.Enter);
    }
    public bool VerifyTaskIsVisible(string taskName)
    {
        var taskList = DriverContext.Driver.FindElements(TaskList);
        bool taskFound = false;
        foreach (var task in taskList)
        {
            if (task.Text == taskName)
            {
                taskFound = true;
                break;
            }
            
        }

        return taskFound;
    }

    public int VerifyTaskListIsEmpty()
    {
        var taskList = DriverContext.Driver.FindElements(TaskList);
        return taskList.Count;
    }
    public void MarkTaskAsComplete(string strTaskName)
    {
        // Find the task checkbox and mark it as completed
        var taskCheckbox = DriverContext.Driver.FindElement(By.XPath($"//label[text()='{strTaskName}']/preceding-sibling::input"));
        taskCheckbox.Click();
    }

    public void MarkANonExistentTaskAsCompleted()
    {
        string nonExistentTaskName = "Non-Existent Task";
        var taskCheckbox = DriverContext.Driver.FindElements(By.XPath($"//label[text()='{nonExistentTaskName}']/preceding-sibling::input"));
    }
    public void VerifyTaskIsCompleted(string strTaskName)
    {
        var completedTask = DriverContext.Driver.FindElement(By.XPath($"//label[text()='{strTaskName}']"));
        var parentListItem = completedTask.FindElement(By.XPath("./../.."));

        Assert.True(parentListItem.GetAttribute("class").Contains("completed"), $"The task '{strTaskName}' is not marked as completed.");
    }
    
    public void DeleteTask(string strTaskName)
    {
        IWebElement deleteButton = DriverContext.Driver.FindElement(By.XPath($"//label[text()='{strTaskName}']/parent::div//following-sibling::button[@class='destroy']"));
        DriverContext.Driver.ClickElementWithJs(deleteButton);
    }
    
    public void EditTask(string strTaskName,string oldTaskName)
    {
        IWebElement currentTask = DriverContext.Driver.FindElement(By.XPath($"//label[text()='{oldTaskName}']"));
        // Create an instance of the Actions class
        Actions actions = new Actions(DriverContext.Driver);

        // Perform the double-click action on the element
        actions.DoubleClick(currentTask).Perform();
        IWebElement editButton = DriverContext.Driver.FindElement(By.XPath($"//label[text()='{oldTaskName}']/parent::div/following-sibling::input[@class='edit']"));
        DriverContext.Driver.ClickElementWithJs(editButton);
        //editButton.Clear();
        editButton.SendKeys(strTaskName);
        editButton.SendKeys(Keys.Enter);
    }
}