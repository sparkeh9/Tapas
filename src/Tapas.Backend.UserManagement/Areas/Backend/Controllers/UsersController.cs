namespace Tapas.Backend.UserManagement.Areas.Backend.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.Areas.Backend.Controllers;
    using Data.EntityFramework.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models.CreateUser;
    using Models.ListUsers;

    [ Authorize( Policy = "Backend:Users:Manage" ) ]
    public class UsersController : BackendControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UsersController( UserManager<ApplicationUser> userManager, IMapper mapper )
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.Select( u => new ListUsersViewModel.UserListing
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email
            } ).ToListAsync();

            return View( new ListUsersViewModel
            {
                Users = users
            } );
        }

        [ HttpGet ]
        public IActionResult New()
        {
            return View( new CreateUserViewModel() );
        }

        [ HttpPost ]
        public IActionResult New( CreateUserDto createUser )
        {
            if ( ModelState.IsValid )
            {
                return RedirectToAction( "Index" );
            }

            var viewModel = mapper.Map<CreateUserViewModel>( createUser );

            return View( viewModel );
        }
    }
}