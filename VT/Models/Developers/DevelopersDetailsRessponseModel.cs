using System;
using System.Collections.Generic;
using VT.Common.Enum;
using VT.Models.Developers;

namespace VT.Model
{
	public class DevelopersDetailsResponseModel : DevelopersDetailsRequestModel
	{
		public DevelopersDetailsResponseModel(List<TicketCleanDataModel> ticketsData, string developerName, DateTime from, DateTime to, List<DevelopersDetailsTicketModel>  tickets)
			: base(ticketsData, developerName, from, to)
		{
			FTE = 1m;                        //TODO: real working hours            
			Project = Projects.Unknown;
			TicketsData = ticketsData;
			Name = developerName;
			From = from;
			To = to;
			Tickets = tickets;
		}
		
		public List<DevelopersDetailsTicketModel> Tickets { get; set; }
	}

	
}
