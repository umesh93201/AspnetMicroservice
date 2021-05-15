using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> getProducts();

        public Task<Product> getProductbyId(string ID);

        public Task<IEnumerable<Product>> GetProductsbyName(string name);

        public Task<IEnumerable<Product>> GetProductbyCategory(string category);

        public Task CreateProduct(Product product);

        public Task<bool> udpateProduct(Product product);

        public Task<bool> deleteProduct(string ID);
    }
}
