using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PcStore.Domain.Abstract;
using PcStore.Domain.Entities;
using PcStore.WebUI.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PcStore.WebUI.HtmlHelper;
using System.Web.Mvc;
using PcStore.WebUI.Models;

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
            ProductListViewModel result =(ProductListViewModel) controller.List(null,2).Model;
            //assert
            Product[] ProductArray = result.Products.ToArray();
            Assert.IsTrue(ProductArray.Length == 3);
            Assert.AreEqual(ProductArray[0].Name , "p4");
            Assert.AreEqual(ProductArray[1].Name , "p5");
            Assert.AreEqual(ProductArray[2].Name, "p6");
        }
        [TestMethod]
        public void Can_Generate_Page_links()
        {
            HtmlHelper myhelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 30,
                ItemsPerPages = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            string expectedResult = "<a class=\"btn btn-default\" href=\"Page1\">1</a>"
                                    + "<a class=\"btn btn-default btn-primary selected\" href=\"Page2\">2</a>"
                                    + "<a class=\"btn btn-default\" href=\"Page3\">3</a>";
            //Act
            MvcHtmlString result = myhelper.PageLinks(pagingInfo, pageUrlDelegate);
            //Assert
            Assert.AreEqual(expectedResult, result.ToString());
        }
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IPcRepository> mock = new Mock<IPcRepository>();
            mock.Setup(b => b.products).Returns(
                new Product[] {
                 new Product {
                     Name="hp"
                 },
                 new Product {
                     Name="dell"
                 },
                 new Product {
                     Name="acer"
                 },
                 new Product
                 {
                     Name = "imb"
                 },
                 new Product
                 {
                     Name = "lenovo"
                 }
                }
                );
            PcController controller = new PcController(mock.Object);
            controller.PageSize = 3;
            //art
            ProductListViewModel result = (ProductListViewModel) controller.List(null,2).Model;
            //assert
            PagingInfo pageinfo = result.PagingInfo;
            Assert.AreEqual(pageinfo.CurrentPage, 2);
            Assert.AreEqual(pageinfo.ItemsPerPages, 3);
            Assert.AreEqual(pageinfo.TotalItems, 5);
            Assert.AreEqual(pageinfo.TotalPages, 2);
        }
        [TestMethod]
        public void Can_Filter_Product()
        {
            Mock<IPcRepository> mock = new Mock<IPcRepository>();
            mock.Setup(b => b.products).Returns(
                new Product[] {
                 new Product {
                     Name="hp",Specilization="lap"
                 },
                 new Product {
                     Name="dell",Specilization="pc"
                 },
                 new Product {
                     Name="acer",Specilization="tablet"
                 },
                 new Product
                 {
                     Name = "imb",Specilization="mobile"
                 },
                 new Product
                 {
                     Name = "lenovo",Specilization="pc"
                 }
                }
                );
            PcController controller = new PcController(mock.Object);
            controller.PageSize = 3;

            //Act 
            Product[] result =((ProductListViewModel) controller.List("pc", 1).Model).Products.ToArray();
            //assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "dell" && result[0].Specilization =="pc");
            Assert.IsTrue(result[1].Name == "lenovo" && result[1].Specilization == "pc");
        }
    }
}
