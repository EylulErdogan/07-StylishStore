namespace ShoeStoreApi.Models
{
    public class Shoe
    {
        public int Id { get; set; }

        public string ShoeName { get; set; }

        public int Price { get; set; }

        public int Stock { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public string BrandName { get; set; }

        public string ImageUrl { get; set; }
    }
}
