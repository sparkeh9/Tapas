namespace Tapas.Backend.Core.Infrastructure.Assets
{
    using System.Linq;
    using Metadata;

    public class BackendStyleSheetViewModel
    {
        public IOrderedEnumerable<BackendStyleSheet> Stylesheets { get; set; }
    }
}