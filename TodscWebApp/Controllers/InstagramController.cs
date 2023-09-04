using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
using TodscWebApp.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using TodscWebApp.ViewModel;

namespace TodscWebApp.Controllers
{
    public class InstagramController : Controller
    {

        static public string connectionString = "DefaultEndpointsProtocol=https;AccountName=instagramsa;AccountKey=1vv4Wy/eFLv4Ljh7++aDc11z4gnXOI7BikE4Abzj71NtkKYNOlvquIlxrUqmkUQQ8P+zzs9mTmLMrp+uy6TsOA==;EndpointSuffix=core.windows.net";
        public JsonSerializer serializer = new JsonSerializer();
        static public string AccountName = "instagramsa";
        static public string ImageContainer = "instagram";
        public string path = @".\wwwroot\data.json";
        IndexViewModel MyViewModel = new IndexViewModel();
        static List<PostsToDisplay> GetSerializedObject = new List<PostsToDisplay>();

        string blobUri = "https://" + AccountName + ".blob.core.windows.net/" + ImageContainer + "/";
        public  ActionResult Index(IndexViewModel MyViewModel)
        {
            PostsToDisplay postToDisplay = new PostsToDisplay();
            if (System.IO.File.Exists(path))
            {
                using (StreamReader file = System.IO.File.OpenText(@"wwwroot\data.json"))
                {
                    GetSerializedObject=(List<PostsToDisplay>)serializer.Deserialize(file, typeof(List<PostsToDisplay>));
                    //postToDisplay = (PostsToDisplay)serializer.Deserialize(file, typeof(PostsToDisplay));
                    //MyViewModel.PostsToDisplayViewModel.Add(postToDisplay);
                }
                MyViewModel.PostsToDisplayViewModel = GetSerializedObject;
                
            }
            else
            {
                GetSerializedObject = new List<PostsToDisplay>();
                MyViewModel.PostsToDisplayViewModel = GetSerializedObject;
            }
            Posts(MyViewModel);
            return View(MyViewModel);
        }


        [HttpPost]
        public ActionResult Create(ViewModel.IndexViewModel model)
        {
            Post Mypost = model.Post;
            PostsToDisplay postsToDisplay = new PostsToDisplay();
            for (int i = 0; i < Mypost.Images.Count(); i++)
            {
                var fileName = Path.GetFileName(Mypost.Images[i].FileName);
                var GetValue = String.Concat(blobUri, fileName);
                postsToDisplay.Urls.Add(GetValue);
                
                BlobClient bc = new BlobClient(connectionString, "instagram", fileName);
                bc.Upload(Mypost.Images[i].OpenReadStream(), new BlobHttpHeaders { ContentType = "image/jpeg" });
            }
            postsToDisplay.Tags = Mypost.ImageTags;
            GetSerializedObject.Add(postsToDisplay);
            using (StreamWriter sw = new StreamWriter(@"wwwroot\data.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, GetSerializedObject); 
            }
            MyViewModel.PostsToDisplayViewModel = GetSerializedObject;
            return RedirectToAction("Index");
        }
        
       
        [HttpPost]
        public IActionResult Posts(ViewModel.IndexViewModel model)
        {
            if (model.PostsToDisplayViewModel == null)
            {
                model.PostsToDisplayViewModel = new List<PostsToDisplay>();
            }
            return PartialView(model.PostsToDisplayViewModel);
        }
    }
}