using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PcStore.Domain.Abstract;
using PcStore.Domain.Entities;
using PcStore.WebUI.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PcStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IPcRepository> mock = new Mock<IPcRepository>();
            mock.Setup(p => p.products).Returns(new Product[] {
                new Product { Id=1,Name="p1",Brands="p1",Description="p1", Price=1},
                new Product { Id=2,Name="p2",Brands="p1",Description="p1", Price=1},
                new Product { Id=3,Name="p3",Brands="p1",Description="p1", Price=1},
                new Product { Id=4,Name="p4",Brands="p1",Description="p1", Price=1},
                new Product { Id=5,Name="p5",Brands="p1",Description="p1", Price=1},
                new Product { Id=6,Name="p6",Brands="p1",Description="p1", Price=1}
            });

            PcController controller = new PcController(mock.Object);
            //art
            IEnumerable<Product> result =(IEnumerable<Product>) controller.List(2).Model;
            //assert
            Product[] ProductArray = result.ToArray();
            Assert.IsTrue(ProductArray.Length == 3);
            Assert.AreEqual(ProductArray[0].Name , "p4");
            Assert.AreEqual(ProductArray[1].Name , "p5");
            Assert.AreEqual(ProductArray[2].Name, "p6");
        }
    }
}
