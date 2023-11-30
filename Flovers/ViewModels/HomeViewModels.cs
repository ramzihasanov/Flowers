using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebApplication7.Models;

namespace WebApplication7.ViewModels
{
    public class HomeViewModels
    {
        public List<Models.Color> Colors { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Product> Latest { get; set; }
        public List<Product> FeaturedProduct { get; set; }
        public List<Product> BestsellerProduct{ get; set; }
        public List<ProductColor> ProductColors { get; set; }
    }
}

