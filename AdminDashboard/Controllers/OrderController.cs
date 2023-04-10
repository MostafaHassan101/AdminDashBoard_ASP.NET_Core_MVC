using Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AdminDashboard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
      
    {
        private readonly DContext _context;

        public OrderController(DContext context)
        {
            _context = context;
        }
        // GET: OrderController
        public IActionResult Index()
        {
            var orders = _context.Order.Include(o => o.User).Include(o => o.OrderItems).ToList();

            return View(orders);
        }

        // GET: OrderController/Details/5
        public IActionResult Details(int id)
        {
            var orderitems = _context.OrderDetails
                .Include("Order").Include("Product").Where(o => o.Order.Id == id);
            ViewBag.orderitems = orderitems;
            return View();
        }
       

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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



        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
