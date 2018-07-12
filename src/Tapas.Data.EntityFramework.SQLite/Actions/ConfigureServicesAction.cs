namespace Tapas.Data.EntityFramework.SQLite.Actions
{
    using System;
    using System.Reflection;
    using Entities;
    using ExtCore.Data.Abstractions;
    using ExtCore.Data.EntityFramework;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 100;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            var storage = serviceProvider.GetService<IOptions<StorageContextOptions>>();

            DesignTimeStorageContextFactory.Initialize( serviceCollection.BuildServiceProvider() );
            serviceCollection.AddScoped( typeof( IStorageContext ), typeof( ApplicationDbContext ) );
            serviceCollection.AddDbContextPool<ApplicationDbContext>( options => options.UseSqlServer( storage.Value.ConnectionString, b => b.MigrationsAssembly( storage?.Value?.MigrationsAssembly ?? typeof( DesignTimeStorageContextFactory ).GetTypeInfo().Assembly.FullName ) ) );
            serviceCollection.AddIdentity<ApplicationUser, ApplicationRole>()
                             .AddEntityFrameworkStores<ApplicationDbContext>()
                             .AddDefaultTokenProviders();
        }
    }
}