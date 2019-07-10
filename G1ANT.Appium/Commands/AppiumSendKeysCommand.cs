using System;
using G1ANT.Language;
using OpenQA.Selenium.Remote;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.sendkeys", Tooltip = "This command clicks choden element.")]
    public class SendKeysCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Provide name of the capaility")]
            public TextStructure Search { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Provide element ID")]
            public TextStructure By { get; set; } = new TextStructure("");

            [Argument(Required = true, Tooltip = "Keys to be sent to element")]
            public TextStructure Keys { get; set; } = new TextStructure("");
        }

        public SendKeysCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            ElementHelper.GetElement((SearchBy)Enum.Parse(typeof(SearchBy), arguments.By.Value), arguments.Search.Value).SendKeys(arguments.Keys.Value);
        }
    }
}

