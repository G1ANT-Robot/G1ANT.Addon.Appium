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
            [Argument(Required = false, Tooltip = "...")]
            public TextStructure Text { get; set; }

            [Argument(Tooltip = "Result variable")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public OpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        // Implement this method
        public void Execute(Arguments arguments)
        {
            ClassInitialize();
        }

        private void OpenAppium()
        {
            var args = new OptionCollector().AddArguments(GeneralOptionList.PreLaunch());
            _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start();
        }

        public static AndroidDriver<AndroidElement> _driver;
        private static AppiumLocalService _appiumLocalService;

        private void ClassInitialize()
        {            
            //string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ApiDemos-debug.apk");
            var desiredCaps = new DesiredCapabilities();
            desiredCaps.SetCapability(MobileCapabilityType.DeviceName, "Blue Stacks");
            desiredCaps.SetCapability(AndroidMobileCapabilityType.AppPackage, "com.instagram.android");
            desiredCaps.SetCapability(MobileCapabilityType.PlatformName, "Android");
            //desiredCaps.SetCapability(MobileCapabilityType.PlatformVersion, "7.1");
            desiredCaps.SetCapability(AndroidMobileCapabilityType.AppActivity, ".activity.MainTabActivity");
            //desiredCaps.SetCapability(MobileCapabilityType.AutomationName, "uiautomator2");
            _driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), desiredCaps);
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

