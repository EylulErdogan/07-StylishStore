using ShoeStoreApi.Models;

namespace ShoeStoreMvc.Models
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Shoe> Shoes { get; set; }
    }
}
