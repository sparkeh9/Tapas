namespace Tapas.Backend.Core.Areas.Backend.ViewComponents
{
    using System.Threading.Tasks;
    using Infrastructure.Assets;
    using Microsoft.AspNetCore.Mvc;

    public class BackendStyleSheetsViewComponent : ViewComponent
    {
        private readonly BackendStylesheetViewModelFactory factory;

        public BackendStyleSheetsViewComponent( BackendStylesheetViewModelFactory factory )
        {
            this.factory = factory;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult<IViewComponentResult>( View( factory.Create() ) );
        }
    }
}