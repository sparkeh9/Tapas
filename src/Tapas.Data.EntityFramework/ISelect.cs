namespace Tapas.Data.EntityFramework
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface ISelect<TEntity> : ISelect<TEntity, TEntity> { }

    public interface ISelect<in TEntity, TOut>
    {
        Task<TOut> ExecuteAsync( IQueryable<TEntity> entities );
    }
}