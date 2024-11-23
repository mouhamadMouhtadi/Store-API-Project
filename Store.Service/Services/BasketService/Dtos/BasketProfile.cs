﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Store.Repository.Basket.Models;

namespace Store.Service.Services.BasketService.Dtos
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto,BasketItem>().ReverseMap();
        }
    }
}
