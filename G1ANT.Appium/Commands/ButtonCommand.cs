using System;
using G1ANT.Language;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.button", Tooltip = "This command clicks choden element.")]
    public class ButtonCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required =true,Tooltip = "Keys to be sent to element")]
            public TextStructure KeyCode { get; set; } = new TextStructure("");
        }

        public ButtonCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            AndroidDriver<AndroidElement> driver = OpenCommand._driver;
            string caseSwitch = arguments.KeyCode.Value;

            switch (caseSwitch)
            {
                case "BACK":
                    driver.PressKeyCode(AndroidKeyCode.Back);
                    break;
                default:
                    throw new ArgumentException($"Provided button name is invalid.");
            }
        }
    }
}

