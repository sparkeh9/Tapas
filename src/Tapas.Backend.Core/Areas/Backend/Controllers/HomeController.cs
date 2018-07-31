namespace Tapas.Backend.Core.Areas.Backend.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BackendControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}