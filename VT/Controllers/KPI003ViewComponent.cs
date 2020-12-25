using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VT.Model;

namespace VT.Controllers
{
    public class KPI003ViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(KPIRequestModel model)
        {
            var result = new Models.Monthly.KPIs.KPI003ViewModel(model);           

            return View(result);
        }
    }
}
