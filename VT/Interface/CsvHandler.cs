using CsvHelper;
using VT.Common;
using VT.Extension;
using VT.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VT.Interface
{
	/// <summary>
	/// https://joshclose.github.io/CsvHelper/getting-started
	/// </summary>
	public class CsvHandler : IReporter
	{
		public void ExportReport(List<StatusReportFormatter> records, string outputPath)
		{
			//TODO: need to change
			//using (var writer = new StreamWriter($"{outputPath}\\calculator.csv"))
			//{
			//	using (var csv = new CsvWriter(writer))
			//	{
			//		csv.WriteRecords(records);
			//	}
			//}
		}

		public List<StatusReportFormatter> MakeReportData(List<List<JiraReportFormatter>> JiraDataByDates)
		{
			var result = new List<StatusReportFormatter>();
			foreach (var dataInDate in JiraDataByDates)
			{
				StatusReportFormatter statusReport = new StatusReportFormatter
				{
					Date = dataInDate.First().DataInDate,
					Doing = dataInDate.DoingTickets(),
					Closed = dataInDate.ClosedTickets(dataInDate.DataFromYesterday(JiraDataByDates)),
					Created = dataInDate.CreatedTickets(),
					Total = dataInDate.TotalTickets(),
					Done = dataInDate.DoneTickets()
				};

				result.Add(statusReport);
			}

			return result;
		}

		public List<JiraReportFormatter> BuildJiraTableData(string filePath)
		{
			var result = new List<JiraReportFormatter>();
			//TODO: need to change
			//using (var reader = new StreamReader(filePath))
			//{
			//	DateTime dateData = filePath.GetDateByFileName();
			//	using (var csv = new CsvReader(reader))
			//	{
			//		result = csv.GetRecords<JiraReportFormatter>().ToList();
			//		Parallel.ForEach(result, r => r.DataInDate = dateData);
			//	}
			//}
			return result;
		}

		public void Read(string filePath, string[] specificSheetName)
		{
			throw new NotImplementedException();
		}
	}
}
