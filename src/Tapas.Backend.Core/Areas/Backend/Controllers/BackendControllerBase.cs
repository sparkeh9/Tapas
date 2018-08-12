namespace Tapas.Backend.Core.Areas.Backend.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ Area( "Backend" ) ]
    [ Authorize( Policy = "Backend:AccessBackend" ) ]
    public abstract class BackendControllerBase : Controller { }
}