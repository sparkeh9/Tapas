namespace Tapas.Authentication.Areas.Authentication.Dto
{
    using System.ComponentModel.DataAnnotations;

    public class LoginDto
    {
        [ Required ]
        [ EmailAddress ]
        public string Email { get; set; }

        [ Required ]
        [ DataType( DataType.Password ) ]
        public string Password { get; set; }
    }
}