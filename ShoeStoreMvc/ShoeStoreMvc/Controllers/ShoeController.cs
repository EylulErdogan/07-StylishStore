using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoeStoreApi.Models;

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

            return View(new Shoe());

        }
        [HttpPost]


        public IActionResult Create(Shoe shoe)
        {
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(shoe),
            System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync("https://localhost:7186/api/Shoes/AddShoe", content).Result;
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Edit(int id)
        {

            HttpClient client = new HttpClient();
            var response = client.GetAsync($"https://localhost:7186/api/Shoes/GetShoeById/{id}").Result;

            var shoe = JsonConvert.DeserializeObject<Shoe>(response.Content.ReadAsStringAsync().Result);

            return View(shoe);
        }

        [HttpPost]
        public IActionResult Edit(Shoe shoe)
        {
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(shoe),
            System.Text.Encoding.UTF8, "application/json");
            var response = client.PutAsync($"https://localhost:7186/api/Shoes/UpdateShoe/{shoe.Id}", content).Result;
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            var response = client.DeleteAsync($"https://localhost:7186/api/Shoes/DeleteShoe /{id}").Result;
            return RedirectToAction("Index");
        }

    }
}
