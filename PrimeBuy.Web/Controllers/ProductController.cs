using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimeBuy.Application.Services.Interfaces;
using PrimeBuy.Application.DTOs;
using PrimeBuy.Web.Models;

namespace PrimeBuy.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var viewModel = new ProductPageViewModel();
            var product = await _productService.GetProductById(id, true);
            viewModel.ProductViewDto = product;
            var similarProducts = await _productService.GetSimilarProducts(product.CategoryId, product.Id);
            viewModel.SimilarProducts = similarProducts;
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var products = await _productService.GetProductByName(query);
            return View(products);
        }
       
    }
}