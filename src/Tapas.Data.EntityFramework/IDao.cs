namespace Tapas.Data.EntityFramework
{
    using System.Linq;
    using System.Threading.Tasks;
    using ExtCore.Data.Entities.Abstractions;

    public interface IDao
    {
        Task<TOut> FindAsync<TEntity, TOut>( ISelect<TEntity, TOut> selector ) where TEntity : class;
        Task<TEntity> GetAsync<TEntity>( long id ) where TEntity : class, IEntity;
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity;
        Task ProcessAsync( IProcessor processor );
        Task SaveOrUpdateAsync<TEntity>( TEntity entity ) where TEntity : class;
        Task DeleteAsync<TEntity>( TEntity entity ) where TEntity : class;
        Task DeleteAsync<T>( long id ) where T : class, IEntity;
    }
}