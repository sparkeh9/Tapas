namespace Tapas.Backend.Core.Areas.Backend.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ Area( "Backend" ) ]
    [ Authorize ]
    public class BackendControllerBase: Controller {}
}