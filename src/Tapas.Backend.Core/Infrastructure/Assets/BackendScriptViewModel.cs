namespace Tapas.Backend.Core.Infrastructure.Assets
{
    using System.Linq;
    using Metadata;

    public class BackendScriptViewModel
    {
        public IOrderedEnumerable<BackendScript> Scripts { get; set; }
    }
}