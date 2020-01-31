using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Collections.Generic;

namespace SportsStore.Models.ViewModels
{
    // 
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        // create Filtering the Product List
        public string CurrentCategory { get; set; }
    }
}
