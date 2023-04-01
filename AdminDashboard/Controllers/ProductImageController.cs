using AdminDashboard.Models;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.Controllers
{
    public class ProductImageController : Controller
    {
        private readonly DContext _context;
        public ProductImageController(DContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Images = await _context.ProductImages.ToListAsync();
            return View(Images);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        { 

            var Image = await _context.ProductImages

                       .Include(i => i.ProductId)
                   .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);


            if (Image == null)
            {
                return NotFound();
            }

            return View(Image);

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductImageModel productImageModel)
        {
            try
            {

                ProductImage img = new ProductImage()
                {
                    ImagePath = productImageModel.ImagePath,
                    ProductId = productImageModel.ProductId



                };
                _context.ProductImages.Add(img);
                _context.SaveChanges();

                //return Ok();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductImage productImage = _context.ProductImages.Single(i => i.Id == id);


            return View(productImage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductImageModel productImageModel)
        {
            try
            {
                ProductImage img = _context.ProductImages.Single(i => i.Id == id);
                img.ImagePath = productImageModel.ImagePath;
              
                _context.ProductImages.Update(img);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        [HttpGet]

        public ActionResult Delete(int id)
        {
            

            try
            {
                ProductImage img = _context.ProductImages.Single(c => c.Id == id);
                _context.ProductImages.Remove(img);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
