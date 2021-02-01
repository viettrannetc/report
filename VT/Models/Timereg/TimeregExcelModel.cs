using System;
using System.Collections.Generic;
using System.Linq;
namespace VT.Models.Timereg
{
	public class TimeregExcelModel
	{
		public TimeregExcelModel()
		{
			Employees = new List<TimeregExcelEmployeeModel>();
		}
		public List<TimeregExcelEmployeeModel> Employees { get; set; }

		public int Month
		{
			get { return Employees.FirstOrDefault() != null ? Employees.First().Date.Month : DateTime.UtcNow.Month; }
		}
		public int Year
		{
			get { return Employees.FirstOrDefault() != null ? Employees.First().Date.Year : DateTime.UtcNow.Year; }
		}
		public string Time { get { return $"{Month}/{Year}"; } }
	}

	public class TimeregExcelEmployeeModel
	{
		public TimeregExcelEmployeeModel(string employeeId, string code)
		{
			EmployeeId = employeeId;
			Code = code;
		}
		public string EmployeeId { get; set; }
		public string Code { get; set; }
		public DateTime Date { get; set; }
		public decimal HoursSpent { get; set; }
	}
}
