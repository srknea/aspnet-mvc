﻿using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "İsim alanı boş olamaz !")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Fiyat alanı boş olamaz !")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Stok alanı boş olamaz !")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "Açıklama alanı boş olamaz !")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Renk alanı boş olamaz !")]
        public string Color { get; set; }
        
        [Required(ErrorMessage = "Yayınlanma tarihi boş olamaz !")]
        public DateTime? PublishDate { get; set; }
        public bool IsPublish { get; set; }
        
        [Required(ErrorMessage = "Yayında kalma süresi boş olamaz !")]
        public int? Expire { get; set; }
    }
}