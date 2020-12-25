using CsvHelper.Configuration.Attributes;
using System;

namespace VT.Model
{
	public class StatusChangedFormatter
	{
		public string CostId { get; set; }
		public string RevisionId { get; set; }
		public string CostStatus { get; set; }
		public string Time { get; set; }
		public StatusChangedEnum MessageType { get; set; }
	} 

	public enum StatusChangedEnum
	{
		Sent,
		Received
	}
}
