using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace G1ANT.Addon.Appium
{
    public static class ElementsHelper
    {
        public static List<AndroidElement> GetElements(SearchBy by, string name)
        {
            switch (by)
            {
                case SearchBy.Id:
                    return GetElementsById(name);
                case SearchBy.AccessibilityId:
                    return GetElementsByAccessibilityId(name);
                case SearchBy.PartialId:
                    return GetElementsByPartialId(name);
                case SearchBy.Text:
                    return GetElementsByText(name);
                default:
                    throw new ArgumentException($"Search criteria is invalid");
            }
        }

        private static List<AndroidElement> GetElementsById(string id)
        {
            var driver = OpenCommand.GetDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
                return new List<AndroidElement>(driver.FindElementsById(id));
            }
            catch
            {
                throw new ArgumentException($"Element with provided id was not found.");
            }
        }

        private static List<AndroidElement> GetElementsByAccessibilityId(string accessibilityId)
        {
            var driver = OpenCommand.GetDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@content-desc,\"" + accessibilityId + "\")]")));
                return new List<AndroidElement>(driver.FindElementsByAccessibilityId(accessibilityId));
            }
            catch
            {
                throw new ArgumentException($"Element with provided accessibility id was not found.");
            }
        }

        private static List<AndroidElement> GetElementsByPartialId(string partialId)
        {
            var driver = OpenCommand.GetDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@content-desc,\"" + partialId + "\")]")));
                return new List<AndroidElement>(driver.FindElements(By.XPath("//*[contains(@content-desc,\"" + partialId + "\")]")));
            }
            catch
            {
                throw new ArgumentException($"Element with provided partial id was not found.");
            }
        }

        private static List<AndroidElement> GetElementsByText(string text)
        {
            var driver = OpenCommand.GetDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(),\"" + text + "\")]")));
                return new List<AndroidElement>(driver.FindElements(By.XPath("//*[contains(text(),\"" + text + "\")]")));
            }
            catch
            {
                throw new ArgumentException($"Element with provided text was not found.");
            }
        }
    }
}
