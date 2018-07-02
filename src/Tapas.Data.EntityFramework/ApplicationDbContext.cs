namespace Tapas.Data.EntityFramework
{
    using Core.Entities;
    using ExtCore.Data.Abstractions;
    using ExtCore.Data.EntityFramework;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>, IStorageContext
    {
        public ApplicationDbContext( DbContextOptions options ) : base( options ) { }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );
            this.RegisterEntities( modelBuilder );
        }
    }
}