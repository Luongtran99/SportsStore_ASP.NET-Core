using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Models;
using SportsStore.Component;

namespace SportsStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        // kiem tra so luong category co dung voi category khoi tao
        [Fact]
        public void Can_Select_Categories()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product
                {
                    ProductID = 1,
                    Name = "P1",
                    Category = "Apples"
                },
                new Product
                {
                    ProductID = 2,
                    Name = "P2",
                    Category = "Apples"
                },
                new Product
                {
                    ProductID = 3,
                    Name = "P3",
                    Category = "Plums"
                },
                new Product
                {
                    ProductID = 4,
                    Name = "P4",
                    Category = "Oranges"
                }
            }).AsQueryable());

            NavigationMenuViewComponent controller = new NavigationMenuViewComponent(mock.Object)
            {

            };

            //Act = get the set of categories
            string[] result = ((IEnumerable<string>)(controller.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            // Assert - compare string[] to results
            Assert.True(Enumerable.SequenceEqual(new string[]
            {
                "Apples",
                "Oranges",
                "Plums"
            }, result));
        }

        // kiem tra xem co hien thi dung category dang chon hay ko
        [Fact]
        public void Indicates_Selected_Category()
        {
            // Arrange
            string categoryToSelect = "Apples";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product
                {
                    ProductID = 1,
                    Name = "P1",
                    Category = "Apples"
                }
                , new Product
                {
                    ProductID = 2,
                    Name = "P2",
                    Category = "Orranges"
                }
            }).AsQueryable());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext
                {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                }
            };

            target.RouteData.Values["category"] = categoryToSelect;

            // Act 
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            // Assert
            Assert.Equal(categoryToSelect, result);
        }
    }
}
