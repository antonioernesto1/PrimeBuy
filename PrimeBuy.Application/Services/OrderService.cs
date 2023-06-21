using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using PrimeBuy.Application.Services.Interfaces;
using PrimeBuy.Application.ViewModels;
using PrimeBuy.Data.Repositories;
using PrimeBuy.Data.Repositories.Interfaces;
using PrimeBuy.Entities.Models;
using Stripe.Checkout;

namespace PrimeBuy.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrdersProductsRepository _ordersProductsRepository;
        public OrderService(IRepository repository, IProductRepository productRepository, IAccountRepository accountRepository, IOrdersProductsRepository ordersProductsRepository, IOrderRepository orderRepository, IConfiguration configuration)
        {
            _repository = repository;
            _productRepository = productRepository;
            _accountRepository = accountRepository;
            _ordersProductsRepository = ordersProductsRepository;
            _orderRepository = orderRepository;
            _configuration = configuration;
        }

        public async Task<bool> UpdateOrderSessionId(int orderId, string sessionId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            order.SessionId = sessionId;
            if (await _repository.SaveChangesAsync())
                return true;
            return false;
        }
        public async Task<Order> UpdateOrderStatus(string sessionId)
        {
            Stripe.StripeConfiguration.ApiKey = _configuration["Stripe:ApiKey"];
            var order = await _orderRepository.GetOrderBySessionId(sessionId);
            if(order == null)
                return null;
            var service = new SessionService();
            var session = await service.GetAsync(sessionId);
            if(session != null)
            {
                if(session.PaymentStatus == "paid")
                    order.StatusId = 2;
                else if(session.PaymentStatus == "canceled")
                    order.StatusId = 3;

                if(await _repository.SaveChangesAsync())
                    return order;
                return null;
            }
            return null;

        }
        public async Task<Order> CreateOrder(List<ProductCartViewModel> productsDto, string username)
        {
            var user = await _accountRepository.GetUserByLogin(username);
            if(user == null)
                return null;
            List<Product> products = new List<Product>();
            foreach(var product in productsDto)
            {
                products.Add(await _productRepository.GetProductById(product.Id));
            }
            Order order = new Order
            {
                Products = products,
                Customer = user,
                StatusId = 1
            };
            _repository.Add(order);
            if(await _repository.SaveChangesAsync() == true)
            {
                foreach(var productDto in productsDto)
                {
                    var orderProduct = await _ordersProductsRepository.GetOrderProduct(productDto.Id, order.Id);
                    orderProduct.Amount = productDto.Amount;
                }
                if(await _repository.SaveChangesAsync())
                    return order;
            }
            return null;
        }
    }
}