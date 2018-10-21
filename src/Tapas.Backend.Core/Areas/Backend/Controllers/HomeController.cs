namespace Tapas.Backend.Core.Areas.Backend.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Razor.Internal;

    public class HomeController : BackendControllerBase
    {
        private readonly IRazorViewEngineFileProviderAccessor razorAccessor;
        public HomeController( IRazorViewEngineFileProviderAccessor razorAccessor )
        {
            this.razorAccessor = razorAccessor;
        }

        public IActionResult Index()
        {
            var providers = razorAccessor.FileProvider;
            return View();
        }
    }
}