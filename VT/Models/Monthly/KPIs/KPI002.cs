using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VT.Model;

namespace VT.Models.Monthly.KPIs
{
    public class KPI002ViewModel
    {
        public KPI002ViewModel(KPIRequestModel model)
        {
            var storyPointAllocatedInTotal = model.TicketsData.Sum(r => r.StoryPoint);
            var storyPointBurntInTotal = model.TicketsData.Where(r => r.PassedTesting).Sum(r => r.StoryPoint);

            Data = new List<KPI002Model>();

            Data.Add(new KPI002Model(9, 6.2m, 2020, 1, 20.82m, 127, 58));
            Data.Add(new KPI002Model(9, 7.66m, 2020, 2, 22.84m, 142, 72));
            Data.Add(new KPI002Model(10, 9.59m, 2020, 3, 22.84m, 216, 183));
            Data.Add(new KPI002Model(10, 9.24m, 2020, 4, 22.84m, 175.5m, 230));
            Data.Add(new KPI002Model(8, 7.37m, 2020, 5, 22.84m, 115, 182));
            Data.Add(new KPI002Model(6, 4.84m, 2020, 6, 22.84m, 111.87m, 185));
            Data.Add(new KPI002Model(7, 6.29m, 2020, 7, 22.84m, 193, 186));
            Data.Add(new KPI002Model(7, 6.5m, 2020, 8, 23.44m, 184, 231));
            Data.Add(new KPI002Model(7.5m, 6.89m, 2020, 9, 23.44m, 183, 214));
            Data.Add(new KPI002Model(7.5m, 7.2m, 2020, 10, 23.44m, 267, 311));
            Data.Add(new KPI002Model(7.5m, model.FTE, 2020, 11, 23.44m, storyPointBurntInTotal, storyPointAllocatedInTotal));
            Data.Add(new KPI002Model(7.5m, 0m, 2020, 12, 23.44m, 0, 0));
        }

        public string Title { get { return "KPI002 - User Story Points per developer"; } }
        public List<KPI002Model> Data { get; set; }
    }

    public class KPI002Model : OverviewMonthlyKPIDataModel
    {
        public KPI002Model(decimal numberOfDevelopers, decimal numberOfFTE,
            int year,
            int month,
            decimal baseStoryPoints,
            decimal totalStoryPointBurnt, decimal totalAllocatedStoryPoints)
            : base(numberOfDevelopers, numberOfFTE)
        {
            Year = year;
            Month = month;
            StoryPointBaselineFTE = baseStoryPoints;
            StoryPointMinimumBaselineFTE = baseStoryPoints * 0.8m;

            if (totalStoryPointBurnt > 0 && totalAllocatedStoryPoints > 0)
            {
                StoryPointBurntInTotal = totalStoryPointBurnt;
                StoryPointsAllocatedInTotal = totalAllocatedStoryPoints;
                StoryPointsCompletedPerMonthPerDev = StoryPointBurntInTotal / numberOfDevelopers;
                StoryPointsAllocatedPerMonthPerDev = StoryPointsAllocatedInTotal / NumberOfFTE;
                StoryPointBurntByNumberOfFTE = StoryPointBurntInTotal / numberOfFTE;
            }
        }

        public int Year { get; set; }
        public int Month { get; set; }

        public decimal StoryPointsCompletedPerMonthPerDev { get; set; }
        public decimal StoryPointsAllocatedPerMonthPerDev { get; set; }
        public decimal StoryPointsAllocatedInTotal { get; set; }
        public decimal StoryPointBaselineFTE { get; set; }
        public decimal StoryPointMinimumBaselineFTE { get; set; }
        public decimal StoryPointBurntByNumberOfFTE { get; set; }
        public decimal StoryPointBurntInTotal { get; set; }

        internal void Update(decimal totalStoryPointBurnt, decimal totalAllocatedStoryPoints)
        {
            throw new NotImplementedException();
        }
    }
}
