using OpenQA.Selenium;
using SeleniumSpecFlow.Base;
using SeleniumSpecFlow.Config;
using SeleniumSpecFlow.Extensions;
using SeleniumSpecFlow.Pages.Trello;

namespace SeleniumSpecFlow.Pages;

public class LoginPage : BasePage
{
    private By HeaderTextLoginToTrello => By.XPath("//h1[contains(text(),'Log in to Trello')]");
    private By HeaderTextLoginToContinue => By.XPath("//div[@data-testid='header-suffix']");
    private By InputEmailAddress => By.CssSelector("input#user");
    private By BtnContinue => By.CssSelector("input#login");
    private By InputPassword => By.CssSelector("input#password");
    private By BtnLogin => By.CssSelector("button#login-submit");

    public bool LogInPageIsDisplayed()
    {
        return DriverContext.Driver.IsDisplayed(HeaderTextLoginToTrello, Settings.DefaultWait);
    }

    public bool AtlassianPageIsDisplayed()
    {
        return DriverContext.Driver.IsDisplayed(HeaderTextLoginToContinue, Settings.DefaultWait);
    }

    public void EnterEmail(string? email)
    {
        DriverContext.Driver.FindElement(InputEmailAddress, Settings.DefaultWait).SendKeys(email);
    }

    public void ClickContinueButton()
    {
        DriverContext.Driver.FindElement(BtnContinue).Click();
    }

    public void EnterPassword(string? password)
    {
        DriverContext.Driver.FindElement(InputPassword, Settings.DefaultWait).SendKeys(password);
    }

    public BoardPage ClickLogInButton()
    {
        if (DriverContext.Driver.IsDisplayed(BtnLogin, Settings.DefaultWait)) DriverContext.Driver.FindElement(BtnLogin).Click();
        return GetInstance<BoardPage>();
    }
}