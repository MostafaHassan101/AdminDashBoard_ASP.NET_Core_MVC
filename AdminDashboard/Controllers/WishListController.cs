using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.Controllers
{
    public class WishListController : Controller
    {
        private readonly DContext _context;
        public WishListController(DContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            //ViewBag.Products = _context.Products.Count();
             var wish =  _context.WishList.Include(w=>w.Products);
            return View(wish);
            //

            //return View(wish);
        }

    }
}
