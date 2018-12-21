using System;
using System.Collections.Generic;
using System.IO;
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
            // Enter all arguments you need
            [Argument(Required = true, Tooltip = "Device Name")]
            public TextStructure DeviceName { get; set; } = new TextStructure("");

            [Argument(Required = true, Tooltip = "App Package")]
            public TextStructure AppPackage { get; set; } = new TextStructure("");

            [Argument(Required = true, Tooltip = "Platform Name")]
            public TextStructure PlatformName { get; set; } = new TextStructure("");

            [Argument(Required = true, Tooltip = "AppActivity")]
            public TextStructure AppActivity { get; set; } = new TextStructure("");

            [Argument(Required = false, Tooltip = "Automation Name")]
            public TextStructure AutomationName { get; set; } = new TextStructure("uiautomator");

            [Argument(Required = false, Tooltip = "Automation Name")]
            public TextStructure Uri { get; set; } = new TextStructure("http://127.0.0.1:4723/wd/hub");
        }

        public OpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        // Implement this method
        public void Execute(Arguments arguments)
        {
            ClassInitialize(arguments);
        }

        private void OpenAppium()
        {
            var args = new OptionCollector().AddArguments(GeneralOptionList.PreLaunch());
            _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start();
        }

        public static AndroidDriver<AndroidElement> _driver;
        private static AppiumLocalService _appiumLocalService;

        private void ClassInitialize(Arguments arguments)
        {            
            //string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ApiDemos-debug.apk");
            var desiredCaps = new DesiredCapabilities();
            desiredCaps.SetCapability(MobileCapabilityType.DeviceName, arguments.DeviceName.Value);
            desiredCaps.SetCapability(AndroidMobileCapabilityType.AppPackage, arguments.AppPackage.Value);
            desiredCaps.SetCapability(MobileCapabilityType.PlatformName, arguments.PlatformName.Value);
            //desiredCaps.SetCapability(MobileCapabilityType.PlatformVersion, "7.1");
            desiredCaps.SetCapability(AndroidMobileCapabilityType.AppActivity, arguments.AppActivity.Value);
            desiredCaps.SetCapability(MobileCapabilityType.AutomationName, arguments.AutomationName.Value);
            _driver = new AndroidDriver<AndroidElement>(new Uri(arguments.Uri.Value), desiredCaps);
        }

        private void AppInitialize()
        {
            if (_driver != null)
            {
                _driver.LaunchApp();
                _driver.StartActivity("com.example.android.apis", ".view.ControlsMaterialDark");

            }
        }

        private void AppCleanup()
        {
            if (_driver != null)
            {
                _driver.CloseApp();
            }
        }

        private void ClassCleanup()
        {
            _appiumLocalService.Dispose();
        }

    }
}

