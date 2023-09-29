using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using TeamsWindowsApp.Driver;
using TeamsWindowsApp.Helper;
using TeamsWindowsApp.Pages;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace TeamsWindowsApp.Hooks
{
    [Binding]
    public class Hook
    {
        private static ISpecFlowOutputHelper _outHelper;
        private readonly FeatureContext _featureContext;
        //private readonly ScenarioContext _scenarioContext;
        private readonly DataReader _reader;
        private static Process _windriver;
        private readonly TeamsLogin _loginPage;
        private readonly IContextManager _scenarioContext;

        public Hook(FeatureContext featureContext, DataReader reader, TeamsLogin loginPage, IContextManager scenarioContext)
        {
            _featureContext = featureContext;
            _reader = reader;
            _loginPage = loginPage;
            _scenarioContext = scenarioContext;

        }

        public ScenarioContext ScenarioContext => _scenarioContext.ScenarioContext;

        [BeforeTestRun]
        public static void StartWinAppDriver()
        {
            try
            {
                string sRerunLog_FileName = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location).Split("bin")[0] + "\\TestData\\RerunLog.txt";
                //Check WinAppDriver running in background
                if (CommonUtil.checkProcessRunning("WinAppDriver"))
                    CommonUtil.KillProcess("WinAppDriver");

                //Initiate the Configuration class reference
                var configurationDriver = new ConfigurationDriver();

                //Getting Environment to Run the scripts
                Environment.SetEnvironmentVariable("ENV", configurationDriver.Configuration["Env"]);

                //Run WinAppDriver
                _windriver = Process.Start(configurationDriver.Configuration["winAppPath"]);
                Console.WriteLine("WinApp Driver Launched successfully");

                //Creation Output folder for Execution
                new Reporting(_outHelper).CreateFolder();

                //Loading object file
				CommonUtil.getObjectData();

                //Create a file
                if (File.Exists(sRerunLog_FileName))
                {
                    File.Delete(sRerunLog_FileName);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not locate WinAppDriver.exe, get it from https://github.com/Microsoft/WinAppDriver/releases and change the winAppPath in app.settings accordingly");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                throw e;
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;
                Console.WriteLine("Get an exception when Start WinAppDriver. Error: " + e.Message);
                Console.Write(e.StackTrace);
            }
        }

        [AfterScenario]
        public void Rerun_FileUpdate()
        {
            //Rerun for Failures in Jenkins
            string sRerunLog_FileName = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location).Split("bin")[0] + "\\TestData\\RerunLog.txt";

            //string errorStatus = _scenarioContext.TestError.ToString();
            Console.WriteLine("Scenario Status: "+ _scenarioContext.ScenarioContext.ScenarioExecutionStatus);
            Console.WriteLine("Scenario Tag: "+_scenarioContext.ScenarioContext.ScenarioInfo.Tags.GetValue(0));
            if (!_scenarioContext.ScenarioContext.ScenarioExecutionStatus.ToString().Equals("OK"))
            {
                foreach (string tags in _scenarioContext.ScenarioContext.ScenarioInfo.Tags)
                {
                    if (tags.Contains("Rerun"))
                    {
                        Console.WriteLine("Rerun: "+tags);
                        TextWriter tw = new StreamWriter(sRerunLog_FileName, true);
                        tw.WriteLine(tags);
                        tw.Close();
                    }
                }

            }
           //TeamsLogin.QuitTeamsApp();
        }
       
        [AfterTestRun]
        public static void KillWinAppDriver()
        {
            Console.WriteLine("------Teams Quit-----------");
            System.Threading.Thread.Sleep(5000);
            //TeamsLogin.QuitTeamsApp();
            Console.WriteLine("------WinAppDriverKill-----------");
            _windriver.Kill();
            CommonUtil.disposeObjectData();
            Console.WriteLine("------Full Report-----------");
            //full Report
            //Reporting.fullReport();
            Console.WriteLine("------Customized Report-----------");
            // Cutomized Report - Only executed scenarios
            //Reporting.customizeFormat_ReportGeneration();
            Console.WriteLine("------######Execution Completed#######-----------");
			Environment.SetEnvironmentVariable("ENV", null);
        }

    }
}
