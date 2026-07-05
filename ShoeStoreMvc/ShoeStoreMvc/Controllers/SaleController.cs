using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoeStoreMvc.Models;

namespace ShoeStoreMvc.Controllers
{
    public class SaleController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();

            var response = client.GetAsync("https://localhost:7186/api/Sales/GetSales").Result;

            List<Sale> sales = new List<Sale>();

            if (response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                sales = JsonConvert.DeserializeObject<List<Sale>>(jsonData);
            }

            return View(sales);
        }
    }
}