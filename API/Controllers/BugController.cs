using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BugController : ControllerBase
    {
        private readonly MyDbContext _context;
        public BugController(MyDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetText()
        {
            return "text";
        }

        [HttpGet("not-found")]
        public ActionResult<User> GetNotFound()
        {
            var smth = _context.Users.Find(-1);
            if (smth == null)
                return NotFound();
            return Ok(smth);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var smth = _context.Users.Find(-1);
            var smthToReturn = smth.ToString();
            return smthToReturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("Not good request.");
        }
    }
}