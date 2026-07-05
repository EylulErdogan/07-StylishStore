using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStoreApi.Data;
using ShoeStoreApi.Models;

namespace projebridgeapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext dbcontext;

        public SalesController(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetSales")]
        public async Task<IEnumerable<Sale>> GetSales()
        {
            return await dbcontext.Sales.ToListAsync();
        }

        [HttpPost]
        [Route("AddSale")]
        public async Task<Sale> AddSale(Sale sale)
        {
            sale.SaleDate = DateTime.Now;
            sale.TotalPrice = sale.Price * sale.Quantity;

            dbcontext.Sales.Add(sale);
            await dbcontext.SaveChangesAsync();

            return sale;
        }
    }
}