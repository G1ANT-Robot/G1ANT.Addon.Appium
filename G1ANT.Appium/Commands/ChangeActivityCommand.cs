using G1ANT.Language;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.startactivity", Tooltip = "This command allows to change activity.")]
    public class ChangeActivityCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Provide app package.")]
            public TextStructure AppPackage { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Provide app activity.")]
            public TextStructure AppActivity { get; set; } = new TextStructure("");
        }

        public ChangeActivityCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            AndroidDriver<AndroidElement> driver = OpenCommand._driver;
            driver.StartActivity(arguments.AppPackage.Value,arguments.AppActivity.Value);
        }
    }
}

