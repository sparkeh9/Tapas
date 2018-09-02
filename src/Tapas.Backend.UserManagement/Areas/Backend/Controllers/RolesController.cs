namespace Tapas.Backend.UserManagement.Areas.Backend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.Areas.Backend.Controllers;
    using Data.EntityFramework.Entities;
    using ExtCore.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models.Roles.CreateRole;
    using Models.Roles.EditRoles;
    using Models.Roles.ListRoles;
    using Tapas.Core.ExtensionMethods;
    using Tapas.Core.Security.Policy;

    [ Authorize( Policy = "Backend:Users:ManageRoles" ) ]
    public class RolesController : BackendControllerBase
    {
        private readonly Dictionary<string, string> claimTypes;
        private readonly IMapper mapper;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private int RowsPerPage;

        public RolesController( UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<ApplicationRole> roleManager )
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.roleManager = roleManager;

            var fieldInfo = typeof( ClaimTypes ).GetFields( BindingFlags.Static | BindingFlags.Public );
            claimTypes = fieldInfo.ToDictionary( i => i.Name, i => (string) i.GetValue( null ) );
        }

        [ HttpGet ]
        public async Task<IActionResult> Index( int page = 1, string query = null )
        {
            RowsPerPage = 10;

            int maxRows = await roleManager.Roles
                                           .Where( role => query.IsNullOrWhiteSpace() || role.Name.Contains( query, StringComparison.InvariantCultureIgnoreCase ) )
                                           .CountAsync();


            int rowsToSkip = RowsPerPage * ( page - 1 ).MinimumValue();
            var usersQuery = roleManager.Roles
                                        .Where( role => query.IsNullOrWhiteSpace() || role.Name.Contains( query, StringComparison.InvariantCultureIgnoreCase ) )
                                        .Skip( rowsToSkip )
                                        .Take( RowsPerPage );

            var roles = await usersQuery.Select( u => new ListRolesViewModel.RoleListing
            {
                Id = u.Id,
                Name = u.Name
            } ).ToListAsync();

            return View( new ListRolesViewModel
            {
                Page = page.MinimumValue( 1 ),
                MaxPages = ( (int) Math.Ceiling( maxRows / (double) RowsPerPage ) ).MinimumValue( 1 ),
                RowsPerPage = RowsPerPage,
                MaxRows = maxRows,
                Query = query,
                Roles = roles
            } );
        }

        [ HttpGet ]
        public IActionResult New()
        {
            return View( new CreateRoleViewModel() );
        }

        [ HttpPost ]
        public async Task<IActionResult> New( CreateRoleDto request )
        {
            if ( ModelState.IsValid )
            {
                var result = await roleManager.CreateAsync( new ApplicationRole( request.Name ) );

                if ( result.Succeeded )
                {
                    var role = await roleManager.FindByNameAsync( request.Name );
                    return RedirectToAction( "Edit", new { id = role.Id } );
                }
            }

            var viewModel = mapper.Map<CreateRoleViewModel>( request );

            return View( viewModel );
        }

        [ HttpGet ]
        public async Task<IActionResult> Edit( string id )
        {
            ApplicationRole role = await roleManager.FindByIdAsync( id );
            if ( role == null )
            {
                return NotFound();
            }

            var viewModel = mapper.Map<EditRoleViewModel>( role );
            await PopulateClaims( role, viewModel );

            return View( viewModel );
        }

        [ HttpPost ]
        public async Task<IActionResult> Edit( string id, EditRoleDto request )
        {
            var role = await roleManager.FindByIdAsync( id );

            if ( role == null )
            {
                return NotFound();
            }

            if ( ModelState.IsValid )
            {
                var result = await roleManager.CreateAsync( new ApplicationRole( request.Name ) );

                if ( result.Succeeded )
                {
                    return RedirectToAction( "Edit", new { id = role.Id } );
                }
            }


            var viewModel = mapper.Map<EditRoleViewModel>( role );
            await PopulateClaims( role, viewModel );
            return View( viewModel );
        }

        [ HttpPost ]
        public async Task<IActionResult> AddClaim( string id, [ FromForm ] string claimType, [ FromForm ] string claimValue )
        {
            var role = await roleManager.FindByIdAsync( id );

            if ( role == null )
            {
                return NotFound();
            }

            await roleManager.AddClaimAsync( role, new Claim( claimType, claimValue ) );

            return Ok();
        }

        [ HttpPost ]
        public async Task<IActionResult> RemoveClaim( string id, [ FromForm ] string claimType, [ FromForm ] string claimValue )
        {
            var role = await roleManager.FindByIdAsync( id );

            if ( role == null )
            {
                return NotFound();
            }

            await roleManager.RemoveClaimAsync( role, new Claim( claimType, claimValue ) );

            return Ok();
        }

        [ HttpGet ]
        [ ActionName( "get-claims" ) ]
        public IActionResult GetClaims( string claimType = "Permission" )
        {
            var permissions = ExtensionManager.GetInstances<IModuleAuthorisationFactory>()
                                              .SelectMany( x => x.GetClaims() )
                                              .Where( x => x.Type == claimType )
                                              .Select( x => x.Value );

            return Json( permissions );
        }

        private async Task PopulateClaims( ApplicationRole role, EditRoleViewModel viewModel )
        {
            viewModel.Claims = ( await roleManager.GetClaimsAsync( role ) ).ToList();
            viewModel.ClaimTypes = new List<string>
            {
                "Permission"
            };
        }
    }
}