using CsvHelper.Configuration.Attributes;
using System;

namespace VT.Model
{
	public class JiraReportFormatter
	{
		[Name("Issue Type")]
		public string IssueType { get; set; }
		[Name("Issue key")]
		public string IssueKey { get; set; }
		[Name("Issue id")]
		public string IssueId { get; set; }
		[Name("Summary")]
		public string Summary { get; set; }
		[Name("Status")]
		public string Status { get; set; }
		[Name("Resolution")]
		public string Resolution { get; set; }
		[Name("Updated")]
		public string Updated { get; set; }
		[Name("Created")]
		public string Created { get; set; }
		[Name("Remaining Estimate")]
		public string RemainingEstimate { get; set; }
		[Name("Time Spent")]
		public string TimeSpent { get; set; }
		[Name("Custom field (Story Points)")]
		public string StoryPoints { get; set; }

		[Ignore]
		public DateTime DataInDate { get; set; }

		[Ignore]
		public bool IsCreatedInDate
		{
			get
			{
				return DateTime.Parse(Created).Date >= DataInDate.Date;
			}
		}
	}
}
