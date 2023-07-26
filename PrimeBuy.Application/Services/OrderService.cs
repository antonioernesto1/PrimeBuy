using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using PrimeBuy.Application.Services.Interfaces;
using PrimeBuy.Application.DTOs;
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
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;
        public OrderService(IRepository repository, IProductRepository productRepository,
            IAccountRepository accountRepository, IOrdersProductsRepository ordersProductsRepository,
            IOrderRepository orderRepository, IConfiguration configuration, IStatusRepository statusRepository, 
            IMapper mapper)
        {
            _repository = repository;
            _productRepository = productRepository;
            _accountRepository = accountRepository;
            _ordersProductsRepository = ordersProductsRepository;
            _orderRepository = orderRepository;
            _configuration = configuration;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<bool> UpdateOrderSessionId(string orderId, string sessionId)
        {
            try
            {
                 var order = await _orderRepository.GetOrderById(orderId);
                order.SessionId = sessionId;
                if (await _repository.SaveChangesAsync())
                    return true;
                return false;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        public async Task<Order> UpdateOrderStatus(string sessionId)
        {
            try
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
            catch (System.Exception)
            {
                
                throw;
            }
        }
        public async Task<Order> CreateOrder(List<ProductCartDto> productsDto, string username)
        {
            try
            {
                var user = await _accountRepository.GetUserByLogin(username);
                if(user == null)
                    return null;
                List<Product> products = new List<Product>();
                foreach(var product in productsDto)
                {
                    products.Add(await _productRepository.GetProductById(product.Id));
                }
                string id = GenerateId();
                var status = await _statusRepository.GetStatusById(1);
                var totalPrice = GetTotalPrice(productsDto);
                Order order = new Order
                {
                    Id = id,
                    Products = products,
                    Customer = user,
                    Status = status,
                    OrderDate = DateTime.UtcNow,
                    TotalPrice = totalPrice
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
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<List<OrderDto>> GetOrdersByUsername(string username)
        {
            try
            {
                var orders = await _orderRepository.GetOrderByUsername(username);
                if(orders.Count() == 0)
                    return null;
                
                var ordersDto = _mapper.Map<List<Order>, List<OrderDto>>(orders);
                foreach(var order in orders)
                {
                    var products = order.Products;
                    var productsDto = _mapper.Map<List<Product>, List<ProductOrderDto>>(products);
                    var orderProducts = await _ordersProductsRepository.GetOrdersProductsByOrders(order.Id);
                    productsDto.ForEach(x => x.Amount = orderProducts.Find(s => s.ProductId == x.Id).Amount);
                    ordersDto.Find(x => x.Id == order.Id).Products = productsDto;
                }
                return ordersDto;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        private string GenerateId()
        {
            var rnd = new Random();
            var id = "";
            for(int i = 0; i < 8; i++)
            {
                var number = rnd.Next(0, 10).ToString();
                id+=number;
            }
            return id;
        }

        private decimal GetTotalPrice(List<ProductCartDto> productsDto)
        {
            decimal totalPrice = 0;
            foreach(var product in productsDto)
            {
                totalPrice+=product.Price*product.Amount;
            }
            return totalPrice;
        }
    }
}