namespace Tapas.Backend.UserManagement.Areas.Backend.Controllers
{
    using System.Collections.Generic;
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
    using Models.EditUser;
    using Models.ListUsers;

    [ Authorize( Policy = "Backend:Users:Manage" ) ]
    public class UsersController : BackendControllerBase
    {
        private readonly IMapper mapper;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController( UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<ApplicationRole> roleManager )
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.roleManager = roleManager;
        }

        [ HttpGet ]
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
        public async Task<IActionResult> New( CreateUserDto request )
        {
            if ( ModelState.IsValid )
            {
                var result = await userManager.CreateAsync( new ApplicationUser
                {
                    UserName = request.Username,
                    Email = request.Email,
                    NormalizedUserName = request.Name
                }, request.Password );

                if ( result.Succeeded )
                {
                    var newUser = await userManager.FindByEmailAsync( request.Email );
                    return RedirectToAction( "Edit", new { id = newUser.Id } );
                }

                AddErrors( result );
            }

            var viewModel = mapper.Map<CreateUserViewModel>( request );

            return View( viewModel );
        }

        [ HttpGet ]
        public async Task<IActionResult> Edit( string id )
        {
            var user = await userManager.FindByIdAsync( id );
            if ( user == null )
            {
                return NotFound();
            }

            var viewModel = mapper.Map<EditUserViewModel>( user );
            await PopulateRoles(user , viewModel );

            return View( viewModel );
        }

        [ HttpPost ]
        public async Task<IActionResult> Edit( string id, EditUserDto request )
        {
            var user = await userManager.FindByIdAsync( id );

            if ( user == null )
            {
                return NotFound();
            }

            var viewModel = mapper.Map<EditUserViewModel>( request );
            await PopulateRoles(user, viewModel );
            return View( viewModel );
        }


        private async Task PopulateRoles( ApplicationUser user, EditUserViewModel viewModel )
        {
            viewModel.Roles = await userManager.GetRolesAsync(user);
            viewModel.AllRoles = await roleManager.Roles.ToDictionaryAsync( x => x.Id.ToString(), x => x.Name );
        }

        private void AddErrors( IdentityResult result )
        {
            string GetPropertyName( IdentityError error )
            {
                switch ( error.Code )
                {
                    case "DuplicateUserName":
                        return nameof( CreateUserViewModel.Username );
                    case "DuplicateEmail":
                        return nameof( CreateUserViewModel.Email );
                    case "PasswordTooShort":
                    case "PasswordRequiresDigit":
                    case "PasswordRequiresUpper":
                    case "PasswordRequiresUniqueChars":
                        return nameof( CreateUserViewModel.Password );
                    default:
                        return string.Empty;
                }
            }


            foreach ( var error in result.Errors )
            {
                ModelState.AddModelError( GetPropertyName( error ), error.Description );
            }
        }
    }
}