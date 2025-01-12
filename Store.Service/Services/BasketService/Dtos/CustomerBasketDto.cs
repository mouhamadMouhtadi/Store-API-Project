﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Repository.Basket.Models;

namespace Store.Service.Services.BasketService.Dtos
{
    public class CustomerBasketDto
    {
        public string? Id { get; set; }
        public int? DeleveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
    }
}
