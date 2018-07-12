namespace Tapas.Authentication.Actions
{
    using System;
    using Data.EntityFramework.Entities;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            var configuration = serviceCollection.BuildServiceProvider()
                                                 .GetService<IConfiguration>();
            serviceCollection.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
//            serviceCollection.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();
            serviceCollection.Configure<IdentityOptions>( options =>
                                                          {
                                                              // Password settings
                                                              options.Password.RequireDigit = true;
                                                              options.Password.RequiredLength = 8;
                                                              options.Password.RequireNonAlphanumeric = false;
                                                              options.Password.RequireUppercase = true;
                                                              options.Password.RequireLowercase = false;
                                                              options.Password.RequiredUniqueChars = 6;

                                                              // Lockout settings
                                                              options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes( 30 );
                                                              options.Lockout.MaxFailedAccessAttempts = 10;
                                                              options.Lockout.AllowedForNewUsers = true;

                                                              // User settings
                                                              options.User.RequireUniqueEmail = true;
                                                          } );

            serviceCollection.ConfigureApplicationCookie( options =>
                                                          {
                                                              options.LoginPath = "/user/login";
                                                              options.LogoutPath = "/user/logout";
                                                              options.AccessDeniedPath = "/user/access-denied";
                                                              options.SlidingExpiration = true;
                                                              options.Cookie = new CookieBuilder
                                                              {
                                                                  HttpOnly = true,
                                                                  Name = configuration.GetValue<string>( "Tapas:CookieName" ),
                                                                  Path = "/",
                                                                  SameSite = SameSiteMode.Lax,
                                                                  SecurePolicy = CookieSecurePolicy.SameAsRequest
                                                              };
                                                          } );
        }
    }
}