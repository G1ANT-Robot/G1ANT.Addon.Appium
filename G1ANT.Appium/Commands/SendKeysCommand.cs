using System;
using G1ANT.Language;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.sendkeys", Tooltip = "This command clicks choden element.")]
    public class SendKeysCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Provide element ID")]
            public TextStructure Id { get; set; }

            [Argument(Required = true, Tooltip = "Keys to be sent to element")]
            public TextStructure Keys { get; set; } = new TextStructure("keys");
        }

        public SendKeysCommand(AbstractScripter scripter) : base(scripter)
        {

        }

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

