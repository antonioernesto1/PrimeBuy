using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Application.ViewModels;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Register(AccountRegisterModel model);
        Task<Customer> Login(AccountLoginModel model);
        Task<List<string>> GetUserRoles(string username);
    }
}