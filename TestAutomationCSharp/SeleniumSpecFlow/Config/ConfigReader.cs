/*
ConfigReader file read the custom XML file
We are using System.XML namespace and will be using
XPathItem and XPathDocument classes to perform operations.
*/

using SeleniumSpecFlow.Base;
using System.Xml.XPath;

namespace SeleniumSpecFlow.Config;

public static class ConfigReader
{

    public static void SetFrameworkSettings()
    {
        XPathItem? URL = null;
        XPathItem? browser = null, defaultWait = null, executionType = null;

        var strFilename = Environment.CurrentDirectory + "/Config/GlobalConfig.xml";
        FileStream stream = new(strFilename, FileMode.Open);
        XPathDocument document = new(stream);
        var navigator = document.CreateNavigator();

        //Get XML Details and pass it in XPathItem type variables
        if (navigator != null)
        {
            URL = navigator.SelectSingleNode("SeleniumCSharpFramework/RunSettings/URL");
            browser = navigator.SelectSingleNode("SeleniumCSharpFramework/RunSettings/Browser");
            defaultWait = navigator.SelectSingleNode("SeleniumCSharpFramework/RunSettings/DefaultWait");
            executionType = navigator.SelectSingleNode("SeleniumCSharpFramework/RunSettings/ExecutionType");
        }

        //Set XML Details in the property to be used accross framework
        if (URL != null) Settings.URL = URL.Value;
        if (browser != null)
            Settings.BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), browser.Value);
        if (defaultWait != null) Settings.DefaultWait = int.Parse(defaultWait.Value);
        if (executionType != null) Settings.ExecutionType = executionType.Value;
    }

    /*
    public static TestSetting? ReadConfig()
    {
        
       // var configFile1 = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"/appsettings.json");
        
        var configFile = File.ReadAllText(Environment.CurrentDirectory + "//Config//appsetting.json");
        //string directoryPath = Directory.GetCurrentDirectory();
        //string configFolderPath = Path.Combine(directoryPath, "Config");
        //string configFilePath = Path.Combine(configFolderPath, "appsetting.json");

        //var configFile = File.ReadAllText(configFilePath);
        var jsonSerializeOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        jsonSerializeOptions.Converters.Add(new JsonStringEnumConverter());

        var testSettings = JsonSerializer.Deserialize<TestSetting>(configFile, jsonSerializeOptions);

        return testSettings;
    }
    */
}