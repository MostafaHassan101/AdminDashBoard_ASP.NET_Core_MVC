using AdminDashboard.Models;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AdminDashboard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly DContext _context;

        public BrandController(DContext context)
        {
            _context = context;
        }

        // GET: BrandController
        [HttpGet]
        public IActionResult Index(string filter)
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;

            var Brands = _context.Brand.ToList();
            if (filter != null)
            {
                Brands = Brands.Where(b => b.Name.ToLower().Contains(filter.ToLower())).ToList();
            }
            
            return View(Brands);
        }

        // GET: BrandController/Create
        public IActionResult Create()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;

            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(BrandModel collection)
        {
            try
            {
                var Brands = _context.Brand.ToList();
                foreach (var Br in Brands)
                {
                    if (collection.Name == Br.Name || collection.NameAr == Br.NameAr)
                    {
                        return View();

                    }
                }
                Brand brand = new Brand()
                  {
                    Name = collection.Name,
                    NameAr = collection.NameAr,
                  };
                await  _context.Brand.AddAsync(brand);
                 await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));                 
            }
            catch
            {
                return View();
            }
        }

        // GET: BrandController/Edit/5
        public IActionResult Edit(int id)
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName; 

            Brand brand = _context.Brand.Single(b => b.Id == id);
            ViewBag.brand = brand;
            return View();
        }

    
        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(int id, BrandModel collection)
        {
            try
            {
                Brand brand = _context.Brand.Single(b => b.Id == id);
                brand.Name = collection.Name;
                brand.NameAr = collection.NameAr;
                _context.Brand.Update(brand);
              await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: BrandController/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;

            ViewBag.Title = "Brand Details";
            List<Product> products = new List<Product>();
            products = _context.Product.Where(Product => Product.Brand.Id == id).ToList();
            ViewBag.Products = products;
            return View();
        }

        // POST: BrandController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Brand brand = _context.Brand.Single(b => b.Id == id);
                _context.Brand.Remove(brand);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
