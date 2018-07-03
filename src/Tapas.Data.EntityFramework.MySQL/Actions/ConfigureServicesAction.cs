namespace Tapas.Data.EntityFramework.MySQL.Actions
{
    using System;
    using System.Reflection;
    using Entities;
    using ExtCore.Data.EntityFramework;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 100;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            var configuration = serviceProvider.GetService<IConfiguration>();

            string defaultConnectionString = configuration.GetConnectionString( "Default" );
            serviceCollection.AddDbContextPool<ApplicationDbContext>( options => options.UseMySql( defaultConnectionString ) );
            serviceCollection.AddIdentity<ApplicationUser, ApplicationRole>()
                             .AddEntityFrameworkStores<ApplicationDbContext>()
                             .AddDefaultTokenProviders();
            serviceCollection.Configure<StorageContextOptions>( options =>
                                                                {
                                                                    options.ConnectionString = defaultConnectionString;
                                                                    options.MigrationsAssembly = typeof( DesignTimeStorageContextFactory ).GetTypeInfo().Assembly.FullName;
                                                                } );


            DesignTimeStorageContextFactory.Initialize( serviceCollection.BuildServiceProvider() );
        }
    }
}