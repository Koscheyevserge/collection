using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/command")]
    public class CommandController : Controller
    {
        public IActionResult CompleteTask()
        {
            return Ok();
        }
    }
}