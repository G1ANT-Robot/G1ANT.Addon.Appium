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
            [Argument(Required =true,Tooltip = "Keycode of the button to be pressed")]
            public TextStructure KeyCode { get; set; } = new TextStructure("");
        }

        public ButtonCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            var driver = OpenCommand.GetDriver();
            string keycode = arguments.KeyCode.Value.ToLower();

            switch (keycode)
            {
                case "back":
                    driver.PressKeyCode(AndroidKeyCode.Back);
                    break;
                default:
                    throw new ArgumentException($"Provided button name is invalid.");
            }
        }
    }
}

