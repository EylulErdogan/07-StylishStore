using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStoreApi.Data;
using ShoeStoreApi.Models;

namespace projebridgeapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext dbcontext;

        public CategoriesController(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetCategories")]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await dbcontext.Categories.ToListAsync();
        }

        [HttpGet]
        [Route("GetCategoryById/{id}")]
        public async Task<Category> GetCategoryById(int id)
        {
            return await dbcontext.Categories.FindAsync(id);
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<Category> AddCategory(Category category)
        {
            dbcontext.Categories.Add(category);
            await dbcontext.SaveChangesAsync();
            return category;
        }

        [HttpPut]
        [Route("UpdateCategory/{id}")]
        public async Task<Category> UpdateCategory(Category category)
        {
            dbcontext.Categories.Update(category);
            await dbcontext.SaveChangesAsync();
            return category;
        }

        [HttpDelete]
        [Route("DeleteCategory/{id}")]
        public bool DeleteCategory(int id)
        {
            var process = false;
            var result = dbcontext.Categories.Find(id);

            if (result != null)
            {
                process = true;
                dbcontext.Categories.Remove(result);
                dbcontext.SaveChanges();
            }

            return process;
        }
    }
}