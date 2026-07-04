using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoeStoreApi.Models;

namespace ShoeStoreMvc.Controllers
{
    public class BrandController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://localhost:7186/api/Brands/GetBrands").Result;
            List<Brand> brands = JsonConvert.DeserializeObject<List<Brand>>(response.Content.ReadAsStringAsync().Result);

            return View(brands);
        }
        public IActionResult Create()
        {

            return View(new Brand());

        }
        [HttpPost]


        public IActionResult Create(Brand brand)
        {
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(brand),
            System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync("https://localhost:7186/api/Brands/AddBrand", content).Result;
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Edit(int id)
        {

            HttpClient client = new HttpClient();
            var response = client.GetAsync($"https://localhost:7186/api/Brands/GetBrandById/{id}").Result;

            var brand = JsonConvert.DeserializeObject<Brand>(response.Content.ReadAsStringAsync().Result);

            return View(brand);
        }

        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(brand),
            System.Text.Encoding.UTF8, "application/json");
            var response = client.PutAsync($"https://localhost:7186/api/Brands/UpdateBrand/{brand.Id}", content).Result;
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            var response = client.DeleteAsync($"https://localhost:7186/api/Brands/DeleteBrand/{id}").Result;
            return RedirectToAction("Index");
        }

    }
}
