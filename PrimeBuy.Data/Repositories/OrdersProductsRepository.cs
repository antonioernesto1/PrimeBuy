using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrimeBuy.Data.Repositories.Interfaces;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Data.Repositories
{
    public class OrdersProductsRepository : IOrdersProductsRepository
    {
        private readonly AppDbContext _context;
        public OrdersProductsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrdersProducts>> GetOrdersProductsByOrders(string orderId)
        {
            return await _context.OrdersProducts.Include(x => x.Product).AsNoTracking().Where(x => x.OrderId == orderId).ToListAsync();
        }

        public async Task<OrdersProducts> GetOrderProduct(int productId, string orderId)
        {
            return await _context.OrdersProducts.FirstOrDefaultAsync(x => x.ProductId == productId && x.OrderId == orderId);
        }
    }
}