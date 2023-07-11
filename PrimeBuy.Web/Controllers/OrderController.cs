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
using PrimeBuy.Web.Utils;

namespace PrimeBuy.Web.Controllers
{
    [Route("{controller}")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [Authorize]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                 var username = User.Identity.Name;
                var orders = await _orderService.GetOrdersByUsername(username);
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

    }
}