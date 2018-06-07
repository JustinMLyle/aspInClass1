using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspInClass1.Models;
using ButterCMS;
using System.Net;


namespace aspInClass1.Controllers
{
    public class HomeController : Controller
    {

        private ButterCMSClient Client;

        private static string _apiToken = "04b12e48548d985f28e28cd1eb1225fc04b16cec";

        public HomeController()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Client = new ButterCMSClient(_apiToken);
        }

        [Route("")]
        [Route("blog")]
        [Route("blog/p/{page}")]
        public async Task<ActionResult> ListAllPosts(int page = 1)
        {
            var response = await Client.ListPostsAsync(page, 10);
            ViewBag.Posts = response.Data;            
            ViewBag.NextPage = response.Meta.NextPage;
            ViewBag.PreviousPage = response.Meta.PreviousPage;
            return View("Posts");
            
        }

        [Route("blog/{slug}")]
        public async Task<ActionResult> ShowPost(string slug)
        {
            var response = await Client.RetrievePostAsync(slug);
            ViewBag.Post = response.Data;
            return View("Post");
        }

        public ViewResult Index()
        {
            


            Product[] array = {
            new Product {Name = "Kayak", Price = 275, Category ="WaterSports", Description="dank assed kayak"},
            new Product {Name = "Lifejacket", Price = 489, Category = "WaterSports", Description ="dank assed survival"},
            new Product {Name = "Soccer ball", Price = 195, Category = "FieldSports", Description = "dank assed feet sports"},
            new Product {Name = "Corner flag", Price = 345, Category = "FieldSports", Description="Dank assed equipment" }
        };
            
            return View(array);
            
        }          

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
