using AdminDashboard.Models;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AdminDashboard.Controllers
{

    public class BrandController : Controller
    {
        private readonly DContext _context;

        public BrandController(DContext context)
        {
            _context = context;
        }
        // GET: BrandController
        [HttpGet]
        public IActionResult Index()
        {
            var Brands = _context.Brand.ToList();
            return View(Brands);
        }




        // GET: BrandController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BrandModel collection)
        {
            //!= 
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
                            //  Products = collection.Products
                        };
                        _context.Brand.Add(brand);
                        _context.SaveChanges();

                        return RedirectToAction(nameof(Index));
                    
                
               // return View();


            }
            catch
            {
                return View();
            }
        }

        // GET: BrandController/Edit/5
        public ActionResult Edit(int id)
        {
            Brand brand = _context.Brand.Single(b => b.Id == id);
            ViewBag.brand = brand;
            return View();
        }
        // GET: BrandController/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewBag.Title = "Brand Details";

            List<Product> products = new List<Product>();
            products = _context.Product.Where(Product => Product.Brand.Id == id).ToList();
            // Brand brand = _context.Brand.Include(b=>b.Products).Single(b => b.Id == id);
            ViewBag.Products = products;
            return View();
        }
        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BrandModel collection)
        {
            try
            {
                Brand brand = _context.Brand.Single(b => b.Id == id);
                brand.Name = collection.Name;
                brand.NameAr = collection.NameAr;
                _context.Brand.Update(brand);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BrandController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Brand brand = _context.Brand.Single(b => b.Id == id);
            ViewBag.brand = brand;
            return View();
        }

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, BrandModel brandModel)
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
