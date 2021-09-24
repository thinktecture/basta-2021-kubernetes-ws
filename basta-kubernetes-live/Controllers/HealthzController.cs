using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BASTA.Kubernetes.Controllers
{
    [ApiController]
    [Route("healthz")]
    public class HealthzController : ControllerBase
    {
        [HttpGet("ready")]
        [AllowAnonymous]
        public IActionResult IsReady() 
        {
            return Ok();
        }

        [HttpGet("alive")]
        [AllowAnonymous]
        public IActionResult IsAlive() 
        {
            return Ok();
        }
    }
}
