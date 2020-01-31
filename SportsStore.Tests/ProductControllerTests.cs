using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using SportsStore.Models;
using SportsStore.Controllers;
using Xunit;
using System.Linq;
using SportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        // test paginate
        [Fact]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product
                {
                    ProductID = 1, Name = "P1"
                },
                new Product
                {
                    ProductID = 2, Name = "P2"
                },
                new Product
                {
                    ProductID = 3, Name = "P3"
                },
                new Product
                {
                    ProductID = 4, Name = "P4"
                },
                new Product
                {
                    ProductID = 5, Name = "P5"
                }
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            //IEnumerable<Product> result =
            //    controller.List(2).ViewData.Model as IEnumerable<Product>;

            ProductsListViewModel result =
                controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2); // True to check condition
            Assert.Equal("P4", prodArray[0].Name); // Compare 
            Assert.Equal("P5", prodArray[1].Name); // Compare
        }
  
        // test pagination view model
        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product
                {
                    ProductID = 1, Name = "P1"
                },
                new Product
                {
                    ProductID = 2, Name = "P2"
                },
                new Product
                {
                    ProductID = 3, Name = "P3"
                },
                new Product
                {
                    ProductID = 4, Name = "P4"
                },
                new Product
                {
                    ProductID = 5, Name = "P5"
                }
            }).AsQueryable<Product>());

            //Arrange
            ProductController controller =
                new ProductController(mock.Object)
                {
                    PageSize = 3
                };

            //Act
            ProductsListViewModel result =
                controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            // Assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);

        }


        // Test category filter
        [Fact]
        public void Can_Filter_Product()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product
                {
                    ProductID = 1,
                    Name = "P1",
                    Category = "Cart1"
                },
                new Product
                {
                    ProductID = 2,
                    Name = "P2",
                    Category = "Cart2"
                }
                ,new Product
                {
                    ProductID = 3,
                    Name = "P3",
                    Category = "Cart1"
                }
                ,new Product
                {
                    ProductID = 4,
                    Name = "P4",
                    Category = "Cart2"
                }
                ,new Product
                {
                    ProductID = 5,
                    Name = "P5",
                    Category = "Cart3"
                }
            }).AsQueryable());

            // creat a controller and make the page size 3 items
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            Product[] result = (
                controller.List("Cart2", 1).ViewData.Model as ProductsListViewModel).Products.ToArray();


            //Assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cart2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cart2");
        }

        [Fact]
        public void Generate_Category_Specific_Count()
        {
            // Arrange 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product
                {
                    ProductID = 1,
                    Name = "P1",
                    Category = "Cat1"
                },
                new Product
                {
                    ProductID = 2,
                    Name = "P2",
                    Category = "Cat2"
                },
                new Product
                {
                    ProductID = 3,
                    Name = "P3",
                    Category = "Cat1"
                },
                new Product
                {
                    ProductID = 4,
                    Name = "P4",
                    Category = "Cat2"
                },
                new Product
                {
                    ProductID = 5,
                    Name = "P5",
                    Category = "Cat3"
                }
            }).AsQueryable<Product>());


            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            Func<ViewResult, ProductsListViewModel> GetModel = result => result?.ViewData?.Model as ProductsListViewModel;

            // Act
            int? res1 = GetModel(target.List("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.List("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.List("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(target.List(null))?.PagingInfo.TotalItems;

            // Assert
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}
