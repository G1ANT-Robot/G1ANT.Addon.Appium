using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using G1ANT.Language;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.scroll", Tooltip = "This command clicks choden element.")]
    public class ScrollCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "ID of the element to scroll to.")]
            public TextStructure Id { get; set; } = new TextStructure("");

            [Argument(Required = true, Tooltip = "Text of the element to scroll to.")]
            public TextStructure Text { get; set; } = new TextStructure("");
        }

        public ScrollCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        // Implement this method
        public void Execute(Arguments arguments)
        {
            AndroidDriver<AndroidElement> driver = OpenCommand._driver;
            TouchAction action = new TouchAction(driver);
            IWebElement el;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            if (arguments.Id.Value != "")
            {
                driver.HideKeyboard();
                SrollToElement(driver, arguments);
            }
            else
            {
                throw new ArgumentException($"The id and text are not defined. You should provide one of them.");
            }
    
        }
        private void SwipeVertical(AppiumDriver<AndroidElement> driver, double startPercentage, double finalPercentage, double anchorPercentage, int duration)
        {
            Size size = driver.Manage().Window.Size;
            int anchor = (int)(size.Width * anchorPercentage);
            int startPoint = (int)(size.Height * startPercentage);
            int endPoint = (int)(size.Height * finalPercentage);
            new TouchAction(driver).Press(anchor, startPoint).Wait(duration).MoveTo(anchor, endPoint).Release().Perform();
        }

        private AndroidElement SrollToElement(AndroidDriver<AndroidElement> driver,Arguments arguments)
        {
            Boolean isFoundElement;
            By myElement = By.Id(arguments.Id.Value);

            isFoundElement = driver.FindElements(myElement).Count > 0;
            while (isFoundElement == false)
            {
                SwipeVertical((AppiumDriver<AndroidElement>)driver, 0.9, 0.1, 0.5, 2000);

                List<AndroidElement> elements = new List<AndroidElement>();
                elements.AddRange(driver.FindElements(myElement));
                isFoundElement = driver.FindElements(myElement).Count > 0;

                if (arguments.Text.Value != "" && isFoundElement)
                {
                    foreach (AndroidElement element in elements)
                    {
                        if (element.Text == arguments.Text.Value)
                        {
                            return element;
                        }
                    }
                }
                else if (arguments.Text.Value == "" && isFoundElement)
                {
                    return elements[0];
                }
                elements.Clear();
                isFoundElement = false;
            }
            throw new ArgumentException($"No elements with specified id or text could be found.");

        }

    }
}

