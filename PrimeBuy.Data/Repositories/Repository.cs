using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PrimeBuy.Data.Repositories.Interfaces;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Data.Repositories
{
    public class Repository : IRepository
    {
        private AppDbContext _context;
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Repository(AppDbContext context, UserManager<Customer> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            if(await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> CreateUser(Customer model, string password)
        {
            var result = await _userManager.CreateAsync(model, password);
            if(result.Succeeded){
                if(!_roleManager.Roles.Any()){
                    IdentityRole admin = new IdentityRole{Name = "Admin"};
                    IdentityRole customer = new IdentityRole{Name = "Customer"};
                    await _roleManager.CreateAsync(admin);
                    await _roleManager.CreateAsync(customer);
                    if(model.UserName == "admin"){
                        string[] roles = {"Admin", "Customer"};
                        await _userManager.AddToRolesAsync(model, roles);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}