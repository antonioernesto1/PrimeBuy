using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Data.Repositories.Interfaces
{
    public interface IOrdersProductsRepository
    {
        Task<OrdersProducts> GetOrderProduct(int productId, string orderId);
        Task<List<OrdersProducts>> GetOrdersProductsByOrders(string orderId);
    }
}