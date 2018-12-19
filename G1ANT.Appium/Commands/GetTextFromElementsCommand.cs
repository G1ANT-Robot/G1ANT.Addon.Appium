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
    [Command(Name = "appium.gettextfromelements", Tooltip = "This command extracts text from selected elements.")]
    public class GetTextFromElementsCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            // Enter all arguments you need
            [Argument(Tooltip = "Provide element ID")]
            public TextStructure Id { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Extracted text will be stored here")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

        }

        public GetTextFromElementsCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        // Implement this method
        public void Execute(Arguments arguments)
        {
            
            AndroidDriver<AndroidElement> driver = OpenCommand._driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            string returnString = "";
            if(arguments.Id.Value != "")
            {
                By myElement = By.Id(arguments.Id.Value);
                List<AndroidElement> elements = new List<AndroidElement>();
                elements.AddRange(driver.FindElements(myElement));

                 foreach (AndroidElement element in elements)
                 {
                    returnString += element.Text + "\n";
                 }
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(returnString));
                returnString = "";
            }
            else
            {
                throw new ArgumentException($"The id and content-desc are not defined. You should provide one of them.");
            }

        }


    }
}

