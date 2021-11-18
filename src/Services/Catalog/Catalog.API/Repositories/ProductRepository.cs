using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreatProductAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            DeleteResult deleteResult = await
                _context
                .Products
                .DeleteOneAsync(filterDefinition);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _context
                        .Products
                        .Find(p => p.Id == id)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context
                        .Products
                        .Find(p => true)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Category, category);

            return await _context
                        .Products
                        .Find(filterDefinition)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await _context
                        .Products
                        .Find(filterDefinition)
                        .ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            ReplaceOneResult updateResult = await 
                _context
                .Products
                .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }
    }
}
