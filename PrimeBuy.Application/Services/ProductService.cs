using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PrimeBuy.Application.Helpers;
using PrimeBuy.Application.Services.Interfaces;
using PrimeBuy.Application.DTOs;
using PrimeBuy.Data.Repositories.Interfaces;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageHandler _imageHandler;

        public ProductService(IRepository repository, IMapper mapper,
            IImageHandler imageHandler, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _imageHandler = imageHandler;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> AddProduct(ProductInputDto model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);
                product.CategoryId = 0;
                var category = await _categoryRepository.GetCategoryById(model.CategoryId);
                product.Category = category;
                string path = _imageHandler.UploadImage(model.Image);
                product.ImagePath = path;
                _repository.Add(product);
                if(await _repository.SaveChangesAsync())
                    return true;
                return false;
            }
            catch (System.Exception)
            {
                
                throw;
            }
            
        }

        public async Task<ProductViewDto> GetProductById(int id, bool includeCategory = false)
        {
            try
            {
                var product = await _productRepository.GetProductById(id, includeCategory);
                var ProductViewDto = _mapper.Map<ProductViewDto>(product);
                return ProductViewDto;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<List<ProductViewDto>> GetFeaturedProducts()
        {
            try
            {
                var products = await _productRepository.GetFeaturedProducts();
                var productsViewModels = _mapper.Map<List<Product>, List<ProductViewDto>>(products);
                return productsViewModels;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<List<ProductViewDto>> GetProductByName(string name)
        {
            try
            {
                var products = await _productRepository.GetProductByName(name);
                var productsViewModels = _mapper.Map<List<Product>, List<ProductViewDto>>(products);
                return productsViewModels;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<List<ProductViewDto>> GetSimilarProducts(int categoryId, int productId)
        {
            try
            {
                var products = await _productRepository.GetSimilarProducts(categoryId, productId);
                var productsViewModels = _mapper.Map<List<Product>, List<ProductViewDto>>(products);
                return productsViewModels;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<List<ProductViewDto>> GetAllProducts()
        {
            try
            {
                var products = await _productRepository.GetAllProducts();
                var productsViewModels = _mapper.Map<List<Product>, List<ProductViewDto>>(products);
                return productsViewModels;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<bool> RemoveProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetProductById(id);
                if(product == null)
                    return false;
                _repository.Remove<Product>(product);
                if(await _repository.SaveChangesAsync() == true)
                    return true;
                return false;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        public async Task<bool> UpdateProduct(int id, ProductInputDto model)
        {
            try
            {
                var product = await _productRepository.GetProductById(id, true);
                if(product == null)
                    return false;
                if(model.Image != null)
                {
                    var imagePath = _imageHandler.UploadImage(model.Image);
                    product.ImagePath = imagePath;
                }
                product.CategoryId = model.CategoryId;
                product.Description = model.Description;
                product.Name = model.Name;
                product.isFeatured = model.isFeatured;
                product.Price = model.Price;
                product.Id = id;
                var response = await _repository.SaveChangesAsync();
                return response;
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}