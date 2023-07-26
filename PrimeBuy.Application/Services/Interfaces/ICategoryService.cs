using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Application.DTOs;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(CategoryInputDto model);
        Task<List<Category>> GetCategories();
    }
}