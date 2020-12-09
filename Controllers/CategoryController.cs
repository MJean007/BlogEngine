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
        private BlogDBContext context = new BlogDBContext();
        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //[Route("categories")]
        //public IEnumerable<string> Get()
        public IActionResult Get()
        {
            IEnumerable<string> liste = context.category.ToArray().OrderBy(n => n.title).Select(n=>n.title);
            //return liste;
            return Ok(new JsonResult(liste));
        }


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

        [HttpGet("{id}", Name = "Get")]
        public IActionResult get(int? id)
        {
            category cat = context.category.Where(c => c.categoryID == id).FirstOrDefault();
            if (cat == null)
            {
                return NotFound(id);
            }
            return Ok(new JsonResult(cat.title));
        }
      


    }
}
