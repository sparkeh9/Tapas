namespace Tapas.Backend.UserManagement.Areas.Backend.Models.Roles.EditRoles
{
    using System.Collections.Generic;
    using System.Security.Claims;

    public class EditRoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Claim> Claims { get; set; }
        public List<string> ClaimTypes { get; set; }
}
}