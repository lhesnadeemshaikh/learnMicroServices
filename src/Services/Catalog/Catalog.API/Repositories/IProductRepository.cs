using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);

        Task CreatProductAsync(Product product);
        Task<bool> DeleteProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
    }
}
