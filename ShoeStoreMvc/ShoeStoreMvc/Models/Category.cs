namespace ShoeStoreApi.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public List<Shoe>? Shoes { get; set; }
    }
}
                                                                       