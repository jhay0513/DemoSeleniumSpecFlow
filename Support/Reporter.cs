using AventStack.ExtentReports;
using OpenQA.Selenium;
using System.Reflection;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using DemoSeleniumSpecFlow.Support;

namespace DemoCRUDAutomation.Utility
{
    public class Reporter
    {
        public static ExtentReports extent;
        public static ExtentTest testlog;

        public static void StartReporter()
        {
            // Setup Reporter Path
            string reportPath = Path.Combine(Util.GetProjectPath(), "Reports");
            Directory.CreateDirectory(reportPath);

            // Setup Reporter
            extent = new ExtentReports();
            ExtentSparkReporter spark = new ExtentSparkReporter(Path.Combine(reportPath, $"Report.html"));
            extent.AttachReporter(spark);

            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Tester", Environment.UserName);
            extent.AddSystemInfo("MachineName", Environment.MachineName);
        }

        /// <summary>
        /// Start Logging
        /// </summary>
        /// <param name="testsToStart"></param>
        public static void StartTest(string testsToStart)
        {
            testlog = extent.CreateTest(testsToStart);
        }

        /// <summary>
        /// Log Result
        /// </summary>
        /// <param name="testContext"></param>
        public static void LoggingTestStatusExtentReport(TestContext testContext)
        {
            try
            {
                var status = testContext.Result.Outcome.Status;
                Status logstatus;
                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        testlog.Log(Status.Fail, "Test steps NOT Completed for Test case " + testContext.Test.MethodName);
                        testlog.Log(Status.Fail, "Test ended with message " + testContext.Result.Message);
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        testlog.Log(Status.Skip, "Test ended with " + Status.Skip);
                        break;
                    case TestStatus.Passed:
                        logstatus = Status.Pass;
                        testlog.Log(Status.Pass, "Test steps finished for test case " + testContext.Test.MethodName);
                        break;
                }
            }
            catch (WebDriverException ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// End Logging
        /// </summary>
        public static void EndReporter()
        {
            //End Logging
            extent.Flush();
        }
    }
}
