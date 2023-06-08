using OpenQA.Selenium;
using SeleniumSpecFlow.Base;
using SeleniumSpecFlow.Config;
using SeleniumSpecFlow.Extensions;

namespace SeleniumSpecFlow.Pages.Trello;

public class BoardPage : BasePage
{
    private By BtnCreate => By.ClassName("szBTSFrvPTLGHM");
    private By BtnCreateBoard => By.CssSelector("button[data-testid='header-create-board-button']");
    private By InputBoardTitle => By.CssSelector("input[type='text']");
    private By BtnCreateBoardSubmit => By.CssSelector("button[data-testid='create-board-submit-button']");

    public bool ConfirmBoardPageIsDisplayed()
    {
        return DriverContext.Driver.IsDisplayed(BtnCreate, Settings.DefaultWait);
    }

    public void CreateNewBoard(string title)
    {
        DriverContext.Driver.FindElement(BtnCreate, Settings.DefaultWait).Click();
        DriverContext.Driver.FindElement(BtnCreateBoard, Settings.DefaultWait).Click();
        DriverContext.Driver.FindElement(InputBoardTitle, Settings.DefaultWait).Clear();
        DriverContext.Driver.FindElement(InputBoardTitle, Settings.DefaultWait).SendKeys(title);
    }

    public CardPage ClickCreateBoardSumbit()
    {
        var element = DriverContext.Driver.FindElement(BtnCreateBoardSubmit, Settings.DefaultWait);
        DriverContext.Driver.ClickElementWithJs(element);
        return GetInstance<CardPage>();
    }
}