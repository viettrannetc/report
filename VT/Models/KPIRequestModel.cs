using System;
using System.Collections.Generic;
using System.Linq;
using VT.Common;
using VT.Common.Enum;
using VT.Extension;

namespace VT.Model
{
    public class KPIRequestModel
    {
        public KPIRequestModel(List<TicketCleanDataModel> ticketsData)
        {
            FTE = 6.66m;
            From = new DateTime(2020, 11, 1);
            To = new DateTime(2020, 11, 30);
            Project = Projects.Unknown;
            TicketsData = ticketsData;
        }

        public Projects Project { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal FTE { get; set; }
        public List<TicketCleanDataModel> TicketsData { get; set; }
    }
}
