using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Application.Services.Interfaces;
using PrimeBuy.Application.DTOs;
using PrimeBuy.Data.Repositories.Interfaces;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> CreateCategory(CategoryInputDto model)
        {
            try
            {
                var category = new Category {Name = model.Name};
                _repository.Add(category);
                if(await _repository.SaveChangesAsync())
                    return true;
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<List<Category>> GetCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetCategories();
                return categories;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}