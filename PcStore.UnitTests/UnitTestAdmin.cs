using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PcStore.WebUI.Infrastructure.Abstract;
using PcStore.WebUI.Models;
using PcStore.WebUI.Controllers;
using System.Web.Mvc;
using PcStore.Domain.Abstract;
using PcStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PcStore.UnitTests
{
    [TestClass]
    public class UnitTestAdmin
    {
        [TestMethod]
        public void Can_login_with_Valid_Credentials()
        {
            //Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "admin")).Returns(true);
            LoginViewModel model = new LoginViewModel
            {
                Username="admin",
                Password="admin"
            };
            AccountController target = new AccountController(mock.Object);
            //art
            ActionResult result = target.Login(model,"/MyUrl");
            //assert
            Assert.IsInstanceOfType(result,typeof(RedirectResult));
            Assert.AreEqual("/MyUrl",((RedirectResult) result).Url);
        }
        [TestMethod]
        public void Index_contains_all_product()
        {
            //Arrange
            Mock<IPcRepository> mock = new Mock<IPcRepository>();
            mock.Setup(m => m.products).Returns(new Product[] {
                new Product {Id=1,Name="hp" },
                new Product {Id=1,Name="dell" },
                new Product {Id=1,Name="ibm" },
                new Product {Id=1,Name="lenovo" }
            });
            AdminController target = new AdminController(mock.Object);
            Product[] result = ((IEnumerable<Product>) target.Index().ViewData.Model).ToArray();

            Assert.AreEqual(result.Length, 4);
            Assert.AreEqual("hp", result[0].Name);
        }
        [TestMethod]
        public void Can_Edit_book()
        {
            //Arrange
            Mock<IPcRepository> mock = new Mock<IPcRepository>();
            mock.Setup(m => m.products).Returns(new Product[] {
                new Product {Id=1,Name="hp" },
                new Product {Id=2,Name="dell" },
                new Product {Id=3,Name="ibm" }
            });
            AdminController target = new AdminController(mock.Object);
            Product result = target.Edit(1).ViewData.Model as Product;
            Product result2 = target.Edit(2).ViewData.Model as Product;
            Product result3 = target.Edit(3).ViewData.Model as Product;
            Assert.AreEqual("hp", result.Name);
            Assert.AreEqual(2, result2.Id);
        }
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            //Arrange
            Mock<IPcRepository> mock = new Mock<IPcRepository>();
            Product product = new Product { Name = "test product" };
            AdminController target = new AdminController(mock.Object);
            ActionResult resutl = target.Edit(product);

            mock.Verify(b => b.SaveProduct(product));
            Assert.IsNotInstanceOfType(resutl, typeof(ViewResult));
        }
        [TestMethod]
        public void Cannot_Save_inValid_Changes()
        {
            //Arrange
            Mock<IPcRepository> mock = new Mock<IPcRepository>();
            Product product = new Product { Name = "test product" };

            AdminController target = new AdminController(mock.Object);
            target.ModelState.AddModelError("error", "error");

            ActionResult resutl = target.Edit(product);

            mock.Verify(b => b.SaveProduct(It.IsAny<Product>()),Times.Never());
            Assert.IsInstanceOfType(resutl, typeof(ViewResult));
        }
        [TestMethod]
        public void Can_Delete_Product()
        {
            //Arrange
            Mock<IPcRepository> mock = new Mock<IPcRepository>();
            Product product = new Product { Id = 1, Name = "test product" };
            mock.Setup(b => b.products).Returns(new Product[] {
                new Product {Id = 2,Name="test2" },
                new Product {Id=3 ,Name="test3"}
            });
            AdminController target = new AdminController(mock.Object);
            target.ModelState.AddModelError("error", "error");

            ActionResult resutl = target.Delete(product.Id);

            mock.Verify(b => b.DeleteProduct(product.Id));
        }
    }
}
