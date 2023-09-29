using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using TeamsWindowsApp.Driver;
using TechTalk.SpecFlow.Infrastructure;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;


namespace TeamsWindowsApp.Helper
{
    public class Reporting : ISpecFlowOutputHelper
    {


        private static ISpecFlowOutputHelper _outHelper;
        //string target = @"c:/temp/Teams_Screenshot/";
        string target = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location).Split("bin")[0] + "Teams_Screenshot/";
        public static string timeStampFolderPath = "";
        public static string AttachmentPath = "";

        public Reporting(ISpecFlowOutputHelper outHelper)
        {
            _outHelper = outHelper;
        }

        
        public void TakeScreenShot(WindowsDriver<WindowsElement> _driver, String fileName)
        {
            fileName = fileName + getTimestamp() + ".png";
            _driver.GetScreenshot().SaveAsFile(timeStampFolderPath + "/" + fileName);
            //var image1 = Win32ApiUtil.CaptureActiveWindow();
            //image1.Save(timeStampFolderPath + "/" + fileName + ".png", ImageFormat.Png);
            //AddAttachment(timeStampFolderPath + "/" + fileName);
            AddAttachment(AttachmentPath + "/" + fileName);
        }

        public void TakeScreenShot(IWebDriver _driver, String fileName)
        {
            Screenshot getScreen = ((ITakesScreenshot)_driver).GetScreenshot();
            fileName = fileName + getTimestamp() + ".png";
            getScreen.SaveAsFile(timeStampFolderPath + "/" + fileName);
            //AddAttachment(timeStampFolderPath + "/" + fileName);
            AddAttachment(AttachmentPath + "/" + fileName);
        }

        public void CreateFolder()
        {
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }

            string fileName = getFileName_Timestamp();

            if (!Directory.Exists(target + fileName))
            {
                Directory.CreateDirectory(target + fileName);
            }
            timeStampFolderPath = target + fileName;
            AttachmentPath = "Teams_Screenshot/" + fileName;
        }

        public String getFileName_Timestamp()
        {
            string scrnShot = "Screenshot_" + getTimestamp();
                                    
            return scrnShot;
        }

        public String getTimestamp()
        {
           
            string dateTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            return dateTimeStamp;
        }


        public void WriteLine(string message)
        {
            _outHelper.WriteLine(message);
        }

        public void WriteLine(string format, params object[] args)
        {
            _outHelper.WriteLine(format, args);
        }

        public void AddAttachment(string filePath)
        {
            _outHelper.AddAttachment(filePath);
        }

        public static void customizeFormat_ReportGeneration()
        {
            string livingDoc_AssemblyCommand = "livingdoc test-assembly --output-type JSON ";
            string livingDoc_featureData = "livingdoc feature-data";

            string dllPath1 = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location) + @"\TeamsWindowsApp.dll";
            string featureJson = @"C:\Temp\TestResults\FeatureData_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".json";
            string prunedJson = @"C:\Temp\TestResults\PrunedJson_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".json";
            string jsonPath1 = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location) + @"\TestExecution.json";
            string removeSkippedScenarios = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location).Split("bin")[0] + "\\Resources\\LivingDoc_Scenarios.ps1";

            System.Threading.Thread.Sleep(25000);
            string featureJsonCommand = livingDoc_AssemblyCommand + dllPath1 + " -o " + featureJson;
            Process.Start("cmd.exe", "/C " + featureJsonCommand);

            WaitUtil.WaitUntilFileExist(featureJson);
            System.Threading.Thread.Sleep(10000);

            String command = "/C powershell -executionpolicy unrestricted " + removeSkippedScenarios + " " + jsonPath1 + " " + featureJson + " " + prunedJson;
            Process.Start("cmd.exe", command);

            WaitUtil.WaitUntilFileExist(prunedJson);
            System.Threading.Thread.Sleep(30000);

            string outputReport = livingDoc_featureData + " " + prunedJson + " --output-type HTML -t " + jsonPath1 + " -o C:\\Temp\\TestResults\\TeamsApp_Execution_Report_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".html";
            Process.Start("cmd.exe", "/C " + outputReport);
        }

        public static void fullReport()
        {
            //Run Report 
            
            string livingDoc = @"livingdoc test-assembly ";
            string dllPath = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location) + @"\TeamsWindowsApp.dll -t ";
            string jsonPath = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location) + @"\TestExecution.json -o C:\Temp\TestResults\TeamsApp_Full_Execution_Report_";
            Process.Start("cmd.exe", "/C " + livingDoc + dllPath + jsonPath+ DateTime.Now.ToString("yyyyMMddHHmmssfff")+".html");
            
            System.Threading.Thread.Sleep(6000);
            
        }
    }
}
