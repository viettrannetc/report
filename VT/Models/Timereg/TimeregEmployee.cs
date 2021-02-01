using System;
using System.Collections.Generic;
using System.Linq;
using VT.Common;
using VT.Database;

namespace VT.Models.Timereg
{
	public class TimeregMonthlyModelBasic
	{
		public TimeregMonthlyModelBasic(TimeregExcelModel timeregExcelData)
		{
			Month = timeregExcelData.Month;
			Year = timeregExcelData.Year;
			Employees = new List<TimeregEmployeeModel>();
		}

		public List<TimeregEmployeeModel> Employees { get; set; }
		public int Month { get; set; }
		public int Year { get; set; }
		public string Time { get { return $"{Month}/{Year}"; } }

		public int NormalWorkingDay
		{
			get
			{
				var result = 0;
				for (int i = 1; i < DateTime.DaysInMonth(Year, Month); i++)
				{
					var today = new DateTime(Year, Month, i);

					if (today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday || Constants.TimeOff.CompanyHolidays.Any(hd => hd.Month == Month && hd.Year == Year && hd.History.Any(d => d.Value.Any(v => v.Day == i))))
						continue;

					result++;
				}
				return result;
			}
		}
		public decimal TotalNormalWorkingHours
		{
			get
			{
				return Constants.JiraData.EmployeeData.Count * TotalNormalWorkingHoursPerPerson;
			}
		}
		public decimal TotalNormalWorkingHoursPerPerson
		{
			get
			{
				return NormalWorkingDay * 8;
			}
		}

		public decimal TotalLoggedHours
		{
			get
			{
				return Employees.Sum(e => e.Timeregs.Sum(t => t.TotalTime));
			}
		}
		public decimal TotalLoggedHoursForNC
		{
			get
			{
				return Employees.Sum(e => e.Timeregs.Where(tr => !tr.IsASHours && !tr.IsHoursOff).Sum(t => t.TotalTime));
			}
		}
		public decimal TotalLoggedHoursForAS
		{
			get
			{
				return Employees.Sum(e => e.Timeregs.Where(tr => tr.IsASHours).Sum(t => t.TotalTime));
			}
		}

		public decimal TotalHoursOff { get { return Employees.Sum(e => e.Timeregs.Where(tr => tr.IsHoursOff).Sum(t => t.TotalTime)); } }
		
		public decimal TotalWorkingHours { get { return TotalLoggedHoursForAS; } }
		public decimal TotalBAHours
		{
			get
			{
				var developers = Constants.JiraData.EmployeeData.Where(e => e.NumberOfBA > 0).ToList();
				decimal result = 0;
				foreach (var developer in developers)
				{
					decimal totalWorkingTimeOfPeople = Employees.First(e => e.JiraName == developer.JiraName).Timeregs.Where(tr => tr.IsASHours).Sum(t => t.TotalTime);
					result += totalWorkingTimeOfPeople * developer.NumberOfBA;
				}

				return result;
			}
		}

		public decimal TotalDevHours
		{
			get
			{
				var developers = Constants.JiraData.EmployeeData.Where(e => e.NumberOfDev > 0).ToList();
				decimal result = 0;
				foreach (var developer in developers)
				{
					decimal totalWorkingTimeOfPeople = Employees.First(e => e.JiraName == developer.JiraName).Timeregs.Where(tr => tr.IsASHours).Sum(t => t.TotalTime);
					result += totalWorkingTimeOfPeople * developer.NumberOfDev;
				}

				return result;
			}
		}

		public decimal TotalQAHours
		{
			get
			{
				var developers = Constants.JiraData.EmployeeData.Where(e => e.NumberOfQC > 0).ToList();
				decimal result = 0;
				foreach (var developer in developers)
				{
					decimal totalWorkingTimeOfPeople = Employees.First(e => e.JiraName == developer.JiraName).Timeregs.Where(tr => tr.IsASHours).Sum(t => t.TotalTime);
					result += totalWorkingTimeOfPeople * developer.NumberOfQC;
				}

				return result;
			}
		}

		public decimal FTEDevResource { get { return TotalDevHours / TotalNormalWorkingHoursPerPerson; } }
	}

	public class TimeregMonthlyModel : TimeregMonthlyModelBasic
	{
		public TimeregMonthlyModel(TimeregExcelModel timeregExcelData)
			: base(timeregExcelData)
		{

		}
	}

	public class TimeregEmployeeModel
	{
		public TimeregEmployeeModel()
		{
			Timeregs = new List<TimeregModel>();
		}
		public List<TimeregModel> Timeregs { get; set; }
		public string JiraName { get; set; }
		public string NCName { get; set; }
		public decimal TotalLoggedHours
		{
			get
			{
				return Timeregs.Sum(t => t.TotalTime);
			}
		}
	}

	public class TimeregModel
	{
		public TimeregModel()
		{
			Timeregs = new List<TimeregDetail>();
		}
		public string Code { get; set; }
		public decimal TotalTime
		{
			get
			{
				return Timeregs.Sum(t => t.TimeSpent);
			}
		}
		public List<TimeregDetail> Timeregs { get; set; }

		//public bool IsNCHours { get { return !Constants.TimeOff.CodesWorkingAdstream.Contains(Code); } }
		public bool IsASHours { get { return Constants.TimeOff.CodesWorkingAdstream.Any(c => Code.Contains(c)); } } //TODO: need to work on the correct code
		public bool IsHoursOff { get { return Constants.TimeOff.CodesTimeOff.Contains(Code); } }
	}

	public class TimeregDetail
	{
		public decimal TimeSpent { get; set; }
		public DateTime Date { get; set; }
	}

	//////////////////
	//public class ASTimereg
	//{
	//	public ASTimereg()
	//	{
	//		Timeregs = new List<ASTimeregDetail>();
	//	}
	//	public string Code { get; set; }
	//	public decimal TotalTime { get; set; }
	//	public List<ASTimeregDetail> Timeregs { get; set; }
	//}

	//public class ASTimeregDetail
	//{
	//	public decimal Time { get; set; }
	//	public DateTime Date { get; set; }
	//}
}
