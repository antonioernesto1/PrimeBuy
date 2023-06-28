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

        public PaymentController(IConfiguration configuration, IOrderService orderService)
        {
            _configuration = configuration;
            _orderService = orderService;
        }

        [HttpGet("Success")]
        public async Task<IActionResult> Success(string session_id)
        {
            if(Request.Cookies.TryGetValue("AuthenticationCookies", out string cookie))
                Console.Write(cookie);
            else
                Console.Write("No cookies");

            var order = await _orderService.UpdateOrderStatus(session_id);
            return RedirectToAction("Index", "Order");
        }
        [Authorize]
        [HttpGet("CreateSession")]
        public async Task<IActionResult> CreateSession(string order_id = null)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:ApiKey"];
            Request.Cookies.TryGetValue("Cart", out string cartJson);
            var cart = JsonConvert.DeserializeObject<Cart>(cartJson);
            var products = cart.Products;
            List<SessionLineItemOptions> items = new List<SessionLineItemOptions>();
            foreach(var product in products)
            {
                items.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmountDecimal = product.Price * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name,
                            Images = new List<string> {product.ImagePath}
                        }
                    },
                    Quantity = product.Amount
                });
            }
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> {"card"},
                LineItems = items,
                SuccessUrl = "http://localhost:5106/Payment/Success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "http://localhost:5106/Payment/Cancel",
                Mode = "payment"
            };
            var service = new SessionService();
            var session = await service.CreateAsync(options);
            var response = await _orderService.UpdateOrderSessionId(order_id, session.Id);
            return Redirect(session.Url);
        }
    }
}