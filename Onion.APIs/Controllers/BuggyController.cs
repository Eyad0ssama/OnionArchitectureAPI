using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.APIs.Errors;
using Onion.Repository.Data;

namespace Onion.APIs.Controllers
{
    
    public class BuggyController : APIBaseController
    {
        private readonly OnionContext _dbContext;

        public BuggyController(OnionContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("NotFound")]
        public IActionResult GetNotFoundResult()
        {
            var Product = _dbContext.Products.Find(100);
            if(Product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(Product);
        }
    }
}
