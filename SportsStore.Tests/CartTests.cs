using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Item()
        {
            // Arrange  create some test products
            Product p1 = new Product
            {
                Name = "P1",
                ProductID = 1
            };
            Product p2 = new Product
            {
                ProductID = 2,
                Name = "P2"
            };

            Cart target = new Cart();

            // Act 
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }
    
    [Fact]
    public void Can_Add_Quantity_For_Existing_Lines()
    {
        //Arrange
        Product p1 = new Product
        {
            Name = "P1",
            ProductID = 1
        };
        Product p2 = new Product
        {
            ProductID = 2,
            Name = "P2"
        };

        Cart target = new Cart();

        // Act
        target.AddItem(p1, 1);
        target.AddItem(p2, 2);
        target.AddItem(p1, 10);

        CartLine[] result = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

        // Assert
        Assert.Equal(2, result.Length);
        Assert.Equal(11, result[0].Quanlity);
        Assert.Equal(2, result[1].Quanlity);

    }
        // kiemt ra co the xoa duoc 1 san pham trong List
    [Fact]
    public void Can_Remove_Line()
    {
            // Arrange
            Product p1 = new Product
            {
                Name = "P1",
                ProductID = 1
            };
            Product p2 = new Product
            {
                ProductID = 2,
                Name = "P2"
            };
            Product p3 = new Product
            {
                ProductID = 3,
                Name = "P3"
            };

            Cart target = new Cart();


            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);


            // Act
            target.RemoveLine(p2);

            // Assert
            Assert.Empty(target.Lines.Where(p => p.Product == p2));
            Assert.Equal(2, target.Lines.Count());
        }
    
        [Fact]
        public void Calculate_Cart_Total()
        {
            // Arrange
            Product p1 = new Product
            {
                Name = "P1",
                ProductID = 1
                ,Price = 100M
            };
            Product p2 = new Product
            {
                ProductID = 2,
                Name = "P2",
                Price = 50M
            };

            Cart target = new Cart();


            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();
            CartLine[] carts = target.Lines.ToArray();

            // Assert
            Assert.Equal(450M, result);
            Assert.Equal(2, carts.Length);
            Assert.Equal(4, carts[0].Quanlity);
            Assert.Equal(1, carts[1].Quanlity);
        }

        [Fact]
        public void Can_Clear_Cart()
        {
            // Arrange
            Product p1 = new Product
            {
                Name = "P1",
                ProductID = 1
               ,
                Price = 100M
            };
            Product p2 = new Product
            {
                ProductID = 2,
                Name = "P2",
                Price = 50M
            };

            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // Act - clear cart
            target.Clear();

            //Assert
            Assert.Empty(target.Lines.ToArray());



        }
    }
}
