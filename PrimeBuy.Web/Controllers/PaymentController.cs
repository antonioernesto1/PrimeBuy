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
using Stripe;
using Stripe.Checkout;

namespace PrimeBuy.Web.Controllers
{
    [Route("{controller}")]
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orderService;
        private readonly IStripeService _stripeService;

        public PaymentController(IConfiguration configuration, IOrderService orderService, IStripeService stripeService)
        {
            _configuration = configuration;
            _orderService = orderService;
            _stripeService = stripeService;
        }

        [HttpGet("Success")]
        public async Task<IActionResult> Success(string session_id)
        {
            try
            {
                var order = await _orderService.UpdateOrderStatus(session_id);
                return RedirectToAction("Index", "Order");
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        [Authorize]
        [HttpGet("CreateSession")]
        public async Task<IActionResult> CreateSession(string order_id = null)
        {
            try
            {
                StripeConfiguration.ApiKey = _configuration["Stripe:ApiKey"];
                Request.Cookies.TryGetValue("Cart", out string cartJson);
                var cart = JsonConvert.DeserializeObject<Cart>(cartJson);
                var products = cart.Products;
                var session = await _stripeService.CreateSession(products);
                await _orderService.UpdateOrderSessionId(order_id, session.Id);
                return Redirect(session.Url);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}