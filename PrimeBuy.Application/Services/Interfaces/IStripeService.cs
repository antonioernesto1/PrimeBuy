using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Application.DTOs;
using Stripe.Checkout;

namespace PrimeBuy.Application.Services.Interfaces
{
    public interface IStripeService
    {
        Task<Session> CreateSession(List<ProductCartDto> products);
    }
}