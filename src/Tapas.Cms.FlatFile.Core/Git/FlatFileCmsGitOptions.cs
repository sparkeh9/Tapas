namespace Tapas.Cms.FlatFile.Core.Git
{
    public class FlatFileCmsGitOptions
    {
        public string FilePath { get; set; }
        public string RepositoryUrl { get; set; }
        public string Branch { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Passphrase { get; set; }
    }
}