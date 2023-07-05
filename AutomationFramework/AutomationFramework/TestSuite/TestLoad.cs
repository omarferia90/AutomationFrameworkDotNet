using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.TestSuite
{
    [TestFixture]
    public class TestLoad : BaseSetup
    {

        [Test]
        public void MyFirstTest() 
        {
            SetUpTC("My Test");
            objBrowser.NavigateTo("https://www.google.com/?hl=es");
            objReport.fnLog("Info", Status.Info, false);
            objReport.fnLog("Pass", Status.Pass, false);
            objReport.fnLog("Faillkljhlkjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj hhjlkkllk", Status.Fail, true);
            objReport.fnLog("Warning", Status.Warning, false);
            CloseTC();
        }



    }
}
