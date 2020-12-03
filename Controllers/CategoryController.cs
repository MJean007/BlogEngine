using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogEngine.Models;


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
        public IEnumerable<category> Get()
        {
            BlogDBContext context = new BlogDBContext();
            IEnumerable<category> liste = context.category.ToArray().OrderBy(t => t.title);
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
            return liste;
        }

        //[Route("categories")]
        //public JsonResult Get()
        //{
        //    List<category> liste = new List<category>();
        //    BlogDBContext context = new BlogDBContext();
        //    liste = context.category.OrderBy(t => t.title).ToList();
        //    return new JsonResult(liste);
        //}


        // add category
        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody]category model)
        {
            int id = 0;
            try
            {
                Task<int> task = Task.Run(() =>
                {
                    BlogDBContext _context = new BlogDBContext();
                    category cat = null;
                    cat.title = model.title;
                    if (_context != null)
                    {
                        // check to make sure this category does not already exist
                        cat = _context.category.Where(t => t.title == model.title).FirstOrDefault();
                        // if the category does not exist, create it.
                        if (cat == null)
                        {
                            _context.category.Add(cat);
                            _context.SaveChanges();
                        }

                    }
                    // return the id of that category created
                    return _context.category.Where(t => t.title == model.title).Select(c => c.categoryID).FirstOrDefault();
                }
                );
                // get the id the new category from  the task
                id = await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error: {0}", ex.Message));
            }
            if (id > 0)
            {
                return Ok(id);
            }
            else
            {
                return NotFound();
            }
        }



        [Route("categories/{id?}")]
        public IActionResult get(int? id)
        {
            BlogDBContext context = new BlogDBContext();
            category cat = context.category.Where(c => c.categoryID == id).FirstOrDefault();

            if (cat == null)
            {
                return NotFound(id);
                //return request.createresponse(httpstatuscode.notfound, id);
            }

            return Ok(new JsonResult(cat));

        }
    }
}
