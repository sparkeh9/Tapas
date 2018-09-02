namespace Tapas.Backend.UserManagement.Areas.Backend.Models.Roles.EditRoles
{
    using FluentValidation;

    public class EditRoleDtoValidator : AbstractValidator<EditRoleDto>
    {
        public EditRoleDtoValidator()
        {
            RuleFor( x => x.Name )
                .NotEmpty();
        }
    }
}