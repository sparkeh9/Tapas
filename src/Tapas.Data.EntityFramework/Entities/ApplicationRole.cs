namespace Tapas.Data.EntityFramework.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole( string name ) : base( name ) { }
    }
}