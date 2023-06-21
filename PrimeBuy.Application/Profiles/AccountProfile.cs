using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrimeBuy.Application.ViewModels;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountRegisterModel, Customer>();
            CreateMap<AccountLoginModel, Customer>();
        }
    }
}