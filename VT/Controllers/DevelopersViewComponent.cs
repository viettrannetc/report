using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VT.Model;

namespace VT.Controllers
{
    public class DevelopersViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(DevelopersRequestModel model)
        {
            var result = new Models.Developers.DeveloperViewModel(model);           

            return View(result);
        }
    }
}
