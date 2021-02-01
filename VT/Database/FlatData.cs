using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VT.Models.Monthly.KPIs;

namespace VT.Database
{
	public class FlatData
	{
		public FlatData()
		{
            KPI002 = new List<KPI002Model>();
            
            KPI002.Add(new KPI002Model(9, 6.2m, 2020, 1, 20.82m, 127, 58));
            KPI002.Add(new KPI002Model(9, 7.66m, 2020, 2, 22.84m, 142, 72));
            KPI002.Add(new KPI002Model(10, 9.59m, 2020, 3, 22.84m, 216, 183));
            KPI002.Add(new KPI002Model(10, 9.24m, 2020, 4, 22.84m, 175.5m, 230));
            KPI002.Add(new KPI002Model(8, 7.37m, 2020, 5, 22.84m, 115, 182));
            KPI002.Add(new KPI002Model(6, 4.84m, 2020, 6, 22.84m, 111.87m, 185));
            KPI002.Add(new KPI002Model(7, 6.29m, 2020, 7, 22.84m, 193, 186));
            KPI002.Add(new KPI002Model(7, 6.5m, 2020, 8, 23.44m, 184, 231));
            KPI002.Add(new KPI002Model(7.5m, 6.89m, 2020, 9, 23.44m, 183, 214));
            KPI002.Add(new KPI002Model(7.5m, 7.2m, 2020, 10, 23.44m, 267, 311));
            //KPI002.Add(new KPI002Model(7.5m, 6.66m, 2020, 11, 23.44m, storyPointBurntInTotal, storyPointAllocatedInTotal));
            KPI002.Add(new KPI002Model(7.5m, 0m, 2020, 12, 23.44m, 0, 0));
        }

		public List<KPI002Model> KPI002 { get; set; }
		public List<KPI003Model> KPI003 { get; set; }
		public List<KPI003Model> KPI004 { get; set; }
	}

    public class BasicDataMonthly
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal NumberOfDevelopers { get; set; }
        public decimal NumberOfFTE { get; set; }
        public decimal TotalSpentHours { get; set; }
        public decimal TotalDevelopmentHours { get; set; }
        public decimal TotalBAHours { get; set; }
        public decimal TotalQAHours { get; set; }
        public decimal TotalTimeOff { get; set; }
    }

    public class EmployeeData
    {
        public string NCName { get; set; }
        public string JiraName { get; set; }
        public decimal NumberOfDev { get; set; }
        public decimal NumberOfBA { get; set; }
        public decimal NumberOfQC { get; set; }
        public decimal NumberOfPM { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
