using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStoreApi.Data;
using ShoeStoreApi.Models;

namespace projebridgeapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoesController : ControllerBase
    {
        private readonly AppDbContext dbcontext;

        public ShoesController(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetShoes")]
        public async Task<IEnumerable<Shoe>> GetShoes()
        {
            return await dbcontext.Shoes.ToListAsync();
        }

        [HttpGet]
        [Route("GetShoesById/{id}")]
        public async Task<Shoe> GetShoesById(int id)
        {
            return await dbcontext.Shoes.FindAsync(id);
        }

        [HttpPost]
        [Route("AddShoes")]
        public async Task<Shoe> AddShoes(Shoe shoe)
        {
            dbcontext.Shoes.Add(shoe);
            await dbcontext.SaveChangesAsync();
            return shoe;
        }

        [HttpPut]
        [Route("UpdateShoe/{id}")]
        public async Task<Shoe> UpdateShoes(Shoe shoe)
        {
            dbcontext.Shoes.Update(shoe);
            await dbcontext.SaveChangesAsync();
            return shoe;
        }

        [HttpDelete]
        [Route("DeleteShoe/{id}")]
        public bool DeleteShoe(int id)
        {
            var process = false;
            var result = dbcontext.Shoes.Find(id);

            if (result != null)
            {
                process = true;
                dbcontext.Shoes.Remove(result);
                dbcontext.SaveChanges();
            }

            return process;
        }
    }
}