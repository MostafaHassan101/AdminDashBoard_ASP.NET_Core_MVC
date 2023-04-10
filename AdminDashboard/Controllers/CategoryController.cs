using AdminDashboard.Models;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AdminDashboard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly DContext _context;
        public CategoryController(DContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Category.ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

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
                };
                _context.Category.Add(cat);
                _context.SaveChanges();
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

        [HttpGet]

        public ActionResult Delete(int id)
        {
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

        [HttpGet]
        public ActionResult AddSubCategory(int id)
        {
            var parentCategory = _context.Category.Single(c => c.Id == id);
            ViewBag.parentCategory = parentCategory;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSubCategory(CategoryModel newCategory)
        {
            try
            {
                Category cat = new Category()
                {
                    Name = newCategory.Name,
                    NameAr = newCategory.NameAr,
                    ParentCategory = _context.Category.Single(c => c.Id == newCategory.parentId)
                };
                _context.Category.Add(cat);
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
