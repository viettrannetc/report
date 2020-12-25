using CsvHelper.Configuration.Attributes;
using VT.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VT.Extension
{
	public static class StatusChangedFormatterExtension
	{
		public static bool ContainsMessage(this List<StatusChangedFormatter> receivedMessages, StatusChangedFormatter message)
		{
			return receivedMessages.Any(m =>
				m.CostId == message.CostId
				&& m.RevisionId == message.RevisionId
				&& m.CostStatus == message.CostStatus
				&& m.Time == message.Time);
		}

		public static void Print(this StatusChangedFormatter message)
		{
			Console.WriteLine($"Cost: {message.CostId} - Revision: {message.RevisionId}  - Time {message.Time} - Status: {message.CostStatus}");
		}

	} 
}
