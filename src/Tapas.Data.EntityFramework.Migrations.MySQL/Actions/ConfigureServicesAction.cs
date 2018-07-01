namespace Tapas.Data.EntityFramework.Migrations.MySQL.Actions
{
    using System;
    using System.Reflection;
    using ExtCore.Data.EntityFramework;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            var configuration = serviceCollection.BuildServiceProvider()
                                                 .GetService<IConfigurationRoot>();

            serviceCollection.Configure<StorageContextOptions>( options =>
                                                                {
                                                                    options.ConnectionString = configuration.GetConnectionString( "Default" );
                                                                    options.MigrationsAssembly = typeof( DesignTimeStorageContextFactory ).GetTypeInfo().Assembly.FullName;
                                                                } );
            DesignTimeStorageContextFactory.Initialize( serviceCollection.BuildServiceProvider() );
        }
    }
}