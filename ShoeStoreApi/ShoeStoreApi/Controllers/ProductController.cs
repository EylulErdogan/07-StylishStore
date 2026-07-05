using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShoeStoreApi.Data;
using ShoeStoreApi.Models;
using System.Text;
namespace ShoeStoreApi.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext db;

        public ProductController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("GetShoesById/{id}")]
        public async Task<Shoe> GetShoesById(int id)
        {
            return await db.Shoes.FindAsync(id);
        }
        [HttpGet]
        [Route("GetShoes")]
        public async Task<IEnumerable<Shoe>> GetShoes()
        {
            return await db.Shoes.ToListAsync();
        }

        [HttpPost]
        public IActionResult BuyNow(Sale sale)
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
