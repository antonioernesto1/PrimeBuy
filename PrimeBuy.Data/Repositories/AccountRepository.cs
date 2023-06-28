using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrimeBuy.Data.Repositories.Interfaces;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<Customer> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(UserManager<Customer> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Customer> GetUserByLogin(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
                return null;
            return user;
        }

        public async Task<bool> CheckPassword(Customer user, string password)
        {
            var response = await _userManager.CheckPasswordAsync(user, password);
            if(response == true)
                return true;
            return false;
        }

        public async Task<List<string>> GetUserRoles(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}