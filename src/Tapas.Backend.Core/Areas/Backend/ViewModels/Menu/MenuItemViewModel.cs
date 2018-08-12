namespace Tapas.Backend.Core.Areas.Backend.ViewModels.Menu
{
    public class MenuItemViewModel
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string DisplayText { get; set; }
        public uint Position { get; set; }
        public string CssClass { get; set; }
    }
}