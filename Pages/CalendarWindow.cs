using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TeamsWindowsApp.Driver;
using TeamsWindowsApp.Helper;
using FluentAssertions;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow.Infrastructure;
using TeamsWindowsApp.Objects;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace TeamsWindowsApp.Helper
{
	public class CalendarWindow
	{
		private readonly WinAppDriver _driver;
		Reporting reportLine;
		private static ISpecFlowOutputHelper _outHelper;
		private readonly ConfigurationDriver _configurationDriver;

		public CalendarWindow(WinAppDriver driver, ISpecFlowOutputHelper outHelper, ConfigurationDriver configurationDriver)
		{
			_driver = driver;
			reportLine = new Reporting(outHelper);
			_configurationDriver = configurationDriver;
		}

		public static class ScheduleTime
		{
			public static string sTestTime;
			public static string sStartDate;
			public static string sEndDate;
		}

		private void SwitchWindowHandler()
		{
			IntPtr windowHandler = CommonUtil.GetMainWindowHandle("Teams");
			Console.WriteLine(windowHandler);
			_driver.Current.SwitchTo().Window(windowHandler.ToString("X"));
		}

		private void TextInputByKeyboardAction(string TargetElem, string value)
		{
			_driver.Current.FindElementByName(TargetElem).SendKeys(Keys.Control + "a" + Keys.Control);
			_driver.Current.FindElementByName(TargetElem).SendKeys(Keys.Delete);
			_driver.Current.FindElementByName(TargetElem).SendKeys(value);
		}

		private void StartsEndFieldInputonRecurrenceDialog(string sDate, string eDate)
		{
			try
			{
				string sStarts = CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_RecurrenceDialog_Sdate);
				string dialog = CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_RecurrenceDialog);
				_driver.Current.FindElementByName(dialog).FindElementByName(sStarts).Click();
				if (sDate.Contains("/"))
				{
					TextInputByKeyboardAction(sStarts, sDate);
					_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_RecurrenceDialog)).Click();
				}
				else
				{
					SelectCalandarPickerDate(sDate);
				}

				System.Threading.Thread.Sleep(1000);
				string sEnd = CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_RecurrenceDialog_Edate);
				// Same name but different tag exists on same view. Text[@Name=\"End\"]
				_driver.Current.FindElementByName(dialog).FindElementByXPath("//ComboBox[@Name=\"" + sEnd + "\"]").Click();
				if (eDate.Contains("/"))
				{
					_driver.Current.FindElementByName(sEnd).Click();
					TextInputByKeyboardAction(sEnd, eDate);
					_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_RecurrenceDialog)).Click();
				}
				else
				{
					SelectCalandarPickerDate(eDate);
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "StartsEndFieldInputonRecurrenceDialog");
			}
		}

		private void SelectCalandarPickerDate(string sDateNum)
		{
			try
			{
				//Console.WriteLine("SelectCalandarPickerDate START");
				DateTime moment = DateTime.Now;
				DateTime itargetDate = DateTime.Now.AddDays(Int32.Parse(sDateNum));
				string targetDate = null;
				string sTargetMonth = null;
				if (itargetDate.Day < 10)
				{
					targetDate = itargetDate.ToString("MMMM d, yyyy");
				}
				else
				{
					targetDate = itargetDate.ToString("MMMM dd, yyyy");
				}
				sTargetMonth = itargetDate.ToString("MMMM yyyy");
				string sPickerTextMonth = _driver.Current.FindElementByXPath(TeamsAppObjects.xpath_cal_DatePickerMonthTitle).Text;
				if (sPickerTextMonth == sTargetMonth)
				{
					Console.WriteLine("DATE PICKER CURRENT MONTH");
					_driver.Current.FindElementByName(targetDate).Click();
					System.Threading.Thread.Sleep(2000);
				}
				else if (itargetDate.Month > moment.Month)
				{
					int num = itargetDate.Month - moment.Month;
					for (int i = 0; i < num; i++)
					{
						string sTargetM = moment.AddMonths(i + 1).ToString("MMMM");
						_driver.Current.FindElementByName(CommonUtil.ConvertLang("Next month ") + sTargetM).Click();
					}
					System.Threading.Thread.Sleep(1000);
					_driver.Current.FindElementByName(targetDate).Click();
				}
				else
				{
					int num = itargetDate.Month - moment.Month;
					int testNum = moment.Month + num;
					for (int i = 0; i < testNum; i++)
					{
						string sTargetM = moment.AddMonths(i + 1).ToString("MMMM");
						_driver.Current.FindElementByName(CommonUtil.ConvertLang("Next month ") + sTargetM).Click();
					}
					System.Threading.Thread.Sleep(1000);
					Console.WriteLine(targetDate);
					_driver.Current.FindElementByName(targetDate).Click();
				}

				if (ScheduleTime.sStartDate == null && ScheduleTime.sEndDate == null)
                {
					ScheduleTime.sStartDate = targetDate;
					Console.WriteLine("START DATE: {0}", ScheduleTime.sStartDate);
				}
				else if (ScheduleTime.sStartDate != null && ScheduleTime.sEndDate == null)
                {
					ScheduleTime.sEndDate = targetDate;
					Console.WriteLine("END DATE: {0}", ScheduleTime.sEndDate);
				}
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "SelectCalandarPickerDate");
			}
		}


		private void RepeatFiealdInput(string type, string value)
		{
			string sTypeToRepeat = CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_Repeat.Replace("#TYPE#", type));
			_driver.Current.FindElementByName(sTypeToRepeat).Click();
			TextInputByKeyboardAction(sTypeToRepeat, value);
		}

		private void EveryWeekdayInput(string sDate, string eDate, string span)
		{
			//span
			RepeatFiealdInput("Weeks", span);

			//weekdate
			_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_RecurrenceDialog)).FindElementByName("Monday").Click();
			_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_RecurrenceDialog)).FindElementByName("Saturday").Click();

			//date
			StartsEndFieldInputonRecurrenceDialog(sDate, eDate);
		}

		private void DailyInput(string sDate, string eDate, string span)
		{
			RepeatFiealdInput("Days", span);
			StartsEndFieldInputonRecurrenceDialog(sDate, eDate);
		}

		private void WeeklyInput(string sDate, string eDate, string span, string[] weekdays)
		{
			//date
			StartsEndFieldInputonRecurrenceDialog(sDate, eDate);
			//weekdate
			DateTime dt = DateTime.Now;
			string todaysDate = dt.ToString("dddd");
			foreach (string weekday in weekdays)
			{
				if (weekday != todaysDate)
				{
					Console.WriteLine(weekday);
					_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_RecurrenceDialog)).FindElementByName(weekday).Click();
				}
			}
			RepeatFiealdInput("Weeks", span);
		}

		private void MonthlyInput(string sDate, string eDate, string span, string targetDate) //sus
		{
			string[] targetDateWords = targetDate.Split(" ");
			var dialog = _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_RecurrenceDialog));
			if (targetDateWords[1] == "day")
			{
				string Onday = CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_RecurrenceDialog_Tdate);
				Console.WriteLine("1:{0}, 2:{1}", targetDateWords[1], targetDateWords[2]);
				dialog.FindElementByXPath("//RadioButton[starts-with(@Name,\"On day\")]").Click();
				//dialog.FindElementByXPath("//Spinner[@Name=\"On day\"][starts-with(@AutomationId,\"TextField\")]").Click();
				dialog.FindElementByName(Onday).Click();
				dialog.FindElementByName(Onday).SendKeys(Keys.Control + "a" + Keys.Control);
				dialog.FindElementByName(Onday).SendKeys(Keys.Delete);
				Console.WriteLine(targetDateWords[2]);
				dialog.FindElementByName(Onday).SendKeys(targetDateWords[2]);
				dialog.Click();
			}
			else
			{
				string sStartDate = targetDateWords[3].ToLower();
				string sWeekNo = targetDateWords[2].ToLower();
				sStartDate = Char.ToUpper(sStartDate[0]) + sStartDate.Substring(1).ToLower();
				sWeekNo = Char.ToUpper(sWeekNo[0]) + sWeekNo.Substring(1).ToLower();
				dialog.FindElementByXPath("//RadioButton[starts-with(@Name,\"On the\")]").Click();
				dialog.FindElementsByName("On")[0].Click();
				_driver.Current.FindElementByName(sWeekNo).Click();
				dialog.FindElementsByName("On")[1].Click();
				_driver.Current.FindElementByName(sStartDate).Click();
			}
			//date
			RepeatFiealdInput("Months", span);
			StartsEndFieldInputonRecurrenceDialog(sDate, eDate);
		}

		private void YearlyInput(string sDate, string eDate, string span, string targetDate) //sus
		{
			string[] targetDateWords = targetDate.Split(" ");
			//var targetfields = _driver.Current.FindElementsByClassName("RadioButton");
			var dialog = _driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_RecurrenceDialog));
			if (targetDateWords[1] == "the")
			{
				string sWeekNo = targetDateWords[2].ToLower();
				string sStartDate = targetDateWords[3].ToLower();
				string sMonth = targetDateWords[5].ToLower();
				sWeekNo = Char.ToUpper(sWeekNo[0]) + sWeekNo.Substring(1);
				sStartDate = Char.ToUpper(sStartDate[0]) + sStartDate.Substring(1);
				sMonth = Char.ToUpper(sMonth[0]) + sMonth.Substring(1);
				dialog.FindElementByXPath("//RadioButton[starts-with(@Name,\"On the\")]").Click();
				dialog.FindElementsByName("On")[0].Click();
				_driver.Current.FindElementByName(sWeekNo).Click();
				dialog.FindElementsByName("On")[1].Click();
				_driver.Current.FindElementByName(sStartDate).Click();
				dialog.FindElementsByName("Month")[1].Click();
				_driver.Current.FindElementByName(sMonth).Click();
			}
			else
			{
				string sMonth = targetDateWords[1].ToLower();
				sMonth = Char.ToUpper(sMonth[0]) + sMonth.Substring(1);
				dialog.FindElementByXPath("//RadioButton[starts-with(@Name,\"On\")]").Click();
				dialog.FindElementByName("Month").Click();
				_driver.Current.FindElementByName(sMonth).Click();
				var dateField = dialog.FindElementByXPath("//Spinner[@Name=\"On day\"][starts-with(@AutomationId,\"TextField\")]");
				dateField.Click();
				dateField.SendKeys(Keys.Control + "a" + Keys.Control);
				dateField.SendKeys(Keys.Delete);
				dateField.SendKeys(targetDateWords[2]);
			}
			//date
			StartsEndFieldInputonRecurrenceDialog(sDate, eDate);
		}

		public Boolean OpenCalendarEditView()
		{
			try
            {
				System.Threading.Thread.Sleep(5000);
				//_driver.Current.FindElementByXPath(TeamsAppObjects.xpath_cal_CreateNewMeetingButton).Click();
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_cal_CreateNewMeetingButton).Click();
				System.Threading.Thread.Sleep(3000);
				SwitchWindowHandler();
				return WaitUtil.WaitForElementAvailability(TeamsAppObjects.xpath_cal_MeetingDetailView, _driver.Current);
			}
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "OpenCalendarEditView");
				return false;
			}
		}

		public Boolean InputTitle(string title)
		{
            try
            {
				if (ScheduleTime.sTestTime == null)
				{
					ScheduleTime.sTestTime = DateTime.Now.ToString("s");
				}
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_cal_EditTitle).Click();
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_cal_EditTitle).SendKeys(title + ":" + ScheduleTime.sTestTime);
				Console.WriteLine("INPUT TITLE: {0}", title + ":" + ScheduleTime.sTestTime);
				return true;
            }
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "InputTitle");
				return false;
			}
		}

		public Boolean InputAttendees(string[] users)
		{
            try
            {
				foreach (string user in users)
				{
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_cal_EditAttendee).Click();
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_cal_EditAttendee).SendKeys(DataReader.getUserName(user));
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_cal_EditAttendeelist_0).Click();
					System.Threading.Thread.Sleep(5000);
				}
				return true;
            }
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "InputAttendees");
				return false;
			}
		}

		public Boolean InputOptionalAttendees(string[] users)
		{
            try
            {
				if (_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_AddOptionalButton)).Displayed)
				{
					_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_AddOptionalButton)).Click();
				}
				foreach (string user in users)
				{
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_cal_EditOptionalAttendeeField).Click();
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_cal_EditOptionalAttendeeField).SendKeys(DataReader.getUserName(user));
					_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_cal_EditAttendeelist_0).Click();
					System.Threading.Thread.Sleep(5000);
				}
				return true;
            }
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "InputOptionalAttendees");
				return false;
			}
		}

		public void InputStartDateTime(string date, string time)
		{
			try
            {
				//date
				string sDateField = CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditStartDate);
				_driver.Current.FindElementByName(sDateField).Click();
				Console.WriteLine("==== Input START date time ====");
				SelectCalandarPickerDate(date);

				//time
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditStartTime)).Click();
				_driver.Current.FindElementByName(time).Click();
				System.Threading.Thread.Sleep(2000);
			}
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "InputStartDateTime");
			}
		}

		public void InputEndDateTime(string date, string time)
		{
			try
            {
				//date
				string eDateField = CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditEndDate);
				_driver.Current.FindElementByName(eDateField).Click();
				Console.WriteLine("==== Input End date time ====");
				SelectCalandarPickerDate(date);
				//time
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditEndTime)).Click();
				_driver.Current.FindElementByName(time).Click();
				System.Threading.Thread.Sleep(2000);
			}
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "InputEndDateTime");
			}
		}

		/*
		public void CheckAllDay()
		{
			try
            {
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditAllDay)).Click();
            }
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "CheckAllDay");
			}
		}

		public void InputRecurrence(string type, string startDate = null, string endDate = null, string reccurence = null, string target = null, string customType = null)
		{
			try
            {
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditRecurrence)).Click();
				_driver.Current.FindElementByName(type).Click();

				//setting dialog
				switch (type)
				{
					case "Does not repeat":
						break;
					case "Every weekday (Mon - Fri)":
						EveryWeekdayInput(startDate, endDate, reccurence);
						break;
					case "Daily":
						DailyInput(startDate, endDate, reccurence);
						break;
					case "Weekly":
						string[] weekdays = target.Split(",");
						WeeklyInput(startDate, endDate, reccurence, weekdays);
						break;
					case "Monthly":
						MonthlyInput(startDate, endDate, reccurence, target);
						break;
					case "Yearly":
						YearlyInput(startDate, endDate, reccurence, target);
						break;
					case "Custom":
						break;
				}
				string saveButton = CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditSaveButton);
				string dialog = CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_RecurrenceDialog);
				_driver.Current.FindElementByName(dialog).FindElementByName(saveButton).Click();
            }
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "InputRecurrence");
			}
		}

		

		public void InputAddChannel(string text)
		{
			try
            {
				string sAddCh = CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditAddChannel);
				_driver.Current.FindElementByName(sAddCh).Click();
				_driver.Current.FindElementByName(sAddCh).SendKeys(text);
            }
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "InputAddChannel");
			}
		}

		public void InputAddLocation(string text)
		{
			try
            {
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_cal_EditLocation).Click();
				_driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_cal_EditLocation).SendKeys(text);
            }
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "InputAddLocation");
			}
		}
		*/

		public Boolean InputDescription(string text)
		{
			try
            {
				string sDescription = CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditDescription);
				Actions action = new Actions(_driver.Current);
				action.MoveToElement(_driver.Current.FindElementByName(sDescription), 10, 10).Click().SendKeys(text).Perform();
				System.Threading.Thread.Sleep(5000);
				return true;
            }
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "InputDescription");
				return false;
			}
		}

		public Boolean ClickScheduleEditSave()
		{
            try
            {
				Console.WriteLine("BEFORE SAVE: {0}", ScheduleTime.sTestTime);
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditSaveButton)).Click();
				System.Threading.Thread.Sleep(30000);
				SwitchWindowHandler();
				return WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_cal_CreateNewMeetingButton, _driver.Current);
			}
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "ClickScheduleEditSave");
				return false;
			}
		}

		public Boolean ClickScheduleEditSend()
		{
            try
            {
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditSendButton)).Click();
				System.Threading.Thread.Sleep(30000);
				SwitchWindowHandler();
				return WaitUtil.WaitForElementAvailability(TeamsAppObjects.autoId_cal_CreateNewMeetingButton, _driver.Current);
			}
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "ClickScheduleEditSend");
				return false;
			}
		}

		/*
		public void ClickScheduleEditCancel(string type)
		{
			try
            {
				var Header = _driver.Current.FindElementByAccessibilityId(TeamsAppObjects.autoId_cal_EditHeader);
				Header.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditCloseButton)).Click();
				switch (type.ToUpper())
				{
					case "DISCARD":
						_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_CloseDialogDiscardButton)).Click();
						break;
					case "CONTINUE":
						_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_CloseDialogContinueEdit)).Click();
						break;
				}
            }
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "ClickScheduleEditCancel");
			}
		}

		public Boolean CkeckEditViewOpen()
		{
			try
            {
				return WaitUtil.WaitForElementAvailability(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_EditCloseButton), _driver.Current, 3000);
            }
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "CkeckEditViewOpen");
				return false;
			}
		}
		*/


		// Need to update this script so that this method can verify the schedule when user created on the week after!
		public Boolean VerifyCreatedSchedule(string sTitle)
		{
			try
            {
				DateTime startdate = Convert.ToDateTime(DateTime.Now);
				System.Threading.Thread.Sleep(5000);
				Console.WriteLine("GLOBAL STARTING DATE: {0}", ScheduleTime.sStartDate);
				string targetElem = "//Group[starts-with(@Name,\"" + sTitle + ":" + ScheduleTime.sTestTime + "\")]";
				DateTime moment = Convert.ToDateTime(DateTime.Now);
				if (ScheduleTime.sStartDate != null)
                {
					startdate = Convert.ToDateTime(ScheduleTime.sStartDate);
                }
				Console.WriteLine("DATES: {0}, {1}", startdate, moment);
				TimeSpan DIFF = startdate - moment;
				Console.WriteLine("CALCURATION: {0}", DIFF.Days / 7);
				int num = DIFF.Days / 7;
				Console.WriteLine(num);
				int i = 1;
				Console.WriteLine("Target Elem: {0}", targetElem);
				Console.WriteLine("Target Elem: {0}", TeamsAppObjects.xpath_cal_NextMonthViewButton);
				WaitUtil.WaitForElementAvailability(TeamsAppObjects.xpath_cal_NextMonthViewButton, _driver.Current);
				while (i < num+1)
				{
					_driver.Current.FindElementByXPath(TeamsAppObjects.xpath_cal_NextMonthViewButton).Click();
					System.Threading.Thread.Sleep(3000);
					i += 1;
				}
				_driver.Current.FindElementByXPath(TeamsAppObjects.xpath_cal_NextMonthViewButton).Click();
				WaitUtil.WaitForElementAvailability(TeamsAppObjects.xpath_cal_NextPreviousViewButton, _driver.Current);
				_driver.Current.FindElementByXPath(TeamsAppObjects.xpath_cal_NextPreviousViewButton).Click();
				if (WaitUtil.WaitForElementAvailability(targetElem, _driver.Current))
				{
					reportLine.TakeScreenShot(_driver.Current, "VerifyCreatedSchedule_succeed");
					return true;
				}
				return false;
			}
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "VerifyCreatedSchedule_failed");
				return false;
			}
		}

		/*
		public void OpenEventInfo(string sTitle)
		{
			try
            {
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xpath_cal_EventGridView)).FindElementByName(sTitle).Click();
				System.Threading.Thread.Sleep(2000);
            }
			catch (Exception e)
            {
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "OpenEventInfo");
			}
		}

		//check not fixed yet
		public void StartVideoCallByLink()
		{
			try
            {
				_driver.Current.FindElementByXPath("//Text[starts-with(@Name,\"https://teams.microsoft.com\")]").Click();
				System.Threading.Thread.Sleep(4000);
            }
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "StartVideoCallByLink");
			}
		}

		public void StartVideoCallByJoinButton()
        {
			try
            {
				_driver.Current.FindElementByName(CommonUtil.ConvertLang(TeamsAppObjects.xname_cal_JoinMTGButton)).Click();
				System.Threading.Thread.Sleep(4000);
            }
			catch (Exception e)
			{
				reportLine.WriteLine(e.ToString());
				reportLine.TakeScreenShot(_driver.Current, "StartVideoCallByJoinButton");
			}
		}
		*/
	}
}
