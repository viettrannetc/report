using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VT.Model;

namespace VT.Controllers
{
    public class KPI004ViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(KPIRequestModel model)
        {
            var result = new Models.Monthly.KPIs.KPI004ViewModel(model);

            return View(result);
        }
    }
}
