using unite.radimaging.source.n2m2.Services;
using System;
using Microsoft.AspNetCore.Mvc;

namespace unite.radimaging.source.n2m2.Controllers {
    [Route("/api/v1/[controller]")]
    public class TestCSVController : Controller {
        // This was really just to test the CSV conversion. No reason not to hard code. 
        string Filename = @"C:\Users\right\Documents\Work\DKFZ\Projects\UNITE\Image data\AW__MRI_feature_indexing\Indexing_data.csv";
        //string Filename = "breaksit";
        [HttpGet]
        public IActionResult Get() {
            var teststr = MriFeaturesToJson.MriFeaturestoJSON (Filename);
            return Ok(teststr);
        }
    }
}