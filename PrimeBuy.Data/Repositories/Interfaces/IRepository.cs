
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Data.Repositories.Interfaces
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<bool> CreateUser(Customer model, string password);
        void Remove<T>(T entity) where T : class;
    }
}