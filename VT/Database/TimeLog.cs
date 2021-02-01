using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VT.Common;
using VT.Models.Monthly.KPIs;

namespace VT.Database
{
	public class TimeLog
	{
		public TimeLog()
		{
			TimeOffRecord = new List<TimeOffRecord>();
			TimeOffRecord.Add(new TimeOffRecord(2021, 1, Constants.JiraData.Nhu, new List<LogTime> { new LogTime(20, 0), new LogTime(21, 0) }));
			TimeOffRecord.Add(new TimeOffRecord(2021, 1, Constants.JiraData.Will, new List<LogTime> { new LogTime(8, 0), new LogTime(9, 0) }));
			TimeOffRecord.Add(new TimeOffRecord(2021, 1, Constants.JiraData.Hung, new List<LogTime> { new LogTime(4, 0), new LogTime(5, 0) }));
			TimeOffRecord.Add(new TimeOffRecord(2020, 12, Constants.JiraData.Khanh, new List<LogTime> { new LogTime(5, 4), new LogTime(15, 0), new LogTime(23, 0), new LogTime(24, 0), new LogTime(28, 0), new LogTime(29, 0) }));
			TimeOffRecord.Add(new TimeOffRecord(2020, 12, Constants.JiraData.Will, new List<LogTime> { new LogTime(1, 5), new LogTime(30, 0), new LogTime(31, 0) }));
			TimeOffRecord.Add(new TimeOffRecord(2020, 12, Constants.JiraData.Quang, new List<LogTime> { new LogTime(21, 0), new LogTime(22, 0), new LogTime(30, 0), new LogTime(31, 0) }));
			TimeOffRecord.Add(new TimeOffRecord(2020, 12, Constants.JiraData.AnhHoang, new List<LogTime> { new LogTime(7, 0), new LogTime(8, 0) }));
			TimeOffRecord.Add(new TimeOffRecord(2020, 12, Constants.JiraData.Hung, new List<LogTime> { new LogTime(3, 0), new LogTime(4, 0), new LogTime(28, 0) }));
			TimeOffRecord.Add(new TimeOffRecord(2020, 12, Constants.JiraData.Nhu, new List<LogTime> { new LogTime(7, 0), new LogTime(8, 0), new LogTime(9, 0), new LogTime(10, 0), new LogTime(11, 0), new LogTime(24, 0), new LogTime(30, 0), new LogTime(31, 0) }));

			CompanyHoliday = new List<TimeOffRecord>();
			CompanyHoliday.Add(new TimeOffRecord(2021, 1, Constants.JiraData.Company, new List<LogTime> { new LogTime(1, 0) }));
			CompanyHoliday.Add(new TimeOffRecord(2020, 12, Constants.JiraData.Company, new List<LogTime> { new LogTime(25, 0) }));
		}

		public List<TimeOffRecord> TimeOffRecord { get; set; }
		public List<TimeOffRecord> CompanyHoliday { get; set; }
	}

	public class LogTime
	{
		public LogTime(int day, int workingHours)
		{
			Day = day;
			WorkingHour = workingHours;
		}
		public int Day { get; set; }
		public int WorkingHour { get; set; }
		public int NormalWorkingHours { get { return 8; } }
	}

	public class TimeOffRecord
	{
		public TimeOffRecord(int year, int month, string name, List<LogTime> listOfDays)
		{
			Year = year;
			Month = month;
			History = new Dictionary<string, List<LogTime>>();
			History.Add(name, listOfDays);
		}

		public int Year { get; set; }
		public int Month { get; set; }
		public Dictionary<string, List<LogTime>> History { get; set; }
	}
}
