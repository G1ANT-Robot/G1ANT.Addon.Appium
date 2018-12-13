using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using G1ANT.Language;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

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

        // Implement this method
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

