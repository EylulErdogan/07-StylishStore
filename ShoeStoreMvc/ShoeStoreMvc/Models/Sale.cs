namespace ShoeStoreMvc.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int ShoeId { get; set; }

        public string ShoeName { get; set; }

        public string BrandName { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public int TotalPrice { get; set; }

        public DateTime SaleDate { get; set; }
    }
}