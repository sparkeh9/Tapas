namespace Tapas.Backend.UserManagement.Actions
{
    using System;
    using Areas.Backend.Models.Roles.CreateRole;
    using Areas.Backend.Models.Users.CreateUser;
    using Areas.Backend.Models.Users.EditUser;
    using ExtCore.Infrastructure.Actions;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using CreateUserDtoValidator = Areas.Backend.Models.Users.CreateUser.CreateUserDtoValidator;

    public class ConfigureServicesAction : IConfigureServicesAction
    {
        public int Priority => 1000;

        public void Execute( IServiceCollection serviceCollection, IServiceProvider serviceProvider )
        {
            serviceCollection.AddTransient<IValidator<CreateUserDto>, CreateUserDtoValidator>();
            serviceCollection.AddTransient<IValidator<EditUserDto>, EditUserDtoValidator>();


            serviceCollection.AddTransient<IValidator<CreateRoleDto>, CreateRoleDtoValidator>();
//            serviceCollection.AddTransient<IValidator<EditUserDto>, EditUserDtoValidator>();
        }
    }
}