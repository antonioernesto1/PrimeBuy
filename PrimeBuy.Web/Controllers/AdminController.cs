using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PrimeBuy.Application.Services.Interfaces;
using PrimeBuy.Application.DTOs;
using PrimeBuy.Web.Models;

namespace PrimeBuy.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public AdminController(ILogger<AdminController> logger, IProductService productService,
            ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Admin/Product/Create")]
        public  async Task<IActionResult> ProductCreate()
        {
            var categories = await _categoryService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        [Route("Admin/Product/Create")]
        public async Task<IActionResult> ProductCreate([FromForm] ProductInputDto model)
        {
            var response = await _productService.AddProduct(model);

            if(response == true){
                TempData["SuccessMessage"] = "Product successfully added";
            }
            else{
                 TempData["SuccessMessage"] = "Error while adding product";
            }
            
            return RedirectToAction("ProductCreate");
        }

        [HttpGet]
        [Route("Admin/Product/Search")]
        public async Task<IActionResult> ProductSearch(string? name)
        {
            var products = new List<ProductViewDto>();
            if(name == null || name == "")
            {
                products = await _productService.GetAllProducts();
                return View(products);
            }
            products = await _productService.GetProductByName(name);
            return View(products);
        }
        [HttpGet]
        [Route("Admin/Product/Delete/{id}")]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var response = await _productService.RemoveProduct(id);
            return View(response);
        }
        [HttpGet]
        [Route("Admin/Product/Edit/{id}")]
        public async Task<IActionResult> ProductEdit(int id)
        {
            var product = await _productService.GetProductById(id, true);
            var categories = await _categoryService.GetCategories();
            var productInput = new ProductInputDto
            {
                Id = product.Id,
                Description = product.Description,
                CategoryId = product.CategoryId,
                isFeatured = product.isFeatured
            };
            var viewModel = new ProductEditViewModel{ProductInputDto = productInput, ProductViewDto = product};
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(viewModel);
        }
        [HttpPost]
        [Route("Admin/Product/Edit/{id}")]
        public async Task<IActionResult> ProductEdit(int id, ProductEditViewModel model)
        {
            var product = model.ProductInputDto;
            var response = await _productService.UpdateProduct(id, product);
            if(response == true){
                TempData["SuccessMessage"] = "Product successfully updated";
            }
            else{
                 TempData["ErrorMessage"] = "Error while updating product";
            }
            var ProductViewDto = await _productService.GetProductById(id, true);
            var categories = await _categoryService.GetCategories();
            var productInput = new ProductInputDto
            {
                Id = product.Id,
                Description = product.Description,
                CategoryId = product.CategoryId,
                isFeatured = product.isFeatured
            };
            var viewModel = new ProductEditViewModel{ProductInputDto = productInput, ProductViewDto = ProductViewDto};
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(viewModel);
        }
        [HttpGet]
        [Route("Admin/Category/Create")]
        public IActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        [Route("Admin/Category/Create")]
        public async Task<IActionResult> CategoryCreate(CategoryInputDto model)
        {
            var response = await _categoryService.CreateCategory(model);

            if(response == true){
                TempData["SuccessMessage"] = "Category successfully created";
            }
            else{
                 TempData["SuccessMessage"] = "Error while adding product";
            }

            return RedirectToAction("CategoryCreate");
        }
    }
}