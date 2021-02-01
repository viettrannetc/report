using CsvHelper.Configuration.Attributes;
using System;
using VT.Common.Enum;

namespace VT.Model
{
    public class ReportRawDataModel
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

        [Ignore]
        public JiraStatus EnumStatus { get; set; }

        [Ignore]
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

        public string Labels { get; set; }
        public string Labels1 { get; set; }
        public string Labels2 { get; set; }
        public string Labels3 { get; set; }
        public string Labels4 { get; set; }
        public string Labels5 { get; set; }
        public string Labels6 { get; set; }
        public string Assignee { get; set; }
        [Name("Original Estimate")]
        public string OriginalEstimateNumber { get; set; }

        public int OriginalEstimate
        {
            get
            {
                int result = 0;
                return string.IsNullOrEmpty(OriginalEstimateNumber)
                    ? 0
					: int.TryParse(OriginalEstimateNumber, out result)
                        ? result
                        : 0;
            }
        }

        public string Priority { get; set; }
        public string Sprint { get; set; }
        //public string Sprint1 { get; set; }
        [Name("Custom field (Need in version)")]
        public string NeedInVersion { get; set; }

        [Ignore]
        public DateTime PulledDataAt { get; set; }
        [Ignore]
        public bool IsCreatedInDate
        {
            get
            {
                return DateTime.Parse(Created).Date >= PulledDataAt.Date;
            }
        }


    }
}
