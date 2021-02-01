using System;
using System.Collections.Generic;
using VT.Database;

namespace VT.Common
{
	public static class Constants
	{
		public static string TimeregNCData = @"D:\\Software\\Tools\\Report\\VT\\Test\\Timereg\\2020-10-01.xlsx";
		public static string JiraDataFolder = @"D:\\Software\\Tools\\Report\\VT\\Test\\Data";
		public static DateTime Start = new DateTime(2020, 01, 01);
		public static DateTime End = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month));

		public static class TimeOff
		{
			public static readonly List<TimeOffRecord> TimeOffHistories = new List<TimeOffRecord>(){

				new TimeOffRecord(2021, 1, Constants.JiraData.Nhu, new List<LogTime> { new LogTime(20, 0), new LogTime(21, 0) }),
				new TimeOffRecord(2021, 1, Constants.JiraData.Will, new List<LogTime> { new LogTime(8, 0), new LogTime(9, 0) }),
				new TimeOffRecord(2021, 1, Constants.JiraData.Hung, new List<LogTime> { new LogTime(4, 0), new LogTime(5, 0) }),
				new TimeOffRecord(2020, 12, Constants.JiraData.Khanh, new List<LogTime> { new LogTime(5, 4), new LogTime(15, 0), new LogTime(23, 0), new LogTime(24, 0), new LogTime(28, 0), new LogTime(29, 0) }),
				new TimeOffRecord(2020, 12, Constants.JiraData.Lam, new List<LogTime> { new LogTime(14, 4), new LogTime(31, 0) }),
				new TimeOffRecord(2020, 12, Constants.JiraData.Will, new List<LogTime> { new LogTime(1, 5), new LogTime(30, 0), new LogTime(31, 0) }),
				new TimeOffRecord(2020, 12, Constants.JiraData.Quang, new List<LogTime> { new LogTime(21, 0), new LogTime(22, 0), new LogTime(30, 0), new LogTime(31, 0) }),
				new TimeOffRecord(2020, 12, Constants.JiraData.AnhHoang, new List<LogTime> { new LogTime(7, 0), new LogTime(8, 0) }),
				new TimeOffRecord(2020, 12, Constants.JiraData.Hung, new List<LogTime> { new LogTime(3, 0), new LogTime(4, 0), new LogTime(28, 0) }),
				new TimeOffRecord(2020, 12, Constants.JiraData.Nhu, new List<LogTime> { new LogTime(7, 0), new LogTime(8, 0), new LogTime(9, 0), new LogTime(10, 0), new LogTime(11, 0), new LogTime(24, 0), new LogTime(30, 0), new LogTime(31, 0) }),
			};

			public static readonly List<TimeOffRecord> CompanyHolidays = new List<TimeOffRecord>()
			{
				new TimeOffRecord(2021, 1, Constants.JiraData.Company, new List<LogTime> { new LogTime(1, 0) }),
				new TimeOffRecord(2020, 12, Constants.JiraData.Company, new List<LogTime> { new LogTime(25, 0) })
			};

			public static readonly string[] CodesTimeOff = new string[3] { "Illness and doctor visits", "Illness and doctors visits", "Vacation" };
			public static readonly string[] CodesWorkingAdstream = new string[1] { "Adstream IT" };
		}

		public static class JiraStatus
		{
			public static string Done = "Done";
			public static string Closed = "Closed";
			public static string PassedTesting = "Passed Testing";
			public static string InAnalysis = "In Analysis";
			public static string InDevelopment = "In Development";
			public static string InReview = "In Review";
			public static string ReadyForDev = "Ready for Dev";
			public static string ReadyForTesting = "Ready for Testing";
			public static string InTesting = "In Testing";

			public static readonly List<string> DoneStatus = new List<string>() { PassedTesting, Done, "On PreProd", "On Live", Closed };
		}

		public static class JiraData
		{
			public static string Will = "Will.Trinh";
			public static string Nhu = "Nhu.Vo";
			public static string Hung = "Hung.Nguyen";
			public static string Tri = "tri.ledung";
			public static string Quang = "Quang.Tran";
			public static string Khanh = "Khanh.Pham";
			public static string Henry = "Henry.Le";
			public static string AnhHoang = "Anh.Hoang";
			public static string Viet = "viet.tran";
			public static string Lam = "Lam.Ninh"; //QA
			public static string Thu = "Thu.Nguyen"; //QA
			public static string Company = "Netcompany";

			public static readonly List<string> DevelopmentTeam = new List<string>() { Viet, Nhu, Khanh, Quang, Tri, Will, Hung, AnhHoang, Henry };

			public static readonly List<EmployeeData> EmployeeData = new List<EmployeeData>() {
				new EmployeeData {  JiraName = Will, NCName = "TXT", NumberOfDev = 1 },
				new EmployeeData {  JiraName = Nhu, NCName = "VNQN", NumberOfDev = 0.5m, NumberOfBA = 0.5m },
				new EmployeeData {  JiraName = Hung, NCName = "NMH", NumberOfDev = 1 },
				new EmployeeData {  JiraName = Tri, NCName = "LDT", NumberOfDev = 1, EndDate = new DateTime (2020, 12, 24) },
				new EmployeeData {  JiraName = Quang, NCName = "TMQ", NumberOfDev = 1 },
				new EmployeeData {  JiraName = Khanh, NCName = "PDTK", NumberOfDev = 1 },
				new EmployeeData {  JiraName = Henry, NCName = "LHH", NumberOfDev = 1 },
				new EmployeeData {  JiraName = AnhHoang, NCName = "HTA", NumberOfDev = 1 , EndDate = new DateTime (2020, 12, 18) },
				//new EmployeeData {  JiraName = Viet, NCName = "VITRA", NumberOfPM = 1 },
				//new EmployeeData {  JiraName = Lam, NCName = "lam.ninh", NumberOfQC = 1 },							//TODO: data in Dec
				//new EmployeeData {  JiraName = Thu, NCName = "NTVT", NumberOfQC = 1 , EndDate = new DateTime (2020, 12, 04) },
			};
		}


	}
}
