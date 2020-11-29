using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogEngine.Models;
using System.Web.Http;

namespace BlogEngine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //public IEnumerable<category> Get()
        public JsonResult Get()
        {
            List<category> liste = new List<category>();
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();

            return new JsonResult(liste);
        }


        // add category


        [System.Web.Http.Route("categories/{ID?}")]
        public IHttpActionResult Get(int? id)
        {
            BlogDBContext context = new BlogDBContext();
            category cat = context.Categories.Where(c => c.categoryID == id).FirstOrDefault();

            if(cat == null)
            {
                return NotFound();
            }

            return   Ok(new JsonResult(cat));

        }
    }
}
