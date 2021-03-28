using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;
        }

[HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(42);                 // force error here as 42 doesn't exist
            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok();
        }

[HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _context.Products.Find(42);
            var thingToReturn = thing.ToString();                       // ToString() can't validate 42 as it doesn't exist
            return Ok();    
        }

[HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));                                        // returns 400 bad request result
        }

[HttpGet("badrequest/{id}")]                                        // passes in string of int id
        public ActionResult GetNotFoundRequest(int id)   // generates validation error by passing in a string instead of int id
        {
            {
            return Ok();
            }
        }
    }
}