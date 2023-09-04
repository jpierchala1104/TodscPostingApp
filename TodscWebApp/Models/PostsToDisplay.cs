using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodscWebApp.Models
{
    public class PostsToDisplay
    {
        public List<string> Tags { get; set; }
        public List<string> Urls { get; set; }
        public PostsToDisplay()
        {
            Tags = new List<string>();
            Urls = new List<string>();
        }
    }

}
