using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public AdminController(ILogger<AdminController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Admin/Product/Create")]
        public IActionResult ProductCreate()
        {
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
    }
}