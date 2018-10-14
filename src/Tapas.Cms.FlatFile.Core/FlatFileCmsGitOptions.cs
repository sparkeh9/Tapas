namespace Tapas.Cms.FlatFile.Core
{
    public class FlatFileCmsGitOptions
    {
        /// <summary>
        /// For non-SSH cloning
        /// </summary>
        public string Username { get; set; }
        public string Password { get; set; }

        public string FilePath { get; set; }
        public string RepositoryUrl { get; set; }
        public string Branch { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Passphrase { get; set; }
    }
}