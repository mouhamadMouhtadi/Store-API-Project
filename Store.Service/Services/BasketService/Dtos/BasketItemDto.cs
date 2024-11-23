﻿using System.ComponentModel.DataAnnotations;

namespace Store.Service.Services.BasketService.Dtos
{
    public class BasketItemDto
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.10, double.MaxValue,ErrorMessage ="Price Must Be Greater Than Zero")]
        public decimal Price { get; set; }

        [Required]
        [Range(1,10, ErrorMessage = "Quantity Must Be between 1 and 10")]

        public int Quantity { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string BrandName { get; set; }
        [Required]
        public string TypeName { get; set; }

    }
}