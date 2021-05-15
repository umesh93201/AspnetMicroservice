using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Repositories;
using Catalog.API.Entities;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          var products = await _repository.getProducts();

            return Ok(products);
        }


        [HttpGet("{id: length(24)}",Name ="GetProduct")]
               [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductbyID(string id)
        {
            var product = await _repository.getProductbyId(id);
            if (product == null)
            {
                _logger.LogError($"Product with ID is { id} missing");
                return NotFound();
                
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("[Action]/{category}",Name = "GetProductsbyCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsbyCategory(string category)
        {
            var Products = await _repository.GetProductbyCategory(category);
            return Ok(Products);
        }

        [HttpGet]
        [Route("[Action]/{name}",Name = "GetProductsbyNamevalue")]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsbyName(string name)
        {
            var products = await _repository.GetProductsbyName(name);

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }


        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
           return Ok(await _repository.udpateProduct(product));
            
        }

        [HttpDelete("{id: length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductbyId(string id)
        {
            return Ok(await _repository.deleteProduct(id));

        }


    }
}
