namespace Tapas.Backend.Core.Actions
{
    using System;
    using ExtCore.Infrastructure.Actions;
    using Infrastructure.Menu;
    using Microsoft.Extensions.DependencyInjection;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            serviceCollection.AddScoped<MenuViewModelFactory>();
        }
    }
}