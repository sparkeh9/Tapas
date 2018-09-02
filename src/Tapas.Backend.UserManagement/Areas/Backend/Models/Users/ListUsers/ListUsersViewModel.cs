namespace Tapas.Backend.UserManagement.Areas.Backend.Models.Users.ListUsers
{
    using System.Collections.Generic;

    public class ListUsersViewModel
    {
        public string Query { get; set; }
        public int MaxPages { get; set; }
        public int MaxRows { get; set; }
        public int Page { get; set; }
        public int RowsPerPage { get; set; }

        public List<UserListing> Users { get; set; }

        public class UserListing
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
        }
    }
}