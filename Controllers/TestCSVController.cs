using unite.radimaging.source.n2m2.CSVParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Unite.Annotations.VEP.Web.Controllers {
    [Route("/api/v1/[controller]")]
    public class TestCSVController : Controller {
        string Filename = @"C:\Users\right\Documents\Work\DKFZ\Projects\UNITE\Image data\AW__MRI_feature_indexing\Indexing_data.csv";
        //string Filename = "breaksit";
        [HttpGet]
        public IActionResult Get() {
            var teststr = MRIFeaturesCSVParser.CSVFiletoJSON(Filename);
            return Ok(teststr);
        }
    }
}