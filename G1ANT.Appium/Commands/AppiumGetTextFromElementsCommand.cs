using System;
using G1ANT.Language;
using OpenQA.Selenium.Remote;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.gettextfromelements", Tooltip = "This command extracts text from selected elements.")]
    public class GetTextFromElementsCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Provide name of the capaility")]
            public TextStructure Search { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Specify by wich capability the element will be searched")]
            public TextStructure By { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Extracted text will be stored here")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public GetTextFromElementsCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            var returnString = string.Empty;
            var elements = ElementsHelper.GetElements((SearchBy)Enum.Parse(typeof(SearchBy), arguments.By.Value), arguments.Name.Value);

            elements.ForEach(e => returnString += e.Text + "\n");

            Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(returnString));
        }
    }

}
