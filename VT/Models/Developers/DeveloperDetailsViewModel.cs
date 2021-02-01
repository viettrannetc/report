using System;
using System.Collections.Generic;
using System.Linq;
using VT.Extension;
using VT.Model;

namespace VT.Models.Developers
{
	public class DeveloperDetailsViewModel
	{
		/// <summary>
		/// Checck the burn down chart, to see how much point that the developer has been burnt
		/// </summary>
		/// <param name="model"></param>
		public DeveloperDetailsViewModel(DevelopersDetailsRequestModel model)
		{
			this.Name = model.Name;
			Data = new List<DevelopersDetailsTicketModel>();
			StoryPointData = new List<DevelopersDetailStoryPointModel>();
			Start = model.From;
			End = model.To;

			var startDate = model.From;
			var endDate = model.To;
			Time = $"{startDate} - {endDate}";
			var involvedTickets = model.TicketsData.Where(t =>                  //TODO: remind people to update remaining points
				(t.InAnalysisAssignees.Select(a => a.Item1).Contains(this.Name)
					|| t.InDevelopmentAssignees.Select(a => a.Item1).Contains(this.Name)
					|| t.InReviewAssignees.Select(a => a.Item1).Contains(this.Name))
				&& t.Histories.Any(h => startDate <= h.UpdateAt && h.UpdateAt <= endDate))
				.ToList();

			if (!involvedTickets.Any())
				return;

			while (startDate < endDate)
			{
				decimal storyPointHasBurnt = 0;
				foreach (var ticket in involvedTickets)
				{
					var infoFromBegining = ticket.Histories.Where(h => h.UpdateAt.InTheSameDay(startDate)).OrderBy(t => t.UpdateAt).FirstOrDefault();
					var infoAtTheEnd = ticket.Histories.Where(h => h.UpdateAt.InTheSameDay(startDate)).OrderBy(t => t.UpdateAt).LastOrDefault();

					if (infoFromBegining != null)
					{
						var record = Data.FirstOrDefault(d => d.TicketNumber == ticket.TicketNumber);
						if (record == null)
						{
							record = new DevelopersDetailsTicketModel(ticket.TicketNumber, infoFromBegining.History.OriginalStoryPoint);
						}

						record.AddRemainingSP(startDate, infoAtTheEnd.History.RemainingStoryPoint);
						if (!Data.Any(d => d.TicketNumber == record.TicketNumber
									&& d.OriginalStoryPoint == record.OriginalStoryPoint
									&& d.RemainingStoryPoint == record.RemainingStoryPoint))
							Data.Add(record);

						storyPointHasBurnt += infoFromBegining.History.RemainingStoryPoint - infoAtTheEnd.History.RemainingStoryPoint;
					}
				}

				StoryPointData.Add(new DevelopersDetailStoryPointModel(startDate, 3, storyPointHasBurnt));

				startDate = startDate.AddDays(1);
			}
		}
		private string Name { get; set; }
		public string Title { get { return $"SP-{this.Name}"; } }
		public List<DevelopersDetailsTicketModel> Data { get; set; }
		public string Time { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public List<DevelopersDetailStoryPointModel> StoryPointData { get; set; }
	}

	public class DevelopersDetailsTicketModel
	{
		public DevelopersDetailsTicketModel(string ticketNumber,
			decimal originalStoryPoint)
		{
			TicketNumber = ticketNumber;
			OriginalStoryPoint = originalStoryPoint;
		}

		public string TicketNumber { get; set; }
		public decimal OriginalStoryPoint { get; set; }
		public Dictionary<DateTime, decimal> RemainingStoryPoint { get; set; }
		public void AddRemainingSP(DateTime date, decimal remainingPoint)
		{
			if (RemainingStoryPoint == null)
				RemainingStoryPoint = new Dictionary<DateTime, decimal>();

			if (!RemainingStoryPoint.ContainsKey(date))
				RemainingStoryPoint.Add(date, remainingPoint);
		}

	}

	public class StoryPointTicketHistor
	{
		public DateTime Date { get; set; }
		public decimal OriginalStoryPoint { get; set; }
		public decimal RemainingStoryPoint { get; set; }
	}

	public class DevelopersDetailStoryPointModel
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="realityRate">Return to development rate for system/QA testing will not exceed the threshold (MTR14 + MTR25)</param>
		/// <param name="baseline"></param>
		/// <param name="baselineMinimun"></param>
		public DevelopersDetailStoryPointModel(DateTime date,
			decimal baseline,
			decimal realityRate)
		{
			Date = date.ToShortDateString();
			Baseline = baseline;
			BaselineMinimun = baseline * 0.8m;
			RealityRate = realityRate;
		}

		public string Date { get; set; }
		public decimal RealityRate { get; set; }
		public decimal Baseline { get; set; }
		public decimal BaselineMinimun { get; set; }
	}
}
