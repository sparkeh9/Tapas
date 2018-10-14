namespace Tapas.Cms.FlatFile.Core
{
    using System;
    using System.Threading.Tasks;
    using LibGit2Sharp;
    using Tapas.Core.ExtensionMethods;

    public class GitRepositoryProvider : IGitRepositoryProvider
    {
        public async Task CloneRepositoryToPathAsync( FlatFileCmsGitOptions options )
        {
            await Task.Delay( 0 );

            Repository.Clone( options.RepositoryUrl, options.FilePath, new CloneOptions
            {
                BranchName = options.Branch,
                CredentialsProvider = ( url, fromUrl, types ) => BuildCredentialsProvider( url, fromUrl, types, options )
            } );
        }

        private Credentials BuildCredentialsProvider( string url, string fromUrl, SupportedCredentialTypes types, FlatFileCmsGitOptions options )
        {
            var repoUri = new Uri( options.RepositoryUrl );

            if ( repoUri.Scheme == "http" || repoUri.Scheme == "https" )
            {
                if ( options.Username.IsNullOrWhiteSpace() )
                {
                    return new DefaultCredentials();
                }

                return new UsernamePasswordCredentials
                {
                    Username = options.Username,
                    Password = options.Password
                };
            }

            if ( repoUri.Scheme == "git" )
            {
                return new SshUserKeyCredentials
                {
                    Username = repoUri.UserInfo.Split( ':' )[ 0 ],
                    PublicKey = options.PublicKey,
                    PrivateKey = options.PrivateKey,
                    Passphrase = options.Passphrase
                };
            }

            throw new ArgumentOutOfRangeException( nameof( options.RepositoryUrl ), "Unknown scheme for git repository" );
        }
    }
}