using MultiTenancyInDotNetCore6.API.Models;

namespace MultiTenancyInDotNetCore6.API.IServices
{
    public interface IProductService
    {
        Task<Product> CreatedAsync(Product product);
        Task<Product?> GetByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetAllAsync();
    }
}
