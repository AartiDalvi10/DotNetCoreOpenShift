using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrbanDevelopmentProj.Models;
using System.IO;

namespace UrbanDevelopmentProj.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string apiUrl = "http://localhost:8083/areaDetail/1";

            Uri address = new Uri(apiUrl);

            // Create the web request
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            // Set type to POST
            request.Method = "GET";
            request.ContentType = "application/json";
           // request.charset = "UTF - 8";

            //using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //{
            //    // Get the response stream
            //    StreamReader reader = new StreamReader(response.GetResponseStream());

            //    // Console application output
            //    ViewData["Message"] = reader.ReadToEnd();
            //}

            return View();
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
