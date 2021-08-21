using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CataolgContext : ICatalogContext
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase database;

        public CataolgContext(IConfiguration configuration)
        {
            client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
