using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models;
using Microsoft.AspNetCore.Mvc;
namespace SportsStore.Component
{
    // a simple view component class that passes on the Cart object to the View method in order to generate the fragment of HTML
    // that will be included in the layout
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;
        public CartSummaryViewComponent(Cart cartService)
        {
            cart = cartService;
        }
        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
