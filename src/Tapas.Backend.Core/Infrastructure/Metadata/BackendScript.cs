namespace Tapas.Backend.Core.Infrastructure.Metadata
{
    public class BackendScript
    {
        public string Url {get;}
        public int Position {get;}

        public BackendScript( string url, int position )
        {
            Url = url;
            Position = position;
        }
    }
}