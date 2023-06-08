using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumSpecFlow.Base;
using SeleniumSpecFlow.Config;
using System.Diagnostics;

namespace SeleniumSpecFlow.Extensions;

public static class WebDriverExtension
{
    internal static void WaitForPageToLoaded(this IWebDriver driver)
    {
        driver.WaitForCondition(drv =>
        {
            var state = drv.ExecuteJs("return document.readyState").ToString()?.ToLower();
            return state == "complete";
        }, Settings.DefaultWait);
    }

    private static void WaitForCondition<T>(this T obj, Func<T, bool> condition, int timeOut)
    {
        bool Execute(T arg)
        {
            try
            {
                return condition(arg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < timeOut)
            if (Execute(obj))
                break;
    }

    private static object ExecuteJs(this IWebDriver driver, string script)
    {
        return ((IJavaScriptExecutor)driver).ExecuteScript(script);
    }

    internal static void SwitchToIFrame(this IWebDriver driver)
    {
        var iframe = driver.FindElement(By.TagName("iframe"));
        DriverContext.Driver.SwitchTo().Frame(iframe);
    }

    public static bool IsDisplayed(this IWebDriver driver, By by, int timeoutInSeconds)
    {
        try
        {
            return driver.FindElement(by, timeoutInSeconds).Displayed;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    ///     Check if element is not visible on page
    /// </summary>
    /// <param name="driver"></param>
    /// <param name="by"></param>
    /// <param name="timeoutInSeconds"></param>
    /// <returns></returns>
    public static bool IsNotDisplayed(this IWebDriver driver, By by, int timeoutInSeconds)
    {
        try
        {
            return driver.FindElement(by, timeoutInSeconds).Displayed.Equals(false);
        }
        catch (Exception)
        {
            return true;
        }
    }

    /// <summary>
    ///     Find the first element in the page that matches the speicfied <paramref name="by" /> criteria within the specified
    ///     <paramref name="timeoutInSeconds" />.
    /// </summary>
    /// <param name="driver">The <see cref="IWebDriver" /> instance to use for the search.</param>
    /// <param name="by">The <see cref="By" /> object that defines the search criteria.</param>
    /// <param name="timeoutInSeconds">The maximum number of seconds to wait for the element to be found.</param>
    /// <returns>The first <see cref="IWebElement" /> that matches the <paramref name="by" /> criteria.</returns>
    /// <exception cref="NoSuchElementException">
    ///     Thrown when no matching element is found within the specified
    ///     <paramref name="timeoutInSeconds" />.
    /// </exception>
    public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
    {
        if (timeoutInSeconds > 0)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.DefaultWait));

            wait.Until(_ =>
            {
                try
                {
                    var element = driver.FindElement(by);
                    return element != null && element.Displayed && element.Enabled;
                }
                catch (NoSuchElementException ex)
                {
                    throw new NoSuchElementException($"Unable to locate element using {by}: {ex.Message}", ex);
                }
            });
            return driver.FindElement(by);
        }

        return driver.FindElement(by);
    }

    /// <summary>
    ///     Finds all elements on the page that match the specified <paramref name="by" /> criteria within the specified
    ///     <paramref name="timeoutInSeconds" />.
    /// </summary>
    /// <param name="driver">The <see cref="IWebDriver" /> instance to use for the search.</param>
    /// <param name="by">The <see cref="By" /> object that defines the search criteria.</param>
    /// <param name="timeoutInSeconds">The maximum number of seconds to wait for the elements to be found.</param>
    /// <returns>
    ///     An <see cref="IReadOnlyCollection{T}" /> of all <see cref="IWebElement" />s that match the
    ///     <paramref name="by" /> criteria.
    /// </returns>
    /// <exception cref="NoSuchElementException">
    ///     Thrown when no matching elements are found within the specified
    ///     <paramref name="timeoutInSeconds" />.
    /// </exception>
    public static IReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, int timeoutInSeconds)
    {
        if (timeoutInSeconds > 0)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.DefaultWait));

            wait.Until(_ =>
            {
                try
                {
                    var element = driver.FindElements(by);
                    return element != null;
                }
                catch (WebDriverTimeoutException ex)
                {
                    throw new TimeoutException(
                        $"Timed out after {timeoutInSeconds} seconds waiting for elements with {by}", ex);
                }
            });
            return driver.FindElements(by);
        }

        return driver.FindElements(by);
    }

    /// <summary>
    ///     Clicks on the specified <see cref="IWebElement" /> using JavaScript.
    /// </summary>
    /// <param name="driver">The <see cref="IWebDriver" /> instance to use for clicking the element.</param>
    /// <param name="element">The <see cref="IWebElement" /> to click on.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="driver" /> or <paramref name="element" /> is null.</exception>
    public static void ClickElementWithJs(this IWebDriver driver, IWebElement element)
    {
        var jsExecutor = (IJavaScriptExecutor)driver;

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.DefaultWait));
        wait.Until(_ => jsExecutor.ExecuteScript("return document.readyState").Equals("complete"));

        jsExecutor.ExecuteScript("arguments[0].click();", element);
    }
}