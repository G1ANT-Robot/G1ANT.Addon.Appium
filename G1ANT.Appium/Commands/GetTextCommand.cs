using System;
using G1ANT.Language;
using OpenQA.Selenium.Remote;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.gettext", Tooltip = "This command extracts text from selected element.")]
    public class GetTextCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Specify by wich capability the element will be searched")]
            public TextStructure By { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Provide name of the capaility")]
            public TextStructure Name { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Extracted text will be stored here")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public GetTextCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            var el = ElementHelper.GetElement((SearchBy)Enum.Parse(typeof(SearchBy), arguments.By.Value), arguments.Name.Value);

            Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(el.Text));
        }
    }
}

