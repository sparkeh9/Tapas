namespace Tapas.Backend.UserManagement.Areas.Backend.Models.EditUser
{
    using System.Collections.Generic;

    public class EditUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
        private List<KeyValuePair<string, string>> Claims { get; set; }
    }
}