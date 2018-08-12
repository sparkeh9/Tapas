namespace Tapas.Backend.Core.Areas.Backend.ViewModels.Menu
{
    using System.Collections.Generic;

    public class MenuViewModel
    {
        public IEnumerable<MenuGroupViewModel> MenuGroups { get; set; }
    }
}