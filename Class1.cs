using System;
using System.Collections.Generic;
using System.Windows.Forms;
using G1ANT.Language;

namespace G1ANT.Appium
{
    [Command(Name = "open", Tooltip = "This command initialises appium server.")]
    public class OpenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            // Enter all arguments you need
            [Argument(Required = true, Tooltip = "...")]
            public TextStructure Text { get; set; }

            [Argument(Tooltip = "Result variable")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        /*public AbstractScripter scripter : base(scripter)
        {
        }*/

        // Implement this method
        public void Execute(Arguments arguments)
        {

        }

        private void Initialize()
        {
            var args = new OptionColector().AddArguments(GeneralOptionList.PreLaunch());
            _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start();
        }
    }
}

