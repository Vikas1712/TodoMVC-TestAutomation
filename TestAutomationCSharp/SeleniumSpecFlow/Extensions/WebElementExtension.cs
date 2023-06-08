using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumSpecFlow.Config;

namespace SeleniumSpecFlow.Extensions;

public static class WebElementExtension
{
    public static void SelectDdl(this IWebElement element, string value)
    {
        SelectElement ddl = new(element);
        ddl.SelectByText(value);
    }

    public static string GetText(IWebElement element)
    {
        return element.Text;
    }

    public static string? GetSelectedDropDown(IWebElement element)
    {
        SelectElement ddl = new(element);
        return ddl.AllSelectedOptions.First().ToString();
    }

    public static IList<IWebElement> GetSelectedListOptions(IWebElement element)
    {
        SelectElement ddl = new(element);
        return ddl.AllSelectedOptions;
    }

    /// <summary>
    ///     Check if the element exist
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private static bool IsElementPresent(IWebElement element)
    {
        try
        {
            return element.IsDisplayed();
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    ///     Return True if element is visible on page
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static bool IsDisplayed(this IWebElement element)
    {
        try
        {
            return element.Displayed;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    ///     Assert if the Element is present
    /// </summary>
    /// <param name="element"></param>
    public static void AssertElementPresent(this IWebElement element)
    {
        if (!IsElementPresent(element))
            throw new AssertionException("AssertElementNotPresent exception");
    }

    public static void ClickWithWait(this IWebElement element)
    {
        try
        {
            var wait = new WebDriverWait(element.GetDriver(), TimeSpan.FromSeconds(Settings.DefaultWait));
            wait.Until(driver => element.Displayed && element.Enabled);
            element.Click();
        }
        catch (Exception ex)
        {
            throw new NoSuchElementException(
                $"Failed to click element after {Settings.DefaultWait} seconds: {ex.Message}");
        }
    }

    public static void SendKeys(this IWebElement element, string text, int timeoutInSeconds)
    {
        try
        {
            var wait = new WebDriverWait(element.GetDriver(), TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(driver => element.Displayed && element.Enabled);
            element.SendKeys(text);
        }
        catch (Exception ex)
        {
            throw new NoSuchElementException(
                $"Failed to send keys '{text}' to element after {timeoutInSeconds} seconds: {ex.Message}");
        }
    }

    public static string GetText(this IWebElement element, int timeoutInSeconds)
    {
        try
        {
            var wait = new WebDriverWait(element.GetDriver(), TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(driver => element.Displayed && element.Enabled);
            return element.Text;
        }
        catch (Exception ex)
        {
            throw new NoSuchElementException(
                $"Failed to get text from element after {timeoutInSeconds} seconds: {ex.Message}");
        }
    }

    public static string GetAttribute(this IWebElement element, string attributeName, int timeoutInSeconds)
    {
        try
        {
            var wait = new WebDriverWait(element.GetDriver(), TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(driver => element.Displayed && element.Enabled);
            return element.GetAttribute(attributeName);
        }
        catch (Exception ex)
        {
            throw new NoSuchElementException(
                $"Failed to get attribute '{attributeName}' from element after {timeoutInSeconds} seconds: {ex.Message}");
        }
    }

    private static IWebDriver GetDriver(this IWebElement element)
    {
        return ((IWrapsDriver)element).WrappedDriver;
    }
}