using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VT.Common;
using VT.Implementation;
using VT.Model;
using VT.Models;

namespace VT.Controllers
{
    public class HomeController : Controller
	{
		public IActionResult Index()
		{
			var reportData = new ReportDataResponseModel();

			try
			{
				var folder = new FolderCollectorHandler();
				var drawData = folder.BuildDrawData(Constants.JiraDataFolder, Constants.Start, Constants.End);
				reportData.From = Constants.Start;
				reportData.To = Constants.End;

				var analyzer = new DataAnalyzerHandler();
				reportData.TicketsData = analyzer.CollectTicketData(drawData);
			}
			catch (Exception ex)
			{

				throw;
			}


			return View(reportData);
		}


		public IActionResult Developer()
		{
			var reportData = new ReportDataResponseModel();

			try
			{
				var folder = new FolderCollectorHandler();
				var drawData = folder.BuildDrawData(Constants.JiraDataFolder, Constants.Start, Constants.End);
				reportData.From = Constants.Start;
				reportData.To = Constants.End;

				var analyzer = new DataAnalyzerHandler();
				reportData.TicketsData = analyzer.CollectTicketData(drawData);
			}
			catch (Exception ex)
			{

				throw;
			}


			return View(reportData);
		}


		public IActionResult Timereg()
		{
			var reportData = new ReportDataResponseModel();
			try
			{
				var timereg = new TimeregExcelHandler();
				var excelData = timereg.Read(Constants.TimeregNCData); //TODO: //var exclData = timereg.Read(Constants.TimeregNCData, 2020, 11);
				reportData.Timereg = timereg.Export(excelData);

			}
			catch (Exception ex)
			{

				throw;
			}


			return View(reportData);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
