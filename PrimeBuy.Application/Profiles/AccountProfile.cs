using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrimeBuy.Application.DTOs;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountRegisterDto, Customer>();
            CreateMap<AccountLoginDto, Customer>();
        }
    }
}