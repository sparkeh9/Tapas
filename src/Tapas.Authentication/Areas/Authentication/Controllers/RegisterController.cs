using Microsoft.AspNetCore.Mvc;

namespace Tapas.Authentication.Areas.Authentication.Controllers
{
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Data.EntityFramework.Entities;
    using Dto;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Logging;

    [ Area( "Authentication" ) ]
    public class RegisterController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterController> logger;
        private readonly IEmailSender emailSender;

        public RegisterController( ILogger<RegisterController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IEmailSender emailSender )
        {
            this.signInManager = signInManager;
            this.logger = logger;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        [ HttpGet ]
        public IActionResult Index(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [ HttpPost ]
        public async Task<IActionResult> Index( RegisterDto registerDto, string returnUrl = null )
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = registerDto.Email, Email = registerDto.Email };
                var result = await userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");

                    string code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    string callbackUrl = Url.Action( "Index", "ConfirmEmail", new { userId = user.Id, code }, Request.Scheme );
                    
                    await emailSender.SendEmailAsync(registerDto.Email, "Confirm your email",
                                                      $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await signInManager.SignInAsync(user, false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(registerDto);
        }
    }
}