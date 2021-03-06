using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogEngine.Models;

namespace BlogEngine.Controllers
{
    
    
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    { 
        private BlogDBContext context = new BlogDBContext();
        // GET: api/Post
        [HttpGet]
        
        public IActionResult Get()
        {
          
            IEnumerable<post> liste = context.post.ToArray().OrderBy(t => t.title);
            if (liste == null)
            {
                return NotFound();
            }
            return Ok(new JsonResult(liste));
        }

        // GET: api/Post/5
        [HttpGet("{id}", Name = "")]
        public IActionResult GetID(int id)
        {
            post _post = context.post.Where(p => p.postID == id).FirstOrDefault();
            if(_post == null)
            {
                return NotFound(id);
            }
            return Ok(new JsonResult(_post));
        }

        // POST: api/Post
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
