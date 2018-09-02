namespace Tapas.Backend.UserManagement.Areas.Backend.Models.Roles.EditRoles
{
    using System.Collections.Generic;

    public class EditRoleViewModel
    {
        public string Name { get; set; }
        public Dictionary<string, string> Claims { get; set; }
        public List<string> ClaimTypes { get; set; }
    }
}