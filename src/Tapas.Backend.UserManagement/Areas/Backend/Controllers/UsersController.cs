namespace Tapas.Backend.UserManagement.Areas.Backend.Controllers
{
    using System.Linq;
    using Core.Areas.Backend.Controllers;
    using Data.EntityFramework.Entities;
    using ExtCore.Data.Abstractions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [ Authorize( Policy = "Backend:Users:Manage" ) ]
    public class UsersController : BackendControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        public UsersController(UserManager<ApplicationUser> userManager )
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = userManager.Users.Select( u => new ListUsersViewModel.UserListing
            {
                Id = u.Id,
                Username = u.UserName
            } ).ToList();

            return View(new ListUsersViewModel
            {
                Users = users
            });
        }
    }
}