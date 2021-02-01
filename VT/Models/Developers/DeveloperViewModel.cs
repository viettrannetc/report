using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VT.Common;
using VT.Extension;
using VT.Model;

namespace VT.Models.Developers
{
	public class DeveloperViewModel
	{
		/// <summary>
		/// KPI 002: No, the number of total story points can't be counted - it included review, etc.
		/// KPI 003: Yes, if the ticket is in development by someone, then it's returned, it's counted
		/// KPI 004: Yes, if the ticket is in development by someone, then it's returned, it's counted
		/// Number of ticket has been developed by this person - this infor can be use to compare with other people and previous months/weeks
		/// Number of ticket has been reviewed by this person - this infor can be use to compare with other people and previous months/weeks
		/// Burnt story point: this will be calculated at each step: TODO: currently the developers wouldn't do that
		/// - development: log time, update the used/remaining points by developers
		/// - review: log time and clean the point before handover to testers
		/// - if it failed, update the remaining point, not original one and give it back to developers
		/// </summary>
		/// <param name="model"></param>
		public DeveloperViewModel(DevelopersRequestModel model)
		{
			this.Name = model.Name;
			Data = new List<DevelopersModel>();
			var startDate = Constants.Start;
			var endDate = Constants.End;
			var involvedTicketsInDevelopment = model.TicketsData.Where(t =>
				t.InDevelopmentAssignees.Select(a => a.Item1).Contains(this.Name)
				|| t.InAnalysisAssignees.Select(a => a.Item1).Contains(this.Name))
				.ToList();
			var involvedTicketsInReview = model.TicketsData.Where(t => t.InReviewAssignees.Select(a => a.Item1).Contains(this.Name)).ToList();

			if (!involvedTicketsInDevelopment.Any() && !involvedTicketsInReview.Any())
				return;

			while (startDate < endDate)
			{
				if (!Data.Any(d => d.Year == startDate.Year && d.Month == startDate.Month))
				{
					var selectedMonth = startDate.Month;
					var selectedYear = startDate.Year;
					var periodStartedDate = new DateTime(selectedYear, selectedMonth, 01);
					var periodEndDate = new DateTime(selectedYear, selectedMonth, DateTime.DaysInMonth(selectedYear, selectedMonth), 23, 59, 59);

					var involvedTicketsInDevelopmentInMonth = involvedTicketsInDevelopment.Where(t => t.TouchByDevInMonths.Contains(new DateTime(selectedYear, selectedMonth, 01))).ToList(); //TODO: review logic
					var involvedTicketsInReviewInMonth = involvedTicketsInReview.Where(t => t.TouchByDevInMonths.Contains(new DateTime(selectedYear, selectedMonth, 01))).ToList();

					var involvedTicketsGotReturnedAtReviewInMonth = involvedTicketsInDevelopmentInMonth.Where(t => t.ReturnedAtReviewStageHistory.Any(d => periodStartedDate <= d && d <= periodEndDate)).ToList();
					var involvedTicketsGotReturnedAtQAInMonth = involvedTicketsInDevelopmentInMonth.Where(t => t.ReturnedAtQAStageHistory.Any(d => periodStartedDate <= d && d <= periodEndDate)).ToList();

					decimal ticketsDeveloped = involvedTicketsInDevelopmentInMonth.Count();
					decimal ticketsReviewed = involvedTicketsInReviewInMonth.Count();

					decimal ticketsGotReturnedAtReview = involvedTicketsGotReturnedAtReviewInMonth.Count();
					decimal ticketsGotReturnedAtQA = involvedTicketsGotReturnedAtQAInMonth.Count();

					//decimal ticketsGotReturnedAtReview = ticketsDeveloped > 0 
					//	? involvedTicketsGotReturnedAtReviewInMonth.Count() / ticketsDeveloped
					//	: 0;
					//decimal ticketsGotReturnedAtQA = ticketsDeveloped > 0
					//	? involvedTicketsGotReturnedAtQAInMonth.Count() / ticketsDeveloped
					//	: 0;

					Data.Add(new DevelopersModel(selectedYear, selectedMonth, ticketsDeveloped, ticketsReviewed, ticketsGotReturnedAtReview, ticketsGotReturnedAtQA));
				}

				startDate = startDate.AddMonths(1);
			}
		}
		private string Name { get; set; }
		public string Title { get { return $"Developer number - {this.Name}"; } }
		public List<DevelopersModel> Data { get; set; }
	}

	public class DevelopersModel
	{
		/// <summary>
		/// KPI 002: No, the number of total story points can't be counted - it included review, etc.
		/// KPI 003: Yes, if the ticket is in development by someone, then it's returned, it's counted
		/// KPI 004: Yes, if the ticket is in development by someone, then it's returned, it's counted
		/// Number of ticket has been developed by this person - this infor can be use to compare with other people and previous months/weeks
		/// Number of ticket has been reviewed by this person - this infor can be use to compare with other people and previous months/weeks
		/// Burnt story point: this will be calculated at each step: TODO: currently the developers wouldn't do that
		/// - development: log time, update the used/remaining points by developers
		/// - review: log time and clean the point before handover to testers
		/// - if it failed, update the remaining point, not original one and give it back to developers
		/// </summary>
		/// <param name="model"></param>
		public DevelopersModel(int year,
			int month,
			decimal ticketsDeveloped,
			decimal ticketsReviewed,
			decimal ticketsGotReturnedAtReview,
			decimal ticketsGotReturnedAtQA,
			decimal burntStoryPoint = 0)
		{
			TicketsDeveloped = ticketsDeveloped;
			TicketsReviewed = ticketsReviewed;
			TicketsGotReturnedAtReview = ticketsGotReturnedAtReview;
			TicketsGotReturnedAtQA = ticketsGotReturnedAtQA;
			Year = year;
			Month = month;
		}

		public decimal TicketsDeveloped { get; set; }
		public decimal TicketsReviewed { get; set; }
		public decimal TicketsGotReturnedAtReview { get; set; }
		public decimal TicketsGotReturnedAtQA { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public string Time { get { return $"{Month}/{Year.Take2LastDigits()}"; } }

	}
}
