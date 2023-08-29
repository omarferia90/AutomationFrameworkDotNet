using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFramework
{
    public class ClsWebElement
    {
        private DefaultWait<IWebDriver> objFW;
        private IWebDriver driver = null;
        private ClsReport objReport;

        public ClsWebElement(IWebDriver driver, ClsReport objReport) 
        {
            this.driver = driver;
            this.objReport = objReport; 
        }

        /// <summary>
        /// Perform an action to a specific web element.
        /// Actions(Displayed, SendKeys, CustomSendKeys, Click, DoubleClick)
        /// </summary>
        /// <param name="objWE"></param>
        /// <param name="strAction"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private object fnGetFluentWait(IWebElement objWE, string strAction, string strValue = "")
        {
            Actions action = new Actions(driver);
            objFW = new DefaultWait<IWebDriver>(driver);
            objFW.Timeout = TimeSpan.FromSeconds(10);
            objFW.PollingInterval = TimeSpan.FromMilliseconds(250);
            objFW.IgnoreExceptionTypes(typeof(WebDriverTimeoutException), typeof(SuccessException));

            switch (strAction.ToUpper())
            {
                case "DISPLAYED":
                    objFW.Until(x => objWE.Displayed);
                    break;
                case "SENDKEYS":
                    objFW.Until(x => objWE).SendKeys(strValue);
                    break;
                case "CLEAR":
                    objFW.Until(x => objWE).Clear();
                    break;
                case "CLICK":
                    objFW.Until(x => objWE).Click();
                    break;
                case "DOUBLECLICK":
                    action.DoubleClick(objWE);
                    break;
                case "CUSTOMSENDKEYS":
                    objWE.Click();
                    action.KeyDown(Keys.Control).SendKeys(Keys.Home).Perform();
                    objWE.Clear();
                    Thread.Sleep(TimeSpan.FromMilliseconds(500));
                    objWE.SendKeys(Keys.Delete);
                    objWE.SendKeys(strValue);
                    break;
            }
            return objFW;
        }


        /// <summary>
        /// Returns a list on web elements base on By By
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public IList<IWebElement> fnGetWeList(By by)
        {
            try
            {
                IList<IWebElement> objWE = driver.FindElements(by);
                return objWE;
            }
            catch (Exception objException)
            {
                return null;
            }
        }


        /// <summary>
        /// Returns a list on web elements base on String Locator (Xpath)
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public IList<IWebElement> fnGetWeList(String locator) => fnGetWeList(By.XPath(locator));


        /// <summary>
        /// Returns a web element based on By by
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public IWebElement fnGetWebElement(By by)
        {
            try
            {
                IWebElement objWE = driver.FindElement(by);
                return objWE;
            }
            catch (Exception objException)
            {
                return null;
            }
        }


        /// <summary>
        /// Returns a web element based on String locator (Xpath)
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public IWebElement fnGetWebElement(String locator) => fnGetWebElement(By.XPath(locator));


        /// <summary>
        /// Wait until 15 seconds to wait that page is loaded
        /// </summary>
        /// <returns></returns>
        public bool fnPageLoad()
        {
            bool blResult = false;
            try
            {
                ClsBrowser browser = new ClsBrowser();
                IJavaScriptExecutor objJS = (IJavaScriptExecutor)driver;
                browser.jsWait.Until(wd => objJS.ExecuteScript("return document.readyState").ToString() == "complete");
                blResult = true;
            }
            catch (Exception objException)
            {
            }
            return blResult;
        }

        /// <summary>
        /// SendKeys a specific element using native interaction
        /// </summary>
        /// <param name="objWE"></param>
        /// <param name="strField"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool fnSendKeys(IWebElement objWE, string strField, string strValue, bool takeScreenshot = false)
        {
            bool blResult = false;
            try
            {
                objReport.fnLog($"Step - Sendkeys for element: \"{strField}\".", Status.Info, false);
                fnGetFluentWait(objWE, "SendKeys", strValue);
                objReport.fnLog($"Sendkeys for element: \"{strField}\" with value \"{strValue}\" was done successfully.", Status.Pass, takeScreenshot);
                blResult = true;
            }
            catch (Exception objException)
            {
                objReport.fnLog($"Sendkeys for element: \"{strField}\" with value \"{strValue}\" failed.", Status.Fail, takeScreenshot);
            }
            return blResult;
        }








        


    }
}
