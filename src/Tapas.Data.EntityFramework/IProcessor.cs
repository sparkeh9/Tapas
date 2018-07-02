namespace Tapas.Data.EntityFramework
{
    using System.Threading.Tasks;

    public interface IProcessor
    {
        Task ProcessAsync( IDatabaseContext context );
    }

    public interface IProcessor<out TOut>
    {
        TOut ProcessAsync( IDatabaseContext context );
    }
}