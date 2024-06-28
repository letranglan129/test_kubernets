using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using WebUIClient.Models;
using WebUIClient.Ultis;

namespace WebUIClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(int id = 1)
        {
            var prod = await ProductRestAPI.GetClient("/api-prod/" + id.ToString()).GetAsync<Product>(new RestRequest());
            return View(prod);
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
