namespace Tapas.Cms.FlatFile.Core.Tests.Git.GitRepositoryProviderTests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Core.Git;
    using Infrastructure.Helpers;
    using Shouldly;
    using Tapas.Core.Tests;
    using Xunit;

    public class When_cloning_a_public_repo : TestBase<GitRepositoryProvider>
    {
        protected static string RepoPath = $"TestArtifacts/{Guid.NewGuid()}/";

        [ Fact ]
        public async Task Should_clone_with_anonymous_credentials_to_relative_folder()
        {
            string gitRepoPath = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, RepoPath );

            try
            {
                var sut = SystemUnderTest();
                await sut.CloneRepositoryToPathAsync( new FlatFileCmsGitOptions
                {
                    RepositoryUrl = "https://github.com/sparkeh9/Tapas.git",
                    FilePath = RepoPath
                } );
                
                Directory.Exists( gitRepoPath )
                         .ShouldBeTrue();
            }
            finally
            {
                gitRepoPath.DeleteDirectory();
            }
        }

        [ Fact ]
        public async Task Should_clone_with_anonymous_credentials_to_absolute_folder()
        {
            string gitRepoPath = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, RepoPath );

            try
            {
                var sut = SystemUnderTest();
                await sut.CloneRepositoryToPathAsync( new FlatFileCmsGitOptions
                {
                    RepositoryUrl = "https://github.com/sparkeh9/Tapas.git",
                    FilePath = gitRepoPath
                } );


                Directory.Exists( gitRepoPath )
                         .ShouldBeTrue();
            }
            finally
            {
                gitRepoPath.DeleteDirectory();
            }
        }
    }
}