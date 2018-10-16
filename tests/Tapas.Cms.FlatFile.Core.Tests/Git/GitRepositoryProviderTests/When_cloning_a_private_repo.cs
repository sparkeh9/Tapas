namespace Tapas.Cms.FlatFile.Core.Tests.Git.GitRepositoryProviderTests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Core.Git;
    using Infrastructure;
    using Infrastructure.Helpers;
    using Shouldly;
    using Tapas.Core.Tests;
    using Xunit;

    [ Collection( nameof( FlatFileGitTestCollection ) ) ]
    public class When_cloning_a_private_repo : TestBase<GitRepositoryProvider>
    {
        protected static string RepoPath = $"TestArtifacts/{Guid.NewGuid()}/";
        private readonly FlatFileGitFixture fixture;

        public When_cloning_a_private_repo( FlatFileGitFixture fixture )
        {
            this.fixture = fixture;
        }

        [ Fact ]
        public async Task Should_clone_private_repo_with_username_and_password()
        {
            string gitRepoPath = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, RepoPath );

            try
            {
                var sut = SystemUnderTest();
                await sut.CloneRepositoryToPathAsync( new FlatFileCmsGitOptions
                {
                    RepositoryUrl = fixture.Options.HttpsPrivateRepo.RepoUrl,
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
        public async Task Should_clone_private_repo_with_ssh_keypair()
        {
            string gitRepoPath = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, RepoPath );

            try
            {
                var sut = SystemUnderTest();
                await sut.CloneRepositoryToPathAsync( new FlatFileCmsGitOptions
                {
                    RepositoryUrl = fixture.Options.SshPrivateRepo.RepoUrl,
                    FilePath = RepoPath,
                    PrivateKey = fixture.Options.SshPrivateRepo.PrivateKey,
                    PublicKey = fixture.Options.SshPrivateRepo.PublicKey,
                    Passphrase = fixture.Options.SshPrivateRepo.Passphrase,
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