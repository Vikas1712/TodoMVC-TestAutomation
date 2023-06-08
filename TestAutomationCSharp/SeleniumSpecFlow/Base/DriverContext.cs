/*
We try to pass the WebDriver object over and over again from one class to another
by the mean of constructor or passing it as a parameter in the method where driver instance is required
Hence we created a static base class and created a static property
*/

using OpenQA.Selenium;

namespace SeleniumSpecFlow.Base;

public static class DriverContext
{
    public static IWebDriver Driver { get; set; }

    public static Browser? Browser { get; set; }
}