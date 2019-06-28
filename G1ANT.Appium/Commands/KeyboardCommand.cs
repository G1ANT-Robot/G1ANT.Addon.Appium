using G1ANT.Language;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.keyboard", Tooltip = "This command clicks choden element.")]
    public class KeyboardCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Keys to be sent to element")]
            public TextStructure Keys { get; set; } = new TextStructure("");
        }

        public KeyboardCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            var driver = OpenCommand._driver;
            new Actions(driver).SendKeys(arguments.Keys.Value).Build().Perform();
        }
    }
}

