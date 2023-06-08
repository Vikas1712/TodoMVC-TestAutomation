using SeleniumSpecFlow.Base;
using SeleniumSpecFlow.Pages;
using SeleniumSpecFlow.Pages.Trello;

namespace SeleniumSpecFlow.Steps;

[Binding]
public class BoardSteps : BasePage
{
    [Given(@"The user is on a Trello board")]
    public void GivenTheUserIsOnATrelloBoard()
    {
        CurrentPage = GetInstance<HomePage>();
        CurrentPage = CurrentPage.As<HomePage>().OpenTrelloSite();
        Assert.True(CurrentPage.As<LoginPage>().LogInPageIsDisplayed(), "Login Page is Not present");
        CurrentPage.As<LoginPage>().ClickContinueButton();
        CurrentPage.As<LoginPage>().AtlassianPageIsDisplayed();
        CurrentPage = CurrentPage.As<LoginPage>().ClickLogInButton();
    }

    [When(@"User create a new board with name ""(.*)"" in the page")]
    public void WhenUserCreateANewBoardWithNameInThePage(string title)
    {
        CurrentPage = GetInstance<BoardPage>();
        Assert.True(CurrentPage.As<BoardPage>().ConfirmBoardPageIsDisplayed(), "BoardPage is Not Displayed");
        CurrentPage.As<BoardPage>().CreateNewBoard(title);
        CurrentPage = CurrentPage.As<BoardPage>().ClickCreateBoardSumbit();
    }

    [Then(@"The new Board is successfully created")]
    public void ThenTheNewBoardIsSuccessfullyCreated()
    {
        Assert.True(CurrentPage.As<CardPage>().ConfirmToDoCardPresent(), "Cart page is Not Present");
    }
}