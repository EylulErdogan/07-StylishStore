using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoeStoreApi.Models;

namespace ShoeStoreMvc.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(int id)
        {

            HttpClient client = new HttpClient();
            var response = client.GetAsync($"https://localhost:7186/GetShoesById/{id}").Result;

            var shoe = JsonConvert.DeserializeObject<Shoe>(response.Content.ReadAsStringAsync().Result);

            return View(shoe);
        }
        [HttpGet]
        public IActionResult ShoeList()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://localhost:7186/GetShoes").Result;
            List<Shoe> shoe = JsonConvert.DeserializeObject<List<Shoe>>(response.Content.ReadAsStringAsync().Result);
            return View(shoe);
        }

    }
}
