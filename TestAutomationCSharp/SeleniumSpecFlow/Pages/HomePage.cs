using OpenQA.Selenium;
using SeleniumSpecFlow.Base;
using SeleniumSpecFlow.Config;
using SeleniumSpecFlow.Extensions;

namespace SeleniumSpecFlow.Pages;

public class HomePage : BasePage
{
    private By LinkLogin => By.CssSelector("a[data-uuid='MJFtCCgVhXrVl7v9HA7EH_login']");
    private By BtnAcceptCookies => By.XPath("//button[normalize-space()='Accept Cookies']");
    private By HeaderToDo => By.CssSelector("header[class='header'] h1");
    private By TaskInput => By.CssSelector("input[placeholder='What needs to be done?']");
    private By TaskList => By.CssSelector("div[class='view'] label");
    public LoginPage OpenTrelloSite()
    {
        DriverContext.Driver.Navigate().GoToUrl(Settings.URL);
        DriverContext.Driver.Manage().Window.Maximize();
        AcceptCookiesIfPresent();
        DriverContext.Driver.FindElement(LinkLogin, Settings.DefaultWait).Click();
        Console.WriteLine("Click on Login Link");
        return GetInstance<LoginPage>();
    }
    
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

    public void MarkTaskAsComplete(string taskName)
    {
        // Find the task checkbox and mark it as completed
        var taskCheckbox = DriverContext.Driver.FindElement(By.XPath($"//label[text()='{taskName}']/preceding-sibling::input"));
        taskCheckbox.Click();
    }
    private void AcceptCookiesIfPresent()
    {
        if (DriverContext.Driver.IsDisplayed(BtnAcceptCookies, Settings.DefaultWait))
        {
            Console.WriteLine("Click on Accept Cookies");
            DriverContext.Driver.FindElement(BtnAcceptCookies).Click();
        }
    }
}