namespace Tapas.Backend.UserManagement.Areas.Backend.Models.Roles.CreateRole
{
    using FluentValidation;

    public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleDtoValidator()
        {
            RuleFor( x => x.Name )
                .NotEmpty();
        }
    }
}