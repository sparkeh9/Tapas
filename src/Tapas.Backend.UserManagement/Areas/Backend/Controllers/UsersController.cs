namespace Tapas.Backend.UserManagement.Areas.Backend.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.Areas.Backend.Controllers;
    using Data.EntityFramework.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models.Users.CreateUser;
    using Models.Users.EditUser;
    using Models.Users.ListUsers;
    using Tapas.Core.ExtensionMethods;

    [ Authorize( Policy = "Backend:Users:ManageUsers" ) ]
    public class UsersController : BackendControllerBase
    {
        private readonly IMapper mapper;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private int RowsPerPage;

        public UsersController( UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<ApplicationRole> roleManager )
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.roleManager = roleManager;
        }

        [ HttpGet ]
        public async Task<IActionResult> Index( int page = 1, string query = null )
        {
            RowsPerPage = 10;

            int maxRows = await userManager.Users
                                           .Where( user => query.IsNullOrWhiteSpace() || user.Email.Contains( query ) ||
                                                           query.IsNullOrWhiteSpace() || user.UserName.Contains( query ) )
                                           .CountAsync();


            int rowsToSkip = RowsPerPage * ( page - 1 ).MinimumValue();
            var usersQuery = userManager.Users
                                        .Where( user => query.IsNullOrWhiteSpace() || user.Email.Contains( query ) ||
                                                        query.IsNullOrWhiteSpace() || user.UserName.Contains( query ) )
                                        .Skip( rowsToSkip )
                                        .Take( RowsPerPage );

            var users = await usersQuery.Select( u => new ListUsersViewModel.UserListing
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email
            } ).ToListAsync();

            return View( new ListUsersViewModel
            {
                Page = page.MinimumValue( 1 ),
                MaxPages = ( (int) Math.Ceiling( maxRows / (double) RowsPerPage ) ).MinimumValue( 1 ),
                RowsPerPage = RowsPerPage,
                MaxRows = maxRows,
                Query = query,
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
            await PopulateRoles( user, viewModel );

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

            var userRoles = await userManager.GetRolesAsync( user );
//            var userClaims = await userManager.GetClaimsAsync( user );

            foreach ( string role in request.Roles.Except( userRoles ) )
            {
                await userManager.AddToRoleAsync( user, role );
            }

            foreach ( string role in userRoles.Except( request.Roles ) )
            {
                await userManager.RemoveFromRoleAsync( user, role );
            }

//            foreach (var kvp in request.Claims.Where(a => !userClaims.Any(b => claimTypes[a.Key] == b.Type && a.Value == b.Value)))
//            {
//                await userManager.AddClaimAsync( user, new Claim( claimTypes[ kvp.Key ], kvp.Value ) );
//            }
//
//            foreach (var claim in userClaims.Where(a => !request.Claims.Any(b => a.Type == claimTypes[b.Key] && a.Value == b.Value)))
//            {
//                await userManager.RemoveClaimAsync( user, claim );
//            }

            var viewModel = mapper.Map<EditUserViewModel>( request );
            await PopulateRoles( user, viewModel );
            return View( viewModel );
        }

        private async Task PopulateRoles( ApplicationUser user, EditUserViewModel viewModel )
        {
            viewModel.Roles = await userManager.GetRolesAsync( user );
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