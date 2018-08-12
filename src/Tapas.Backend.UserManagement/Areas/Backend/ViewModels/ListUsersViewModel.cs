namespace Tapas.Backend.UserManagement.Areas.Backend.ViewModels
{
    using System.Collections.Generic;

    public class ListUsersViewModel
    {
        public List<UserListing> Users { get; set; }

        public class UserListing
        {
            public string Username { get; set; }
            public int Id { get; set; }
        }
    }
}