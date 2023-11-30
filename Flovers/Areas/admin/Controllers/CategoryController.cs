using Microsoft.AspNetCore.Mvc;
using WebApplication7.DAL;
using WebApplication7.Models;

namespace WebApplication7.Areas.admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _DbContext;
        public CategoryController(AppDbContext _context)
        {
            _DbContext = _context;
        }
        public IActionResult Index()
        {
            List<Category> cat = _DbContext.Categories.ToList();

            return View(cat);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category cat)
        {
            if (!ModelState.IsValid) return View();

            if (_DbContext.Categories.Any(t => t.Name.ToLower() == cat.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "cat has already created!");
                return View();
            }

            _DbContext.Categories.Add(cat);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == null) return NotFound();

            Category cat = _DbContext.Categories.FirstOrDefault(t => t.Id == id);

            if (cat == null) return NotFound();

            return View(cat);
        }

        [HttpPost]
        public IActionResult Update(Category cat)
        {
            if (!ModelState.IsValid) return View();

            Category existcat = _DbContext.Categories.FirstOrDefault(t => t.Id == cat.Id);
            if (existcat == null) return NotFound();

            if (_DbContext.Tags.Any(t => t.Id != cat.Id && t.Name.ToLower() == cat.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "category has already created!");
                return View();
            }

            existcat.Name = cat.Name;

            _DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();

            Category cat = _DbContext.Categories.FirstOrDefault(t => t.Id == id);
            if (cat == null) return NotFound();

            _DbContext.Categories.Remove(cat);
            _DbContext.SaveChanges();
            return Ok();
        }

    }
}
