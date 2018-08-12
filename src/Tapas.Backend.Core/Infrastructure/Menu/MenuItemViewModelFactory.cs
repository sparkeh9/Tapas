namespace Tapas.Backend.Core.Infrastructure.Menu
{
    using Areas.Backend.ViewModels.Menu;

    public class MenuItemViewModelFactory
    {
        public MenuItemViewModel Create( MenuItem menuItem )
        {
            return new MenuItemViewModel
            {
                Url = menuItem.Url,
                Name = menuItem.Name,
                DisplayText = menuItem.DisplayText,
                Position = menuItem.Position,
                CssClass = menuItem.CssClass
            };
        }
    }
}