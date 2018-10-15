namespace Tapas.Cms.FlatFile.Core.Tests.Infrastructure
{
    public class TestFlatFileCmsOptions
    {
        public HttpsPrivateRepo HttpsPrivateRepo { get; set; }
        public SshPrivateRepo SshPrivateRepo { get; set; }
    }

    public class SshPrivateRepo
    {
        public string RepoUrl { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Passphrase { get; set; }
    }

    public class HttpsPrivateRepo
    {
        public string RepoUrl { get; set; }
    }
}