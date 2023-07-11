using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrimeBuy.Data.Repositories.Interfaces;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.AsQueryable().ToListAsync();
        }
        public async Task<Product> GetProductById(int id, bool includeCategory = false)
        {
            var product = _context.Products.AsQueryable();
            if(includeCategory == true)
                product = product.Include(x => x.Category);
            return await product.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Product>> GetFeaturedProducts()
        {
            return await _context.Products.AsQueryable().Where(x => x.isFeatured == true).ToListAsync();
        }

        public async Task<List<Product>> GetProductByName(string name)
        {
            return await _context.Products.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task<List<Product>> GetSimilarProducts(int categoryId, int productId)
        {
            return await _context.Products.Where(x => x.CategoryId == categoryId && x.Id != productId).OrderBy(x => x.isFeatured).Take(4).ToListAsync();
        }
    }
}