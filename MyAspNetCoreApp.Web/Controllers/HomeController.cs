﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;
using System.Diagnostics;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .OrderByDescending(p => p.Id)
                .Select(x => new ProductPartialViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Stock = x.Stock
                })
                .ToList();

            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };


            return View();
        }

        public IActionResult Privacy()
        {
            var products = _context.Products
               .OrderByDescending(p => p.Id)
               .Select(x => new ProductPartialViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Price = x.Price,
                   Stock = x.Stock
               })
               .ToList();

            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Visitor()
        {
            return View();
        }

        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {
            try
            {
                var visitor = _mapper.Map<Visitor>(visitorViewModel);

                visitor.CreatedDate = DateTime.Now;

                _context.Visitors.Add(visitor);

                _context.SaveChanges();

                TempData["Message"] = "Yorum başarıyla kaydedildi.";

                return RedirectToAction(nameof(HomeController.Visitor));
            }
            catch (Exception)
            {
                TempData["Message"] = "Yorum kaydedilirken bir hatayla karşılaşıldı.";

                return RedirectToAction(nameof(HomeController.Visitor));
            }
        }
    }
}