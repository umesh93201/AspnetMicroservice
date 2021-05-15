using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseConnections:ConnectionsString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseConnections:DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseConnections:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }


        public IMongoCollection<Product> Products { get; }
    }
}
