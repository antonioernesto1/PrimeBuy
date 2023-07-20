using PrimeBuy.Application.ViewModels;

namespace PrimeBuy.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProduct(ProductInputModel model);
        Task<ProductViewModel> GetProductById(int id, bool includeCategory);
        Task<List<ProductViewModel>> GetFeaturedProducts();
        Task<List<ProductViewModel>> GetProductByName(string name);
        Task<List<ProductViewModel>> GetSimilarProducts(int categoryId, int productId);
        Task<List<ProductViewModel>> GetAllProducts();
        Task<bool> RemoveProduct(int id);
    }
}