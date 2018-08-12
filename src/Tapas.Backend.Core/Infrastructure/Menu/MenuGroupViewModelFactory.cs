namespace Tapas.Backend.Core.Infrastructure.Menu
{
    using System.Collections.Generic;
    using Areas.Backend.ViewModels.Menu;

    public class MenuGroupViewModelFactory
    {
        public MenuGroupViewModel Create( MenuGroup menuGroup )
        {
            return new MenuGroupViewModel
            {
                Name = menuGroup.Name,
                DisplayText = menuGroup.DisplayText,
                Position = menuGroup.Position,
                CssClass = menuGroup.CssClass,
                MenuItems = new List<MenuItemViewModel>()
            };
        }
    }
}