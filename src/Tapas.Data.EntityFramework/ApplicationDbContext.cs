namespace Tapas.Data.EntityFramework
{
    using Entities;
    using ExtCore.Data.Abstractions;
    using ExtCore.Data.EntityFramework;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>, IStorageContext
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options ) : base( options ) { }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );
            this.RegisterEntities( modelBuilder );
        }
    }
}