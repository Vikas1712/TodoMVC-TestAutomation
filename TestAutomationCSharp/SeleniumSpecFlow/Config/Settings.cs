using SeleniumSpecFlow.Base;

namespace SeleniumSpecFlow.Config;

public abstract class Settings
{
    public static string? URL { get; set; }
    public static BrowserType BrowserType { get; set; }
    public static int DefaultWait { get; set; }
    public static string? ExecutionType { get; set; }
}
