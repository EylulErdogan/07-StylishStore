using Microsoft.EntityFrameworkCore;
using ShoeStoreApi.Models;

namespace ShoeStoreApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Shoe> Shoes { get; set; }
    }
}
