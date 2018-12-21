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
    [Command(Name = "appium.startactivity", Tooltip = "This command allows to change activity.")]
    public class ChangeActivityCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            // Enter all arguments you need
            [Argument(Tooltip = "Provide app package.")]
            public TextStructure AppPackage { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Provide app activity.")]
            public TextStructure AppActivity { get; set; } = new TextStructure("");

        }

        public ChangeActivityCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        // Implement this method
        public void Execute(Arguments arguments)
        {
            AndroidDriver<AndroidElement> driver = OpenCommand._driver;
            driver.StartActivity(arguments.AppPackage.Value,arguments.AppActivity.Value);
        }
    }
}

