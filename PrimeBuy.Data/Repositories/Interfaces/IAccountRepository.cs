using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Data.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Customer> GetUserByLogin(string username);
        Task<bool> CheckPassword(Customer user, string password);
        Task<List<string>> GetUserRoles(string username);
    }
}