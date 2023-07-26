using PrimeBuy.Application.DTOs;

namespace PrimeBuy.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProduct(ProductInputDto model);
        Task<ProductViewDto> GetProductById(int id, bool includeCategory);
        Task<List<ProductViewDto>> GetFeaturedProducts();
        Task<List<ProductViewDto>> GetProductByName(string name);
        Task<List<ProductViewDto>> GetSimilarProducts(int categoryId, int productId);
        Task<List<ProductViewDto>> GetAllProducts();
        Task<bool> UpdateProduct(int id, ProductInputDto model);
        Task<bool> RemoveProduct(int id);
    }
}