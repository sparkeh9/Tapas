namespace Tapas.Backend.Core.Actions
{
    using System;
    using ExtCore.Infrastructure.Actions;
    using Infrastructure.Assets;
    using Infrastructure.Menu;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Security;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            serviceCollection.Configure<BackendOptions>( configuration.GetSection( "Tapas:Backend" ) );

            serviceCollection.AddScoped<MenuViewModelFactory>();
            serviceCollection.AddScoped<BackendScriptsViewModelFactory>();
            serviceCollection.AddScoped<BackendStylesheetViewModelFactory>();
            serviceCollection.AddSingleton<IAuthorizationHandler, ClaimOrSuperAdminHandler>();
        }
    }
}