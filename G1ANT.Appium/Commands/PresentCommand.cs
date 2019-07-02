using G1ANT.Language;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.present", Tooltip = "This command waits for the element to be present on the screen.")]
    public class PresentCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Provide element ID.")]
            public TextStructure Id { get; set; } = new TextStructure("");

            [Argument(Tooltip = "The result is true when the element is present and false if it is not.")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public PresentCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            var driver = OpenCommand._driver;
            bool isPresent = false;
            if (driver.FindElements(By.Id(arguments.Id.Value)).Count > 0)
            {
                isPresent = true;
            }

            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(isPresent));
        }
    }
}

