using System.Collections.Generic;
using System.Linq;
using VT.Model;

namespace VT.Models.Monthly.KPIs
{
    public class KPI004ViewModel
    {
        public KPI004ViewModel(KPIRequestModel model) //TODO need to add range to search?
        {
            var storyPointAllocatedInTotal = model.TicketsData.Sum(r => r.StoryPoint);
            var storyPointBurntInTotal = model.TicketsData.Where(r => r.PassedTesting).Sum(r => r.StoryPoint);
            var totalCompletedTickets = model.TicketsData.Count(r => r.PassedTesting);
            var returnedToDevelopmentTickets = model.TicketsData.Where(r => r.ReturnedAtQAStageHistory.Any()).ToList(); //TODO: check in range what we want to search?
            var totalReturnedToDevelopmentTickets = model.TicketsData.Count(r => r.ReturnedAtQAStageHistory.Any()); //TODO: check in range what we want to search?

            Data = new List<KPI004Model>();
            Data.Add(new KPI004Model(2020, 01, 17m, 8.69m));
            Data.Add(new KPI004Model(2020, 02, 17m, 9m));
            Data.Add(new KPI004Model(2020, 03, 17m, 12.5m));
            Data.Add(new KPI004Model(2020, 04, 17m, 15.15m));
            Data.Add(new KPI004Model(2020, 05, 17m, 11.9m));
            Data.Add(new KPI004Model(2020, 06, 17m, 10.06m));
            Data.Add(new KPI004Model(2020, 07, 17m, 8.62m));
            Data.Add(new KPI004Model(2020, 08, 17m, 2.12m));
            Data.Add(new KPI004Model(2020, 09, 17m, 6.25m));
            Data.Add(new KPI004Model(2020, 10, 17m, 13.79m));
            Data.Add(new KPI004Model(2020, 11, 17m, totalReturnedToDevelopmentTickets));
            Data.Add(new KPI004Model(2020, 12, 17m, 0));
        }

        public string Title { get { return "KPI004 - QA returned to Development"; } }
        public List<KPI004Model> Data { get; set; }
    }

    public class KPI004Model
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="realityRate">Return to development rate for system/QA testing will not exceed the threshold (MTR14 + MTR25)</param>
        /// <param name="baseline"></param>
        /// <param name="baselineMinimun"></param>
        public KPI004Model(int year,
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