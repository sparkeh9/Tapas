namespace Tapas.Data.EntityFramework
{
    public interface IProcessor
    {
        Task ProcessAsync( IDatabaseContext context );
    }

    public interface IProcessor<out TOut>
    {
        TOut ProcessAsync( IDatabaseContext context );
    }
}