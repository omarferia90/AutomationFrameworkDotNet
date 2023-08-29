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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [OneTimeSetUp]
        public void BeforeClass()
        {
            objBrowser = new ClsBrowser();
            objReport = new ClsReport();
            objReport.fnExtentSetup();
        }

        public void SetUpTC(string TestCaseName) 
        {
            log.Info("log4net test");
            objReport.objTest = objReport.objExtent.CreateTest(TestCaseName);
            objBrowser.StartBrowser(TestContext.Parameters["BrowserName"]);
            objWE = new ClsWebElement(objBrowser.getDriver(), objReport);
            objReport.initDriver(objBrowser.getDriver());

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
