namespace Tapas.Backend.Core.Areas.Backend.ViewComponents
{
    using System.Threading.Tasks;
    using Infrastructure.Menu;
    using Microsoft.AspNetCore.Mvc;

    public class MenuViewComponent : ViewComponent
    {
        private readonly MenuViewModelFactory viewmodelFactory;

        public MenuViewComponent( MenuViewModelFactory viewmodelFactory )
        {
            this.viewmodelFactory = viewmodelFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = await viewmodelFactory.CreateAsync();
            return View( menu );
        }
    }
}