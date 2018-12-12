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
    [Command(Name = "appium.sendkeys", Tooltip = "This command clicks choden element.")]
    public class SendKeysCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            // Enter all arguments you need
            [Argument(Required = true, Tooltip = "Provide element ID")]
            public TextStructure Id { get; set; }

            [Argument(Required = true, Tooltip = "Keys to be sent to element")]
            public TextStructure Keys { get; set; } = new TextStructure("keys");
        }

        public SendKeysCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        // Implement this method
        public void Execute(Arguments arguments)
        {
            AndroidDriver<AndroidElement> driver = OpenCommand._driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(arguments.Id.Value)));
            IWebElement el = driver.FindElement(By.Id(arguments.Id.Value));
            el.SendKeys(arguments.Keys.Value);
        }


    }
}

