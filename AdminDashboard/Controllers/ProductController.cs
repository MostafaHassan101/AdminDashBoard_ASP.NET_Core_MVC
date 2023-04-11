using AdminDashboard.Models;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AdminDashboard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly DContext _context;

        public ProductController(DContext context)
        {
            _context = context;
        }

        // GET: ProductController
        [HttpGet]
        public IActionResult Index(string filter, int PageIndex = 1, int PageSize = 3)
        {
            var products = _context.Product.ToList();
            List<Product> filtered = new List<Product>();

            if (filter != null)
            {
                foreach (var product in products)
                {

                    if (product.Name.ToLower().Contains(filter.ToLower()))
                    {
                        filtered.Add(product);
                    }

                }
                return View(filtered);

                //products = filtered;
            }

            return View(products);
        }
            public ActionResult Create()
            {
                ViewBag.brands = _context.Brand.ToList();
                ViewBag.categories = _context.Category.Where(p => p.ParentCategory != null).ToList();
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
                    Category cat = _context.Category.Single(c => c.Id == collection.CategoryId);
                    Brand brand = _context.Brand.Single(b => b.Id == collection.BrandId);

                    string fileNameImage = collection.ImagePath.FileName;
                    fileNameImage = Path.GetFileName(fileNameImage);
                    string uploadpathImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/", fileNameImage);
                    var streamoneImage = new FileStream(uploadpathImage, FileMode.Create);
                    string path = "/ProductImages/" + fileNameImage;
                    await collection.ImagePath.CopyToAsync(streamoneImage);



                    ///////////////

                    List<ProductImage> ProductImagess = new List<ProductImage>();
                    try
                    {
                        foreach (var file in collection.Images)
                        {
                            string fileName = file.FileName;
                            fileName = Path.GetFileName(fileName);
                            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/", fileName);
                            var stream = new FileStream(uploadpath, FileMode.Create);
                            string pathnew = "/ProductImages/" + fileName;
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
                    foreach (long c in collection.ProductColorsidsIds)
                    {
                        ProductColor productColor = _context.ProductColors.Single(co => co.Id == c);
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
                        Price = collection.Price,
                        ModelNumber = collection.ModelNumber,
                        Quantity = collection.Quantity,
                        ProductColors = productColorss,
                        ProductImages = ProductImagess,
                        ImagePath = path,

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

                    .Include(p => p.Category).Include(a => a.ProductColors)
                    .Include(p => p.Brand).Include(a => a.ProductImages).Single(b => b.Id == id);

                var categories = _context.Category.Where(p => p.ParentCategory != null).ToList();
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
                var product = _context.Product.Include("Brand")
                .Include("Category")
                .Include(c=>c.ProductColors)
                .Include(p=>p.ProductImages)
                .Single(p => p.Id == id);
            if (collection.ProductColorsidsIds != null)
            {
                foreach (var colorId in collection.ProductColorsidsIds)
                {
                    var color = _context.ProductColors.Single(c => c.Id == colorId);
                    product.AddColor(color);

                }
            }
            try
            {
                foreach (var file in collection.Images)
                {
                    string fileName = file.FileName;
                    fileName = Path.GetFileName(fileName);
                    string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/", fileName);
                    var stream = new FileStream(uploadpath, FileMode.Create);
                    string pathnew = "/ProductImages/" + fileName;
                    await file.CopyToAsync(stream);
                    product.AddImage(new ProductImage()
                    {
                        ImagePath = pathnew
                    });
                    //ProductImagess.Add(new ProductImage()
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
            try
                {

                    Category cat = _context.Category.Single(c => c.Id == collection.CategoryId);
                    Brand brand = _context.Brand.Single(b => b.Id == collection.BrandId);

                    product.Name = collection.Name;
                    product.NameAr = collection.NameAr;
                    product.Discount = collection.Discount;
                    product.Description = collection.Description;
                    product.DescriptionAr = collection.DescriptionAr;
                    //product.ProductImages = ProductImagess;
                    product.Category = cat;
                    product.Brand = brand;
                    product.Price = collection.Price;
                    product.ModelNumber = collection.ModelNumber;
                    product.Quantity = collection.Quantity;
                    _context.Product.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            public async Task<ActionResult> Delete(int id)
            {
                try
                {
                    var product = _context.Product.Single(p => p.Id == id);
                    _context.Product.Remove(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }


            [HttpGet]
            public IActionResult Details(int id)
            {
                var prd = _context.Product.Include("ProductColors").Include("ProductImages").Include("ProductReview").Single(p => p.Id == id);
                ViewBag.Product = prd;

                var colors = prd.ProductColors;
                ViewBag.ProductColors = colors;

                var Images = prd.ProductImages;
                ViewBag.ProductImages = Images;

                var Reviews = prd.ProductReview;
                ViewBag.ProductReview = Reviews;

                return View();
            }



            //Product Colors Create and Delete

            public ActionResult CreateColorProduct(long id)
            {
                var prodid = _context.Product.Single(p => p.Id == id);
                ViewBag.Product = prodid;
                ViewBag.ProductColors = _context.ProductColors.ToList();
                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> CreateColorProduct(ProductColorModel productColorModel)
            {
                var color = _context.ProductColors.Single(a => a.Id == productColorModel.ColorId);
                var prodid = _context.Product.Include(p => p.ProductColors).Single(p => p.Id == productColorModel.prodid);

                prodid.ProductColors.Add(color);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Product", new { id = prodid.Id });
            }


            public async Task<ActionResult> DeleteColorProduct(long id, long prd)
            {
                var product = _context.Product.Include(p => p.ProductColors).Single(p => p.Id == prd);
                var color = _context.ProductColors.Single(c => c.Id == id);
                try
                {
                    product.ProductColors.Remove(color);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Edit", "Product", new { id = prd });
                }
                catch
                {
                    return View();
                }
            }


            //Product Images Create and Delete

            public ActionResult CreateImageeProduct(long id)
            {
                var prodid = _context.Product.Single(p => p.Id == id);
                ViewBag.Product = prodid;
                ViewBag.ProdImages = _context.ProductImages.ToList();
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> CreateImageeProduct(ProductImageesModel productImageModel)//, ProductModel productModel)
            {
                //var ImagePath = _context.ProductImages.Single(a => a.Id == productImageModel.Id);
                var prodid = _context.Product.Include(p => p.ProductImages).Single(p => p.Id == productImageModel.ProductId);
                try
                {

                    List<ProductImage> ProductImagess = new List<ProductImage>();
                    try
                    {
                   



                        foreach (var file in productImageModel.ImagePaths)
                        {
                            string fileName = file.FileName;
                            fileName = Path.GetFileName(fileName);
                            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/", fileName);
                            var stream = new FileStream(uploadpath, FileMode.Create);
                            string pathnew = "/ProductImages/" + fileName;
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
                    Product product = new Product()
                    {
                        ProductImages = ProductImagess,
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



            public async Task<ActionResult> DeleteImageProduct(long id, long prd)
            {
                var product = _context.Product.Include(p => p.ProductImages).Single(p => p.Id == prd);
                var Image = _context.ProductImages.Single(c => c.Id == id);
                try
                {
                    product.ProductImages.Remove(Image);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Edit", "Product", new { id = prd });
                }
                catch
                {
                    return View();
                }
            }
        public ActionResult ProductWishlidst()
        {
            var poroducts = _context.Product.Include(p => p.WishLists)
               .Where(p=>p.WishLists.Count >0) .OrderByDescending(p => p.WishLists.Count)
                .ToList();

            ViewBag.Products = poroducts;
            return View();

        }
    }
}
