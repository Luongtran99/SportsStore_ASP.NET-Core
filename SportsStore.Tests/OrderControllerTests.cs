using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
namespace SportsStore.Tests
{
    public class OrderControllerTests
    {

        // test ensure that can not check out with an empty cart, check this by SaveOrder of the mock IOrderRepository
        [Fact]
        public void Can_Checkout_Empty_Cart()
        {
            // Arrange
            Mock<IOrderRespository> mock = new Mock<IOrderRespository>();
            Cart cart = new Cart();
            Order order = new Order();
            OrderController controller = new OrderController(mock.Object, cart);

            // Act
            ViewResult result = controller.Checkout(new Order()) as ViewResult;

            // Assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never); // check that the order has not been stored
            Assert.True(string.IsNullOrEmpty(result.ViewName)); // chekc that the method is returning the default view
            Assert.False(result.ViewData.ModelState.IsValid); // check that I am passing an invalud model to the view
        }

        [Fact]
        public void Can_Checkout_Invalid_ShippingDetails()
        {
            // Arrange
            Mock<IOrderRespository> mock = new Mock<IOrderRespository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            OrderController target = new OrderController(mock.Object, cart);
            target.ModelState.AddModelError("error", "error");
            Order order = new Order();

            // Act
            ViewResult result = target.Checkout() as ViewResult;

            // Assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);

        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange
            Mock<IOrderRespository> mock = new Mock<IOrderRespository>();

            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            OrderController target = new OrderController(mock.Object, cart);

            // Act
            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            // Assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
