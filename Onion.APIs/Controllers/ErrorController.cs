using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.APIs.Errors;

namespace Onion.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class ErrorController : ControllerBase
    {
        public ActionResult Error(int Code)
        {
            return NotFound(new ApiResponse(Code));
        }
    }
}
