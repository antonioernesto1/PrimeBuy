using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PrimeBuy.Application.Helpers;
using PrimeBuy.Application.Services.Interfaces;
using PrimeBuy.Application.ViewModels;
using PrimeBuy.Data.Repositories.Interfaces;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        private readonly IImageHandler _imageHandler;

        public ProductService(IRepository repository, IMapper mapper, IImageHandler imageHandler, IProductRepository productRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _imageHandler = imageHandler;
            _productRepository = productRepository;
        }

        public async Task<bool> AddProduct(ProductInputModel model)
        {
            var product = _mapper.Map<Product>(model);
            string path = _imageHandler.UploadImage(model.Image);
            product.ImagePath = path;
            _repository.Add(product);
            if(await _repository.SaveChangesAsync())
                return true;
            return false;
        }

        public async Task<ProductViewModel> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            var productViewModel = _mapper.Map<ProductViewModel>(product);
            return productViewModel;
        }

        public async Task<List<ProductViewModel>> GetFeaturedProducts()
        {
            var products = await _productRepository.GetFeaturedProducts();
            var productsViewModels = _mapper.Map<List<Product>, List<ProductViewModel>>(products);
            return productsViewModels;
        }

        public async Task<List<ProductViewModel>> GetProductByName(string name)
        {
            var products = await _productRepository.GetProductByName(name);
            var productsViewModels = _mapper.Map<List<Product>, List<ProductViewModel>>(products);
            return productsViewModels;
        }
    }
}