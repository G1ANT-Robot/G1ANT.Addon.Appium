using G1ANT.Language;
using OpenQA.Selenium.Remote;
using System;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.present", Tooltip = "This command waits for the element to be present on the screen.")]
    public class PresentCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Provide name of the capaility")]
            public TextStructure Search { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Provide element ID")]
            public TextStructure By { get; set; } = new TextStructure("");

            [Argument(Tooltip = "The result is true when the element is present and false if it is not.")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public PresentCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            bool isPresent = false;

            if (ElementsHelper.GetElements((SearchBy)Enum.Parse(typeof(SearchBy), arguments.By.Value), arguments.Name.Value).Count > 0)
            {
                isPresent = true;
            }

            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(isPresent));
        }
    }
}

