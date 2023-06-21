using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderById(int id);
        Task<Order> GetOrderBySessionId(string sessionId);
    }
}