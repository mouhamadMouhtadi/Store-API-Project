﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Store.Data.Entity;

namespace Store.Service.Services.Products.Dtos
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName , option => option.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.TypeName, option => option.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.ImageUrl, option => option.MapFrom<ProductImageResolver>());
            CreateMap<ProductBrand, BrandTypeDetailsDto>();
            CreateMap<ProductType, BrandTypeDetailsDto>();
        }
    }
}
