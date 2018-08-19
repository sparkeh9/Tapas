namespace Tapas.Backend.UserManagement.Areas.Backend.Models.CreateUser
{
    using FluentValidation;

    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor( x => x.Username )
                .NotEmpty();

            RuleFor( x => x.Email )
                .NotEmpty()
                .EmailAddress();

            RuleFor( x => x.Password )
                .NotEmpty();
        }
    }
}