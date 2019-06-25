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
    [Command(Name = "appium.isopened", Tooltip = "This command clicks choden element.")]
    public class IsOpened : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required =true,Tooltip = "Keys to be sent to element")]
            public TextStructure Result { get; set; } = new TextStructure("result");
        }

        public IsOpened(AbstractScripter scripter) : base(scripter)
        {
        }

        // Implement this method
        public void Execute(Arguments arguments)
        {
            AndroidDriver<AndroidElement> driver = OpenCommand._driver;
            bool isOpened = false;
            if (driver.)
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(returnString));

        }
    }
}

