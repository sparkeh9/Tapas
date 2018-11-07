namespace Tapas.Cms.FlatFile.Backend.Areas.Backend.Controllers
{
    using System.IO;
    using System.Threading.Tasks;
    using Core.Git;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models;
    using Tapas.Backend.Core.Areas.Backend.Controllers;

    [ Authorize( Policy = "Backend:Pages:ManagePages" ) ]
    public class PagesController : BackendControllerBase
    {
        private readonly FlatFileCmsGitOptions flatFileOptions;

        public PagesController( IOptions<FlatFileCmsGitOptions> flatFileOptions )
        {
            this.flatFileOptions = flatFileOptions.Value;
        }

        [ HttpGet ]
        public async Task<IActionResult> Index( string filePath )
        {
            return View();
        }

        [ HttpGet ]
        public async Task<IActionResult> Load( string filePath )
        {
            var fullFilePath = Path.Combine( flatFileOptions.FilePath, filePath );

            return Json( new GrapesEditorLoadViewModel
            {
                Html = @"

   <div class=""container"">
                                 <div class=""item"" data-gjs-droppable=""false""
                                 style=""background-color: #f14c4c""></div>
                                 <div class=""item"" data-gjs-droppable=""false""
                                 style=""background-color: #68d368""></div>
                                 <div class=""item"" data-gjs-droppable=""false""
                                 style=""background-color: #708fdc""></div>
                                 </div>
                                 <style>
                                 .container {
                                 width: 50%;
                                 margin: 50px auto;
                                 padding: 5px;
                             }

                             .item {
                min-height: 50px;
                margin: 5px;
            }
            </style>


"
            } );
        }
    }
}