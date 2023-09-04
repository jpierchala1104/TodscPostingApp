using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodscWebApp.Models;

namespace TodscWebApp.ViewModel
{
    public class IndexViewModel
    {
        public Post Post { get; set; }
        public List<PostsToDisplay> PostsToDisplayViewModel { get; set; }

        public IndexViewModel()
        {
            Post = new Post();
           
        }
    }
}
