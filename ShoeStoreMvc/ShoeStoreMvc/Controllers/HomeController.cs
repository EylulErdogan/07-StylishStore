using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoeStoreApi.Models;
using ShoeStoreMvc.Models;
using System.Diagnostics;

namespace ShoeStoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();

            HomeViewModel model = new HomeViewModel();

            var categoryResponse = client.GetAsync("https://localhost:7186/api/Categories/GetCategories").Result;
            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryJson = categoryResponse.Content.ReadAsStringAsync().Result;
                model.Categories = JsonConvert.DeserializeObject<List<Category>>(categoryJson);
            }

            var shoeResponse = client.GetAsync("https://localhost:7186/api/Shoes/GetShoes").Result;
            if (shoeResponse.IsSuccessStatusCode)
            {
                var shoeJson = shoeResponse.Content.ReadAsStringAsync().Result;
                model.Shoes = JsonConvert.DeserializeObject<List<Shoe>>(shoeJson);
            }

            return View(model);
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