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
				var drawData = folder.BuildDrawData(Constants.RootFolder, new DateTime(2020, 11, 01), new DateTime(2020, 11, 30));

				var analyzer = new DataAnalyzerHandler();
				reportData.TicketsData = analyzer.CollectTicketData(drawData);


			}
			catch (Exception ex)
			{

				throw;
			}


			return View(reportData);
		}

		public IActionResult Privacy()
		{
			//var jira = new JiraHandler();
			//var combinedData = jira.CombineDataWareHouse();
			
			var reportData = new ReportDataResponseModel();

			try
            {
                var folder = new FolderCollectorHandler();
                var drawData = folder.BuildDrawData(Constants.RootFolder, new DateTime(2020, 11, 01), new DateTime(2020, 11, 30));

                var analyzer = new DataAnalyzerHandler();
				reportData.TicketsData = analyzer.CollectTicketData(drawData);


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
