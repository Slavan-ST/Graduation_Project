using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    //markers
    [ApiController]
    [Route("[controller]")]
    public class MarkersController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return StatusCode(503);
        }
    }
}
