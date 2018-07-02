namespace Tapas.Data.EntityFramework
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    public interface IDatabaseContext
    {
        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>( TEntity entity ) where TEntity : class;
        Task<int> SaveChangesAsync( CancellationToken cancellationToken = default( CancellationToken ) );
    }
}