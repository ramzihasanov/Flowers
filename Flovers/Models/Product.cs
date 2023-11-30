using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Price { get; set; }
        public string SKU { get; set; }
        public int CategoyId { get; set; }
        public bool isBestseller { get; set; }
        public bool isLatest { get; set; }
        public bool isFeatured { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        [NotMapped]
        public List<int>? ColorIds { get; set; }
        [NotMapped]
        public List<IFormFile>? SlideFotos { get; set; }
        [NotMapped]
        public IFormFile? CoverFoto { get; set; }
        [NotMapped]
        public IFormFile? BackFoto { get; set; }
        [NotMapped]
        public List<int>? ProductImageIds { get; set; }






    }
}
