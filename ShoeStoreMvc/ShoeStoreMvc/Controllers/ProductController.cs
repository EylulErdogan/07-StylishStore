using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoeStoreApi.Models;
using System.Text;

namespace ShoeStoreMvc.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(int id)
        {
            HttpClient client = new HttpClient();

            var response = client.GetAsync($"https://localhost:7186/api/Shoes/GetShoesById/{id}").Result;

            var shoe = JsonConvert.DeserializeObject<Shoe>(
                response.Content.ReadAsStringAsync().Result
            );

            return View(shoe);
        }

        [HttpGet]
        public IActionResult ShoeList(string search, string sort)
        {
            HttpClient client = new HttpClient();

            var response = client.GetAsync("https://localhost:7186/api/Shoes/GetShoes").Result;

            List<Shoe> shoes = JsonConvert.DeserializeObject<List<Shoe>>(
                response.Content.ReadAsStringAsync().Result
            );

            if (!string.IsNullOrEmpty(search))
            {
                shoes = shoes
                    .Where(x => x.ShoeName.ToLower().Contains(search.ToLower()) ||
                                x.BrandName.ToLower().Contains(search.ToLower()))
                    .ToList();
            }

            if (sort == "name-asc")
                shoes = shoes.OrderBy(x => x.ShoeName).ToList();
            else if (sort == "name-desc")
                shoes = shoes.OrderByDescending(x => x.ShoeName).ToList();
            else if (sort == "price-asc")
                shoes = shoes.OrderBy(x => x.Price).ToList();
            else if (sort == "price-desc")
                shoes = shoes.OrderByDescending(x => x.Price).ToList();

            ViewBag.Search = search;
            ViewBag.Sort = sort;
            ViewBag.TotalCount = shoes.Count;

            return View(shoes);
        }

        [HttpPost]
        public IActionResult BuyNow(ShoeStoreMvc.Models.Sale sale)
        {
            HttpClient client = new HttpClient();

            StringContent content = new StringContent(
                JsonConvert.SerializeObject(sale),
                Encoding.UTF8,
                "application/json"
            );

            var response = client.PostAsync("https://localhost:7186/api/Sales/AddSale", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}