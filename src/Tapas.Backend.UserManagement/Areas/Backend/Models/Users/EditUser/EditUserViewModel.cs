namespace Tapas.Backend.UserManagement.Areas.Backend.Models.Users.EditUser
{
    using System.Collections.Generic;

    public class EditUserViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public Dictionary<string, string> AllRoles { set; get; }
        //        public Dictionary<string, string> Claims { get; set; }
    }
}