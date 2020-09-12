using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(42);
            if(thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
                return Ok();
        }
        [HttpGet("serverError")]
        public ActionResult GetServerError()
        {

             var thing = _context.Products.Find(42);
             var thingToFind = thing.ToString();

           return Ok();
        }

        [HttpGet("badRequest")]
        public ActionResult GetBadRequest()
        {
                return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badRequesst/{id}")]
        public ActionResult GetNotFoundRequest(string id)
        {
                return Ok();
        }
    }
}