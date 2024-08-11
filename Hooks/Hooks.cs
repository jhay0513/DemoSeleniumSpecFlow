using BoDi;
using DemoCRUDAutomation.Utility;
using DemoSeleniumSpecFlow.DataModel;
using DemoSeleniumSpecFlow.Drivers;
using DemoSeleniumSpecFlow.Support;
using NUnit.Framework;
using OpenQA.Selenium;
using RestSharp;

[assembly:Parallelizable(ParallelScope.Fixtures)]
namespace DemoSeleniumSpecFlow.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _container;

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Reporter.StartReporter();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {

            Reporter.EndReporter();
        }


        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(TestContext testContext)
        {
            BrowserDriver browserDriver = new BrowserDriver();
            CleanUpModel cleanUpModel = new CleanUpModel();

            _container.RegisterInstanceAs<BrowserDriver>(browserDriver);
            _container.RegisterInstanceAs<CleanUpModel>(cleanUpModel);


            Reporter.StartTest(testContext.Test.Name);
        }

        [AfterScenario]
        public void AfterScenario(TestContext testContext)
        {
            BrowserDriver browserDriver = _container.Resolve<BrowserDriver>();
            CleanUpModel cleanUpModel = _container.Resolve<CleanUpModel>();

            if (cleanUpModel.Id != null) 
            { 
                var client = new RestClient(Util.GetAppSetting("Url"));
                var request = new RestRequest($"/contacts/{cleanUpModel.Id}", Method.Delete);
                request.AddHeader("Authorization", $"Bearer {cleanUpModel.Token}");
                RestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
            }

            Reporter.LoggingTestStatusExtentReport(testContext);

            browserDriver.Dispose();
        }
    }
}