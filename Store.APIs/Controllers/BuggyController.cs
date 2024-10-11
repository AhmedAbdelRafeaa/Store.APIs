using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Errors;
using Store.Repository.Data.Contexts;

namespace Store.APIs.Controllers
{
   
    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")] // Get: /api/Buggy/notfounf
        public async Task<IActionResult> GetNotFoundRequestError()
        {
            var brand = await _context.Brands.FindAsync(100);
            if(brand is null)  return NotFound(new ApiErrorResponse(404 ));

            return Ok(brand);
        }


        [HttpGet("servererror")] // Get: /api/Buggy/servererror
        public async Task<IActionResult> GetServerError()
        {
            var brand = await _context.Brands.FindAsync(100);

            var brandToString = brand.ToString(); // will Throw Exception (NullReferenceException)

            return Ok(brand);
        }


        [HttpGet("badrequest")] // Get: /api/Buggy/badrequest
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest(new ApiErrorResponse(400)); 
        }

        [HttpGet("badrequest/{id}")] // Get: /api/Buggy/badrequest
        public async Task<IActionResult> GetBadRequestError(int id) // validation Error
        {
            
            return Ok();
        }


        [HttpGet("unauthorized")] // Get: /api/Buggy/unauthorized
        public async Task<IActionResult> GetUnauthorizedError()
        {
            return Unauthorized(new ApiErrorResponse(401));
        }

    }
}
