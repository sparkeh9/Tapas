namespace Tapas.Cms.FlatFile.Core
{
    using System.Threading.Tasks;

    public interface IGitRepositoryProvider
    {
        Task CloneRepositoryToPathAsync( FlatFileCmsGitOptions options );
    }
}