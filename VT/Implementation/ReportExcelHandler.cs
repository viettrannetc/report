using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using VT.Model;
using System.Linq;
using System;
using VT.Interface;
using VT.Models.Timereg;

namespace VT.Implementation
{
	/// <summary>
	/// https://github.com/ExcelDataReader/ExcelDataReader
	/// https://archive.codeplex.com/?p=exceldatareader
	/// </summary>
	public class ReportExcelHandler : IReporter
	{
		public List<JiraReportFormatter> BuildJiraTableData(string filePath)
		{
			throw new System.NotImplementedException();
		}

		public List<StatusReportFormatter> MakeReportData(List<List<JiraReportFormatter>> JiraDataByDates)
		{
			throw new System.NotImplementedException();
		}

		public void ExportReport(List<StatusReportFormatter> drawTableData, string outputPath)
		{
			throw new System.NotImplementedException();
		}

		public void Read(string filePath, string[] specificSheetNames = null)
		{
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

			using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
			using (var reader = ExcelReaderFactory.CreateReader(stream))
			{
				var result = reader.AsDataSet(new ExcelDataSetConfiguration()
				{
					ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
					{
						//UseHeaderRow = true
					}
				});

				var impactedCosts = new List<Tuple<string, string, string, string>>();
				var receivedMessages = new List<Tuple<string, string, string>>();

				foreach (DataTable sheet in result.Tables)
				{
					if (specificSheetNames != null && !specificSheetNames.Any(n => sheet.TableName.Contains(n))) continue;

					foreach (DataRow row in sheet.Rows)
					{
						var receivedMessage = row[0].ToString();
						if (string.IsNullOrWhiteSpace(receivedMessage)) continue;
						var paths = receivedMessage.Split(' ');
						if (paths.Length > 2)
						{
							var costIdInMessage = paths[9];
							var revisionIdInMessage = paths[6];
							var statusInMessage = paths[14];

							receivedMessages.Add(new Tuple<string, string, string>(costIdInMessage, revisionIdInMessage, statusInMessage));
						}

						var costNumber = row[1].ToString();
						if (string.IsNullOrWhiteSpace(costNumber)) continue;
						var costId = row[2].ToString();
						var revisionId = row[3].ToString();
						var updatedStatus = row[4].ToString();						
					}
				}
			}
		}
	}
}
