namespace Tapas.Data.EntityFramework.Actions
{
    using System;
    using ExtCore.Data.Abstractions;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.Extensions.DependencyInjection;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            serviceCollection.AddScoped( typeof( IStorageContext ), typeof( ApplicationDbContext ) );
        }
    }
}