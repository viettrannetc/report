using System;
using System.Collections.Generic;

namespace VT.Model
{
	public class TicketHistory
	{
		public TicketHistory()
		{
			
		}
		public DateTime UpdateAt { get; set; }
		public TicketDetailsHistory History {get;set;}
	}

	public class TicketDetailsHistory
	{
		public string Assignee { get; set; }
		public string Status { get; set; }
		public decimal OriginalStoryPoint { get; set; }
		public decimal RemainingStoryPoint { get; set; }

	}

	public class KeyCompareHistory
	{
		public string From { get; set; }
		public string To { get; set; }
	}
}
