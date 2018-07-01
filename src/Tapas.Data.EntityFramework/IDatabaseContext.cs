namespace Tapas.Data.EntityFramework
{
    public interface IDatabaseContext
    {
        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>( TEntity entity ) where TEntity : class;
        Task<int> SaveChangesAsync( CancellationToken cancellationToken = default( CancellationToken ) );
    }
}