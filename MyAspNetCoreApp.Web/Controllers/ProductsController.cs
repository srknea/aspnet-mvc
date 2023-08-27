﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private readonly ProductRepository _productRepository;
        private readonly IHelper _helper;

        //Dependency Injection Pattern
        public ProductsController(AppDbContext context, IHelper helper) //DI Container
        {
            _productRepository = new ProductRepository();
            _context = context;
            _helper = helper;

            /*
            if (!_context.Products.Any() == null)
            {
            _context.Products.Add(new Product() { Name = "Kalem", Price = 120, Stock = 10, Color = "Red" });
            _context.Products.Add(new Product() { Name = "Silgi", Price = 40, Stock = 20, Color = "Blue" });
            _context.Products.Add(new Product() { Name = "Defter", Price = 20, Stock = 30, Color = "Green" });

            _context.SaveChanges();
            }
            */
        }

        public IActionResult Index()
        {
            var text = "Asp.Net";
            var upperText = _helper.ConvertCase(text);

            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);

            _context.SaveChanges();

            return RedirectToAction("Index");
            //return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        public IActionResult Add()
        {
            ViewBag.DictionaryExpire = new Dictionary<string, int>()
            {
                { "1 Ay", 1},
                { "3 Ay", 3},
                { "6 Ay", 6},
                { "12 Ay", 12}
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new ColorSelectList() { Text = "Red", Value = "Red" },
                new ColorSelectList() { Text = "Blue", Value = "Blue" },
                new ColorSelectList() { Text = "Green", Value = "Green" }
            }, "Value", "Text");


            return View();
        }

        [HttpPost]
        public IActionResult SaveProduct(Product newProduct)
        {
            _context.Products.Add(newProduct);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);

            ViewBag.ExpireValue = product.Expire;
            ViewBag.DictionaryExpire = new Dictionary<string, int>()
            {
                { "1 Ay", 1},
                { "3 Ay", 3},
                { "6 Ay", 6},
                { "12 Ay", 12}
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new ColorSelectList() { Text = "Red", Value = "Red" },
                new ColorSelectList() { Text = "Blue", Value = "Blue" },
                new ColorSelectList() { Text = "Green", Value = "Green" }
            }, "Value", "Text", product.Color);

            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product updateProduct, int productId)
        {
            updateProduct.Id = productId;
            _context.Products.Update(updateProduct);
            _context.SaveChanges();

            TempData["status"] = "Ürün başarıyla güncellendi.";

            return RedirectToAction("Index");
        }
    }
}
