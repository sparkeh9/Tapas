namespace Tapas.Authentication.Areas.Authentication.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ AllowAnonymous ]
    [Route("[area]/access-denied")]
    public class AccessDeniedController : AuthenticationBaseController
    {
        [ HttpGet ]
        public IActionResult Index( string returnUrl = null )
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}