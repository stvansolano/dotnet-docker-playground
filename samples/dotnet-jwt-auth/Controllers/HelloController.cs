using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Dotnet_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [AllowAnonymous, HttpGet]
        public IActionResult Get() => Ok(new string[] { "Hello", "World!" });

        [Authorize]
        [HttpGet(nameof(Private), Name = nameof(Private))]
        public IActionResult Private() 
        {
            var list = new List<string>(new string[] { "Hello", "World!" });

            list.AddRange(User.Claims.Select(c => c.Type + "=" + c.Value));
            
            return Ok(list.ToArray());
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("Admin")]
        public IActionResult HelloAdmin()
        {
            var list = new List<string>(new string[] { "Hello", "Admin!" });

            list.AddRange(User.Claims.Select(c => c.Type + "=" + c.Value));

            return Ok(list.ToArray());
        }
    }
}
