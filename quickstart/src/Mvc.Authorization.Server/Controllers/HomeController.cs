using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Mvc.Authorization.Server.Models;
using Newtonsoft.Json.Linq;

namespace Mvc.Authorization.Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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
        
        public async Task<IActionResult> PrivacyAsync()
        {
            var accessToken = ((Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpRequestHeaders)HttpContext.Request.Headers).HeaderAuthorization.ToArray()[0].Split(" ")[1];
            var user = ((DefaultHttpContext)((DefaultHttpRequest)HttpContext.Request).HttpContext).User;

            var t = HttpContext.User;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content =  await client.GetStringAsync("http://localhost:5001/identity/claims");
            
            ViewBag.Json = JArray.Parse(content).ToString();
            //return View("CallApi");
            
            ViewResult vrs = View("Privacy", ViewBag.Json);

            //JsonResult jr = new JsonResult(JArray.Parse(content));

            return vrs;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
