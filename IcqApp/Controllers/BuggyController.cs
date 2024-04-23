using IcqApp.Data;
using IcqApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IcqApp.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _dataContext;

        public BuggyController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {

            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var user = _dataContext.Users.Find(-1);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var user = _dataContext.Users.Find(-1);
            return user.ToString();
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {

            return BadRequest("Invalid request");
        }

    }
}
