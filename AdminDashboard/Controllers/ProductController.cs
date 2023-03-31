﻿using AdminDashboard.Models;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Drawing2D;

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
        

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
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

                string fileNamex = collection.ImagePath.FileName;

                fileNamex = Path.GetFileName(fileNamex);

                string uploadpathx = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\testimages", fileNamex);

                var streamx = new FileStream(uploadpathx, FileMode.Create);

                string path = "wwwroot\\testimages\\" + fileNamex;
                await collection.ImagePath.CopyToAsync(streamx);
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

                        string fileName = file.FileName;

                        fileName = Path.GetFileName(fileName);

                        string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\testimages", fileName);

                        var stream = new FileStream(uploadpath, FileMode.Create);
                        
                        string pathnew = "wwwroot\\testimages\\" + fileName;
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
            return View();
        }

        // POST: ProductController/Edit/5
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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
