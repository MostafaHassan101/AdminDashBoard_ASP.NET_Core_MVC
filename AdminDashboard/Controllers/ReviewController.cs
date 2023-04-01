using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace AdminDashboard.Controllers
{
    public class ReviewController : Controller
    {
        private readonly DContext _context;
        public ReviewController(DContext context)
        {
            _context = context;
        }
        [HttpGet]
    public async Task<IActionResult> Details(long id)
        {
            //dynamic dy = new ExpandoObject();
        var Rev = await _context.ProductReviews.Include("Product")
                //.Include(b=>b.Product.Brand)
                //.Include(b=>b.Product.Category)
                .FirstOrDefaultAsync(i => i.Product.Id == id);


            //var Rev2 = await _context.Products.FirstOrDefaultAsync();

            //    .Include(i => i.ProductId)
            //.AsNoTracking()



            if (Rev == null)
            {
                return NotFound();
            }

            return View(Rev);
    }
    }
    
}
