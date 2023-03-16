﻿using AutoMapper;

namespace Products.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {
            CreateMap<Models.Domain.Product, Models.DTO.Product>().ReverseMap();
        }
    }
}
