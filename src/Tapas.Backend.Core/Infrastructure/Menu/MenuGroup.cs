namespace Tapas.Backend.Core.Infrastructure.Menu
{
    using System.Collections.Generic;
    using System.Linq;

    public class MenuGroup
    {
        public string Name {get; }
        public uint Position {get;}
        public string CssClass { get; }
        public IEnumerable<MenuItem> MenuItems {get;}

        public MenuGroup( string name, uint position, IEnumerable<MenuItem> menuItems = null, string cssClass = "fa fa-bars")
        {
            Name = name;
            Position = position;
            MenuItems = menuItems ?? Enumerable.Empty<MenuItem>();
            CssClass = cssClass;
        }
    }
}