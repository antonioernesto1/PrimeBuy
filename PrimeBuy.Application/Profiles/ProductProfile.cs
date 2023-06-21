using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrimeBuy.Application.ViewModels;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductInputModel, Product>();
            CreateMap<Product, ProductViewModel>();
        }
    }
}