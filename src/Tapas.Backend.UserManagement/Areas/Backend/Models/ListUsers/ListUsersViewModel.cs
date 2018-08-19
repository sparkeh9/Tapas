namespace Tapas.Backend.UserManagement.Areas.Backend.Models.ListUsers
{
    using System.Collections.Generic;

    public class ListUsersViewModel
    {
        public List<UserListing> Users { get; set; }

        public class UserListing
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
        }
    }
}