namespace Tapas.Data.EntityFramework.SQLite
{
    using ExtCore.Data.EntityFramework;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    public class StorageContext : StorageContextBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StorageContext">StorageContext</see> class.
        /// </summary>
        /// <param name="connectionString">The connection string that is used to connect to the MySQL database.</param>
        public StorageContext( IOptions<StorageContextOptions> options ) : base( options ) { }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            base.OnConfiguring( optionsBuilder );

            if ( string.IsNullOrEmpty( MigrationsAssembly ) )
            {
                optionsBuilder.UseSqlite( ConnectionString );
            }

            else
            {
                optionsBuilder.UseSqlite( ConnectionString, options => { options.MigrationsAssembly( MigrationsAssembly ); } );
            }
        }
    }
}