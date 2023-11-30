namespace WebApplication7.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public List<int>  ProductIds { get; set; }
    }
}
