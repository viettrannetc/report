using System;
using System.Collections.Generic;
using System.Linq;
using VT.Common;
using VT.Common.Enum;
using VT.Extension;

namespace VT.Model
{
    public class DevelopersRequestModel
    {
        public DevelopersRequestModel(List<TicketCleanDataModel> ticketsData, string developerName)
        {
            FTE = 6.66m;                        //TODO: real working hours            
            Project = Projects.Unknown;
            TicketsData = ticketsData;
            Name = developerName;
        }

        public Projects Project { get; set; }
        public decimal FTE { get; set; }
        public string Name { get; set; }
        public List<TicketCleanDataModel> TicketsData { get; set; }
    }
}
