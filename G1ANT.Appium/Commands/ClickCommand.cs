﻿using System;
using G1ANT.Language;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Remote;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.click", Tooltip = "This command clicks chosen element")]
    public class ClickCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Provide element ID")]
            public TextStructure By { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Provide name of the capaility")]
            public TextStructure Name { get; set; } = new TextStructure("");
        }

        public ClickCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            var by = (SearchBy)Enum.Parse(typeof(SearchBy), arguments.By.Value);

            if(by == SearchBy.XY)
            {
                TouchAction a2 = new TouchAction(OpenCommand.GetDriver());
                var coordinates = arguments.Name.Value.Split(',');
                a2.Tap(int.Parse(coordinates[0]), int.Parse(coordinates[1])).Perform();
            }
            else
            {
                ElementHelper.GetElement(by, arguments.Name.Value).Click();
            }
        }
    }
}
