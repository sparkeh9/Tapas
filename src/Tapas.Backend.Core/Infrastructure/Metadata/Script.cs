namespace Tapas.Backend.Core.Infrastructure.Metadata
{
    public class Script
    {
        public string Url {get;}
        public int Position {get;}

        public Script( string url, int position )
        {
            Url = url;
            Position = position;
        }
    }
}