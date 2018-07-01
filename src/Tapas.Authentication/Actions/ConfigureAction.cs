namespace Tapas.Authentication.Actions
{
    using System;
    using ExtCore.Infrastructure.Actions;
    using Microsoft.AspNetCore.Builder;

    public class ConfigureAction : IConfigureAction
    {
        public int Priority { get; } = 1000;

        public void Execute( IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider )
        {
            applicationBuilder.UseAuthentication();
        }
    }
}