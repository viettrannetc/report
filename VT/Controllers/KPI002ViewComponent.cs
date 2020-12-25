using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VT.Model;

namespace VT.Controllers
{
    public class KPI002ViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(KPIRequestModel model)
        {
            var result = new Models.Monthly.KPIs.KPI002ViewModel(model);           

            return View(result);
        }
    }
}
