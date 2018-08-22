namespace Tapas.Backend.UserManagement.Areas.Backend.Models.EditUser
{
    using System.Collections.Generic;

    public class EditUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
//        public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();
    }
}