using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VT.Common;
using VT.Implementation;
using VT.Model;

namespace VT.Controllers
{
    public class ReportViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ReportDataRequestModel model)
        {
            var reportData = new ReportDataResponseModel();

            try
            {
                //TODO: when we have database, we should check from db before reading data from csv file
                var folder = new FolderCollectorHandler();                
                var drawData = folder.BuildDrawData(Constants.JiraDataFolder, model.From, model.To);

                var analyzer = new DataAnalyzerHandler();
                reportData.TicketsData = analyzer.CollectTicketData(drawData);

                //TODO: when we have database, we should store the result to db for re-using purpose.
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(reportData);
        }
    }
}
