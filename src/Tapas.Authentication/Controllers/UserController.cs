namespace Tapas.Authentication.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class UserController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public UserController( UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [ HttpPost ]
        [ ValidateAntiForgeryToken ]
        public async Task<IActionResult> Login( LoginViewModel model )
        {
            if ( !ModelState.IsValid )
            {
                return View( model );
            }

            var user = await userManager.FindByNameAsync( model.Username );
            if ( user != null )
            {
                if ( !await userManager.IsEmailConfirmedAsync( user ) )
                {
                    ModelState.AddModelError( string.Empty, "You must confirm your account before logging in" );
                    return View( model );
                }
            }

            var result = await signInManager.PasswordSignInAsync( model.Username, model.Password, false, false );

            if ( result.Succeeded )
            {
                return RedirectToAction( "Index", "Home" );
            }

            ModelState.AddModelError( string.Empty, "Login Failed" );
            return View( model );
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction( "Index", "Home" );
        }

        [ HttpPost ]
        [ ValidateAntiForgeryToken ]
        public async Task<IActionResult> Register( RegisterViewModel model )
        {
            if ( !ModelState.IsValid )
            {
                return View( model );
            }

            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync( user, model.Password );
            if ( result.Succeeded )
            {
                await signInManager.SignInAsync( user, false );

                string confrimationCode =
                    await userManager.GenerateEmailConfirmationTokenAsync( user );

                string callbackurl = Url.Action(
                                                controller : "User",
                                                action : "Confirm",
                                                values : new { userId = user.Id, code = confrimationCode },
                                                protocol : Request.Scheme );

//                await this.emailSender.SendEmailAsync(
//                                                      email : user.Email,
//                                                      subject : "Confirm Email",
//                                                      message : callbackurl );
//
                return RedirectToAction( "Index", "Home" );
            }

            return View( model );
        }

        public async Task<IActionResult> Confirm( string userId, string code )
        {
            if ( userId == null || code == null )
                return RedirectToAction( "Index", "Home" );

            var user = await userManager.FindByIdAsync( userId );
            if ( user == null )
                throw new ApplicationException( $"Could not find user with ID '{userId}'." );

            var result = await userManager.ConfirmEmailAsync( user, code );
            if ( result.Succeeded )
                return View();

            return RedirectToAction( "Index", "Home" );
        }
    }
}