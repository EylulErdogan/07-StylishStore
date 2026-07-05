using ShoeStoreApi.Models;

namespace ShoeStoreMvc.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalProductCount { get; set; }
        public int TotalStockCount { get; set; }
        public int CategoryCount { get; set; }
        public int BrandCount { get; set; }

        public Shoe MostExpensiveProduct { get; set; }
        public List<Shoe> LastProducts { get; set; }
        public List<Shoe> LowStockProducts { get; set; }
    }
}