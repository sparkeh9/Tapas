namespace Tapas.Backend.UserManagement.Areas.Backend.Models.Users.EditUser
{
    using FluentValidation;

    public class EditUserDtoValidator : AbstractValidator<EditUserDto>
    {
        public EditUserDtoValidator()
        {
            RuleFor( x => x.Username )
                .NotEmpty();

            RuleFor( x => x.Email )
                .NotEmpty()
                .EmailAddress();
        }
    }
}