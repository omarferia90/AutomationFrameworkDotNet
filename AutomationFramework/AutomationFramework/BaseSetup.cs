using DocumentFormat.OpenXml.Math;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework
{
    public class BaseSetup
    {
        //Init Clases
        public ClsReport objReport;
        public ClsBrowser objBrowser;
        public ClsWebElement objWE;

        [OneTimeSetUp]
        public void BeforeClass()
        {
            objBrowser = new ClsBrowser();
            objReport = new ClsReport();
            objWE = new ClsWebElement();
            objReport.fnExtentSetup();
        }

        public void SetUpTC(string TestCaseName) 
        {
            objReport.objTest = objReport.objExtent.CreateTest(TestCaseName);
            objBrowser.StartBrowser(TestContext.Parameters["BrowserName"]);
            objReport.InitDriver(objBrowser.getDriver());
            objWE.InitDriver(objBrowser.getDriver());
        }


        public void CloseTC() 
        {
            objBrowser.CloseBrowser();
            objReport.fnExtentClose();
        }



        [OneTimeTearDown]
        public void AfterClass() 
        {
            objReport.objExtent.Flush();
        }





    }
}
