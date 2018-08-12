namespace Tapas.Backend.Core.Infrastructure.Metadata
{
    public class StyleSheet
    {
        public string Url { get; }
        public int Position { get; }

        public StyleSheet( string url, int position )
        {
            Url = url;
            Position = position;
        }
    }
}