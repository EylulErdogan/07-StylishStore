using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoeStoreApi.Models;

namespace ShoeStoreMvc.Controllers
{
    public class CategoryController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://localhost:7186/api/Categories/GetCategories").Result;
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(response.Content.ReadAsStringAsync().Result);

            return View(categories);
        }
        public IActionResult Create()
        {

            return View(new Category());

        }
        [HttpPost]


        public IActionResult Create(Category category)
        {
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(category),
            System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync("https://localhost:7186/api/Categories/AddCategory", content).Result;
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Edit(int id)
        {

            HttpClient client = new HttpClient();
            var response = client.GetAsync($"https://localhost:7186/api/Categories/GetCategoryById/{id}").Result;

            var category = JsonConvert.DeserializeObject<Category>(response.Content.ReadAsStringAsync().Result);

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(category),
            System.Text.Encoding.UTF8, "application/json");
            var response = client.PutAsync($"https://localhost:7186/api/Categories/UpdateCategory/{category.Id}", content).Result;
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            var response = client.DeleteAsync($"https://localhost:7186/api/Categories/DeleteCategory/{id}").Result;
            return RedirectToAction("Index");
        }

    }
}
