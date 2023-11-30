using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using WebApplication7.DAL;
using WebApplication7.Models;

namespace WebApplication7.Areas.admin.Controllers
{
    public class ColorController : Controller
    {
        private readonly AppDbContext _DbContext;
        public ColorController(AppDbContext _context)
        {
            _DbContext = _context;
        }
        public IActionResult Index()
        {
            List<Models.Color> color = _DbContext.Colors.ToList();

            return View(color);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Models.Color color)
        {
            if (!ModelState.IsValid) return View();

            if (_DbContext.Colors.Any(t => t.Name.ToLower() == color.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "color has already created!");
                return View();
            }

            _DbContext.Colors.Add(color);
            _DbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == null) return NotFound();

            Models.Color color = _DbContext.Colors.FirstOrDefault(t => t.Id == id);

            if (color == null) return NotFound();

            return View(color);
        }

        [HttpPost]
        public IActionResult Update(Models.Color color)
        {
            if (!ModelState.IsValid) return View();

            Models.Color existcolor = _DbContext.Colors.FirstOrDefault(t => t.Id == color.Id);
            if (existcolor == null) return NotFound();

            if (_DbContext.Colors.Any(t => t.Id != color.Id && t.Name.ToLower() == color.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "color has already created!");
                return View();
            }

            existcolor.Name = color.Name;

            _DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();

            Models.Color color = _DbContext.Colors.FirstOrDefault(t => t.Id == id);
            if (color == null) return NotFound();

            _DbContext.Colors.Remove(color);
            _DbContext.SaveChanges();
            return Ok();
        }
    }
}
