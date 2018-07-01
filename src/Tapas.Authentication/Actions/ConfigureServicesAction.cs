namespace Tapas.Authentication.Actions
{
    using System;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            var configuration = serviceCollection.BuildServiceProvider()
                                                 .GetService<IConfigurationRoot>();

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