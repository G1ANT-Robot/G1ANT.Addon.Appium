using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using G1ANT.Language;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.click", Tooltip = "This command clicks choden element.")]
    public class ClickCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            // Enter all arguments you need
            [Argument(Tooltip = "Provide element ID")]
            public TextStructure Id { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Provide element Accesibility ID")]
            public TextStructure AccessibilityId { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Provide Text, which should be present in element.")]
            public TextStructure Text { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Provide Text, which should be present in ID.")]
            public TextStructure PartialID { get; set; } = new TextStructure("");


            [Argument(Tooltip = "Provide X cordinate")]
            public IntegerStructure X { get; set; } = new IntegerStructure(-1);

            [Argument(Tooltip = "Provide Y cordinate")]
            public IntegerStructure Y { get; set; } = new IntegerStructure(-1);
        }

        public ClickCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        // Implement this method
        public void Execute(Arguments arguments)
        {
            
            AndroidDriver<AndroidElement> driver = OpenCommand._driver;
            IWebElement el;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            if (arguments.Id.Value != ""&& arguments.Text.Value==""&& arguments.PartialID.Value =="")
            {
                try
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(By.Id(arguments.Id.Value)));
                    el = driver.FindElementById(arguments.Id.Value);
                    el.Click();
                }
                catch
                {
                    throw new ArgumentException($"Element with provided id was not found.");
                }
            }
            else if (arguments.Id.Value == "" && arguments.AccessibilityId.Value != "" && arguments.Text.Value == "" && arguments.PartialID.Value == "")
            {
                try
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@content-desc,\"" + arguments.AccessibilityId.Value + "\")]")));
                    el = driver.FindElementByAccessibilityId(arguments.AccessibilityId.Value);
                    el.Click();
                }
                catch
                {
                    throw new ArgumentException($"Element with provided accessibility id was not found.");
                }
            }
            else if(arguments.Id.Value != "" && arguments.Text.Value != ""&& arguments.PartialID.Value == "")
            {
                By myElement = By.Id(arguments.Id.Value);
                List<AndroidElement> elements = new List<AndroidElement>();
                elements.AddRange(driver.FindElements(myElement));
                if (elements.Count > 0)
                {
                    foreach (AndroidElement element in elements)
                    {
                        if (element.Text == arguments.Text.Value)
                        {
                            element.Click();
                            break;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException($"Element with provided id was not found.");
                }
            }
            else if(arguments.Id.Value == "" && arguments.Text.Value == "" && arguments.PartialID.Value !="")
            {
                el = driver.FindElement(By.XPath("//*[contains(@content-desc,\"" + arguments.PartialID.Value + "\")]"));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(arguments.Id.Value)));
                el.Click();
            }
            else if(arguments.X.Value!=-1&& arguments.Y.Value != -1)
            {
                TouchAction a2 = new TouchAction(driver);
                a2.Tap(arguments.X.Value, arguments.Y.Value).Perform();
            }
            else
            {
                throw new ArgumentException($"The id and content-desc are not defined. You should provide one of them.");
            }

        }


    }
}

