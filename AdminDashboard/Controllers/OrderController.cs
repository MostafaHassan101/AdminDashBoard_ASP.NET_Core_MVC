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
            var orders = _context.Order
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .OrderBy(o=>o.CreatedAt).ToList();

            return View(orders);
        }

        // GET:Pending Order
        [HttpGet]
        public IActionResult PendingOrders()
        {
            var orders = _context.Order.Include(o => o.User)
                .Include(o => o.OrderItems)
                .Where(o=>o.Status== "Processing")
                .OrderBy(o=>o.CreatedAt)
                .ToList();

            return View(orders);
        }

        // GET: OrderController/Details/5
        public IActionResult Details(long id)
        {
            var orderitems = _context.OrderDetails
                .Include("Order").Include("Product").Where(o => o.Order.Id == id);
            ViewBag.orderitems = orderitems;
            ViewBag.orderid=id;
            return View();
        }
      

       // [HttpPost]
        public async Task<IActionResult>AcceptOrder(long id)
        {
            try
            {
                var order = _context.Order.Single(o => o.Id == id);
                order.Status = "Shipping";
                _context.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
           
        }


        // GET: OrderController/Delete/5

        public async Task <IActionResult> Delete(int id)
        {
            try
            {
                var order = await _context.Order.SingleAsync(o => o.Id == id);
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

          
           
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
