using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using VT.Model;
using System.Linq;
using System;
using VT.Interface;
using VT.Models.Timereg;
using VT.Common;

namespace VT.Implementation
{
	/// <summary>
	/// https://github.com/ExcelDataReader/ExcelDataReader
	/// https://archive.codeplex.com/?p=exceldatareader
	/// </summary>
	public class TimeregExcelHandler : ITimereg
	{
		public TimeregExcelModel Read(string filePath)
		{
			var result = new TimeregExcelModel();

			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
			using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
			using (var reader = ExcelReaderFactory.CreateReader(stream))
			{
				var excelData = reader.AsDataSet(new ExcelDataSetConfiguration()
				{
					ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
					{
						//UseHeaderRow = true
					}
				});

				var impactedCosts = new List<Tuple<string, string, string, string>>();
				var receivedMessages = new List<Tuple<string, string, string>>();

				foreach (DataTable sheet in excelData.Tables)
				{
					if (sheet.Rows.Count <= 100) continue;

					foreach (DataRow row in sheet.Rows)
					{
						try
						{
							if (sheet.Rows.IndexOf(row) <= 3) continue;
							var employeeId = row[6].ToString();
							if (string.IsNullOrWhiteSpace(employeeId) || !Constants.JiraData.EmployeeData.Any(e => e.NCName == employeeId)) continue;

							var timeregCode = row[3].ToString();
							if (string.IsNullOrWhiteSpace(timeregCode)) continue;

							for (int i = 9; i < sheet.Columns.Count - 2; i++)
							{
								try
								{
									TimeregExcelEmployeeModel rowData = new TimeregExcelEmployeeModel(employeeId, timeregCode);

									var logDate = sheet.Rows[3][i].ToString();
									rowData.Date = DateTime.Parse(logDate);

									var logHour = row[i].ToString();
									rowData.HoursSpent = string.IsNullOrWhiteSpace(logHour) ? 0 : decimal.Parse(logHour);

									result.Employees.Add(rowData);
								}
								catch (Exception ex1)
								{

									throw;
								}
							}
						}
						catch (Exception ex2)
						{

							throw;
						}
					}
				}
			}

			return result;
		}

		public TimeregMonthlyModel Export(TimeregExcelModel excelModel)
		{
			var result = new TimeregMonthlyModel(excelModel);

			foreach (var timeLog in excelModel.Employees)
			{
				try
				{
					var employeeBasicData = Constants.JiraData.EmployeeData.First(e => e.NCName == timeLog.EmployeeId);
					var existingEmployeeData = result.Employees.FirstOrDefault(e => e.NCName == employeeBasicData.NCName);
					if (existingEmployeeData == null)
					{
						existingEmployeeData = new TimeregEmployeeModel()
						{
							NCName = employeeBasicData.NCName,
							JiraName = employeeBasicData.JiraName
						};

						result.Employees.Add(existingEmployeeData);
					}

					var existingTimereg = existingEmployeeData.Timeregs.FirstOrDefault(tr => tr.Code == timeLog.Code);
					if (existingTimereg == null)
					{
						existingTimereg = new TimeregModel() { Code = timeLog.Code };
						existingEmployeeData.Timeregs.Add(existingTimereg);
					}

					existingTimereg.Timeregs.Add(new TimeregDetail()
					{
						Date = timeLog.Date,
						TimeSpent = timeLog.HoursSpent
					});
				}
				catch (Exception ex1)
				{

					throw;
				}
			};
			return result;
		}
	}
}
