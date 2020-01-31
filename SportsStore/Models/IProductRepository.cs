using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SportsStore.Models
{
    // when you subclass of interface class,, you have to define all method'Interface to subclass
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; } // easier to use data

        void SaveProduct(Product product);

        Product DeleteProduct(int productID);
    }
}
