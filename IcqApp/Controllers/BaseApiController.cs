using IcqApp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace IcqApp.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}
