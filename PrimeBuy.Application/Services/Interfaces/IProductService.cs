using PrimeBuy.Application.ViewModels;

namespace PrimeBuy.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProduct(ProductInputModel model);
        Task<ProductViewModel> GetProductById(int id);
        Task<List<ProductViewModel>> GetFeaturedProducts();
        Task<List<ProductViewModel>> GetProductByName(string name);
    }
}