using System;
using System.Collections.Generic;
using G1ANT.Language;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.gettextfromelements", Tooltip = "This command extracts text from selected elements.")]
    public class GetTextFromElementsCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Provide element name to search by")]
            public TextStructure Name { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Extracted text will be stored here")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(Tooltip = "Id or ClassName")]
            public TextStructure By { get; set; } = new TextStructure("Id");

        }

        public GetTextFromElementsCommand(AbstractScripter scripter) : base(scripter)
        {

        }

        public void Execute(Arguments arguments)
        {

            var driver = OpenCommand._driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            string returnString = "";

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
                List<AppiumWebElement> elements = new List<AppiumWebElement>();
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

