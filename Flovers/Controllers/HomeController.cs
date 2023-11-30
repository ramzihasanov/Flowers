using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication7.DAL;
using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModels homeViewModels = new HomeViewModels()
            {
                Categories = _context.Categories.ToList(),
                Colors = _context.Colors.ToList(),
                ProductImages = _context.ProductImages.ToList(),
                Products = _context.Products.ToList(),
                Latest = _context.Products.Include(b => b.ProductImages).Where(b => b.isLatest).ToList(),
                FeaturedProduct = _context.Products.Include(b => b.ProductImages).Where(b => b.isFeatured).ToList(),
                BestsellerProduct = _context.Products.Include(b => b.ProductImages).Where(b => b.isBestseller).ToList()

            };
            return View(homeViewModels);
        }

        public IActionResult Detail()
        {
            return View();
        }

      
        
    }
}