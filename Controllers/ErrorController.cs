using System;
using Microsoft.AspNetCore.Mvc;

namespace unite.radimaging.source.n2m2.Controllers {

    [ApiController]
    public class ErrorController : ControllerBase {
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}