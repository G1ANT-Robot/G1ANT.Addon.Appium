using System;
using System.Collections.Generic;
using G1ANT.Language;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.gettext", Tooltip = "This command extracts text from selected element.")]
    public class GetTextCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Provide element ID")]
            public TextStructure Id { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Provide Text, which should be present in element.")]
            public TextStructure Text { get; set; } = new TextStructure("");

            [Argument(Tooltip = "Extracted text will be stored here")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public GetTextCommand(AbstractScripter scripter) : base(scripter)
        {

        }

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
                    Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(el.Text));
                }
                catch
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
                        Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(element.Text));
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

