namespace Tapas.Backend.Core.Areas.Backend.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ Area( "Backend" ) ]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}