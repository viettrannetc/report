using System.Collections.Generic;

namespace VT.Common
{
	public static class Constants
	{
		
		public static string RootFolder = @"C:\\Projects\\Adstream\\Report\\Database\\Files\\";

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
			public static string Will = "https://jira.adstream.com/sr/jira.issueviews:searchrequest-csv-current-fields/18141/will.csv";
			public static string Nhu = "https://jira.adstream.com/sr/jira.issueviews:searchrequest-csv-current-fields/18141/will.csv";
			public static string Hung = "https://jira.adstream.com/sr/jira.issueviews:searchrequest-csv-current-fields/18141/will.csv";
			public static string Tri = "https://jira.adstream.com/sr/jira.issueviews:searchrequest-csv-current-fields/18141/will.csv";
			public static string Quang = "https://jira.adstream.com/sr/jira.issueviews:searchrequest-csv-current-fields/18141/will.csv";
			public static string Khanh = "https://jira.adstream.com/sr/jira.issueviews:searchrequest-csv-current-fields/18141/will.csv";
			public static string Henry = "https://jira.adstream.com/sr/jira.issueviews:searchrequest-csv-current-fields/18141/will.csv";
			public static string HTA = "https://jira.adstream.com/sr/jira.issueviews:searchrequest-csv-current-fields/18141/will.csv";
		}
	}
}
