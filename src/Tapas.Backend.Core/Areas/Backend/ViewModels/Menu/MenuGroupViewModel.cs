namespace Tapas.Backend.Core.Areas.Backend.ViewModels.Menu
{
    using System.Collections.Generic;

    public class MenuGroupViewModel
    {
        public string Name { get; set; }
        public string DisplayText { get; set; }
        public uint Position { get; set; }
        public string CssClass { get; set; }
        public List<MenuItemViewModel> MenuItems { get; set; }
    }
}