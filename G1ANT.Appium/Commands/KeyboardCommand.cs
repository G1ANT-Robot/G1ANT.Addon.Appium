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
    [Command(Name = "appium.keyboard", Tooltip = "This command clicks choden element.")]
    public class KeyboardCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Keys to be sent to element")]
            public TextStructure Keys { get; set; } = new TextStructure("keys");
        }

        public KeyboardCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        // Implement this method
        public void Execute(Arguments arguments)
        {
            AndroidDriver<AndroidElement> driver = OpenCommand._driver;
           
           // for(int i=0;i<arguments.Keys.Value.Length-1;i++)
           // {
                driver.Keyboard.PressKey(arguments.Keys.Value);
                //RobotMessageBox.Show(arguments.Keys.Value[i].ToString());
               // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(30));
           // }

        }


    }
}

