namespace Tapas.Cms.FlatFile.Core.Git
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using LibGit2Sharp;
    using Tapas.Core.ExtensionMethods;

    public class GitRepositoryProvider : IGitRepositoryProvider
    {
        public async Task CloneRepositoryToPathAsync( FlatFileCmsGitOptions options )
        {
            await Task.Delay( 0 );

            string absoluteFilePath = options.FilePath;
            if ( !Path.IsPathFullyQualified( absoluteFilePath ) )
            {
                absoluteFilePath = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, absoluteFilePath );
            }

            Repository.Clone( options.RepositoryUrl, absoluteFilePath, new CloneOptions
            {
                BranchName = options.Branch,
                CredentialsProvider = ( url, fromUrl, types ) => BuildCredentialsProvider( url, fromUrl, types, options )
            } );
        }

        private Credentials BuildCredentialsProvider( string url, string fromUrl, SupportedCredentialTypes types, FlatFileCmsGitOptions options )
        {
            if ( types.HasFlag( SupportedCredentialTypes.Ssh ) )
            {
                string username = fromUrl.IsNullOrWhiteSpace() ? "git" : fromUrl;

                return new SshUserKeyCredentials
                {
                    Username = username,
                    PublicKey = options.PublicKey,
                    PrivateKey = options.PrivateKey,
                    Passphrase = options.Passphrase
                };
            }
            
            if ( types.HasFlag( SupportedCredentialTypes.UsernamePassword ) )
            {
                var repoUri = new Uri( url );
                var userInfo = repoUri.UserInfo.Split( ':' );

                if ( !userInfo.Any() )
                {
                    return new DefaultCredentials();
                }

                string username = userInfo[ 0 ];
                string password = userInfo.ElementAtOrDefault( 1 ) ?? string.Empty;

                return new UsernamePasswordCredentials
                {
                    Username = username,
                    Password = password
                };
            }

            return new DefaultCredentials();
        }
    }
}