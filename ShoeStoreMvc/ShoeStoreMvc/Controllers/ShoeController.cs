using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ShoeStoreApi.Models;
using System.Text;

namespace ShoeStoreMvc.Controllers
{
    public class ShoeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://localhost:7186/api/Shoes/GetShoes").Result;
            List<Shoe> shoes = JsonConvert.DeserializeObject<List<Shoe>>(response.Content.ReadAsStringAsync().Result);

            return View(shoes);
        }
        public IActionResult Create()
        {
            HttpClient client = new HttpClient();

            var response = client.GetAsync("https://localhost:7186/api/Categories/GetCategories").Result;

            var categories = JsonConvert.DeserializeObject<List<Category>>
            (
                response.Content.ReadAsStringAsync().Result
            );

            ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");

            return View();
        }
        [HttpPost]
        public IActionResult Create(Shoe shoe)
        {
            HttpClient client = new HttpClient();

            StringContent content = new StringContent
            (
                JsonConvert.SerializeObject(shoe),
                Encoding.UTF8,
                "application/json"
            );

            var response = client.PostAsync("https://localhost:7186/api/Shoes/AddShoes", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(shoe);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();

            var categoryResponse = client.GetAsync("https://localhost:7186/api/Categories/GetCategories").Result;
            var categories = JsonConvert.DeserializeObject<List<Category>>(
                categoryResponse.Content.ReadAsStringAsync().Result
            );

            ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");

            var response = client.GetAsync($"https://localhost:7186/api/Shoes/GetShoesById/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                var value = JsonConvert.DeserializeObject<Shoe>(jsonData);
                return View(value);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Shoe shoe)
        {
            HttpClient client = new HttpClient();

            StringContent content = new StringContent(
                JsonConvert.SerializeObject(shoe),
                Encoding.UTF8,
                "application/json"
            );

            var response = client.PutAsync($"https://localhost:7186/api/Shoes/UpdateShoe/{shoe.Id}", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(shoe);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            var response = client.DeleteAsync($"https://localhost:7186/api/Shoes/DeleteShoe/{id}").Result;
            return RedirectToAction("Index");
        }

    }
}
