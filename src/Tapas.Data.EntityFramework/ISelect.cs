namespace Tapas.Data.EntityFramework
{
    public interface ISelect<TEntity> : ISelect<TEntity, TEntity> { }

    public interface ISelect<in TEntity, TOut>
    {
        Task<TOut> ExecuteAsync( IQueryable<TEntity> entities );
    }
}