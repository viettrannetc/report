using System;
using System.Collections.Generic;
using System.Linq;
using VT.Common;
using VT.Common.Enum;
using VT.Extension;

namespace VT.Model
{
    public class ReportDataRequestModel
    {
        public ReportDataRequestModel()
        {
            //reportCleanDataModels = new List<ReportCleanDataModel>();
        }

        public Projects Project { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public decimal FTE { get; set; }
    }
}
