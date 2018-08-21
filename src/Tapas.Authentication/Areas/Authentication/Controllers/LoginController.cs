namespace Tapas.Authentication.Areas.Authentication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Data.EntityFramework.Entities;
    using Dto;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    [ AllowAnonymous ]
    public class LoginController : AuthenticationBaseController
    {
        private readonly ILogger<LoginController> logger;
        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginController( ILogger<LoginController> logger, SignInManager<ApplicationUser> signInManager )
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [ HttpGet ]
        public IActionResult Index( string returnUrl = null )
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [ HttpPost ]
        public async Task<IActionResult> Index( LoginDto loginDto, string returnUrl = null )
        {
            returnUrl = returnUrl ?? Url.Content( "~/" );
            ViewBag.ReturnUrl = returnUrl;

            if ( ModelState.IsValid )
            {
                var result = await signInManager.PasswordSignInAsync( loginDto.Email, loginDto.Password, false, lockoutOnFailure : false );

                if ( result.Succeeded )
                {
                    return LocalRedirect( returnUrl );
                }

                ModelState.AddModelError( string.Empty, "Invalid login attempt." );
                return View( loginDto );
            }

            return View( loginDto );
        }
    }
}