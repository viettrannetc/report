using System;
using System.Collections.Generic;
using System.Linq;
using VT.Common;
using VT.Common.Enum;
using VT.Extension;

namespace VT.Model
{
    public class DevelopersDetailsRequestModel
    {
		public DevelopersDetailsRequestModel()
		{

		}

        public DevelopersDetailsRequestModel(List<TicketCleanDataModel> ticketsData, string developerName, DateTime from, DateTime to)
        {
            FTE = 1m;                        //TODO: real working hours            
            Project = Projects.Unknown;
            TicketsData = ticketsData;
            Name = developerName;
            From = from;
            To = to;
        }

        public Projects Project { get; set; }
        public decimal FTE { get; set; }        //TODO: it should be calculated by 1 - time off
        public string Name { get; set; }
        public List<TicketCleanDataModel> TicketsData { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
