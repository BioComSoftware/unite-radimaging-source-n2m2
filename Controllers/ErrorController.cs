using unite.radimaging.source.n2m2.CSVParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Unite.Annotations.VEP.Web.Controllers {

    [ApiController]
    public class ErrorController : ControllerBase {
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}