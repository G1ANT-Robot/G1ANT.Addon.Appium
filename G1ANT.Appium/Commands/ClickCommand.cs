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
    [Command(Name = "appium.click", Tooltip = "This command clicks choden element.")]
    public class ClickCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            // Enter all arguments you need
            [Argument(Tooltip = "Provide element ID")]
            public TextStructure Id { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Provide Text, which should be present in element.")]
            public TextStructure Text { get; set; } = new TextStructure("");

        }

        public ClickCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        // Implement this method
        public void Execute(Arguments arguments)
        {
            
            AndroidDriver<AndroidElement> driver = OpenCommand._driver;
            IWebElement el;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            
            if (arguments.Id.Value != ""&& arguments.Text.Value=="")
            {
                try
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(arguments.Id.Value)));
                    el = driver.FindElement(By.Id(arguments.Id.Value));
                    el.Click();
                }
                catch(Exception e)
                {
                    throw new ArgumentException($"Element with provided id was not found.");
                }
            }
            else if(arguments.Id.Value != "" && arguments.Text.Value != "")
            {
                By myElement = By.Id(arguments.Id.Value);
                List<AndroidElement> elements = new List<AndroidElement>();
                elements.AddRange(driver.FindElements(myElement));

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
                throw new ArgumentException($"The id and content-desc are not defined. You should provide one of them.");
            }

        }


    }
}

