using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PcStore.Domain.Entities;
using System.Linq;

namespace PcStore.UnitTests
{
    [TestClass]
    public class UnitTestCart
    {
        [TestMethod]
        public void Can_add_New_lines()
        {
            Product product = new Product
            {
                Id=1,
                Name="pc"
            };
            Product product2 = new Product
            {
                Id = 2,
                Name = "laptop"
            };

            //art
            Cart target = new Cart();
            target.Additem(product);
            target.Additem(product2, 3);
            CardLine[] res = target.lines.ToArray();
            //Assert
            Assert.AreEqual(res[0].products, product);
            Assert.AreEqual(res[1].products, product2);
        }
        [TestMethod]
        public void Can_add_QTY_for_existing_lines()
        {
            Product product = new Product
            {
                Id = 1,
                Name = "pc"
            };
            Product product2 = new Product
            {
                Id = 2,
                Name = "laptop"
            };

            //art
            Cart target = new Cart();
            target.Additem(product);
            target.Additem(product2, 3);
            target.Additem(product,2);
            CardLine[] res = target.lines.OrderBy(c => c.products.Id).ToArray();
            //Assert
            Assert.AreEqual(res.Length, 2);
            Assert.AreEqual(res[0].Quantity, 3);
        }
        [TestMethod]
        public void Can_add_Remove_lines()
        {
            Product product = new Product
            {
                Id = 1,
                Name = "pc"
            };
            Product product2 = new Product
            {
                Id = 2,
                Name = "laptop"
            };
            Product product3 = new Product
            {
                Id = 3,
                Name = "laptop"
            };

            //art
            Cart target = new Cart();
            target.Additem(product);
            target.Additem(product2, 3);
            target.Additem(product3, 2);
            target.Additem(product2, 1);
            target.Removeline(product2);
            //Assert
            Assert.AreEqual(target.lines.Where(c=>c.products==product2).Count(), 0);
            Assert.AreEqual(target.lines.Count(), 2);
        }
        [TestMethod]
        public void Calc_cart_total()
        {
            Product product = new Product
            {
                Id = 1,
                Name = "pc",
                Price=100m
            };
            Product product2 = new Product
            {
                Id = 2,
                Name = "laptop",
                Price=50m
            };
            Product product3 = new Product
            {
                Id = 3,
                Name = "laptop",
                Price=70m
            };

            //art
            Cart target = new Cart();
            target.Additem(product,1);
            target.Additem(product2, 2);
            target.Additem(product3, 1);
            decimal result = target.ComputeTotalValue();
            //Assert
            Assert.AreEqual(result, 270m);
        }
        [TestMethod]
        public void Can_Clear_contents()
        {
            Product product = new Product
            {
                Id = 1,
                Name = "pc",
                Price = 100m
            };
            Product product2 = new Product
            {
                Id = 2,
                Name = "laptop",
                Price = 50m
            };
            Product product3 = new Product
            {
                Id = 3,
                Name = "laptop",
                Price = 70m
            };

            //art
            Cart target = new Cart();
            target.Additem(product, 1);
            target.Additem(product2, 2);
            target.Additem(product3, 1);
            target.Clear();
            //Assert
            Assert.AreEqual(target.lines.Count(), 0);
        }
    }
}
