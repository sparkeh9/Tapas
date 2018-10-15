namespace Tapas.Cms.FlatFile.Core.Tests.Infrastructure
{
    using Xunit;

    [ CollectionDefinition( nameof( FlatFileGitTestCollection ) ) ]
    public class FlatFileGitTestCollection : ICollectionFixture<FlatFileGitFixture> { }
}