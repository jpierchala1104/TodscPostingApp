using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodscWebApp.Models
{
    public class Post
    {
        public List<string> ImageTags { set; get; }
        public List<IFormFile> Images { set; get; }

        public List<string> ImagesPaths { get; set; }
        public Post()
        {
            ImagesPaths = new List<string>();
        }

    }

}
