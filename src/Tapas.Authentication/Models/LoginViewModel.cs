namespace Tapas.Authentication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [ Required ]
        public string UserName { get; set; }

        [ Required ]
        [ DataType( DataType.Password ) ]
        public string Password { get; set; }

        [ Required ]
        [ DataType( DataType.Password ) ]
        public string ConfirmPassword { get; set; }

        [ Required ]
        [ DataType( DataType.EmailAddress ) ]
        public string Email { get; set; }
    }
}