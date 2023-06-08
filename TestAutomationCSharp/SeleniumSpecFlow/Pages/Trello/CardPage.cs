using OpenQA.Selenium;
using SeleniumSpecFlow.Base;
using SeleniumSpecFlow.Config;
using SeleniumSpecFlow.Extensions;

namespace SeleniumSpecFlow.Pages.Trello;

public class CardPage : BasePage
{
    private readonly By _btnAddDoingCard =
        By.XPath("(//span[@class='js-add-a-card'][normalize-space()='Add a card'])[2]");

    private readonly By _btnAddDoneCard =
        By.XPath("(//span[@class='js-add-a-card'][normalize-space()='Add a card'])[3]");

    private readonly By _btnAddToDoCard =
        By.XPath("(//span[@class='js-add-a-card'][normalize-space()='Add a card'])[1]");

    private readonly By _btnConfirmCloseBoard = By.XPath("//input[@value='Close']");
    private readonly By _btnConfirmPermanentlyBoardDelete = By.XPath("//button[normalize-space()='Delete']");

    private readonly By _linkPermanentlyBoardDelete =
        By.XPath("//button[normalize-space()='Permanently delete board']");

    private readonly By _showMenuActiveBoard = By.XPath("//button[@aria-label='Show menu']//span[@class='css-snhnyn']");
    private By HeaderTitleCardDisplay => By.ClassName("HKTtBLwDyErB_o");
    private By TitleToDoCard => By.CssSelector("textarea[aria-label='To Do']");
    private By TitleDoingCard => By.CssSelector("textarea[aria-label='Doing']");
    private By TitleDone => By.CssSelector("textarea[aria-label='Done']");
    private By TextAreaTitleCard => By.CssSelector("textarea[placeholder='Enter a title for this card…']");
    private By BtnAddCard => By.CssSelector("input[value='Add card']");
    private By TextAreaListCard => By.CssSelector("input[placeholder='Enter list title…']");
    private By BtnAddList => By.CssSelector("input[value='Add list']");
    private By CardTitleToDo => By.XPath("//div[@class='list js-list-content']/div");
    private By ActionMenuToDoCard => By.XPath("//div[@class='list-header-extras']");
    private By BtnArchive => By.XPath("//a[contains(text(),'Archive all cards in this list…')]");
    private By BtnArchiveAll => By.CssSelector("input[value='Archive all']");
    private By ActionMore => By.XPath("//a[@class='board-menu-navigation-item-link js-open-more']");
    private By ActionCloseBoard => By.XPath("//a[@class='board-menu-navigation-item-link js-close-board']");

    public bool ConfirmToDoCardPresent()
    {
        return DriverContext.Driver.FindElement(HeaderTitleCardDisplay, Settings.DefaultWait).IsDisplayed();
    }

    public bool ConfirmCardThereOnTheBoard()
    {
        //ToDoCardCreate("CreateCardIsMissing");
        return DriverContext.Driver.FindElement(ActionMenuToDoCard).IsDisplayed();
    }

    public void DeleteCard()
    {
        //DriverContext.Driver.IsDisplayed(ActionMenuToDoCard);
        DriverContext.Driver.FindElement(ActionMenuToDoCard, Settings.DefaultWait).Click();
        DriverContext.Driver.FindElement(BtnArchive, Settings.DefaultWait).Click();
        DriverContext.Driver.FindElement(BtnArchiveAll, Settings.DefaultWait).Click();
    }

    public BoardPage DeleteActiveBoard()
    {
        DriverContext.Driver.FindElement(_showMenuActiveBoard, Settings.DefaultWait).Click();
        DriverContext.Driver.FindElement(ActionMore, Settings.DefaultWait).Click();
        DriverContext.Driver.FindElement(ActionCloseBoard, Settings.DefaultWait).Click();
        DriverContext.Driver.WaitForPageToLoaded();
        DriverContext.Driver.IsDisplayed(_btnConfirmCloseBoard, Settings.DefaultWait);
        DriverContext.Driver.FindElement(_btnConfirmCloseBoard, Settings.DefaultWait).Click();
        DriverContext.Driver.FindElement(_linkPermanentlyBoardDelete, Settings.DefaultWait).Click();
        DriverContext.Driver.FindElement(_btnConfirmPermanentlyBoardDelete, Settings.DefaultWait).Click();
        return GetInstance<BoardPage>();
    }

    private void ToDoCardCreate(string title)
    {
        if (DriverContext.Driver.IsDisplayed(TitleToDoCard, Settings.DefaultWait))
        {
            DriverContext.Driver.IsDisplayed(_btnAddToDoCard, Settings.DefaultWait);
            DriverContext.Driver.FindElement(BtnAddCard, Settings.DefaultWait).Click();
            EnterCardTitle(title);
        }
        else
        {
            EnterCardList(title);
        }
    }

    private void ToDoingCreate(string title)
    {
        if (DriverContext.Driver.IsDisplayed(TitleDoingCard, Settings.DefaultWait))
        {
            DriverContext.Driver.IsDisplayed(_btnAddDoingCard, Settings.DefaultWait);
            DriverContext.Driver.FindElement(_btnAddDoingCard, Settings.DefaultWait).Click();
            EnterCardTitle(title);
        }
        else
        {
            EnterCardList(title);
        }
    }

    private void ToDoneCreate(string title)
    {
        if (DriverContext.Driver.IsDisplayed(TitleDone, Settings.DefaultWait))
        {
            DriverContext.Driver.IsDisplayed(_btnAddDoneCard, Settings.DefaultWait);
            DriverContext.Driver.FindElement(_btnAddDoneCard, Settings.DefaultWait).Click();
            EnterCardTitle(title);
        }
        else
        {
            EnterCardList(title);
        }
    }

    private void EnterCardList(string title)
    {
        //DriverContext.Driver.IsDisplayed(TextAreaListCard);
        DriverContext.Driver.FindElement(TextAreaListCard, Settings.DefaultWait).SendKeys(title);
        DriverContext.Driver.FindElement(BtnAddList, Settings.DefaultWait).Click();
    }

    private void EnterCardTitle(string title)
    {
        DriverContext.Driver.FindElement(TextAreaTitleCard, Settings.DefaultWait).SendKeys(title);
        DriverContext.Driver.FindElement(BtnAddCard, Settings.DefaultWait).Click();
    }

    public void AddToDoCard(string title)
    {
        ToDoCardCreate(title);
    }

    public void AddDoingCard(string title)
    {
        ToDoCardCreate("Created Missing To Do Card");
        ToDoingCreate(title);
    }

    public void AddDoneCard(string title)
    {
        ToDoCardCreate("Created Missing To Do Card");
        ToDoingCreate("Created Missing Doing Card");
        ToDoneCreate(title);
    }

    public IReadOnlyCollection<IWebElement> ValidateToDoCardCount()
    {
        return DriverContext.Driver.FindElements(CardTitleToDo, Settings.DefaultWait);
    }
}