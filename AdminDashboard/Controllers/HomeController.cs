using AdminDashboard.Models;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace AdminDashboard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DContext _context;

        public HomeController(ILogger<HomeController> logger, DContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult _Layout()
        {

            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;

            return View();
        }
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

            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;

            return View();
        }
        public IActionResult Dashboard2()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;

            return View();
        }
        public IActionResult Dashboard3()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;

            return View();
        }
        public IActionResult Chartjs()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;

            return View();
        }
        public IActionResult Inline()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;

            return View();
        }

        public IActionResult General_Elements()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Advanced_Elements()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Validation()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }

        public IActionResult DataTables()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }

        public IActionResult jsGrid()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }

        public IActionResult Calendar()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }

        public IActionResult Inbox()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }

        public IActionResult Compose()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }

        public IActionResult Read()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        
        public IActionResult Invoice()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Invoice_Print()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Profile()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult E_commerce()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Project_Add()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Project_Edit()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Contact_us()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Login()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Register()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Forgot_Password()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Recover_Password()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Lockscreen()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Language_Menu()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Error_404()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }
        public IActionResult Simple_Search()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }

        public IActionResult Enhanced()
        {
            string userName = HttpContext.Request.Cookies["UserName"];
            ViewData["UserName"] = userName;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}