namespace Tapas.Data.EntityFramework.PostgreSql.Actions
{
    using System;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class ConfigureAction : IConfigureAction
    {
        public int Priority { get; } = 1000;

        public void Execute( IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider )
        {
            var env = serviceProvider.GetService<IHostingEnvironment>();

            if ( env.IsDevelopment() )
            {
                applicationBuilder.UseDatabaseErrorPage();
            }
        }
    }
}