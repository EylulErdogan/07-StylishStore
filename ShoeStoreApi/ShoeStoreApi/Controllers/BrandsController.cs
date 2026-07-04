using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStoreApi.Data;
using ShoeStoreApi.Models;

namespace projebridgeapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly AppDbContext dbcontext;

        public BrandsController(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetBrands")]
        public async Task<IEnumerable<Brand>> GetBrands()
        {
            return await dbcontext.Brands.ToListAsync();
        }

        [HttpGet]
        [Route("GetBrandById/{id}")]
        public async Task<Brand> GetBrandById(int id)
        {
            return await dbcontext.Brands.FindAsync(id);
        }

        [HttpPost]
        [Route("AddBrand")]
        public async Task<Brand> AddBrand(Brand brand)
        {
            dbcontext.Brands.Add(brand);
            await dbcontext.SaveChangesAsync();
            return brand;
        }

        [HttpPut]
        [Route("UpdateBrand/{id}")]
        public async Task<Brand> UpdateBrand(Brand brand)
        {
            dbcontext.Brands.Update(brand);
            await dbcontext.SaveChangesAsync();
            return brand;
        }

        [HttpDelete]
        [Route("DeleteBrand/{id}")]
        public bool DeleteBrand(int id)
        {
            var process = false;
            var result = dbcontext.Brands.Find(id);

            if (result != null)
            {
                process = true;
                dbcontext.Brands.Remove(result);
                dbcontext.SaveChanges();
            }

            return process;
        }
    }
}