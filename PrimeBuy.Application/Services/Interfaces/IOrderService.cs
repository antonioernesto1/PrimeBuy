using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Application.DTOs;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByUsername(string username);
        Task<Order> CreateOrder(List<ProductCartDto> products, string username);
        Task<Order> UpdateOrderStatus(string sessionId);
        Task<bool> UpdateOrderSessionId(string orderId, string sessionId);
    }
}