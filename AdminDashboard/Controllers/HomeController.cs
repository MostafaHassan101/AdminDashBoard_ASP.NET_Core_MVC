using AdminDashboard.Models;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AdminDashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DContext _context;

        public HomeController(ILogger<HomeController> logger, DContext context)
        {
            _logger = logger;
            _context = context;

        }

        //public IActionResult _Layout()
        //{
        //    return View();
        //}
        public IActionResult Index()
        {
            var User = _context.Users.Count();
            ViewBag.Users = User;

            var Orders = _context.Order.Count();
            ViewBag.Order = Orders;

            var Categorys = _context.Category.Count();
            ViewBag.Category = Categorys;

            var Brands = _context.Brand.Count();
            ViewBag.Brand = Brands;

            var Product = _context.Product.Count();
            ViewBag.Product = Product;

            var ProductReviews = _context.ProductReviews.Count();
            ViewBag.ProductReviews = ProductReviews;

            return View();
        }
        public IActionResult Dashboard2()
        {
            return View();
        }
        public IActionResult Dashboard3()
        {
            return View();
        }
        public IActionResult Chartjs()
        {
            return View();
        }
        public IActionResult Inline()
        {
            return View();
        }

        public IActionResult General_Elements()
        {
            return View();
        }
        public IActionResult Advanced_Elements()
        {
            return View();
        }
        public IActionResult Validation()
        {
            return View();
        }

        public IActionResult DataTables()
        {
            return View();
        }

        public IActionResult jsGrid()
        {
            return View();
        }

        public IActionResult Calendar()
        {
            return View();
        }

        public IActionResult Inbox()
        {
            return View();
        }

        public IActionResult Compose()
        {
            return View();
        }

        public IActionResult Read()
        {
            return View();
        }
        
        public IActionResult Invoice()
        {
            return View();
        }
        public IActionResult Invoice_Print()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult E_commerce()
        {
            return View();
        }
        public IActionResult Project_Add()
        {
            return View();
        }
        public IActionResult Project_Edit()
        {
            return View();
        }
        public IActionResult Contact_us()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Forgot_Password()
        {
            return View();
        }
        public IActionResult Recover_Password()
        {
            return View();
        }
        public IActionResult Lockscreen()
        {
            return View();
        }
        public IActionResult Language_Menu()
        {
            return View();
        }
        public IActionResult Error_404()
        {
            return View();
        }
        public IActionResult Simple_Search()
        {
            return View();
        }

        public IActionResult Enhanced()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}