using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Order> GetOrderById(string id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Order> GetOrderBySessionId(string sessionId)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.SessionId == sessionId);
        }

        public async Task<List<Order>> GetOrderByUsername(string username)
        {
            return await _context.Orders.AsQueryable().Where(x => x.Customer.UserName == username).Include(x => x.Status).Include(x => x.Customer).Include(x => x.Products).ToListAsync();
        }
    }
}