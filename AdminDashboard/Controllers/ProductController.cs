using AdminDashboard.Models;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using static Azure.Core.HttpHeader;
using static System.Net.Mime.MediaTypeNames;

namespace AdminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly DContext _context;

        public ProductController(DContext context)
        {
            _context = context;
        }
        // GET: ProductController

        [HttpGet]
        public IActionResult Index(int PageIndex = 1, int PageSize = 3)
        {
            var products = _context.Product.ToList();
           // ViewBag.products = products;

            return View(products);
        }
        [HttpGet]
        public ActionResult productColors(int id)
        {
            var productcolor = _context.ProductColors.Where(p=> p.Id == id);
            ViewBag.colors = productcolor;
            return View();
        }
        
        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.brands = _context.Brand.ToList();
            ViewBag.categories = _context.Category.ToList();
            ViewBag.ProductColors = _context.ProductColors.ToList();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductModel collection)
        {
            try
            {
                Category cat= _context.Category.Single(c=>c.Id==collection.CategoryId);
                Brand brand = _context.Brand.Single(b =>b.Id == collection.BrandId);
                // var x = collection.ProductColors; 

                string fileNameImage = collection.ImagePath.FileName;

                fileNameImage = Path.GetFileName(fileNameImage);

                string uploadpathImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ProductImages", fileNameImage);

                var streamoneImage = new FileStream(uploadpathImage, FileMode.Create);

                string path = "wwwroot\\ProductImages\\" + fileNameImage;
                await collection.ImagePath.CopyToAsync(streamoneImage);
                //ProductImagess.Add(new ProductImage()
                //{
                //    ImagePath = path
                //});
                

                ////////////////////////////////////
                List<ProductImage> ProductImagess = new List<ProductImage>();
                try

                {

                    foreach (var file in collection.Images)

                    {
                        //var allFilenames = Directory.EnumerateFiles(file.FileName).Select(p => Path.GetFileName(p));

     
                        //var candidates = allFilenames.Where(fn => Path.GetExtension(fn) == ".txt")
                        //                             .Select(fn => Path.GetFileNameWithoutExtension(fn));

                        string fileName = file.FileName;

                        fileName = Path.GetFileName(fileName);
                        //private string[] permittedExtensions = { ".txt", ".pdf" };

                       //   var ext = Path.GetExtension(uploadedFileName).ToLowerInvariant();

                        //    if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                        //  {
                             // The extension is invalid ... discontinue processing the file
                        //  }


                        string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ProductImages", fileName);

                        var stream = new FileStream(uploadpath, FileMode.Create);
                        
                        string pathnew = "wwwroot\\ProductImages\\" + fileName;
                        await file.CopyToAsync(stream);
                        ProductImagess.Add(new ProductImage()
                        {
                            ImagePath = pathnew
                        });

                    }

                    ViewBag.Message = "File uploaded successfully.";
                }

                catch

                {

                    ViewBag.Message = "Error while uploading the files.";


                }
                ///////////////
               
                List<ProductColor> productColorss = new List<ProductColor>();
                foreach(long c in collection.ProductColorsidsIds)
                {
                    ProductColor productColor=_context.ProductColors.Single(co=>co.Id==c);
                    productColorss.Add(productColor);

                }
               

                Product product = new Product()
                {
                    Name = collection.Name,
                    NameAr = collection.NameAr,
                    Discount = collection.Discount,
                    Description = collection.Description,
                    DescriptionAr = collection.DescriptionAr,
                    Category = cat,
                    Brand = brand,
                    Price= collection.Price,
                    ModelNumber= collection.ModelNumber,
                    Quantity=collection.Quantity,
                    ProductColors = productColorss,
                    ProductImages = ProductImagess,
                    ImagePath= path,

                };
               await _context.Product.AddAsync(product);
               await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product Product = _context.Product
                .Include(p => p.Category)
                .Include(p => p.Brand).Include(a=>a.ProductImages).Single(b => b.Id == id);
            var categories = _context.Category.ToList();
            var brands = _context.Brand.ToList();
            var ProductColors = _context.ProductColors.ToList();
            ViewBag.brands = brands;
            ViewBag.categories = categories;
            ViewBag.Product = Product;
            ViewBag.ProductColors = ProductColors;
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductModel collection)
        {
            var product = _context.Product.Single(p => p.Id == id);
            try
            {
                string fileNameImage = collection.ImagePath.FileName;

                fileNameImage = Path.GetFileName(fileNameImage);

                string uploadpathImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ProductImages", fileNameImage);

                var streamoneImage = new FileStream(uploadpathImage, FileMode.Create);

                string path = "wwwroot\\ProductImages\\" + fileNameImage;
                await collection.ImagePath.CopyToAsync(streamoneImage);
                //List<ProductImage> ProductImagess = new List<ProductImage>();
                try

                {
                    foreach (var file in collection.Images)
                    {
                        string fileName = file.FileName;

                        fileName = Path.GetFileName(fileName);
                        string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ProductImages", fileName);

                        var stream = new FileStream(uploadpath, FileMode.Create);

                        string pathnew = "wwwroot\\ProductImages\\" + fileName;
                        await file.CopyToAsync(stream);
                        product.AddImage(new ProductImage() { ImagePath = pathnew });
                        //product.ProductImages.Add(new ProductImage()
                        //{
                        //    ImagePath = pathnew
                        //});
                    }
                    ViewBag.Message = "File uploaded successfully.";
                }
                catch

                {
                     ViewBag.Message = "Error while uploading the files.";
                }
               // List<ProductColor> productColorss = new List<ProductColor>();
                foreach (long c in collection.ProductColorsidsIds)
                {
                    ProductColor productColor = _context.ProductColors.Single(co => co.Id == c);
                    product.AddColor(new ProductColor() { Name = productColor.Name,HexValue=productColor.HexValue });
                   //product.ProductColors.Add(productColor);

                }

                
              
                Category cat = _context.Category.Single(c => c.Id == collection.CategoryId);
                Brand brand = _context.Brand.Single(b => b.Id == collection.BrandId);
               
                product.Name = collection.Name;
                product.NameAr = collection.NameAr;
                product.Discount = collection.Discount;
                product.Description = collection.Description;
                product.DescriptionAr = collection.DescriptionAr;
                product.Category =cat;
                product.Brand = brand;
                product.Price = collection.Price;
                product.ModelNumber = collection.ModelNumber;
                product.Quantity = collection.Quantity;
                product.ImagePath = path;
                _context.Product.Update(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)

        {
            var product = _context.Product.Single(a => a.Id == id) ;
            ViewBag.product=product;
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var product= _context.Product.Single(p=>p.Id== id);
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
