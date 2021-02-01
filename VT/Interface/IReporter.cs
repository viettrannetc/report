using System.Collections.Generic;
using VT.Model;
using VT.Models.Timereg;

namespace VT.Interface
{
	public interface IReporter
	{
		List<JiraReportFormatter> BuildJiraTableData(string filePath);

		List<StatusReportFormatter> MakeReportData(List<List<JiraReportFormatter>> JiraDataByDates);

		void ExportReport(List<StatusReportFormatter> drawTableData, string outputPath);

		void Read(string filePath, string[] specificSheetName);
	}
}
