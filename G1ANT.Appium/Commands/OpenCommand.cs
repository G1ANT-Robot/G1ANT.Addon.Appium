using System;
using System.Windows.Forms;
using G1ANT.Language;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using OpenQA.Selenium.Remote;

namespace G1ANT.Addon.Appium
{
    [Command(Name = "appium.open", Tooltip = "This command initialises appium server.")]
    public class OpenCommand : Language.Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Device Name")]
            public TextStructure DeviceName { get; set; } = new TextStructure("Android");

            [Argument(Required = true, Tooltip = "App Package")]
            public TextStructure AppPackage { get; set; } = new TextStructure("");

            [Argument(Required = true, Tooltip = "Platform Name")]
            public TextStructure PlatformName { get; set; } = new TextStructure("");

            [Argument(Required = true, Tooltip = "AppActivity")]
            public TextStructure AppActivity { get; set; } = new TextStructure("");

            [Argument(Required = false, Tooltip = "Automation Name")]
            public TextStructure AutomationName { get; set; } = new TextStructure("UiAutomator2");
        }

        public OpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            Initialize(arguments);
        }

        private Uri OpenAppium()
        {
            var args = new OptionCollector().AddArguments(GeneralOptionList.PreLaunch());
            try
            {
                _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Invalid"))
                {
                    var result = RobotMessageBox.Show("It seems you have no Appium driver installed. Would you like to install it now?", "Error", MessageBoxButtons.YesNo);
                    if(result == DialogResult.Yes)
                    {
                        CmdHelper.RunCommand("\"C:\\Program Files\\nodejs\\npm.cmd\"", "install appium");
                    }
                }
                return null;
            }
            _appiumLocalService.Start();
            return _appiumLocalService.ServiceUrl;
        }

        public static AndroidDriver<AndroidElement> _driver;
        private static AppiumLocalService _appiumLocalService;

        private void Initialize(Arguments arguments)
        {
            var desiredCaps = new DesiredCapabilities();
            desiredCaps.SetCapability(MobileCapabilityType.DeviceName, arguments.DeviceName.Value);
            desiredCaps.SetCapability(AndroidMobileCapabilityType.AppPackage, arguments.AppPackage.Value);
            desiredCaps.SetCapability(MobileCapabilityType.PlatformName, arguments.PlatformName.Value);
            desiredCaps.SetCapability(AndroidMobileCapabilityType.AppActivity, arguments.AppActivity.Value);
            desiredCaps.SetCapability(MobileCapabilityType.AutomationName, arguments.AutomationName.Value);
            var appiumServerUri = OpenAppium();

            if (appiumServerUri != null)
            {
                _driver = new AndroidDriver<AndroidElement>(appiumServerUri, desiredCaps);
            }
        }
    }
}

