using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework
{
    public class ClsReport : BaseSetup
    {
        public ExtentReports objExtent;
        public ExtentTest objTest;
        #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        public ExtentV3HtmlReporter objHtmlReporter;
        #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        public bool TC_Status;
        public bool DevOpsResult;
        public bool isWarning;
        public string BaseReportFolder;
        public string FullReportFolder;
        public string TestPlanSuite;
        private IWebDriver driver = null;

        
        /// <summary>
        /// Inits driver that will be used to take screenshot in the report during execution time.
        /// </summary>
        /// <param name="driver"></param>
        public void InitDriver(IWebDriver driver) 
        {
            this.driver = driver;
        }

        /// <summary>
        /// Setup the instances for Extent Report
        /// </summary>
        public void fnExtentSetup()
        {
            try
            {
                //Get Result Path
                string folderID = $"_{Environment.UserName.ToString()}";
                BaseReportFolder = TestContext.Parameters["ReportLocation"] + DateTime.Now.ToString("MMddyyyy_hhmmss") + folderID + @"\";
                FullReportFolder = BaseReportFolder + TestContext.Parameters["ProjectName"] + @"\" + TestContext.Parameters["ReportName"] + ".html";
                fnFolderSetup();

                //Setup Report
                #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                objHtmlReporter = new ExtentV3HtmlReporter(FullReportFolder);
                #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                objHtmlReporter.Config.ReportName = TestContext.Parameters["ReportName"];
                objHtmlReporter.Config.DocumentTitle = TestContext.Parameters["ProjectName"] + " - " + TestContext.Parameters["ReportName"];
                objHtmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
                objHtmlReporter.Config.Encoding = "utf-8";

                //Adding Details to Report
                objExtent = new ExtentReports();
                objExtent.AddSystemInfo("Project", TestContext.Parameters["ProjectName"]);
                objExtent.AddSystemInfo("Browser", TestContext.Parameters["BrowserName"]);
                objExtent.AddSystemInfo("Env", TestContext.Parameters["TestEnvironment"]);
                objExtent.AddSystemInfo("Executed By", Environment.UserName.ToString());
                objExtent.AddSystemInfo("Executed Machine", Environment.MachineName.ToString());
                objExtent.AddSystemInfo("Execution Time", DateTime.Now.ToString("MM/ddy/yyy hh:mm:ss"));
                objExtent.AttachReporter(objHtmlReporter);
            }
            catch (Exception pobjException)
            {
                throw (pobjException);
            }
        }

        /// <summary>
        /// Closes the report and generate it
        /// </summary>
        public void fnExtentClose()
        {
            try
            {
                var objStatus = TestContext.CurrentContext.Result.Outcome.Status;
                var objStacktrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
                var objErrorMessage = TestContext.CurrentContext.Result.Message;
                Status objLogstatus;
                switch (objStatus)
                {
                    case TestStatus.Failed:
                        objLogstatus = Status.Fail;
                        objTest.Log(objLogstatus, "Test ended with " + objLogstatus + " – " + objErrorMessage);
                        break;
                    case TestStatus.Passed:
                        objLogstatus = Status.Pass;
                        break;
                    case TestStatus.Inconclusive:
                        objLogstatus = Status.Pass;
                        break;
                    default:
                        objLogstatus = Status.Warning;
                        Console.WriteLine("The status: " + objLogstatus + " is not supported.");
                        break;
                }
            }
            catch (Exception pobjException)
            {
                throw (pobjException);
            }

        }

        /// <summary>
        /// fnLog create a log with current step, this step is added into the report generated
        /// </summary>
        /// <param name="pstrStepName"></param>
        /// <param name="pstrDescription"></param>
        /// <param name="pstrStatus"></param>
        /// <param name="pblScreenShot"></param>
        public void fnLog(string pDetails, Status pstrStatus, bool pblScreenShot)
        {
            MediaEntityModelProvider ss = null;
            if (pblScreenShot)
            {
                string strSCLocation = fnGetScreenshot();
                ss = MediaEntityBuilder.CreateScreenCaptureFromPath(strSCLocation).Build();
            }

            objTest.Log(pstrStatus == Status.Skip ? Status.Info : pstrStatus, pDetails, ss);
        }

        
        /// <summary>
        /// Takes a screenshot during  execution time
        /// </summary>
        /// <returns></returns>
        public string fnGetScreenshot()
        {
            string strSCName = "SC_" + TestContext.Parameters["ProjectName"].Replace(" ", "_") + "_" + DateTime.Now.ToString("MMddyyyy_hhmmss");
            Screenshot objFile = ((ITakesScreenshot)driver).GetScreenshot();
            string strFileLocation = BaseReportFolder + @"Screenshots\" + strSCName + ".jpg";
            objFile.SaveAsFile(strFileLocation, ScreenshotImageFormat.Jpeg);

            return strFileLocation;
        }


        /// <summary>
        /// Generates the folder for the report generated
        /// </summary>
        private void fnFolderSetup()
        {
            try
            {
                string[] strSubFolders = new string[2] { "ScreenShots", TestContext.Parameters["ProjectName"] };
                bool blFExist = System.IO.Directory.Exists(BaseReportFolder);
                if (!blFExist)
                {
                    System.IO.Directory.CreateDirectory(BaseReportFolder);
                }
                else
                    blFExist = false;

                foreach (string strFolder in strSubFolders)
                {
                    blFExist = System.IO.Directory.Exists(BaseReportFolder + strFolder);

                    if (!blFExist)
                    {
                        System.IO.Directory.CreateDirectory(BaseReportFolder + strFolder);
                    }
                }
            }
            catch {}
        }


    }
}
