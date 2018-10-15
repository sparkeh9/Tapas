namespace Tapas.Cms.FlatFile.Core.Git
{
    using System.Threading.Tasks;

    public interface IGitRepositoryProvider
    {
        Task CloneRepositoryToPathAsync( FlatFileCmsGitOptions options );
    }
}