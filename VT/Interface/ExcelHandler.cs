using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using VT.Model;
using System.Linq;
using System;

namespace VT.Interface
{
	/// <summary>
	/// https://github.com/ExcelDataReader/ExcelDataReader
	/// https://archive.codeplex.com/?p=exceldatareader
	/// </summary>
	public class ExcelHandler : IReporter
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
						CostStageRevisionStatus status = (CostStageRevisionStatus)Enum.Parse(typeof(CostStageRevisionStatus), updatedStatus, true);

						if (status == CostStageRevisionStatus.Approved && !impactedCosts.Any(r => r.Item1 == costNumber))
							impactedCosts.Add(new Tuple<string, string, string, string>(costNumber, costId, revisionId, status.ToString()));
					}
				}

				//Cost has been changed but AdCosts didn't receive the messages
				foreach (var impactedCost in impactedCosts)
				{
					if (!receivedMessages.Any(m => m.Item1 == impactedCost.Item2
											&& m.Item2 == impactedCost.Item3
											&& m.Item3 == impactedCost.Item4))
					{
						Console.WriteLine($"Cost { impactedCost.Item1 } - Revision { impactedCost.Item3 } - status { impactedCost.Item4 }");
					}
				}
				Console.ReadLine();
			}
		}

		//public void Read(string filePath, string[] specificSheetNames = null)
		//{
		//	System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

		//	using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
		//	using (var reader = ExcelReaderFactory.CreateReader(stream))
		//	{
		//		var result = reader.AsDataSet(new ExcelDataSetConfiguration()
		//		{
		//			ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
		//			{
		//				UseHeaderRow = true
		//			}
		//		});

		//		var listOfMissingMessages = new Dictionary<string, List<string>>();
		//		var listOfSentMessages = new Dictionary<string, List<string>>();
		//		var listOfReceiveMessages = new Dictionary<string, List<string>>();

		//		var listOfSentMessagesInADay = new List<string>();
		//		var listOfReceiveMessagesInADay = new List<string>();

		//		foreach (DataTable sheet in result.Tables)
		//		{
		//			if (specificSheetNames != null && !specificSheetNames.Any(n => sheet.TableName.Contains(n))) continue;

		//			foreach (DataRow row in sheet.Rows)
		//			{
		//				var dataProvedMessageHasBeenSent = row[0].ToString();
		//				if (string.IsNullOrWhiteSpace(dataProvedMessageHasBeenSent)) continue;

		//				var costIdHasBeenSent = dataProvedMessageHasBeenSent.Substring(dataProvedMessageHasBeenSent.Length - 32, 32).Trim();
		//				listOfSentMessagesInADay.Add(costIdHasBeenSent);

		//				var dataProvedMessageHasBeenReceive = row[0].ToString();
		//				if (string.IsNullOrWhiteSpace(dataProvedMessageHasBeenReceive)) continue;


		//				var firstPart = dataProvedMessageHasBeenReceive.Split(']')[1];
		//				var secondPart = firstPart.Split("of cost")[1];
		//				var thirdPart = secondPart.Split(' ')[1];

		//				listOfReceiveMessagesInADay.Add(thirdPart);
		//			}


		//		}
		//	}
		//}
	}

	public enum CostStageRevisionStatus
	{
		Draft,
		PendingTechnicalApproval,
		PendingBrandApproval,
		Approved,
		Rejected,
		PendingRecall,
		Recalled,
		PendingCancellation,
		Cancelled,
		PendingReopen,
		ReopenRejected,
		Reopened,
		Deleted
	}
}
