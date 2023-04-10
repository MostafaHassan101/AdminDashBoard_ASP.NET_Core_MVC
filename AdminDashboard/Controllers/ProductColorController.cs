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
    public class ProductColorController : Controller
    {
        private readonly DContext _context;

        public ProductColorController(DContext context)
        {
            _context = context;
        }
        // GET: ProductColorController1
        public ActionResult Index()
        {
            var colors=_context.ProductColors.ToList();
            return View(colors);
        }


        // GET: ProductColorController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductColorController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductColor collection)
        {
            try
            {
                var Color = _context.ProductColors.ToList();
                foreach (var Br in Color)
                {
                    if (collection.Name == Br.Name || collection.HexValue == Br.HexValue)
                    {
                        return View();

                    }
                }
                ProductColor colorr = new ProductColor()
                {
                    Name = collection.HexValue,
                    HexValue = collection.HexValue,
                };
                _context.ProductColors.Add(colorr);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id, ProductColor productColor)
        {
            try
            {
                ProductColor productColor2 = _context.ProductColors.Single(b => b.Id == id);
                _context.ProductColors.Remove(productColor2);
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
