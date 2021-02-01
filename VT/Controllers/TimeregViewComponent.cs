using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VT.Models.Timereg;

namespace VT.Controllers
{
    public class TimeregViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(TimeregMonthlyModel model)
        {
            return View(model);
        }
    }
}
