namespace Tapas.Data.EntityFramework.MySQL.Actions
{
    using System;
    using Entities;
    using ExtCore.Data.Abstractions;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            var configuration = serviceCollection.BuildServiceProvider()
                                                 .GetService<IConfigurationRoot>();

            serviceCollection.AddDbContext<ApplicationDbContext>( options => options.UseMySQL( configuration.GetConnectionString( "Default" ) ) );

            serviceCollection.AddIdentity<ApplicationUser, ApplicationRole>()
                             .AddEntityFrameworkStores<ApplicationDbContext>()
                             .AddDefaultTokenProviders();

            serviceCollection.AddScoped( typeof( IStorageContext ), typeof( ApplicationDbContext ) );
        }
    }
}