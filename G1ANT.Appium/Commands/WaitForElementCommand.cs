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
    [Command(Name = "appium.waitforelement", Tooltip = "This command extracts text from selected elements.")]
    public class WaitForElementCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            // Enter all arguments you need
            [Argument(Tooltip = "Provide element name to search by")]
            public TextStructure Name { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Id or ClassName")]
            public TextStructure By { get; set; } = new TextStructure("Id");

            [Argument(Tooltip = "Wait Timeout")]
            public IntegerStructure WaitTimeout { get; set; } = new IntegerStructure(10);
        }

        public WaitForElementCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        // Implement this method
        public void Execute(Arguments arguments)
        {
            
            AndroidDriver<AndroidElement> driver = OpenCommand._driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            if (arguments.Name.Value != "")
            {
                By myElement;
                if (arguments.By.Value.ToLower() == "classname")
                {
                    myElement = By.ClassName(arguments.Name.Value);
                }
                else
                {
                    myElement = By.Id(arguments.Name.Value);
                }

                List<AndroidElement> elements = new List<AndroidElement>();
                for (int i = 0; i < arguments.WaitTimeout.Value; i++)
                {
                    elements.AddRange(driver.FindElements(myElement));
                    if(elements.Count>0)
                    {
                        return;
                    }
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                }
                throw new ArgumentException($"Element with id or class name {arguments.Name.Value} did not show up.");
            }
            else
            {
                throw new ArgumentException($"The id and content-desc are not defined. You should provide one of them.");
            }

        }


    }
}

