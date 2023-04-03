using Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Controllers
{
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

        // GET: ProductColorController1/Details/5
        public ActionResult ProductColorDetails(int id)
        {
            return View();
        }

        // GET: ProductColorController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductColorController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductColorController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductColorController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductColorController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductColorController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
