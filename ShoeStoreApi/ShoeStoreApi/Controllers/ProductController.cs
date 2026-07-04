using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStoreApi.Data;
using ShoeStoreApi.Models;

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
    }
}
