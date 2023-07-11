using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PrimeBuy.Application.Services.Interfaces;
using PrimeBuy.Application.ViewModels;
using PrimeBuy.Web.Utils;

namespace PrimeBuy.Web.Controllers
{
    [Authorize]
    [Route("{controller}")]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            Cart cart = GetCartFromCookies();
            return View(cart);
        }
        
        [HttpGet]
        [Route("Cart/Product/Add")]
        public async Task<IActionResult> AddProduct(int id)
        {
            Cart cart = GetCartFromCookies();
            if(cart.Products.Select(x => x.Id).Contains(id))
            {
                var productAtCart = cart.Products.
                FirstOrDefault(x => x.Id == id);

                productAtCart.Amount++;
                SetCartCookie(cart);
                var product = await _productService.GetProductById(productAtCart.Id, false);
                return View(product);
            }
            else
            {
                var product = await _productService.GetProductById(id, false);
                cart.Products.Add(new ProductCartViewModel{
                    Id = product.Id, Amount = 1,
                    ImagePath = product.ImagePath, Price = product.Price,
                    Name = product.Name}
                    );
                SetCartCookie(cart);
                return View(product);
            }
           
        }

        private Cart GetCartFromCookies()
        {
            if(Request.Cookies.TryGetValue("Cart", out string cartJson))
            {
                return JsonConvert.DeserializeObject<Cart>(cartJson);
            }
            else{
                return new Cart {Products = new List<ProductCartViewModel>()};
            }
        }

        private void SetCartCookie(Cart cart)
        {
            string cartJson = JsonConvert.SerializeObject(cart);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            };
            Response.Cookies.Append("Cart", cartJson, options);
        }
    }
}