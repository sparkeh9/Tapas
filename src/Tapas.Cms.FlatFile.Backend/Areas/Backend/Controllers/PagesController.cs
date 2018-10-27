namespace Tapas.Cms.FlatFile.Backend.Areas.Backend.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Tapas.Backend.Core.Areas.Backend.Controllers;

    [ Authorize( Policy = "Backend:Pages:ManagePages" ) ]
    public class PagesController : BackendControllerBase
    {
        [ HttpGet ]
        public async Task<IActionResult> Index( int page = 1, string query = null )
        {
            return View();
        }
    }
}