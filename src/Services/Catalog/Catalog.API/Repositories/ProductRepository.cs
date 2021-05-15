using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> getProductbyId(string ID)
        {
            return await _context.Products.Find(p => p.Id == ID).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> getProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync<Product>();
        }

        public async Task<IEnumerable<Product>> GetProductsbyName(string name)
        {
            return await _context.Products.Find(p => p.Name == name).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> deleteProduct(string ID)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id , ID);
            var deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductbyCategory(string category)
        {
            return await _context.Products.Find(p => p.Category == category).ToListAsync<Product>();
        }

        public async Task<bool> udpateProduct(Product product)
        {
            var updateProduct = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateProduct.IsAcknowledged && updateProduct.ModifiedCount > 0; 
        }
    }
}
