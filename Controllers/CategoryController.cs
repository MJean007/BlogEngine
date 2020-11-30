using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogEngine.Models;
using System.Web.Http;
//using System.Net;
//using System.Net.Http;


namespace BlogEngine.Controllers
{
    [ApiController]
    [System.Web.Http.Route("[controller]")]
    public class CategoryController : ApiController
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


        [System.Web.Http.Route("categories")]
        public JsonResult Get()
        {
            List<category> liste = new List<category>();
            BlogDBContext context = new BlogDBContext();
            liste = context.Categories.OrderBy(t=> t.title).ToList();
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
                //return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }

            return   Ok(new JsonResult(cat));

        }
    }
}
