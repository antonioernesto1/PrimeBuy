using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Application.ViewModels;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(CategoryInputModel model);
        Task<List<Category>> GetCategories();
    }
}