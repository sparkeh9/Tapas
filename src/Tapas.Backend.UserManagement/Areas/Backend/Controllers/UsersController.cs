namespace Tapas.Backend.UserManagement.Areas.Backend.Controllers
{
    using Core.Areas.Backend.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ Authorize( Policy = "Backend:Users:Manage" ) ]
    public class UsersController : BackendControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}