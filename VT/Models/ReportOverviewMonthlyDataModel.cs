using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using VT.Common;
using VT.Extension;

namespace VT.Model
{
	/// <summary>
	/// Overview in month
	///- how many FTE resource that we have in month? get it from HR team
	///- story points have been burnt in last month in total
	///- story points have been burnt in last month per person
	///- compare with KPI 002 
	///- Ticket has been re-opened from review? Compare with KPI 003
	///- Ticket has been re-opened from QC? Compare with KPI 004
	///- Metric - get from tickets
	///		- defect - total time? 
	///		- defect - total completed per month
	///		- defect - completed per developer
	///		- defect - reopen by review
	///		- defect - review by QC 
	///		- defect - regression efficiency
	///		- US/TA - time for story?
	///		- US/TA - total completed per month
	///		- US/TA - total story points completed per month
	///		- US/TA - story points completed per developer
	///		- US/TA - reopen by review
	///		- US/TA - review by QC
	/// </summary>
	public class ReportOverviewMonthlyDataModel
	{
		public ReportOverviewMonthlyDataModel(List<ReportRawDataModel> reportDataModels, int month)
		{
			Month = month;
			ReportMonthlyDataModels = reportDataModels.Where(r => r.PulledDataAt.Month == month).ToList();

			KPI002 = new ReportOverviewMonthlyKPI002DataModel(ReportMonthlyDataModels);
		}

		public List<ReportRawDataModel> ReportMonthlyDataModels { get; set; }
		public int Month { get; set; }

		public List<ReportOverviewMonthlyResourceDataModel> NumberOfDevelopers
		{
			get
			{
				var result = new List<ReportOverviewMonthlyResourceDataModel>();
				result.Add(new ReportOverviewMonthlyResourceDataModel(2020, 9, 7.5m));
				result.Add(new ReportOverviewMonthlyResourceDataModel(2020, 10, 7.5m));
				return result;
			}
		}

		/// <summary>
		/// Full time employee (8 working hrs per day)
		/// </summary>
		public List<ReportOverviewMonthlyResourceDataModel> FTE
		{
			get
			{
				var result = new List<ReportOverviewMonthlyResourceDataModel>();
				result.Add(new ReportOverviewMonthlyResourceDataModel(2020, 9, 6.89m));
				result.Add(new ReportOverviewMonthlyResourceDataModel(2020, 10, 7.2m));
				return result;
			}
		}

		public ReportOverviewMonthlyKPI002DataModel KPI002 { get; set; }
		public ReportOverviewMonthlyKPI003DataModel KPI003 { get; set; }
		public ReportOverviewMonthlyKPI004DataModel KPI004 { get; set; }
		public ReportOverviewMonthlyKPI005DataModel KPI005 { get; set; }
		public ReportOverviewMonthlyKPI006DataModel KPI006 { get; set; }
		public ReportOverviewMonthlyMetricDataModel Metric { get; set; }
		public ReportOverviewMonthlyDeveloperNumberDataModel DeveloperNumber { get; set; }
	}

	public class ReportOverviewMonthlyKPI002DataModel : ReportOverviewMonthlyKPIDataModel
	{
		public ReportOverviewMonthlyKPI002DataModel(List<ReportRawDataModel> ticketHistories)
		{
			if (!ticketHistories.Any()) return;

			foreach (var ticketHistory in ticketHistories)
			{
				if (!TicketHistory.Any(th => th.IssueKey == ticketHistory.IssueKey))
					TicketHistory.Add(new OverviewMonthlyTicketDataModel() { IssueKey = ticketHistory.IssueKey, Histories = new List<ReportRawDataModel>() { } });

				TicketHistory.First(th => th.IssueKey == ticketHistory.IssueKey).Histories.Add(ticketHistory);
			}

			Month = TicketHistory.First().Histories.First().PulledDataAt.Month;
		}

		private int Month { get; set; }
		private List<OverviewMonthlyKPI002DataModel> GenerateData()
		{
			var result = new List<OverviewMonthlyKPI002DataModel>();
			result.Add(new OverviewMonthlyKPI002DataModel(9, 6.2m, 2020, 1, 20.82m, 127, 58));
			result.Add(new OverviewMonthlyKPI002DataModel(9, 7.66m, 2020, 2, 22.84m, 142, 72));
			result.Add(new OverviewMonthlyKPI002DataModel(10, 9.59m, 2020, 3, 22.84m, 216, 183));
			result.Add(new OverviewMonthlyKPI002DataModel(10, 9.24m, 2020, 4, 22.84m, 175.5m, 230));
			result.Add(new OverviewMonthlyKPI002DataModel(8, 7.37m, 2020, 5, 22.84m, 115, 182));
			result.Add(new OverviewMonthlyKPI002DataModel(6, 4.84m, 2020, 6, 22.84m, 111.87m, 185));
			result.Add(new OverviewMonthlyKPI002DataModel(7, 6.29m, 2020, 7, 22.84m, 193, 186));
			result.Add(new OverviewMonthlyKPI002DataModel(7, 6.5m, 2020, 8, 23.44m, 184, 231));
			result.Add(new OverviewMonthlyKPI002DataModel(7.5m, 6.89m, 2020, 9, 23.44m, 183, 214));
			result.Add(new OverviewMonthlyKPI002DataModel(7.5m, 7.2m, 2020, 10, 23.44m, 267, 311));
			result.Add(new OverviewMonthlyKPI002DataModel(7.5m, 6.66m, 2020, 11, 23.44m, 0, 0));
			result.Add(new OverviewMonthlyKPI002DataModel(7.5m, 0m, 2020, 12, 23.44m, 0, 0));

			var currentMonth = result.First(r => r.Month == Month);
			Func<List<OverviewMonthlyTicketDataModel>, decimal> calTotaBurntStoryPoints = th =>
			{
				return th.Sum(t => Constants.JiraStatus.DoneStatus.Contains(t.Histories.OrderByDescending(h => h.PulledDataAt).First().Status)
					? t.Histories.OrderByDescending(h => h.PulledDataAt).First().OriginalEstimate.ConvertToSP()
					: 0);
			};

			Func<List<OverviewMonthlyTicketDataModel>, decimal> calTotaAllocatedStoryPoints = th =>
			{
				return th.Sum(t => t.Histories.OrderByDescending(h => h.OriginalEstimate).First().OriginalEstimate.ConvertToSP());
			};

			var totalStoryPointBurnt = calTotaBurntStoryPoints(TicketHistory);
			var totalAllocatedStoryPoints = calTotaAllocatedStoryPoints(TicketHistory);
			currentMonth.Update(totalStoryPointBurnt, totalAllocatedStoryPoints);

			return result;
		}

		public List<OverviewMonthlyKPI002DataModel> HistoryData
		{
			get
			{
				return GenerateData();
			}
		}
	}

	/// <summary>
	/// Code reviews return to development from review
	/// KPI003 Filter: https://jira.adstream.com/issues/?filter=17726
	/// </summary>
	public class ReportOverviewMonthlyKPI003DataModel
	{
	}

	/// <summary>
	/// Code reviews return to development from Test
	/// KPI004 Filter: https://jira.adstream.com/issues/?filter=17727
	/// </summary>
	public class ReportOverviewMonthlyKPI004DataModel
	{
	}

	/// <summary>
	/// Adstream quality, collaboration and responsive feedback. (scored 1 to 5) Average of MTR01, MTR02 and MTR03
	/// </summary>
	public class ReportOverviewMonthlyKPI005DataModel
	{
	}

	/// <summary>
	/// Responsiveness to Level 3 Requests
	/// </summary>
	public class ReportOverviewMonthlyKPI006DataModel
	{
	}

	public class ReportOverviewMonthlyMetricDataModel
	{
		public ReportOverviewMonthlyMetricDefectDataModel Defect { get; set; }
		public ReportOverviewMonthlyMetricUSDataModel UserStory { get; set; }
		/// <summary>
		/// time to move ticket from in development to Ready for code review
		/// </summary>
		public decimal AverageSpeedOfDevelopment { get; set; }
		/// <summary>
		/// time to move ticket from in Pass testing to On LIVE
		/// </summary>
		public decimal AverageSpeedOfDeployment { get; set; }
	}

	public class ReportOverviewMonthlyMetricBaseDataModel
	{
		public int HoursSpent { get; set; }
		public int TotalTicketsCompletedInMonth { get; set; }
		public int TotalTicketsCompletedPerDev { get; set; }
		public decimal ReopenedByReview { get; set; }
		public decimal ReopenedByQc { get; set; }
	}

	/// <summary>
	/// Defects/Bugs only
	/// </summary>
	public class ReportOverviewMonthlyMetricDefectDataModel : ReportOverviewMonthlyMetricBaseDataModel
	{
		public decimal StoryPointsCompletedPerDev { get; set; }
	}

	/// <summary>
	/// US and TD, improvement, etc.
	/// </summary>
	public class ReportOverviewMonthlyMetricUSDataModel : ReportOverviewMonthlyMetricBaseDataModel
	{
		public decimal StoryPointsCompletedPerDev { get; set; }
	}

	public class ReportOverviewMonthlyDeveloperNumberDataModel
	{
		public string Developer { get; set; }
		public int NumberOfReviewedTickets { get; set; }
		public int NumberOfImplementedTickets { get; set; }
		public int NumberOfImplementedDefectTickets { get; set; }
		public int NumberOfImplementedStoryTickets { get; set; }
	}

	public class ReportOverviewMonthlyResourceDataModel
	{
		public ReportOverviewMonthlyResourceDataModel(int v1, int v2, decimal v3)
		{
			Year = v1;
			Month = v2;
			Value = v3;
		}

		public int Year { get; set; }
		public int Month { get; set; }
		public decimal Value { get; set; }
	}

	public class ReportOverviewMonthlyKPIDataModel
	{
		public ReportOverviewMonthlyKPIDataModel()
		{
			TicketHistory = new List<OverviewMonthlyTicketDataModel>();
		}

		public List<OverviewMonthlyTicketDataModel> TicketHistory { get; set; }
	}

	public class OverviewMonthlyKPIDataModel
	{
		public OverviewMonthlyKPIDataModel(decimal numberOfDev, decimal numberOfFTE)
		{
			NumberOfDev = numberOfDev;
			NumberOfFTE = numberOfFTE;
			TicketHistory = new List<OverviewMonthlyTicketDataModel>();
		}
		public decimal NumberOfDev { get; set; }
		public decimal NumberOfFTE { get; set; }

		public List<OverviewMonthlyTicketDataModel> TicketHistory { get; set; }
	}

	public class OverviewMonthlyKPI002DataModel : OverviewMonthlyKPIDataModel
	{
		public OverviewMonthlyKPI002DataModel(decimal numberOfDevelopers, decimal numberOfFTE,
			int year,
			int month,
			decimal baseStoryPoints,
			decimal totalStoryPointBurnt, decimal totalAllocatedStoryPoints)
			: base(numberOfDevelopers, numberOfFTE)
		{
			Year = year;
			Month = month;
			StoryPointBurntInTotal = totalStoryPointBurnt;
			StoryPointBurntByNumberOfDev = totalStoryPointBurnt / numberOfDevelopers;
			StoryPointBurntByNumberOfFTE = totalStoryPointBurnt / numberOfFTE;
			AllocatedStoryPointsPerDev = totalAllocatedStoryPoints / NumberOfFTE;
			BaseStoryPoints = baseStoryPoints;
		}

		public int Year { get; set; }
		public int Month { get; set; }
		public decimal StoryPointBurntByNumberOfDev { get; set; }
		public decimal StoryPointBurntByNumberOfFTE { get; set; }
		public decimal StoryPointBurntInTotal { get; set; }
		public decimal AllocatedStoryPointsPerDev { get; set; }
		public decimal BaseStoryPoints { get; set; }

		internal void Update(decimal totalStoryPointBurnt, decimal totalAllocatedStoryPoints)
		{
			throw new NotImplementedException();
		}
	}

	public class OverviewMonthlyTicketDataModel
	{
		public string IssueKey { get; set; }
		public List<ReportRawDataModel> Histories { get; set; }
	}

}
