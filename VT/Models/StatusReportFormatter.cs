using System;

namespace VT.Model
{
	public class StatusReportFormatter
	{
		public DateTime Date { get; set; }
		public int Doing { get; set; }
		public int Done { get; set; }
		public int Closed { get; set; }
		public int Created { get; set; }
		public int Total { get; set; }
	}
}
