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
using PrimeBuy.Application.ViewModels;

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
        public async Task<IActionResult> ProductCreate([FromForm] ProductInputModel model)
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
        [Route("Admin/Category/Create")]
        public IActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        [Route("Admin/Category/Create")]
        public async Task<IActionResult> CategoryCreate(CategoryInputModel model)
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