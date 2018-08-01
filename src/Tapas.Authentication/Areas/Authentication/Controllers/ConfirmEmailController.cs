namespace Tapas.Authentication.Areas.Authentication.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Data.EntityFramework.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ AllowAnonymous ]
    public class ConfirmEmailController : AuthenticationBaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<ConfirmEmailController> logger;
        private readonly IEmailSender emailSender;

        public ConfirmEmailController( ILogger<ConfirmEmailController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IEmailSender emailSender )
        {
            this.signInManager = signInManager;
            this.logger = logger;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        [ HttpGet ]
        public async Task<IActionResult> Index( string userId, string code )
        {
            if ( userId == null || code == null )
            {
                return Redirect( Url.Content( "~/" ) );
            }

            var user = await userManager.FindByIdAsync( userId );
            if ( user == null )
            {
                return NotFound( $"Unable to load user with ID '{userId}'." );
            }

            var result = await userManager.ConfirmEmailAsync( user, code );
            if ( !result.Succeeded )
            {
                throw new InvalidOperationException( $"Error confirming email for user with ID '{userId}':" );
            }

            return View();
        }
    }
}