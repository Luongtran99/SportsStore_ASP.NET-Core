using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    // use serivecs.AddTransient ( A, B) it mean B is subclass of A, and any obj of A can use method of B , obj A call B
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;

        public void SaveProduct(Product product)
        {
            // if obj is not exitsed , it will be added to Database
            if(product.ProductID == 0)
            {
                context.Products.Add(product); // create action 
            }
            // if not , it will be changes base on the obj inputed
            else
            {
                Product dbEntry = context.Products.FirstOrDefault(m => m.ProductID == product.ProductID);
                if(dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Category = product.Category;
                    dbEntry.Price = product.Price;
                }
            }
            context.SaveChanges(); // save to SQL , update it
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.FirstOrDefault(m => m.ProductID == productID);
            if(dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
