using SeleniumSpecFlow.Base;
using SeleniumSpecFlow.Pages.Trello;

namespace SeleniumSpecFlow.Steps;

[Binding]
public class CardSteps : BasePage
{
    [Given(@"Register User is on the Card page")]
    public void GivenRegisterUserIsOnTheCardPage()
    {
        CurrentPage = GetInstance<BoardPage>();
        Assert.True(CurrentPage.As<BoardPage>().ConfirmBoardPageIsDisplayed(), "Board Page is Displayed");
        CurrentPage.As<BoardPage>().CreateNewBoard("TestingDemo");
        CurrentPage = CurrentPage.As<BoardPage>().ClickCreateBoardSumbit();
        Assert.True(CurrentPage.As<CardPage>().ConfirmToDoCardPresent(), "User is on Card Page");
    }

    [Then(@"That new to do card is added successfully on the board")]
    public void ThenThatNewToDoCardIsAddedSuccessfullyOnTheBoard()
    {
        Assert.GreaterOrEqual(CurrentPage.As<CardPage>().ValidateToDoCardCount().Count, 1,
            "Expected at least 1 matching cards");
    }

    [When(@"User create a new card with title ""(.*)"" in ToDo Lane")]
    public void WhenUserCreateANewCardWithTitleInToDoLane(string title)
    {
        CurrentPage.As<CardPage>().AddToDoCard(title);
    }

    [When(@"User create a new card with title ""(.*)"" in Doing Lane")]
    public void WhenUserCreateANewCardWithTitleInDoingLane(string title)
    {
        CurrentPage.As<CardPage>().AddDoingCard(title);
    }

    [When(@"User create a new card with title ""(.*)"" in Done Lane")]
    public void WhenUserCreateANewCardWithTitleInDoneLane(string title)
    {
        CurrentPage.As<CardPage>().AddDoneCard(title);
    }

    [Given(@"User can view the Card on the board")]
    public void GivenUserCanViewTheCardOnTheBoard()
    {
        GivenRegisterUserIsOnTheCardPage();
        WhenUserCreateANewCardWithTitleInToDoLane("CreateCardIsMissing");
        CurrentPage = GetInstance<CardPage>();
        CurrentPage.As<CardPage>().ConfirmCardThereOnTheBoard();
    }

    [When(@"User deletes all the cards")]
    public void WhenUserDeletesAllTheCards()
    {
        CurrentPage.As<CardPage>().DeleteCard();
    }

    [When(@"User delete the active board too")]
    [Then(@"User delete the active board too")]
    public void WhenUserDeleteTheActiveBoardToo()
    {
        CurrentPage = GetInstance<CardPage>();
        CurrentPage = CurrentPage.As<CardPage>().DeleteActiveBoard();
    }

    [Then(@"That cards are no longer visible on board")]
    public void ThenThatCardsAreNoLongerVisibleOnBoard()
    {
        Assert.True(CurrentPage.As<BoardPage>().ConfirmBoardPageIsDisplayed(), "Board Page is Displayed");
    }
}