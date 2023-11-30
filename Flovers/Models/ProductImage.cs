using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public bool? IsCheck { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }


    }
}
