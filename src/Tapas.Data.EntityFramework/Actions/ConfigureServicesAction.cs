namespace Tapas.Data.EntityFramework.Actions
{
    using System;
    using ExtCore.Data.EntityFramework;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            serviceCollection.Configure<StorageContextOptions>( configuration.GetSection( "Tapas:StorageContext" ) );
        }
    }
}