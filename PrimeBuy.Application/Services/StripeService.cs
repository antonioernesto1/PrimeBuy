using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PrimeBuy.Application.DTOs;
using PrimeBuy.Application.Services.Interfaces;
using Stripe.Checkout;

namespace PrimeBuy.Application.Services
{
    public class StripeService : IStripeService
    {
        private readonly IConfiguration _configuration;

        public StripeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Session> CreateSession(List<ProductCartDto> products)
        {
            Stripe.StripeConfiguration.ApiKey = _configuration["Stripe:ApiKey"];
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
                return session;
        }
    }
}