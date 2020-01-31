using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models;

namespace SportsStore.Models
{
    public class FakeProductRepository /*: Models.IProductRepository*/
    {
        // use lambda expression 
        public IQueryable<Product> Products => new List<Product>
        {
            new Product
            {
                Name = "Bong Da", Price = 25M
            },
            new Product
            {
                Name = "Truot van", Price = 179M
            }
        }.AsQueryable<Product>(); // List to Queryable

    }
}
