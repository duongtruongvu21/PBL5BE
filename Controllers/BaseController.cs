using Microsoft.AspNetCore.Mvc;

namespace PBL5BE.API.Controllers
{
    [Microsoft.AspNetCore.Cors.EnableCors("_myCORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}