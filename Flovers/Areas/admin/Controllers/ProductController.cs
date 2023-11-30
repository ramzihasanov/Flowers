using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.DAL;
using WebApplication7.Helpers;
using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Areas.admin.Controllers
{
    [Area("admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext Context, IWebHostEnvironment env)
        {
            _context = Context;
            _env = env;
        }
        public IActionResult Index()
        {

            var Product = _context.Products.ToList();
            return View(Product);
        }








        public IActionResult Detail(int id)
        {
            
            return View();
        }








        public IActionResult Create()
        {
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product pro)
        {
            if (!ModelState.IsValid) return View(pro);
            if (!_context.Categories.Any(x => x.Id == pro.CategoyId))
            {
                ModelState.AddModelError("CategoyId", "Categoy not found!!!");
                return View();
            }
            var check = false;
            if (pro.ColorIds != null)
            {
                foreach (var item in pro.ColorIds)
                {
                    if (!_context.Colors.Any(x => x.Id == item))
                    {
                        check = true;
                        break;
                    }
                }
            }
            if (check)
            {
                ModelState.AddModelError("ColorId", "Color not Faund");
                return View(pro);
            }
            else
            {
                if (pro.ColorIds != null)
                {
                    foreach (var item in pro.ColorIds)
                    {
                        ProductColor productcolor = new ProductColor
                        {
                            Product = pro,
                            ColorId = item
                        };
                        _context.ProductColors.Add(productcolor);
                    }
                }
            }
            if (pro.CoverFoto != null)
            {

                if (pro.CoverFoto.ContentType != "image/png" && pro.CoverFoto.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("CoverFoto", "ancaq sekil yukle");
                    return View();
                }

                if (pro.CoverFoto.Length > 1048576)
                {
                    ModelState.AddModelError("CoverFoto", "1 mb dan az yukle pul yazir ");
                    return View();
                }

                string newFileName = Helper.GetFileName(_env.WebRootPath, "upload", pro.CoverFoto);
                ProductImage productimage = new ProductImage
                {
                    Product = pro,
                    ImgUrl = newFileName,
                    IsCheck = true,
                };
                _context.ProductImages.Add(productimage);
            };
            if (pro.BackFoto != null)
            {

                if (pro.BackFoto.ContentType != "image/png" && pro.BackFoto.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("BackFoto", "ancaq sekil yukle");
                    return View();
                }

                if (pro.BackFoto.Length > 1048576)
                {
                    ModelState.AddModelError("BackFoto", "1 mb dan az yukle pul yazir ");
                    return View();
                }

                string newFileName = Helper.GetFileName(_env.WebRootPath, "upload", pro.BackFoto);
                ProductImage productimage = new ProductImage
                {
                    Product = pro,
                    ImgUrl = newFileName,
                    IsCheck = false,
                };
                _context.ProductImages.Add(productimage);
            };
            if (pro.SlideFotos != null)
            {
                foreach (var item in pro.SlideFotos)
                {


                    if (item.ContentType != "image/png" && item.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("SlideFotos", "ancaq sekil yukle");
                        return View();
                    }

                    if (item.Length > 1048576)
                    {
                        ModelState.AddModelError("SlideFotos", "1 mb dan az yukle pul yazir ");
                        return View();
                    }


                    string newFileName = Helper.GetFileName(_env.WebRootPath, "upload", item);
                    ProductImage productimage = new ProductImage
                    {
                        Product = pro,
                        ImgUrl = newFileName,
                        IsCheck = null,
                    };
                    _context.ProductImages.Add(productimage);
                };
            }

            _context.Products.Add(pro);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Update(int id)
        {
            HomeViewModels homeViewModels = new HomeViewModels();

            if (!ModelState.IsValid) return View();
            var existProd = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);
            existProd.ProductImageIds = existProd.ProductImages.Select(x => x.Id).ToList();

            return View(existProd);
        }


        [HttpPost]
        public IActionResult Update(Product pro)
        {
            var existPord=_context.Products.Include(x=>x.ProductColors).Include(x=>x.ProductImages).FirstOrDefault(x => x.Id == pro.Id);    
            if(existPord==null) return NotFound();
            if(!ModelState.IsValid)return View(pro);


            existPord.ProductColors.RemoveAll(x => !pro.ColorIds.Contains(x.Id));
            foreach (var item in pro.ColorIds.Where(x => !existPord.ProductColors.Any(y => y.ColorId == x)))
            {
                ProductColor productcolor = new ProductColor()
                {
                    ColorId = item
                };
                existPord.ProductColors.Add(productcolor);
            }
            existPord.ProductImages.RemoveAll(x => !pro.ProductImageIds.Contains(x.Id) && x.IsCheck == true);




            if (pro.CoverFoto != null)
            {

                if (pro.CoverFoto.ContentType != "image/png" && pro.CoverFoto.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("CoverFoto", "ancaq sekil yukle");
                    return View();
                }

                if (pro.CoverFoto.Length > 1048576)
                {
                    ModelState.AddModelError("CoverFoto", "1 mb dan az yukle pul yazir ");
                    return View();
                }

                string newFileName = Helper.GetFileName(_env.WebRootPath, "upload", pro.CoverFoto);
                ProductImage productimage = new ProductImage
                {
                    Product = pro,
                    ImgUrl = newFileName,
                    IsCheck = true,
                };
                _context.ProductImages.Add(productimage);
            };
            if (pro.BackFoto != null)
            {

                if (pro.BackFoto.ContentType != "image/png" && pro.BackFoto.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("BackFoto", "ancaq sekil yukle");
                    return View();
                }

                if (pro.BackFoto.Length > 1048576)
                {
                    ModelState.AddModelError("BackFoto", "1 mb dan az yukle pul yazir ");
                    return View();
                }

                string newFileName = Helper.GetFileName(_env.WebRootPath, "upload", pro.BackFoto);
                ProductImage productimage = new ProductImage
                {
                    Product = pro,
                    ImgUrl = newFileName,
                    IsCheck = false,
                };
                _context.ProductImages.Add(productimage);
            };
            if (pro.SlideFotos != null)
            {
                foreach (var item in pro.SlideFotos)
                {


                    if (item.ContentType != "image/png" && item.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("SlideFotos", "ancaq sekil yukle");
                        return View();
                    }

                    if (item.Length > 1048576)
                    {
                        ModelState.AddModelError("SlideFotos", "1 mb dan az yukle pul yazir ");
                        return View();
                    }


                    string newFileName = Helper.GetFileName(_env.WebRootPath, "upload", item);
                    ProductImage productimage = new ProductImage
                    {
                        Product = pro,
                        ImgUrl = newFileName,
                        IsCheck = null,
                    };
                    _context.ProductImages.Add(productimage);
                };
            }

            existPord.Name=pro.Name;
            existPord.Desc=pro.Desc;
            existPord.Price=pro.Price;
            existPord.SKU=pro.SKU;
            existPord.CategoyId=pro.CategoyId;
            existPord.ColorIds=pro.ColorIds;
            existPord.CategoyId = pro.CategoyId;

            _context.SaveChanges();
            return RedirectToAction("Index");


        }







        public IActionResult Delete(int id)
        {

            if (id == null) return NotFound("Error");

            Product pro = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);

            if (pro == null) return NotFound("Error");
            _context.Products.Remove(pro);
            _context.SaveChanges();


            return Ok();
        }

    }
}

