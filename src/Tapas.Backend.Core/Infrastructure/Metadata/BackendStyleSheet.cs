namespace Tapas.Backend.Core.Infrastructure.Metadata
{
    public class BackendStyleSheet
    {
        public string Url { get; }
        public int Position { get; }

        public BackendStyleSheet( string url, int position )
        {
            Url = url;
            Position = position;
        }
    }
}