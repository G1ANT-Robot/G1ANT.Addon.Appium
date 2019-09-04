using System;
using System.Drawing;
using G1ANT.Language;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Remote;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.scroll", Tooltip = "This command is used to perform scroll action")]
    public class ScrollCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Provide name of the capaility")]
            public TextStructure Search { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Specify by wich capability the element will be searched")]
            public TextStructure By { get; set; } = new TextStructure("");

            [Argument(Required = false, Tooltip = "Direction of swipe")]
            public TextStructure SwipeDir { get; set; } = new TextStructure("up");

            [Argument(Tooltip = "Scroll by certain percentage of screen - from 0 to 100")]
            public IntegerStructure ScrollAmount { get; set; } = new IntegerStructure();
        }

        public ScrollCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {
            var driver = OpenCommand.GetDriver();

            if (arguments.By.Value != "" && arguments.Search.Value != "")
            {
                driver.HideKeyboard();
                SrollToElement(driver, arguments);
            }
            else if (arguments.ScrollAmount.Value != 0)
            {
                float top = 0.9f;
                float bottom = 0.1f;
                float swipeAmmount = arguments.ScrollAmount.Value / 100f * (top - bottom);
                SwipeVertical(driver, top, top - swipeAmmount, 0.5, 250, arguments.SwipeDir.Value);
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

            if (dir.ToLower() == "up")
            {
                new TouchAction(driver).Press(anchor, startPoint).Wait(duration).MoveTo(anchor, endPoint).Release().Perform();
            }
            else if (dir.ToLower() == "down")
            {
                new TouchAction(driver).Press(anchor, endPoint).Wait(duration).MoveTo(anchor, startPoint).Release().Perform();
            }
            else
            {
                throw new ArgumentException($"Provided swipe direction is invalid. Use \"up\" or \"down\"");
            }
        }

        private void SrollToElement(AndroidDriver<AndroidElement> driver, Arguments arguments)
        {
            while (true)
            {
                if (ElementsHelper.GetElements(arguments.By.Value, arguments.Search.Value).Count > 0)
                {
                    return;
                }

                SwipeVertical(driver, 0.9, 0.1, 0.5, 2000, arguments.SwipeDir.Value);
            }
        }
    }
}

