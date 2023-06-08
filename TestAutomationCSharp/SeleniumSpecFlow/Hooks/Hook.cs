using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using SeleniumSpecFlow.Base;
using SeleniumSpecFlow.Config;
using SeleniumSpecFlow.Pages;

namespace eleniumSpecFlow.Hooks;

[Binding]
public class Hooks
{
    private static ExtentTest? _featureName;
    private static ExtentReports? _extent;
    private static readonly string ReportPath =
        $"{Directory.GetParent(@"../../../")?.FullName}\\Result\\Result_{DateTime.Now:ddMMyyyy HHmmss}";

    private readonly ScenarioContext _scenarioContext;
    private ExtentTest? _currentScenarioName;

    public Hooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [AfterScenario]
    public void AfterScenario()
    {
        var type = _scenarioContext.ScenarioExecutionStatus.ToString();
        if (type == "UndefinedStep") _currentScenarioName?.Skip(_scenarioContext.ScenarioExecutionStatus.ToString());
        DriverContext.Driver.Quit();
        if (Settings.BrowserType.ToString() == "Chrome") CommonStep.KillProcessLocally("chromedriver");
    }

    [BeforeTestRun]
    public static void InitializeReport()
    {
        _extent = new ExtentReports();
        var reporter = new ExtentSparkReporter($"{ReportPath}.html")
        {
            Config =
            {
                DocumentTitle = "Automation Testing Report",
                ReportName = "Regression Testing",
                Theme = Theme.Standard
            }
        };
        _extent.AttachReporter(reporter);
    }

    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        _featureName =
            _extent?.CreateTest<Feature>(featureContext.FeatureInfo.Title);
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        ConfigReader.SetFrameworkSettings();
        OpenBrowser(Settings.BrowserType);
        _currentScenarioName = _featureName?.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
    }

    [AfterStep]
    public void InsertReportingStep()
    {
        var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

        if (_scenarioContext.TestError == null)
        {
            switch (stepType)
            {
                case "Given":
                    _currentScenarioName?.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                    break;

                case "When":
                    _currentScenarioName?.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                    break;

                case "Then":
                    _currentScenarioName?.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                    break;

                case "And":
                    _currentScenarioName?.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
                    break;
            }
        }
        else if (_scenarioContext.TestError != null)
        {
            var screenshot = ((ITakesScreenshot)DriverContext.Driver).GetScreenshot().AsBase64EncodedString;
            var mediaEntity = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, "screenshot").Build();
            switch (stepType)
            {
                case "Given":
                    _currentScenarioName?.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text)
                        .Fail(mediaEntity);
                    break;

                case "When":
                    _currentScenarioName?.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(mediaEntity);
                    break;

                case "Then":
                    _currentScenarioName?.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(mediaEntity);
                    break;
            }
        }
    }

    [AfterTestRun]
    public static void TearDownReport()
    {
        //Flush report once test completes
        _extent?.Flush();
    }

    private static void OpenBrowser(BrowserType browserType)
    {
        switch (browserType)
        {
            case BrowserType.Edge:
                DriverContext.Driver = new EdgeDriver();
                break;

            case BrowserType.Firefox:
                DriverContext.Driver = new FirefoxDriver();
                break;

            case BrowserType.Chrome:

                ChromeOptions options = new();
                options.AddArguments("start-maximized");
                options.AddArguments("--disable-gpu");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--no-sandbox");
                //options.AddArgument("--headless");
                options.AddArgument("--ignore-certificate-errors");
                Console.WriteLine("Setup");
                if (Settings.ExecutionType == "Local")
                {
                    DriverContext.Driver = new ChromeDriver();
                }
                else
                {
                    // Specify the Docker container URL
                    Uri dockerUri = new Uri("http://localhost:4444/wd/hub");
                    DriverContext.Driver = new RemoteWebDriver(dockerUri, options.ToCapabilities(), TimeSpan.FromSeconds(60));
                }
                break;

            case BrowserType.Safari:
                DriverContext.Driver = new SafariDriver();
                break;
        }
    }
}