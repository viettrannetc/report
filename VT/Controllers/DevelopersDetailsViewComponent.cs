using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VT.Model;

namespace VT.Controllers
{
    public class DevelopersDetailsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(DevelopersDetailsRequestModel model)
        {
            var result = new Models.Developers.DeveloperDetailsViewModel(model);           

            return View(result);
        }
    }
}
