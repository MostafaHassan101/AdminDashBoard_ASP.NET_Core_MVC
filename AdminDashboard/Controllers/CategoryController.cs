using AdminDashboard.Models;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DContext _context;
        public CategoryController(DContext context)
        {
            _context = context;
        }
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Category.ToListAsync();
            return View(categories);
        }
        //[HttpGet]
        //public async Task<IActionResult> Details(int id)
        //{
        //    //if (id == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //var category = await _context.Category
        //    //    .Include(c => c.SubCategories)
        //    //       .AsNoTracking()
        //    //    .FirstOrDefaultAsync(c => c.Id == id);  


        //    //if (category == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //return View(category);
        //    return View();
        //}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var category = await _context.Category
                .Include(c => c.SubCategories)
                   .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);


            if (category == null)
            {
                return NotFound();
            }

            return View(category);
           
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryModel newCategory)
        {
            try
            {

                Category cat = new Category()
                {
                    Name = newCategory.Name,
                    NameAr = newCategory.NameAr
                    //SubCategories=newCategory.SubCategories,
                    //ParentCategory=newCategory.ParentCategory

                };
                _context.Category.Add(cat);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

                //return Ok();
                //return View();
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category category = _context.Category.Single(c => c.Id == id);


            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryModel categoryModel)
        {
            try
            {
                Category category = _context.Category.Single(c => c.Id == id);
                category.Name = categoryModel.Name;
                category.NameAr = categoryModel.NameAr;
                _context.Category.Update(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }






        //Delete Category////
        //[HttpGet]
        //public ActionResult Delete()
        //{
        //    return View();
        //}
        [HttpGet]

        public ActionResult Delete(int id)
        {
            //var cat = await _context.Category.FindAsync(id);
            //if (cat == null)
            //{
            //    return RedirectToAction(nameof(Index));
            //}

            try
            {
                Category category =  _context.Category.Single(c => c.Id == id);
                _context.Category.Remove(category);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception )
            {

                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Employees/Delete/1
        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

    }
}
