using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Application.DTOs;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Register(AccountRegisterDto model);
        Task<Customer> Login(AccountLoginDto model);
        Task<List<string>> GetUserRoles(string username);
    }
}