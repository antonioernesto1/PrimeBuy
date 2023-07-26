using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PrimeBuy.Application.DTOs;
using PrimeBuy.Application.Services.Interfaces;
using PrimeBuy.Data.Repositories.Interfaces;
using PrimeBuy.Web.Utils;

namespace PrimeBuy.Web.Controllers
{
    [Route("{controller}")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IStripeService _stripeService;
        private readonly IOrdersProductsRepository _ordersProductsRepository;
        public OrderController(IOrderService orderService, IStripeService stripeService,
                        IOrdersProductsRepository ordersProductsRepository)
        {
            _orderService = orderService;
            _stripeService = stripeService;
            _ordersProductsRepository = ordersProductsRepository;
        }

        [Authorize]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var username = User.Identity.Name;
                var orders = await _orderService.GetOrdersByUsername(username); 
                if(orders == null)
                    orders = new List<OrderDto>();
                return View(orders);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        [Authorize]
        [HttpGet("CreateOrder")]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                Request.Cookies.TryGetValue("Cart", out string jsonCart);
                var cart = JsonConvert.DeserializeObject<Cart>(jsonCart);
                var username = User.Identity.Name;
                var order = await _orderService.CreateOrder(cart.Products, username);
                if(order != null)
                    return RedirectToAction("CreateSession", "Payment", new {order_id = order.Id});
                return RedirectToAction("Index", "Home");
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        [Authorize]
        [HttpGet("UpdateOrder/{id}")]
        public async Task<IActionResult> UpdateOrder(string id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                var orderProducts = await _ordersProductsRepository.GetOrdersProductsByOrders(order.Id);
                List<ProductCartDto> productCartDtos = new List<ProductCartDto>();
                foreach(var product in orderProducts)
                {
                    var productCartDto = new ProductCartDto{Name = product.Product.Name, Price = product.Product.Price, 
                                                        Amount = product.Amount, Id = product.Product.Id, 
                                                        ImagePath = product.Product.ImagePath};
                    productCartDtos.Add(productCartDto);
                }
                var session = await _stripeService.CreateSession(productCartDtos);
                await _orderService.UpdateOrderSessionId(order.Id, session.Id);
                return Redirect(session.Url);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

    }
}