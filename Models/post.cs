using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Models
{
    public class post
    {
        public string title { get; set; }

        public DateTime publicationDate { get; set; }
        public string content { get; set; }
    }
}
