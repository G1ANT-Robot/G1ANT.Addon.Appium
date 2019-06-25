using System;
using System.Collections.Generic;
using System.Drawing;
using G1ANT.Language;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.scroll", Tooltip = "This command clicks choden element.")]
    public class ScrollCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = false, Tooltip = "ID of the element to scroll to.")]
            public TextStructure Id { get; set; } = new TextStructure("");

            [Argument(Required = false, Tooltip = "Text of the element to scroll to.")]
            public TextStructure Text { get; set; } = new TextStructure("");

            [Argument(Required = false, Tooltip = "Direction of swipe")]
            public TextStructure SwipeDir { get; set; } = new TextStructure("up");

            [Argument(Tooltip = "Provide Text, which should be present in ID.")]
            public TextStructure PartialID { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Scroll by certain percentage of screen - from 0 to 100")]
            public IntegerStructure AmmountToScroll { get; set; } = new IntegerStructure();
        }

        public ScrollCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            AndroidDriver<AndroidElement> driver = OpenCommand._driver;
            TouchAction action = new TouchAction(driver);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            if (arguments.Id.Value != ""|| arguments.PartialID.Value!="")
            {
                driver.HideKeyboard();
                SrollToElement(driver, arguments);
            }
            else if (arguments.AmmountToScroll.Value != 0)
            {
                float top = 0.9f;
                float bottom = 0.1f;
                float swipeAmmount = arguments.AmmountToScroll.Value / 100f * (top - bottom);
                SwipeVertical(driver,top, top-swipeAmmount, 0.5, 250, arguments.SwipeDir.Value);
            }
            else
            {
                throw new ArgumentException($"The id and text are not defined. You should provide one of them.");
            }
    
        }
        private void SwipeVertical(AppiumDriver<AndroidElement> driver, double startPercentage, double finalPercentage, double anchorPercentage, int duration, string dir)
        {
            Size size = driver.Manage().Window.Size;
            int anchor = (int)(size.Width * anchorPercentage);
            int startPoint = (int)(size.Height * startPercentage);
            int endPoint = (int)(size.Height * finalPercentage);
            if (dir == "up")
            {
                new TouchAction(driver).Press(anchor, startPoint).Wait(duration).MoveTo(anchor, endPoint).Release().Perform();
            }
            else if(dir == "down")
            {
                new TouchAction(driver).Press(anchor, endPoint).Wait(duration).MoveTo(anchor, startPoint).Release().Perform();
            }
            else
            {
                throw new ArgumentException($"Provided swipe direction is invalid. Use \"up\" or \"down\"");
            }
        }

        private AndroidElement SrollToElement(AndroidDriver<AndroidElement> driver,Arguments arguments)
        {
            Boolean isFoundElement=false;
            By myElement;

            if (arguments.Id.Value != ""&& arguments.PartialID.Value == "")
            {
                myElement = By.Id(arguments.Id.Value);
            }
            else if(arguments.PartialID.Value != ""&& arguments.Id.Value == "")
            {
                myElement = By.XPath("//*[contains(@content-desc,\"" + arguments.PartialID.Value + "\")]");
            }
            else
            {
                throw new ArgumentException($"No ID was provided or it is invalid.");
            }

            while (isFoundElement == false)
            {
                SwipeVertical((AppiumDriver<AndroidElement>)driver, 0.9, 0.1, 0.5, 2000,arguments.SwipeDir.Value);

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

