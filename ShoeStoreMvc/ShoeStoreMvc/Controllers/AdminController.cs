using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoeStoreApi.Models;
using ShoeStoreMvc.Models;

namespace ShoeStoreMvc.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();

            var model = new AdminDashboardViewModel();

            var shoeResponse = client.GetAsync("https://localhost:7186/api/Shoes/GetShoes").Result;
            var categoryResponse = client.GetAsync("https://localhost:7186/api/Categories/GetCategories").Result;
            var brandResponse = client.GetAsync("https://localhost:7186/api/Brands/GetBrands").Result;

            if (shoeResponse.IsSuccessStatusCode)
            {
                var jsonData = shoeResponse.Content.ReadAsStringAsync().Result;
                var shoes = JsonConvert.DeserializeObject<List<Shoe>>(jsonData);

                model.TotalProductCount = shoes.Count;
                model.TotalStockCount = shoes.Sum(x => x.Stock);
                model.MostExpensiveProduct = shoes.OrderByDescending(x => x.Price).FirstOrDefault();
                model.LastProducts = shoes.OrderByDescending(x => x.Id).Take(5).ToList();
                model.LowStockProducts = shoes.Where(x => x.Stock <= 10).ToList();
            }

            if (categoryResponse.IsSuccessStatusCode)
            {
                var jsonData = categoryResponse.Content.ReadAsStringAsync().Result;
                var categories = JsonConvert.DeserializeObject<List<Category>>(jsonData);

                model.CategoryCount = categories.Count;
            }

            if (brandResponse.IsSuccessStatusCode)
            {
                var jsonData = brandResponse.Content.ReadAsStringAsync().Result;
                var brands = JsonConvert.DeserializeObject<List<Brand>>(jsonData);

                model.BrandCount = brands.Count;
            }

            return View(model);
        }
    }
}