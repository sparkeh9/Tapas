namespace Tapas.Authentication.Areas.Authentication.Controllers
{
    using System.Threading.Tasks;
    using Data.EntityFramework.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ AllowAnonymous ]
    public class LogoutController : AuthenticationBaseController
    {
        private readonly ILogger<LogoutController> logger;
        private readonly SignInManager<ApplicationUser> signInManager;

        public LogoutController( ILogger<LogoutController> logger, SignInManager<ApplicationUser> signInManager )
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [ HttpGet ]
        public async Task<IActionResult> Index( string returnUrl = null )
        {
            await signInManager.SignOutAsync();
            return LocalRedirect( returnUrl ?? Url.Content( "~/" ) );
        }
    }
}