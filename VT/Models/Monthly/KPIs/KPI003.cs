using System.Collections.Generic;
using System.Linq;
using VT.Model;

namespace VT.Models.Monthly.KPIs
{
    public class KPI003ViewModel
    {
        public KPI003ViewModel(KPIRequestModel model) //TODO need to add range to search?
        {
            var storyPointAllocatedInTotal = model.TicketsData.Sum(r => r.StoryPoint);
            var storyPointBurntInTotal = model.TicketsData.Where(r => r.PassedTesting).Sum(r => r.StoryPoint);
            var totalCompletedTickets = model.TicketsData.Count(r => r.PassedTesting);
            var returnedToDevelopmentTickets = model.TicketsData.Where(r => r.ReturnedAtReviewStageHistory.Any()).ToList(); //TODO: check in range what we want to search?

            var totalReturnedToDevelopmentTickets = model.TicketsData.Count(r => r.ReturnedAtReviewStageHistory.Any()); //TODO: check in range what we want to search?

            Data = new List<KPI003Model>();
            Data.Add(new KPI003Model(2020, 01, 23.7m, 15.21m));
            Data.Add(new KPI003Model(2020, 02, 20m, 16.66m));
            Data.Add(new KPI003Model(2020, 03, 20m, 17.18m));
            Data.Add(new KPI003Model(2020, 04, 20m, 15.15m));
            Data.Add(new KPI003Model(2020, 05, 20m, 21.42m));
            Data.Add(new KPI003Model(2020, 06, 20m, 12.76m));
            Data.Add(new KPI003Model(2020, 07, 20m, 6.89m));
            Data.Add(new KPI003Model(2020, 08, 20m, 14.89m));
            Data.Add(new KPI003Model(2020, 09, 20m, 14.58m));
            Data.Add(new KPI003Model(2020, 10, 20m, 10.34m));
            Data.Add(new KPI003Model(2020, 11, 20m, totalReturnedToDevelopmentTickets));
            Data.Add(new KPI003Model(2020, 12, 20m, 0));
        }

        public string Title { get { return "KPI003 - Code reviews returned to Development"; } }
        public List<KPI003Model> Data { get; set; }
    }

    public class KPI003Model
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="realityRate">Return to development rate for system/QA testing will not exceed the threshold (MTR14 + MTR25)</param>
        /// <param name="baseline"></param>
        /// <param name="baselineMinimun"></param>
        public KPI003Model(int year,
            int month,
            decimal baseline,
            decimal realityRate)
        {
            Year = year;
            Month = month;
            Baseline = baseline;
            BaselineMinimun = baseline * 0.8m;
            RealityRate = realityRate;
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public decimal RealityRate { get; set; }
        public decimal Baseline { get; set; }
        public decimal BaselineMinimun { get; set; }
    }
}