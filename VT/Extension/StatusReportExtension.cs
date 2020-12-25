using VT.Common;
using VT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VT.Extension
{
	public static class StatusReportExtension
	{
		public static int DoneTickets(this List<JiraReportFormatter> jiraTicketToday)
		{
			return jiraTicketToday.Count(j => j.Status == Constants.JiraStatus.Done);
		}

		public static int DoingTickets(this List<JiraReportFormatter> jiraTicketToday)
		{
			return jiraTicketToday.TotalTickets()
				- jiraTicketToday.DoneTickets()
				- jiraTicketToday.CreatedTickets();
		}

		public static int ClosedTickets(this List<JiraReportFormatter> jiraTicketToday, List<JiraReportFormatter> jiraTicketPreviousDay)
		{
			var totalTicketsRemainingFromYesterdayShouldHave = jiraTicketToday.TotalTickets() - jiraTicketToday.CreatedTickets();
			var closedTickets = jiraTicketPreviousDay.Count - totalTicketsRemainingFromYesterdayShouldHave;
			return closedTickets > 0
				? closedTickets
				: 0;
		}

		public static int CreatedTickets(this List<JiraReportFormatter> jiraTicketToday)
		{
			return jiraTicketToday.Count(j => j.IsCreatedInDate);
		}

		public static int TotalTickets(this List<JiraReportFormatter> jiraTicketToday)
		{
			return jiraTicketToday.Count();
		}

		public static List<JiraReportFormatter> DataFromYesterday(this List<JiraReportFormatter> jiraTicketToday, List<List<JiraReportFormatter>> JiraDataByDates)
		{
			var indexYesderdayData = JiraDataByDates.IndexOf(jiraTicketToday);
			if (indexYesderdayData > 0)
			{
				indexYesderdayData -= 1;
			}

			return JiraDataByDates[indexYesderdayData];
		}

		
	}
}
