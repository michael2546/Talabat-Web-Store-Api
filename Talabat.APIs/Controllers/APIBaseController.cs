using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talabat.APIs.Controllers
{
    //these attriputes Will also inherited
    
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {

        //parent class to all controllers
    }
}
