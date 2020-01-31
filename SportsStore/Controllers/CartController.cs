using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart cart;
        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
            
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl // ở đấy sẽ khởi tạo 2 giá trị truyền vào biến của CartIndexViewModel để View Index sử dụng
            });
        }
        //private void SaveCart(Cart cart)
        //{
        //    HttpContext.Session.SetJson("Cart", cart);
        //}
        // 
        // no need to use 2 methods any more because we have define a Cart object , it will be called to SessionCart
        // no need 2 methods read and write data from the session and steps required to write updates
        // no need to care about how Cart objects are created and presisted 
        //private Cart GetCart()
        //{
        //    Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        //    return cart;
        //}
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if(product != null)
            {
                cart.AddItem(product, 1);
            
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemovedFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if(product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}