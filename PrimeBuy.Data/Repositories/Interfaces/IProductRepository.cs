using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id, bool includeCategory = false);
        Task<List<Product>> GetFeaturedProducts(); 
        Task<List<Product>> GetProductByName(string name);
        Task<List<Product>> GetSimilarProducts(int categoryId, int productId);
    }
}