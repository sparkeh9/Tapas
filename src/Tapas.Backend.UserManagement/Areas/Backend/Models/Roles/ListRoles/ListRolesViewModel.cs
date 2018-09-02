namespace Tapas.Backend.UserManagement.Areas.Backend.Models.Roles.ListRoles
{
    using System.Collections.Generic;

    public class ListRolesViewModel
    {
        public string Query { get; set; }
        public int MaxPages { get; set; }
        public int MaxRows { get; set; }
        public int Page { get; set; }
        public int RowsPerPage { get; set; }

        public List<RoleListing> Roles { get; set; }

        public class RoleListing
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}