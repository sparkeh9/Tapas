namespace Tapas.Backend.UserManagement.Actions
{
    using System;
    using Areas.Backend.Models.CreateUser;
    using ExtCore.Infrastructure.Actions;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            serviceCollection.AddTransient<IValidator<CreateUserDto>, CreateUserDtoValidator>();
        }
    }
}